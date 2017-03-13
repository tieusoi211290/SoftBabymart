using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SoftBabymartVn.Models.Module;
namespace SoftBabymartVn.Controllers
{
    public class WarehouseController : Controller
    {//
        // GET: /ProductGroup/
        //private CRUD _crud;
        //private babymart_vnEntities _context;
        //public WarehouseController()
        //{
        //    _crud = new CRUD();
        //    _context = new babymart_vnEntities();
        //}
        //public ActionResult RenderView()
        //{
        //    return PartialView("~/Views/Shared/Partial/module/CN/_warehouse.cshtml");
        //}
        //public ActionResult ListOrderProduct()
        //{
        //    var result = _context.soft_KHO_Product.ToList();
        //    foreach (var item in result)
        //    {
        //        item.soft_KHO_Product_Detail = null;
        //    }
        //    var r = Mapper.Map<List<KHO_ProductModel>>(result);
        //    return Json(r, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult RenderViewDetail(int id)
        //{
        //    var r = _context.soft_KHO_Product.Find(id);
        //    var result = Mapper.Map<KHO_ProductModel>(r);
        //    return PartialView("~/Views/Shared/Partial/module/CN/_warehouseDetail.cshtml", result);
        //}
        //public ActionResult SaveEdit(KHO_ProductModel model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        var item = Mapper.Map<soft_KHO_Product>(model);
        //        _crud.Update<soft_KHO_Product>(item, o => o.DateUpdate,
        //                                            o => o.IdKHO,
        //                                            o => o.IdNPP,
        //                                            o => o.Note,
        //                                            o => o.SL
        //                                            );
        //        foreach (var o in model.ItemsKHO_Product_DetailModel)
        //        {
        //            if (o.Id <= 0)
        //            {
        //                var objM = Mapper.Map<soft_KHO_Product_Detail>(o);
        //                _crud.Add(objM);
        //            }
        //        }
        //        _crud.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Cập nhật đơn hàng nhập kho thành công";
        //    }
        //    catch
        //    {
        //        Messaging.success = false;
        //        Messaging.messaging = "Do sự cố mạng vui lòng thử lại.";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult SaveAdd(KHO_ProductModel model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        var item = Mapper.Map<soft_KHO_Product>(model);
        //        _crud.Add(item);
        //        foreach (var o in model.ItemsKHO_Product_DetailModel)
        //        {
        //            if (o.Id <= 0)
        //            {
        //                var objM = Mapper.Map<soft_KHO_Product_Detail>(o);
        //                _crud.Add(objM);
        //            }
        //        }
        //        _crud.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Nhập đơn hàng kho thành công";
        //    }
        //    catch
        //    {
        //        Messaging.success = false;
        //        Messaging.messaging = "Do sự cố mạng vui lòng thử lại.";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}

    }
}
