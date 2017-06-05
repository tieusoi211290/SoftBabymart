using AutoMapper;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class GroupCustomer_ProductController : Controller
    {

        //private CRUD _crud;
        //private babymart_vnEntities _context;
        //public GroupCustomer_ProductController()
        //{
        //    _crud = new CRUD();
        //    _context = new babymart_vnEntities();
        //}
        //public ActionResult RenderView()
        //{
        //    return PartialView("~/Views/Shared/Partial/module/DM/_groupCustomer_product.cshtml");
        //}
        //public JsonResult GetListGroupProduct()
        //{
        //    var list = _context.soft_group_product.ToList();
        //    var result = Mapper.Map<List<GroupProductModel>>(list);
        //    foreach (var item in result)
        //    {
        //        item.countPrd = _context.shop_sanpham.Where(o => o.soft_IdGroupProduct.HasValue &&
        //            o.soft_IdGroupProduct.Value == item.Id).Count(); ;
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetList(List<int> groupProductIds, List<int> groupNPP)
        //{
        //    var result = from product in _context.shop_sanpham
        //                 join group_cus_pro in _context.soft_group_customer_product
        //                 on product.id equals group_cus_pro.ProductId
        //                 select new GroupCus_ProductViewModel
        //                 {
        //                     Id = group_cus_pro.Id,
        //                     ProductId = product.id,
        //                     Si = group_cus_pro.Si,
        //                     Online = group_cus_pro.Online,
        //                     Mymall = group_cus_pro.Mymall,
        //                     Lazada = group_cus_pro.Lazada,
        //                     Shopee = group_cus_pro.Shopee,
        //                     BanleCH = group_cus_pro.BanleCH,
        //                     Date_CH = group_cus_pro.Date_CH,
        //                     Chietkhau_CH = group_cus_pro.Chietkhau_CH,
        //                     Date_Onl = group_cus_pro.Date_Onl,
        //                     Chietkhau_Onl = group_cus_pro.Chietkhau_Onl,

        //                     masp = product.masp,
        //                     tensp = product.tensp,
        //                     soft_IdGroupProduct = product.soft_IdGroupProduct,
        //                     soft_IdNPP = product.soft_IdNPP,
        //                     soft_IdDvt = product.soft_IdDvt,
        //                     soft_Barcode = product.soft_Barcode,
        //                     soft_GiaM = product.soft_GiaM,
        //                     soft_GiaTC = product.soft_GiaTC


        //                 };
        //    int ispass = 0;
        //    if (groupProductIds != null && groupProductIds.Count > 0)
        //    {
        //        result = result.Where(o => groupProductIds.Contains(o.soft_IdGroupProduct.Value));
        //        ispass++;

        //    }
        //    if (groupNPP != null && groupNPP.Count > 0)
        //    {
        //        result = result.Where(o => groupNPP.Contains(o.soft_IdNPP.Value));
        //        ispass++;
        //    }
        //    if (ispass > 0)
        //    {
        //        var list = result.ToList();
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return Json(new List<ProductModel>(), JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Add(shop_sanpham model)
        //{
        //    model.hide = true;
        //    model.FromSoft = true;
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        _context.shop_sanpham.Add(model);
        //        _context.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Thêm Sản phẩm thành công";
        //    }
        //    catch
        //    {
        //        Messaging.isError = true;
        //        Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPut]
        //public JsonResult Edit(soft_group_customer_product model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        _crud.Update<soft_group_customer_product>(model,
        //                                            o => o.BanleCH,
        //                                            o => o.Online,
        //                                            o => o.Si,
        //                                            o => o.Lazada,
        //                                            o => o.Shopee,
        //                                            o => o.Mymall,
        //                                            o => o.Date_CH,
        //                                            o => o.Date_Onl,
        //                                            o => o.Chietkhau_CH,
        //                                            o => o.Chietkhau_Onl
        //                                            );
        //        _crud.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Cập nhật đơn giá thành công";
        //    }
        //    catch
        //    {    //catch (DbEntityValidationException e)        
        //        //string str = string.Empty;
        //        //foreach (var eve in e.EntityValidationErrors)
        //        //{
        //        //    str = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //        //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //        //    foreach (var ve in eve.ValidationErrors)
        //        //    {
        //        //        str += string.Format("- Property: \"{0}\", Error: \"{1}\"",
        //        //            ve.PropertyName, ve.ErrorMessage);
        //        //    }
        //        //}  // throw;
        //        Messaging.isError = true;
        //        Messaging.messaging = "Do sự cố mạng vui lòng thử lại.";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}
    }
}
