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
    public class GroupProductController : Controller
    {
        //
        // GET: /ProductGroup/
        private CRUD _crud;
        private babymart_vnEntities _context;
        public GroupProductController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }

        public JsonResult GetList()
        {
            var list = _context.soft_group_product.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_groupProduct.cshtml");
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {

            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.soft_group_product.Find(id);
                _context.soft_group_product.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm sản phẩm thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_group_product model)
        {

            var Messaging = new RenderMessaging();

            try
            {

                _context.soft_group_product.Add(model);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm sản phẩm thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPut]
        public JsonResult Edit(soft_group_product model)
        {

            var Messaging = new RenderMessaging();

            try
            {

                var data = _context.soft_group_product.Find(model.Id);
                _context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
                //_crud.Update<soft_group_product>(model, o => o.tenloai);
                //_crud.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm sản phẩm thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
    }
}
