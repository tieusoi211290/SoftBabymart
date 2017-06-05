using AutoMapper;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Enum;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftBabymartVn.Infractstructure;
using SoftBabymartVn.Infractstructure.Security;
using System.Web.Security;
using AutoMapper;
namespace SoftBabymartVn.Controllers
{
    public class CommonController : BaseController
    {
        private babymart_vnEntities _context = new babymart_vnEntities();


        public JsonResult GetConfigSys()
        {
            var Messaging = new RenderMessaging();
            try
            {
                if (User == null || User.ChannelId <= 0)
                {
                    Messaging.isError = true;
                    Messaging.messaging = "Vui lòng đăng nhập lại !";
                }

                var config = new ConfigModel();
                config.User = Mapper.Map<Config_UserModel>(User);
                config.User.Branches = new List<BranchesModel>();
                config.User.Channel = new List<ChannelModel>();
                config.User.BranchesId = User.BranchesId;
                config.User.ChannelId = User.ChannelId;
                var lstBranches = Mapper.Map<List<BranchesModel>>(_context.soft_Branches.ToList());
                var lstChannel = Mapper.Map<List<ChannelModel>>(_context.soft_Channel.ToList());
                var BranchesIds = User.BranchesIds.Distinct().ToArray();

                foreach (var it in BranchesIds)
                {
                    var branche = lstBranches.FirstOrDefault(o => o.BranchesId == it);
                    if (branche != null)
                    {
                        var channel = branche.soft_Channel.Where(o => User.ChannelIds.Contains(o.Id)).ToList();

                        config.User.Branches.Add(new BranchesModel
                        {
                            BranchesId = branche.BranchesId,
                            BranchesName = branche.BranchesName,
                            soft_Channel = Mapper.Map<List<ChannelModel>>(channel)
                        });
                    }

                }

                Messaging.Data = config;
            }
            catch
            {
                Messaging.isError = true;
                Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
            }
            return Json(Messaging, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadLstBranches()
        {
            var branches = Mapper.Map<List<BranchesModel>>(_context.soft_Branches.ToList());

            return Json(branches, JsonRequestBehavior.AllowGet);
        }
    }
}
