using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
namespace SoftBabymartVn.Controllers
{
    public class KHO_OrderController : Controller
    {
        //
        // GET: /KHO_Order/
        private babymart_vnEntities _context;
        public KHO_OrderController()
        {
            _context = new babymart_vnEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/CN/KHO/_Kho_Order.cshtml");
        }
        public JsonResult GetList()
        {
            var list = _context.soft_KHO_Order.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
  

        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.soft_KHO_Order.Find(id);
                _context.soft_KHO_Order.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Xóa Đơn vị tính thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_KHO_Order model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_KHO_Order.Add(model);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm Đơn vị tính thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(soft_KHO_Order model)
        {
            var Messaging = new RenderMessaging();
            var data = _context.soft_KHO_Order.Find(model.Id);
            if (data == null)
            {
                Messaging.success = false;
                Messaging.messaging = "Đơn hàng không tồn tại.";
            }
            try
            {
                data.Note = model.Note;
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Cập nhật đơn vị tính thành công";
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
