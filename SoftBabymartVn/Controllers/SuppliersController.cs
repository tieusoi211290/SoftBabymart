using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftBabymartVn.Controllers
{
    public class SuppliersController :  BaseController
    {
        //
        // GET: /NPP/

        private CRUD _crud;
        private babymart_vnEntities _context;
        public SuppliersController()
        {
            _crud = new CRUD();
            _context = new babymart_vnEntities();
        }
        public ActionResult LoadLstSuppliers()
        {
            var result = _context.soft_Suppliers.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList()
        {
            var list = _context.soft_Suppliers.ToList();
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
                var item = _context.soft_Suppliers.Find(id);
                _context.soft_Suppliers.Remove(item);
                _context.SaveChanges();
                Messaging.messaging = "Xóa Nhà phân phối tính thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Add(soft_Suppliers model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                _context.soft_Suppliers.Add(model);
                _context.SaveChanges();
                Messaging.messaging = "Thêm Nhà phân phối thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(soft_Suppliers model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_Suppliers.Find(model.SuppliersId);
                //_context.Entry(data).CurrentValues.SetValues(model);// = EntityState.Modified;
                //_context.SaveChanges();
                _crud.Update<soft_Suppliers>(model);
                _crud.SaveChanges();
                Messaging.messaging = "Cập nhật Nhà phân phối thành công";
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
