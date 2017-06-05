using AutoMapper;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class KHO_OrderChildController : Controller
    {
        //
        // GET: /KHO_OrderChild/

        private babymart_vnEntities _context;
        public KHO_OrderChildController()
        {
            _context = new babymart_vnEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RenderView(int id)
        {
            ViewBag.Id = id;
            return PartialView("~/Views/Shared/Partial/module/CN/KHO/_Kho_Order_Clild.cshtml");
        }
        public JsonResult GetListChild(int id)
        {
            var list = Mapper.Map<List<KHO_Order_ChildModel>>(
                _context.soft_KHO_Order_Child.Where(o => o.IdOrder == id).ToList());
            foreach (var item in list)
            {
                if (item.IdNPP > 0)
                {
                    var NPP = _context.soft_NPP.Find(item.IdNPP);
                    if (NPP != null)
                        item.TenNPP = NPP.TenNPP;
                }
                if (item.IdProduct > 0)
                {
                    var product = _context.shop_sanpham.Find(item.IdProduct);
                    if (product != null)
                        item.product = Mapper.Map<ModelPropetiesProduct>(product);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult Remove(int id)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var item = _context.soft_KHO_Order_Child.Find(id);
                _context.soft_KHO_Order_Child.Remove(item);
                _context.SaveChanges();
                Messaging.success = true;
                Messaging.messaging = "Xóa sản phẩm khỏi đơn hàng thành công";
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
