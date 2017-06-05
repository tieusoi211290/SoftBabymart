using AutoMapper;
using DotNetOpenAuth.Messaging;
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
    public class Order_SuppliersController : BaseController
    {
        private CRUD _crud;
        private babymart_vnEntities _context;
        public Order_SuppliersController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderSuppliers/_Channel_Order_Suppliers_List.cshtml");
        }

        public ActionResult RenderViewCreate()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderSuppliers/_Channel_Order_Suppliers_Create.cshtml");
        }
        public JsonResult GetOrder_Suppliers(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var lstTmp = from order in _context.soft_Order where order.TypeOrder == (int)TypeOrder.OrderProduct orderby order.DateCreate descending select order;
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
                var orderSuppliers = Mapper.Map<List<OrderModel>>(lstTmp.ToList());
                Channel_Paging<OrderModel> lstInfo = new Channel_Paging<OrderModel>();
                if (lstTmp != null && orderSuppliers.Count > 0)
                {
                    int min = Helpers.FindMin(pageinfo.pageindex, pageinfo.pagesize);

                    lstInfo.totalItems = lstTmp.Count();
                    int quantity = Helpers.GetQuantity(lstInfo.totalItems, pageinfo.pageindex, pageinfo.pagesize);
                    if (pageinfo.pagesize < orderSuppliers.Count)
                        if (quantity > 0)
                            orderSuppliers = orderSuppliers.GetRange(min, quantity);
                    lstInfo.listTable = Mapper.Map<List<OrderModel>>(lstTmp);
                    lstInfo.startItem = min;
                }
                Messaging.Data = lstInfo;
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateOrder_Suppliers_Print(OrderModel model)
        {
            var Messaging = new RenderMessaging();
            var result = new List<OrderChannel_GroupModel>();
            try
            {
                if (User == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Phiên đăng nhập đã hết hạn.";
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
                    item.Product = Mapper.Map<ProductSampleModel>(product);

                }

                var product_suppliers = model.Detail.GroupBy(o => o.Product.SuppliersId).Select(pro => new OrderChannel_GroupModel
                {
                    products = Mapper.Map<List<Order_DetialModel>>(pro.ToList()),
                    suppliers = Mapper.Map<SuppliersModel>(pro.FirstOrDefault().Product.soft_Suppliers)
                }).ToList();

                Messaging.Data = product_suppliers;
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateOrder_Suppliers(OrderModel model)
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

                foreach (var item in model.Detail)
                {
                    var product = _context.soft_Product.Find(item.ProductId);
                    if (product == null)
                    {
                        Messaging.isError = true;
                        Messaging.messaging = "Sản phẩm không tồn tại, vui lòng thử lại.";
                        return Json(Messaging, JsonRequestBehavior.AllowGet);
                    }                 
                }
                
                var objOrder = Mapper.Map<soft_Order>(model);
               // objOrder.Status = (int)StatusOrder.Waitting;
                objOrder.DateCreate = DateTime.Now;
                objOrder.EmployeeCreate = User.UserId;
                objOrder.TypeOrder = (int)TypeOrder.OrderProduct;
                objOrder.Id_From = null;
                objOrder.Id_To = null;
                _crud.Add<soft_Order>(objOrder);
                _crud.SaveChanges();
                Messaging.messaging = "Đã tạo phiếu đặt hàng.";
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
