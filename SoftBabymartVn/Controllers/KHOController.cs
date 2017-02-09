using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class KHOController : Controller
    {
          private babymart_vnEntities _context;
          public KHOController()
        {
            _context = new babymart_vnEntities();
        }      
        public JsonResult GetList()
        {
            var list = _context.soft_Kho.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_KHO.cshtml");
        }
        [HttpDelete]
        public JsonResult Remove(string id)
        {
            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.soft_Kho.Find(id);
                _context.soft_Kho.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Xóa Kho tính thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_Kho model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_Kho.Add(model);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm Kho thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(soft_Kho model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_Kho.Find(model.Id);
                _context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Cập nhật Kho thành công";
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
