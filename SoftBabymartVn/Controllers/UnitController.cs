using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class UnitController : Controller
    {
        private babymart_vnEntities _context;
        public UnitController()
        {
            _context = new babymart_vnEntities();
        }
        public JsonResult LoadLstUnit()
        {
            var list = _context.soft_Unit.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_dvt.cshtml");
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();

            try
            {
                var item = _context.soft_Unit.Find(id);
                _context.soft_Unit.Remove(item);
                _context.SaveChanges();
                Messaging.messaging = "Xóa Đơn vị tính thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_Unit model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_Unit.Add(model);
                _context.SaveChanges();
                Messaging.messaging = "Thêm Đơn vị tính thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(soft_Unit model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_Unit.Find(model.Id);
                _context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
                _context.SaveChanges();
                Messaging.messaging = "Cập nhật đơn vị tính thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

    }
}
