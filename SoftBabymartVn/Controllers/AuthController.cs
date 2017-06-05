using Newtonsoft.Json;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftBabymartVn.Controllers
{
    public class AuthController : Controller
    {
        private babymart_vnEntities _context;
        public AuthController()
        {
            _context = new babymart_vnEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _context.sys_Employee.Where(u => u.Email == model.Username && u.Pwd == model.Password).FirstOrDefault();
                if (user != null)
                {
                    var roles = user.sys_Employee_Role.Select(m => m.sys_Role.Role).ToArray();
                    var channels = user.sys_Employee_Role.Select(o => o.sys_Role.ChannelId).ToArray();
                    var branches = user.sys_Employee_Role.Select(o => o.sys_Role.soft_Channel.soft_Branches.BranchesId).ToArray();
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.Id;
                    serializeModel.UserName = user.Name;
                    serializeModel.Email = user.Email;
                    serializeModel.roles = roles;

                    serializeModel.BranchesId = branches.FirstOrDefault();
                    serializeModel.ChannelId = channels.FirstOrDefault();
                    serializeModel.ChannelIds = channels;
                    serializeModel.BranchesIds = branches;

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                             user.Email,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(15),
                             false,
                             userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");

                    //if (roles.Contains("Admin"))
                    //{
                    //    return RedirectToAction("Index", "Admin");
                    //}
                    //else if (roles.Contains("User"))
                    //{
                    //    return RedirectToAction("Index", "User");
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(model);
        }
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Auth", "Index", null);
        }

    }
}
