﻿using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class DvtController : Controller
    {
        private babymart_vnEntities _context;
        public DvtController()
        {
            _context = new babymart_vnEntities();
        }
        public JsonResult GetList()
        {
            var list = _context.soft_Dvt.ToList();
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
                var item = _context.soft_Dvt.Find(id);
                _context.soft_Dvt.Remove(item);
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
        public JsonResult Add(soft_Dvt model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_Dvt.Add(model);
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
        public JsonResult Edit(soft_Dvt model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_Dvt.Find(model.Id);
                _context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
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
