using Newtonsoft.Json;
using SoftBabymartVn.Infractstructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SoftBabymartVn
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           // AuthConfig.RegisterAuth();
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);

                newUser.UserId = serializeModel.UserId;
                newUser.ChannelId = serializeModel.ChannelId;
                newUser.BranchesId= serializeModel.BranchesId;
                newUser.ChannelName = serializeModel.ChannelName;
                newUser.UserName = serializeModel.UserName;
                newUser.Email = serializeModel.Email;
                newUser.roles = serializeModel.roles;

                newUser.BranchesIds = serializeModel.BranchesIds;
                newUser.ChannelIds = serializeModel.ChannelIds;
                HttpContext.Current.User = newUser;
            }

        }
    }
}