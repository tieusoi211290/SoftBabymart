using Newtonsoft.Json;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using SoftBabymartVn.Models.Module;
using System.Collections.Generic;

namespace SoftBabymartVn.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
