using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public class Config_UserModel
    {
        public int UserId { get; set; }

        public int BranchesId { get; set; }
        public int ChannelId { get; set; }

        public List<ChannelModel> Channel { get; set; }
        public List<BranchesModel> Branches { get; set; }

        public int[] ChannelIds { get; set; }
        public int[] BranchesIds { get; set; }


        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] roles { get; set; }
    }
    public class ConfigModel
    {
        public Config_UserModel User { get; set; }
    }
}