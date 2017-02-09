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
    public class NPPController : Controller
    {
        //
        // GET: /NPP/

        private CRUD _crud;
        private babymart_vnEntities _context;
        public NPPController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public JsonResult GetList()
        {
            var list = _context.soft_NPP.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_NPP.cshtml");
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.soft_NPP.Find(id);
                _context.soft_NPP.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Xóa Nhà phân phối tính thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_NPP model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_NPP.Add(model);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Thêm Nhà phân phối thành công";
            }
            catch
            {
                Messaging.success = false;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(soft_NPP model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_NPP.Find(model.Id);
                //_context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
                //_context.SaveChanges();
                _crud.Update<soft_NPP>(model);
                _crud.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Cập nhật Nhà phân phối thành công";
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
