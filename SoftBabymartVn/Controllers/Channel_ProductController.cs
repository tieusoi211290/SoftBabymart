using AutoMapper;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Enum;
using SoftBabymartVn.Models.Module;
using SoftBabymartVn.Models.View;
using SoftBabymartVn.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class Channel_ProductController : BaseController
    {
        //
        // GET: /KENH_Product/
        private CRUD _crud;
        private babymart_vnEntities _context = new babymart_vnEntities();
        public Channel_ProductController()
        {
            _crud = new CRUD();
        }
        [CustomAuthorize(RolesEnums = new RolesEnum[] { RolesEnum.Read_OrderInput })]
        public ActionResult RenderView()
        {
            var a = User.ChannelId;

            return PartialView("~/Views/Shared/Partial/module/Product/_Channel_product.cshtml");
        }
        public ActionResult RenderViewAdd()
        {
            var a = User.ChannelId;
            return PartialView("~/Views/Shared/Partial/module/Product/_Channel_Product_Add.cshtml");
        }
        public JsonResult LoadListChannel_product(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }

                var lstTmp = _context.soft_Channel_Product_Price.Where(o => o.ChannelId.Equals(User.ChannelId)).ToList();

                #region Search
                if (!string.IsNullOrEmpty(pageinfo.keyword))
                {
                    pageinfo.keyword = pageinfo.keyword.ToLower();
                    lstTmp = lstTmp.Where(o =>
                       (!string.IsNullOrEmpty(o.soft_Product.ProductName) && Helpers.convertToUnSign3(o.soft_Product.ProductName.ToLower()).Contains(pageinfo.keyword))
                     || (!string.IsNullOrEmpty(o.soft_Product.Barcode) && Helpers.convertToUnSign3(o.soft_Product.Barcode.ToLower()).Contains(pageinfo.keyword))
                     || (!string.IsNullOrEmpty(o.soft_Product.Code) && Helpers.convertToUnSign3(o.soft_Product.Code.ToLower()).Contains(pageinfo.keyword))
                    ).ToList();
                }
                #endregion
                #region Fillter
                if (pageinfo.filterby != null && pageinfo.filterby.Count > 0)
                {
                    foreach (var item in pageinfo.filterby)
                    {
                        switch (item.Name)
                        {
                            case "Catalog":
                                if (item.Values != null && item.Values.Length > 0)
                                    lstTmp = lstTmp.Where(o => item.Values.Contains(o.soft_Product.Catalog.ToString())).ToList();
                                break;
                            case "Suppliers":
                                if (item.Values != null && item.Values.Length > 0)
                                    lstTmp = lstTmp.Where(o => item.Values.Contains(o.soft_Product.SuppliersId.ToString())).ToList();
                                break;
                            case "Unit":
                                if (item.Values != null && item.Values.Length > 0)
                                    lstTmp = lstTmp.Where(o => item.Values.Contains(o.soft_Product.UnitId.ToString())).ToList();
                                break;
                        }
                    }

                }
                #endregion
                #region Sort
                if (!string.IsNullOrEmpty(pageinfo.sortby))
                {
                    switch (pageinfo.sortby)
                    {
                        case "ProductName":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.soft_Product.ProductName).ToList();
                            else
                                lstTmp = lstTmp.OrderBy(o => o.soft_Product.ProductName).ToList();
                            break;
                        case "Code":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.soft_Product.Code).ToList();
                            else
                                lstTmp = lstTmp.OrderBy(o => o.soft_Product.Code).ToList();
                            break;
                    }

                }
                #endregion
                Channel_Paging<Channel_Product_PriceModel> lstInfo = new Channel_Paging<Channel_Product_PriceModel>();
                if (lstTmp != null && lstTmp.Count > 0)
                {
                    int min = Helpers.FindMin(pageinfo.pageindex, pageinfo.pagesize);

                    lstInfo.totalItems = lstTmp.Count();
                    int quantity = Helpers.GetQuantity(lstInfo.totalItems, pageinfo.pageindex, pageinfo.pagesize);
                    if (pageinfo.pagesize < lstTmp.Count)
                        if (quantity > 0)
                            lstTmp = lstTmp.GetRange(min, quantity);
                    lstInfo.listTable = Mapper.Map<List<Channel_Product_PriceModel>>(lstTmp);
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
        public JsonResult ChangePrice(List<Channel_Product_PriceModel> model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                foreach (var item in model)
                {
                    var data = new soft_Channel_Product_Price
                    {
                        Id = item.Id,
                        Price = item.PriceChange,
                        ChannelId = item.ChannelId,
                    };
                    _crud.Update<soft_Channel_Product_Price>(data, o => o.Price);
                }
                _crud.SaveChanges();
                Messaging.messaging = "Đã thay đổi giá thành công !";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveProduct(int Id)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var hasIts = _context.soft_Channel_Product_Price.Find(Id);
                if (hasIts != null)
                {
                    _context.soft_Channel_Product_Price.Remove(hasIts);
                    _context.SaveChanges();
                    Messaging.messaging = "Đã xóa sản phẩm này ra khỏi Kênh thành công !";

                }
                else
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Không tìm thấy sản phẩm này trong Kênh.";
                }
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddProductIntoChannel(int ProductId)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId > 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var product = _context.soft_Product.Find(ProductId);
                if (product != null)
                {
                    var data = new soft_Channel_Product_Price()
                    {
                        Price = product.PriceInput.HasValue ? product.PriceInput.Value : 0,
                        ProductId = ProductId,
                        ChannelId = User.ChannelId,
                        EmployeeCreate = User.UserId,
                        DateCreate = DateTime.Now
                    };
                    _crud.Add<soft_Channel_Product_Price>(data);

                }
                _crud.SaveChanges();
                Messaging.messaging = "Đã thêm sản phẩm vào kênh thành công !";
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult LoadListKenh_product(List<int> groupTeam, List<int> groupNPP)
        //{
        //    var result = from s in _context.soft_Channel_Product select s;
        //    int ispass = 0;
        //    if (groupTeam != null && groupTeam.Count > 0)
        //    {
        //        result = result.Where(o => groupTeam.Contains(o.shop_sanpham.soft_IdGroupProduct.Value));
        //        ispass++;
        //    }
        //    if (groupNPP != null && groupNPP.Count > 0)
        //    {
        //        result = result.Where(o => groupNPP.Contains(o.shop_sanpham.soft_IdNPP.Value));
        //        ispass++;
        //    }
        //    if (ispass > 0)
        //    {
        //        var list = Mapper.Map<List<Channel_ProductModel>>(result.ToList());
        //        foreach (var item in list)
        //        {
        //            //item.masp = item.shop_sanpham.tensp;
        //            //item.barcode = item.shop_sanpham.soft_Barcode;
        //            //item.tensp = item.shop_sanpham.tensp;
        //        }
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(new List<Channel_ProductModel>(), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPut]
        //public JsonResult Edit(soft_Channel_Product model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        var data = _context.soft_Channel_Product.Find(model.Id);
        //        _context.Entry(data).CurrentValues.SetValues(model);
        //        _context.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Cập nhật giá bán thành công";
        //    }
        //    catch
        //    {
        //        Messaging.isError = true;
        //        Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}
    }
}
