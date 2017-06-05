using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Enum;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftBabymartVn.Controllers
{
    public class PartialController : Controller
    {
        private babymart_vnEntities _context = new babymart_vnEntities();
        public ActionResult _navbar()
        {
            return PartialView("~/Views/Shared/Partial/_navbar.cshtml");
        }
        public JsonResult SetChannel(int ChannelId = 0)
        {
            if (ChannelId > 0)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var Channel = _context.soft_Channel.Find(ChannelId);

                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    CustomPrincipalSerializeModel serializeModel = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                    serializeModel.ChannelId = ChannelId;
                    serializeModel.BranchesId = Channel.soft_Branches.BranchesId;

                    string userData = Newtonsoft.Json.JsonConvert.SerializeObject(serializeModel);
                    var newticket = new FormsAuthenticationTicket(
                                              authTicket.Version,
                                              authTicket.Name,
                                              authTicket.IssueDate,
                                              authTicket.Expiration,
                                              true,
                                             userData);

                    authCookie.Value = FormsAuthentication.Encrypt(newticket);
                    HttpContext.Response.Cookies.Set(authCookie);
                }

            }
            return null;
        }
    }
}
