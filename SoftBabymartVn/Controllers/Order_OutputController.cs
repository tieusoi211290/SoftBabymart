using AutoMapper;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Enum;
using SoftBabymartVn.Models.Module;
using SoftBabymartVn.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class Order_OutputController : BaseController
    {
        private CRUD _crud;
        private babymart_vnEntities _context;
        public Order_OutputController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderOutput/_Channel_Order_Output_List.cshtml");
        }

        public ActionResult RenderViewCreate()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderOutput/_Channel_Order_Output_Create.cshtml");
        }
        public JsonResult GetOrder_Output(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var lstTmp = from order in _context.soft_Order where order.TypeOrder == (int)TypeOrder.Output && order.Id_From == User.BranchesId orderby order.DateCreate descending select order;
                #region Sort
                if (!string.IsNullOrEmpty(pageinfo.sortby))
                {
                    switch (pageinfo.sortby)
                    {
                        case "ChannelsTo":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Id_To);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Id_To);
                            break;
                        case "ChannelsFrom":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Id_From);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Id_From);
                            break;
                        case "DateCreate":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.DateCreate);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.DateCreate);
                            break;
                        case "Total":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Total);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Total);
                            break;
                        case "Status":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Status);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Status);
                            break;
                        case "Id":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Id);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Id);
                            break;
                    }

                }
                #endregion
                var add = lstTmp.ToList();
                var orderInput = Mapper.Map<List<OrderModel>>(lstTmp.ToList());
                Channel_Paging<OrderModel> lstInfo = new Channel_Paging<OrderModel>();
                if (lstTmp != null && orderInput.Count > 0)
                {
                    int min = Helpers.FindMin(pageinfo.pageindex, pageinfo.pagesize);

                    lstInfo.totalItems = lstTmp.Count();
                    int quantity = Helpers.GetQuantity(lstInfo.totalItems, pageinfo.pageindex, pageinfo.pagesize);
                    if (pageinfo.pagesize < orderInput.Count)
                        if (quantity > 0)
                            orderInput = orderInput.GetRange(min, quantity);

                    var lstOrder = Mapper.Map<List<OrderModel>>(lstTmp);

                    foreach (var item in lstOrder)
                    {
                        if (item.Id_From > 0)
                        {
                            var branch = _context.soft_Branches.Find(item.Id_From);
                            if (branch != null)
                                item.Name_From = branch.BranchesName;
                        }
                        if (item.Id_To > 0)
                        {
                            var branch = _context.soft_Branches.Find(item.Id_To);
                            if (branch != null)
                                item.Name_To = branch.BranchesName;
                        }

                        var userCreate = _context.sys_Employee.Find(item.EmployeeCreate);
                        item.EmployeeNameCreate = userCreate.Name + "(" + userCreate.Email + ")";

                        if (item.EmployeeUpdate.HasValue)
                        {
                            var userUpdate = _context.sys_Employee.Find(item.EmployeeUpdate);
                            item.EmployeeNameUpdate = userUpdate.Name + "(" + userUpdate.Email + ")";
                        }
                    }

                    lstInfo.listTable = lstOrder;
                    lstInfo.startItem = min;
                }
                #region addproduct
                #endregion


                Messaging.Data = lstInfo;
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateOrder_Output(OrderModel model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.BranchesId <= 0 || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Phiên đăng nhập đã hết hạn.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                model.Id_From = User.BranchesId;

                if (model.Id_To <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng chọn Kho bạn xuất đến.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }


                if (model.Id_To == User.BranchesId || model.Detail.Count <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }


                foreach (var item in model.Detail)
                {
                    var product = _context.soft_Product.Find(item.ProductId);
                    if (product == null)
                    {
                        Messaging.isError = true;
                        Messaging.messaging = "Sản phẩm không tồn tại, vui lòng thử lại.";
                        return Json(Messaging, JsonRequestBehavior.AllowGet);
                    }

                    var productstock = _context.soft_Branches_Product_Stock.FirstOrDefault(o => o.BranchesId == User.BranchesId && o.ProductId == item.ProductId);

                    if (productstock != null)
                    {
                        if (productstock.Stock_Total < item.Total)
                        {
                            Messaging.isError = true;
                            Messaging.messaging = "Số lượng sản phẩm " + product.ProductName + " không đủ để xuất, vui lòng thử lại.";
                            return Json(Messaging, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        Messaging.isError = true;
                        Messaging.messaging = "Số lượng sản phẩm " + product.ProductName + " không đủ để xuất, vui lòng thử lại.";
                        return Json(Messaging, JsonRequestBehavior.AllowGet);
                    }
                }

                var objOrder = Mapper.Map<soft_Order>(model);
                objOrder.Status = (int)StatusOrderProduct.Waitting;
                objOrder.DateCreate = DateTime.Now;
                objOrder.EmployeeCreate = User.UserId;
                objOrder.TypeOrder = (int)TypeOrder.Output;

                _crud.Add<soft_Order>(objOrder);
                _crud.SaveChanges();
                Messaging.messaging = "Đã tạo phiếu xuất hàng.";
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
    }
}
