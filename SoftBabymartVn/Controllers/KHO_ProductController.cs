using SoftBabymartVn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class KHO_ProductController : Controller
    {
        //
        // GET: /KHO_Product/

        private babymart_vnEntities _context;
        public KHO_ProductController()
        {
            _context = new babymart_vnEntities();
        }
      
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/CN/KHO/_Kho_Product.cshtml");
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
