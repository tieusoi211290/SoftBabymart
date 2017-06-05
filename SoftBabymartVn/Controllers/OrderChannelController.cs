using AutoMapper;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftBabymartVn.Models;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models.View;
using SoftBabymartVn.Models.Enum;

namespace SoftBabymartVn.Controllers
{
    public class OrderChannelController : BaseController
    {
        //
        // GET: /OrderChannel/
        private CRUD _crud;
        private babymart_vnEntities _context;
        public OrderChannelController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderViewList()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderChannel/_Channel_Order_List.cshtml");
        }
        public ActionResult RenderViewOrder()
        {
            return PartialView("~/Views/Shared/Partial/module/Order/OrderChannel/_OrderSale.cshtml");
        }
        public JsonResult GetOrder(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var lstTmp = from order in _context.soft_Order where order.TypeOrder == (int)TypeOrder.Sale && order.Id_From == User.ChannelId orderby order.DateCreate descending select order;
                #region Sort
                if (!string.IsNullOrEmpty(pageinfo.sortby))
                {
                    switch (pageinfo.sortby)
                    {
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
                var orderSale = Mapper.Map<List<OrderModel>>(lstTmp.ToList());

                Channel_Paging<OrderModel> lstInfo = new Channel_Paging<OrderModel>();
                if (lstTmp != null && orderSale.Count > 0)
                {
                    int min = Helpers.FindMin(pageinfo.pageindex, pageinfo.pagesize);

                    lstInfo.totalItems = lstTmp.Count();
                    int quantity = Helpers.GetQuantity(lstInfo.totalItems, pageinfo.pageindex, pageinfo.pagesize);
                    if (pageinfo.pagesize < orderSale.Count)
                        if (quantity > 0)
                            orderSale = orderSale.GetRange(min, quantity);
                    lstInfo.listTable = Mapper.Map<List<OrderModel>>(lstTmp);
                    lstInfo.startItem = min;

                    foreach (var item in lstInfo.listTable)
                    {
                        if (item.Id_From > 0)
                        {
                            var channel = _context.soft_Channel.Find(item.Id_From);
                            if (channel != null)
                                item.Name_From = channel.Channel;
                        }
                        if (item.Id_To > 0)
                        {
                            var customer = _context.soft_Customer.Find(item.Id_To);
                            if (customer != null)
                            {
                                item.Customer = Mapper.Map<CustomerModel>(customer);
                            }
                        }

                        var userCreate = _context.sys_Employee.Find(item.EmployeeCreate);
                        item.EmployeeNameCreate = userCreate.Name + "(" + userCreate.Email + ")";

                        if (item.EmployeeUpdate.HasValue)
                        {
                            var userUpdate = _context.sys_Employee.Find(item.EmployeeUpdate);
                            item.EmployeeNameUpdate = userUpdate.Name + "(" + userUpdate.Email + ")";
                        }


                    }
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
        public JsonResult CreateOrder_Sale(OrderModel model)
        {
            var ks = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Phiên đăng nhập đã hết hạn.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                var objOrder = Mapper.Map<soft_Order>(model);
                objOrder.Id_From = User.ChannelId;
                objOrder.DateCreate = DateTime.Now;
                objOrder.EmployeeCreate = User.UserId;
                objOrder.TypeOrder = (int)TypeOrder.Sale;
                // objOrder.Status = (int)StatusOrder.Done;
                objOrder.Id_To = null;
                _crud.Add<soft_Order>(objOrder);

                //foreach (var item in objOrder.soft_Order_Channel_Child)
                //{
                //    var product = Mapper.Map<ProductSampleModel>(_context.soft_Product.Find(item.ProductId));

                //    if (product != null)
                //    {
                //        product.Stock_Total += item.Total.Value;
                //        product.PriceInput_Old = product.PriceInput;
                //        product.PriceInput_New = (int)item.Price.Value;
                //        product.PriceInput = (product.PriceInput + product.PriceInput_New) / 2;
                //        _crud.Update(Mapper.Map<soft_Product>(product), o => o.PriceInput, o => o.PriceInput_New, o => o.PriceInput_Old, o => o.Stock_Total);
                //    }
                //}
                _crud.SaveChanges();
                Messaging.messaging = "Đã thanh toán thành công !";
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDone_Order_Sale(OrderModel model)
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

                var order = _context.soft_Order.Find(model.Id);

                if (order == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Không tìm thấy đơn hàng này.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                var objOrder = new soft_Order
                {
                    Id = model.Id,
                    Status = model.Status,
                    Note = model.Note,
                    Id_From = model.Id_From,

                    Id_To = model.Id_To,
                    DateUpdate = DateTime.Now,
                    EmployeeUpdate = User.UserId,
                    IsShip = !model.IsShip ? false : true
                };


                if (model.IsShip)
                {
                    if (string.IsNullOrEmpty(model.Customer.AddressShip)
                       || string.IsNullOrEmpty(model.Customer.PhoneShip))
                    {
                        Messaging.isError = true;
                        Messaging.messaging = "Vui lòng kiểm tra thông tin giao hàng";
                        return Json(Messaging, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var customer = _context.soft_Customer.Find(model.Id_To);
                        if (customer == null)
                        {
                            Messaging.isError = true;
                            Messaging.messaging = "Không tồn tại khách hàng này.";
                            return Json(Messaging, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            customer.NameShip = model.Customer.NameShip;
                            customer.PhoneShip = model.Customer.PhoneShip;
                            customer.AddressShip = model.Customer.AddressShip;
                            customer.ProvinceIdShip = model.Customer.ProvinceIdShip;
                            customer.DistrictIdShip = model.Customer.DistrictIdShip;
                            _crud.Update<soft_Customer>(customer, o => o.NameShip, o => o.PhoneShip, o => o.AddressShip, o => o.ProvinceIdShip, o => o.DistrictIdShip);

                        }
                    }
                }



                _crud.Update(objOrder, o => o.Status, o => o.Note, o => o.EmployeeUpdate, o => o.DateUpdate, o => o.IsShip);

                if (order.Status != (int)StatusOrderSale.Done && objOrder.Status == (int)StatusOrderSale.Done)
                    UpdateStockByBranches(objOrder.Id);

                _crud.SaveChanges();

            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        private void UpdateStockByBranches(long Id)
        {
            var order = _context.soft_Order.Find(Id);

            var channel = _context.soft_Channel.Find(order.Id_From);
            if (channel == null || channel.BranchesId <= 0)
                return;

            foreach (var item in order.soft_Order_Child)
            {
                var stockTo = _context.soft_Branches_Product_Stock.FirstOrDefault(o => o.ProductId == item.ProductId && o.BranchesId == channel.BranchesId);

                if (stockTo != null)
                {
                    var newstock = new soft_Branches_Product_Stock
                    {
                        BranchesId = stockTo.BranchesId,
                        ProductId = stockTo.ProductId,
                        Stock_Total = stockTo.Stock_Total - item.Total.Value
                    };
                    _crud.Update(newstock, o => o.Stock_Total);
                }

            }
        }

        [HttpPost]
        public JsonResult CreatOrderSale(OrderModel model, bool isDone)
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

                model.Id_From = User.ChannelId;

                if (model.Detail == null || model.Detail.Count <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Không có sản phẩm nào trong đơn hàng.";
                    return Json(Messaging, JsonRequestBehavior.AllowGet);
                }

                if (model.IsShip)
                {
                    if (string.IsNullOrEmpty(model.Customer.AddressShip)
                       || string.IsNullOrEmpty(model.Customer.PhoneShip))
                    // || model.Customer.DistrictIdShip <= 0
                    //  || model.Customer.ProvinceIdShip <= 0)
                    {
                        Messaging.isError = true;
                        Messaging.messaging = "Vui lòng kiểm tra thông tin giao hàng";
                        return Json(Messaging, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var customer = _context.soft_Customer.Find(model.Customer.Id);
                        if (customer == null)
                        {
                            model.Id_To = 0;
                        }
                        else
                        {
                            customer.NameShip = model.Customer.NameShip;
                            customer.PhoneShip = model.Customer.PhoneShip;
                            customer.AddressShip = model.Customer.AddressShip;
                            customer.ProvinceIdShip = model.Customer.ProvinceIdShip;
                            customer.DistrictIdShip = model.Customer.DistrictIdShip;
                            model.Id_To = model.Customer.Id;
                            _crud.Update<soft_Customer>(customer, o => o.NameShip, o => o.PhoneShip, o => o.AddressShip, o => o.ProvinceIdShip, o => o.DistrictIdShip);

                        }
                    }
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
                objOrder.TypeOrder = (int)TypeOrder.Sale;

                _crud.Add<soft_Order>(objOrder);
                _crud.SaveChanges();
                if (isDone)
                {
                    objOrder.Status = (int)StatusOrderSale.Done;
                    _crud.Update<soft_Order>(objOrder, o => o.Status);
                    UpdateStockByBranches(objOrder.Id);
                    _crud.SaveChanges();
                }



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
