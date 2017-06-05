using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;
using SoftBabymartVn.Models.Enum;

namespace SoftBabymartVn.Infractstructure.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (role.Contains(Enum.GetName(typeof(RolesEnum), RolesEnum.Administrator)))
                return true;
            else
            {
                if (roles.Any(r => role.Contains(r)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string ChannelName { get; set; }
        public int BranchesId { get; set; }
        public int ChannelId { get; set; }
        public int[] ChannelIds { get; set; }
        public int[] BranchesIds { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string[] roles { get; set; }

    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public int BranchesId { get; set; }
        public int ChannelId { get; set; }
        public int[] ChannelIds { get; set; }
        public int[] BranchesIds { get; set; }
        public string ChannelName { get; set; }
        public string[] roles { get; set; }
    }
}