using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Models;

namespace SoftBabymartVn.Controllers
{
    public class BranchesController : BaseController
    {
        //
        // GET: /Branches/
        private CRUD _crud;
        private babymart_vnEntities _context = new babymart_vnEntities();
        public BranchesController()
        {
            _crud = new CRUD();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBranches()
      {
            var Messaging = new RenderMessaging();

            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }
                var result = _context.soft_Branches.ToList();
                var lst = Mapper.Map<List<BranchesModel>>(result);
                Messaging.Data = lst;
            }
            catch (Exception ex)
            {

            }

            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }
    }
}
