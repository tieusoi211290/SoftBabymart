using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models.View;
using System;

namespace SoftBabymartVn.Controllers
{
    public class ProductController : BaseController
    {
        private CRUD _crud;
        private babymart_vnEntities _context = new babymart_vnEntities();
        public ProductController()
        {
            _crud = new CRUD();
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/Product/Product.cshtml");
        }
        public JsonResult GetProductbyId(int Id)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var product = _context.soft_Product.Find(Id);
                if (product == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Sản phẩm không tồn tại.";
                }
                Messaging.Data = Mapper.Map<ProductSampleModel>(product);
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductby(PagingInfo pageinfo)
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }

                var lstTmp = from product in _context.soft_Product select product;


                // IQueryable<soft_Product> lstTmp = _context.soft_Product;

                #region Fillter
                if (pageinfo.filterby != null && pageinfo.filterby.Count > 0)
                {
                    foreach (var item in pageinfo.filterby)
                    {
                        if (item.Values != null && item.Values.Length > 0)
                        {
                            int[] myInts = item.Values.Select(int.Parse).ToArray();
                            switch (item.Name)
                            {
                                case "Catalog":
                                    lstTmp = lstTmp.Where(o => myInts.Any(i => i.Equals(o.Catalog.Value)));
                                    break;
                                case "Suppliers":
                                    lstTmp = lstTmp.Where(o => myInts.Any(i => i.Equals(o.SuppliersId.Value)));
                                    break;
                                case "Unit":
                                    lstTmp = lstTmp.Where(o => myInts.Any(i => i.Equals(o.UnitId.Value)));
                                    break;
                            }
                        }
                    }
                }
                #endregion
                #region Sort
                if (!string.IsNullOrEmpty(pageinfo.sortby))
                {
                    switch (pageinfo.sortby)
                    {
                        case "Id":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Id);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Id);
                            break;
                        case "Barcode":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Barcode);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Barcode);
                            break;
                        case "PriceBase":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.PriceBase);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.PriceBase);
                            break;
                        case "PriceBase_Old":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.PriceBase_Old);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.PriceBase_Old);
                            break;
                        case "PriceInput":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.PriceInput);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.PriceInput);
                            break;
                        case "ProductName":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.ProductName);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.ProductName);
                            break;
                        case "Code":
                            if (pageinfo.sortbydesc)
                                lstTmp = lstTmp.OrderByDescending(o => o.Code);
                            else
                                lstTmp = lstTmp.OrderBy(o => o.Code);
                            break;
                    }

                }
                #endregion
                var products = new List<ProductSampleModel>();
                //if (!string.IsNullOrEmpty(pageinfo.keyword) || pageinfo.filterby.Count > 0)
                products = Mapper.Map<List<ProductSampleModel>>(lstTmp.ToList());

                #region Search
                if (!string.IsNullOrEmpty(pageinfo.keyword))
                {
                    pageinfo.keyword = pageinfo.keyword.ToLower();
                    products = products.Where(o =>
                       (!string.IsNullOrEmpty(o.ProductName) && Helpers.convertToUnSign3(o.ProductName.ToLower()).Contains(pageinfo.keyword))
                     || (!string.IsNullOrEmpty(o.Barcode) && Helpers.convertToUnSign3(o.Barcode.ToLower()).Contains(pageinfo.keyword))
                     || (!string.IsNullOrEmpty(o.Code) && Helpers.convertToUnSign3(o.Code.ToLower()).Contains(pageinfo.keyword))
                    ).ToList();
                }
                #endregion
                Channel_Paging<Prodcut_Branches_PriceChannel> lstInfo = new Channel_Paging<Prodcut_Branches_PriceChannel>();
                if (products != null && products.Count > 0)
                {
                    int min = Helpers.FindMin(pageinfo.pageindex, pageinfo.pagesize);

                    lstInfo.totalItems = products.Count();
                    int quantity = Helpers.GetQuantity(lstInfo.totalItems, pageinfo.pageindex, pageinfo.pagesize);
                    if (pageinfo.pagesize < products.Count)
                        if (quantity > 0)
                            products = products.GetRange(min, quantity);
                    lstInfo.listTable = new List<Prodcut_Branches_PriceChannel>();
                    lstInfo.startItem = min;

                    foreach (var item in products)
                    {
                        var price = Mapper.Map<Product_PriceModel>(_context.soft_Channel_Product_Price.FirstOrDefault(o => o.ProductId == item.Id && o.ChannelId == User.ChannelId));
                        var stock = Mapper.Map<Product_StockModel>(_context.soft_Branches_Product_Stock.FirstOrDefault(o => o.ProductId == item.Id && o.BranchesId == User.BranchesId));
                        var productInfo = new Prodcut_Branches_PriceChannel
                        {
                            product_price = price ?? price,
                            product_stock = stock ?? stock,
                            product = item
                        };

                        lstInfo.listTable.Add(productInfo);
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
        public JsonResult ChangePrice(List<Prodcut_Branches_PriceChannel> model)
        {
            var Messaging = new RenderMessaging();
            try
            {

                foreach (var item in model)
                {
                    var pricechannel = _context.soft_Channel_Product_Price.FirstOrDefault(o => o.ProductId == item.product.Id && o.ChannelId == User.ChannelId);
                    if (pricechannel != null)
                    {
                        var data = new soft_Channel_Product_Price
                        {
                            Id = pricechannel.Id,
                            Price = item.product_price.PriceChange,
                            DateUpdate = DateTime.Now,
                            EmployeeUpdate = User.UserId,
                        };

                        _crud.Update<soft_Channel_Product_Price>(data, o => o.Price, o => o.DateUpdate, o => o.EmployeeUpdate);
                    }
                    else
                    {
                        var data = new soft_Channel_Product_Price
                        {
                            Price = item.product_price.PriceChange,
                            ChannelId = User.ChannelId,
                            ProductId = item.product.Id,
                            DateCreate = DateTime.Now,
                            EmployeeCreate = User.UserId

                        };
                        _crud.Add<soft_Channel_Product_Price>(data);
                    }
                }
                _crud.SaveChanges();
                Messaging.messaging = "Đã thay đổi giá thành công !";
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateProduct(ProductSampleModel model)
        {
            var Messaging = new RenderMessaging();
            Messaging.messaging = "Cập nhật thành công.";
            try
            {
                if (User == null || User.UserId < 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại!";
                }
                var product = Mapper.Map<soft_Product>(model);
                var productEnity = Mapper.Map<soft_Product>(Mapper.Map<ProductSampleModel>(_context.soft_Product.Find(model.Id)));
                productEnity.DateUpdate = DateTime.Now;
                productEnity.EmployeeUpdate = User.UserId;

                if (productEnity == null || product == null)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Sản phẩm không tồn tại.";
                }
                if (productEnity.ProductName != product.ProductName)
                {
                    productEnity.ProductName = product.ProductName;
                    _crud.Update<soft_Product>(productEnity, o => o.ProductName, o => o.EmployeeUpdate, o => o.DateUpdate);
                }

                if (productEnity.Code != product.Code)
                {
                    productEnity.Code = product.Code;
                    _crud.Update<soft_Product>(productEnity, o => o.Code, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                if (productEnity.Barcode != product.Barcode)
                {
                    productEnity.Barcode = product.Barcode;
                    _crud.Update<soft_Product>(productEnity, o => o.Barcode, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                if (productEnity.Catalog != product.Catalog)
                {
                    productEnity.Catalog = product.Catalog;
                    _crud.Update<soft_Product>(productEnity, o => o.Catalog, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                if (productEnity.Detail_Info != product.Detail_Info)
                {
                    productEnity.Detail_Info = product.Detail_Info;
                    _crud.Update<soft_Product>(productEnity, o => o.Detail_Info, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                if (productEnity.SuppliersId != product.SuppliersId)
                {
                    productEnity.SuppliersId = product.SuppliersId;
                    _crud.Update<soft_Product>(productEnity, o => o.SuppliersId, o => o.EmployeeUpdate, o => o.DateUpdate);
                }
                _crud.SaveChanges();
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Research(string keyword)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var lstTmp = from product in _context.soft_Product select product;
                var products = Mapper.Map<List<ProductSampleModel>>(lstTmp.ToList());
                #region Search
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    products = products.Where(o =>
                       (!string.IsNullOrEmpty(o.ProductName) && Helpers.convertToUnSign3(o.ProductName.ToLower()).Contains(keyword))
                     || (!string.IsNullOrEmpty(o.Barcode) && Helpers.convertToUnSign3(o.Barcode.ToLower()).Contains(keyword))
                     || (!string.IsNullOrEmpty(o.Code) && Helpers.convertToUnSign3(o.Code.ToLower()).Contains(keyword))
                    ).ToList();
                }
                #endregion

              foreach (var item in products)
                {
                    var priceChannel = _context.soft_Channel_Product_Price.FirstOrDefault(o => o.ProductId == item.Id && o.ChannelId == User.ChannelId);
                    if (priceChannel != null)
                        item.PriceBase = priceChannel.Price;
                }
                Messaging.Data = products;
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