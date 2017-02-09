using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SoftBabymartVn.Infractstructure;
using System.Data.Entity.Validation;
namespace SoftBabymartVn.Controllers
{
    public class ProductController : Controller
    {
        private CRUD _crud;
        private babymart_vnEntities _context;
        public ProductController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public JsonResult GetListGroupProduct()
        {
            var list = _context.soft_group_product.ToList();
            var result = Mapper.Map<List<GroupProductModel>>(list);
            foreach (var item in result)
            {
                item.countPrd = _context.shop_sanpham.Where(o => o.soft_IdGroupProduct.HasValue &&
                    o.soft_IdGroupProduct.Value == item.Id).Count(); ;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList(List<int> groupProductIds, List<int> groupNPP)
        {
            var result = from s in _context.shop_sanpham select s;
            int ispass = 0;
            if (groupProductIds != null && groupProductIds.Count > 0)
            {
                result = result.Where(o => groupProductIds.Contains(o.soft_IdGroupProduct.Value));
                ispass++;

            }
            if (groupNPP != null && groupNPP.Count > 0)
            {
                result = result.Where(o => groupNPP.Contains(o.soft_IdNPP.Value));
                ispass++;
            }
            if (ispass > 0)
            {
                var list = Mapper.Map<List<ProductModel>>(result.ToList());
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new List<ProductModel>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_product.cshtml");
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.shop_sanpham.Find(id);
                _context.shop_sanpham.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Xóa sản phẩm thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(shop_sanpham model)
        {
            model.hide = true;
            model.FromSoft = true;
            var Messaging = new RenderMessaging();
            try
            {
                _context.shop_sanpham.Add(model);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm Sản phẩm thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(shop_sanpham model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _crud.Update<shop_sanpham>(model, o => o.tensp,
                                                    o => o.masp,
                                                    o => o.soft_IdDvt,
                                                    o => o.soft_IdGroupProduct,
                                                    o => o.soft_IdNPP,
                                                    o => o.soft_Barcode,
                                                    o => o.soft_GiaM,
                                                    o => o.soft_GiaTC
                                                    );
                _crud.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Cập nhật sản phẩm thành công";
            }
            catch
            {    //catch (DbEntityValidationException e)        
                //string str = string.Empty;
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    str = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        str += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}  // throw;
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng vui lòng thử lại.";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
    }
}
