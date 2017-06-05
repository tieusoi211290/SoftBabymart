using AutoMapper;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace SoftBabymartVn.Controllers
{
    public class ChannelCatalogController : BaseController
    {
        //
        // GET: /KENH/
        private babymart_vnEntities _context;
        public ChannelCatalogController()
        {
            _context = new babymart_vnEntities();
        }

        public ActionResult LoadLstCatalog_Channel()
        {
            var result = _context.soft_Catalog.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/DM/_Kenh.cshtml");
        }
        public JsonResult LoadListKenh()
        {
            var list = Mapper.Map<List<ChannelModel>>(_context.soft_Channel.ToList());
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Add(ChannelModel model)
        {
            var Messaging = new RenderMessaging();
            //try
            //{
            //    _context.soft_Channel.Add(Mapper.Map<soft_Channel>(model));
            //    _context.SaveChanges();
            //    Messaging.success = true;
            //    Messaging.messaging = "Thêm kênh thành công";
            //}
            //catch
            //{
            //    Messaging.isError = true;
            //    Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            //}
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult Edit(ChannelModel model)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var data = _context.soft_Channel.Find(model.Id);
                _context.Entry(data).CurrentValues.SetValues(Mapper.Map<soft_Channel>(model));
                _context.SaveChanges();
                Messaging.messaging = "Cập nhật kênh thành công";
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();

            //try
            //{
            //    var item = _context.soft_Channel.Find(id);
            //    _context.soft_Channel.Remove(item);
            //    _context.SaveChanges();
            //    Messaging.success = true;
            //    Messaging.messaging = "Xóa Kênh thành công";
            //}
            //catch
            //{
            //    Messaging.isError = true;
            //    Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            //}
            return Json(Messaging, JsonRequestBehavior.AllowGet);

        }
    }
}
