using AutoMapper;
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
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        private babymart_vnEntities _context = new babymart_vnEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Research(string keyword)
        {
            var Messaging = new RenderMessaging();
            try
            {
                var customer = _context.soft_Customer.FirstOrDefault(o =>
                  o.Email.ToLower().Contains(keyword)
                   ||(o.Code.ToLower().Contains(keyword)
                  || o.Phone.ToLower().Contains(keyword)));


                if (customer != null)
                    Messaging.Data = Mapper.Map<CustomerModel>(customer);
                else
                    Messaging.Data = null;
            }
            catch (Exception ex)
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

    }
}
