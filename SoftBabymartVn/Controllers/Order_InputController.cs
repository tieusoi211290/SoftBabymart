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
    public class Order_InputController : BaseController
    {
        //
        // GET: /Order_Input/
        private CRUD _crud;
        private babymart_vnEntities _context;
        public Order_InputController()
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
            return PartialView("~/Views/Shared/Partial/module/Order/OrderInut/_Channel_Order_Input_List.cshtml");
        }

        public ActionResult RenderViewCreate(List<Order_DetialModel> model)
        {
            if (model != null)
            {
                var rs = new List<Order_InputTmpModel>();
                foreach (var item in model)
                {
                    rs.Add(new Models.Module.Order_InputTmpModel
                    {
                        Code = item.Product.Code,
                        ProductId = item.Product.Id,
                        ProductName = item.Product.ProductName
                    });
                }
                ViewBag.ProductLst = Newtonsoft.Json.JsonConvert.SerializeObject(rs);
            }
            return PartialView("~/Views/Shared/Partial/module/Order/OrderInut/_Channel_Order_Input_Create.cshtml");
        }
        public JsonResult GetOrder_Inside(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var lstTmp = from order in _context.soft_Order
                             where (order.TypeOrder == (int)TypeOrder.Input && order.Id_To == User.BranchesId) ||
                                   (order.TypeOrder == (int)TypeOrder.Output && order.Id_To == User.BranchesId)
                             orderby order.DateCreate descending
                             select order;
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

                    lstInfo.listTable = Mapper.Map<List<OrderModel>>(lstTmp);

                    foreach (var item in lstInfo.listTable)
                    {
                        var userCreate = _context.sys_Employee.Find(item.EmployeeCreate);
                        item.EmployeeNameCreate = userCreate.Name + "(" + userCreate.Email + ")";

                        if (item.EmployeeUpdate.HasValue)
                        {
                            var userUpdate = _context.sys_Employee.Find(item.EmployeeUpdate);
                            item.EmployeeNameUpdate = userUpdate.Name + "(" + userUpdate.Email + ")";
                        }

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


                    }

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
        public JsonResult CreateOrder_Inside(OrderModel model, bool isDone = true)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Phiên đăng nhập đã hết hạn.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                if (model.Detail.Count <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Dữ liệu không hợp lệ, vui lòng kiểm tra lại";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                var objOrder = Mapper.Map<soft_Order>(model);
                objOrder.Id_From = User.BranchesId;
                objOrder.Id_To = User.BranchesId;
                objOrder.DateCreate = DateTime.Now;
                objOrder.EmployeeCreate = User.UserId;
                objOrder.TypeOrder = (int)TypeOrder.Input;
                objOrder.Status = isDone ? (int)StatusOrderProduct.Done : (int)StatusOrderProduct.Waitting;

                _crud.Add<soft_Order>(objOrder);

                if (isDone)
                    UpdateStockByBranches(objOrder);

                _crud.SaveChanges();
                Messaging.messaging = "Đã Nhập hàng thành công !";
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateDone_Order_Inside(int id)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Phiên đăng nhập đã hết hạn.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                var order = _context.soft_Order.Find(id);

                if (order == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Không tìm thấy đơn hàng này.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                var objOrder = new soft_Order
                {
                    Id = id,
                    Status = (int)StatusOrderProduct.Done,
                    DateUpdate = DateTime.Now,
                    EmployeeUpdate = User.UserId
                };

                _crud.Update(objOrder, o => o.Status, o => o.EmployeeUpdate, o => o.DateUpdate);

                UpdateStockByBranches(order);

                _crud.SaveChanges();

            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        private void UpdateStockByBranches(soft_Order order)
        {
            foreach (var item in order.soft_Order_Child)
            {
                if (order.TypeOrder == (int)TypeOrder.Input)
                {
                    var product = _context.soft_Product.Find(item.ProductId);
                    if (product != null)
                    {
                        var productObj = new soft_Product
                        {
                            Id = product.Id,
                            PriceBase_Old = product.PriceInput,
                            PriceInput = (int)item.Price.Value,
                            PriceBase = product.PriceBase <= 0 ? product.PriceInput : (product.PriceBase + product.PriceInput) / 2
                        };

                        _crud.Update(productObj, o => o.PriceBase, o => o.PriceInput, o => o.PriceBase_Old, o => o.Stock_Total);
                    }
                }

                var stockTo = _context.soft_Branches_Product_Stock.FirstOrDefault(o => o.ProductId == item.ProductId && o.BranchesId == order.Id_To);

                if (stockTo != null)
                {

                    var newstock = new soft_Branches_Product_Stock
                    {
                        BranchesId = stockTo.BranchesId,
                        ProductId = stockTo.ProductId,
                        Stock_Total = stockTo.Stock_Total + item.Total.Value,
                        DateCreate = stockTo.DateCreate,
                        EmployeeCreate = stockTo.EmployeeCreate,
                        DateUpdate = DateTime.Now,
                        EmployeeUpdate = User.UserId,
                    };
                    _crud.Update(newstock, o => o.Stock_Total, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                else
                {
                    stockTo = new soft_Branches_Product_Stock
                    {
                        ProductId = item.ProductId.Value,
                        BranchesId = User.BranchesId,
                        DateCreate = DateTime.Now,
                        Stock_Total = item.Total.Value,
                        EmployeeCreate = User.UserId
                    };
                    _crud.Add(stockTo);
                }

                if (order.TypeOrder == (int)TypeOrder.Output)
                {
                    var stockFrom = _context.soft_Branches_Product_Stock.FirstOrDefault(o => o.ProductId == item.ProductId && o.BranchesId == order.Id_From);

                    if (stockFrom != null)
                    {

                        var newstock = new soft_Branches_Product_Stock
                        {
                            BranchesId = stockFrom.BranchesId,
                            ProductId = stockFrom.ProductId,
                            Stock_Total = stockFrom.Stock_Total - item.Total.Value,
                            DateCreate = stockFrom.DateCreate,
                            EmployeeCreate = stockFrom.EmployeeCreate,
                            DateUpdate = order.DateCreate,
                            EmployeeUpdate = order.EmployeeCreate,
                        };
                        _crud.Update(newstock, o => o.Stock_Total, o => o.EmployeeUpdate, o => o.DateUpdate);
                    }
                    else
                    {
                        stockTo = new soft_Branches_Product_Stock
                        {

                            BranchesId = order.Id_From.Value,
                            ProductId = item.ProductId.Value,
                            Stock_Total = 0 - item.Total.Value,
                            DateCreate = order.DateCreate,
                            EmployeeCreate = order.EmployeeCreate,
                            DateUpdate = null,
                            EmployeeUpdate = null
                        };
                        _crud.Add(stockTo);
                    }
                }




            }



        }
    }
}
