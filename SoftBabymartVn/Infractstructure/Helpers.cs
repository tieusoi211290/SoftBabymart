using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SoftBabymartVn.Infractstructure
{
    public class Helpers
    {
        public static int FindMin(int pageindex, int pagesize)
        {
            return ((pageindex - 1) * pagesize);
        }
        public static int GetQuantity(int total, int pageindex, int pagesize)
        {
            int max = (pageindex * pagesize);
            if (total > max)
            {
                return pagesize;
            }
            else
            {
                return pagesize - (max - total);
            }
        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static List<SoftBabymartVn.Models.Module.ChannelModel> ListChannel(bool All = true)
        {
            var db = new SoftBabymartVn.Models.babymart_vnEntities();
            var channel = db.soft_Channel.ToList();
            if (All == false)
                channel = channel.Where(o => o.Type != (int)TypeChannel.IsChannelOnline).ToList();
            var rs = new List<SoftBabymartVn.Models.Module.ChannelModel>();
            foreach (var item in channel)
            {
                rs.Add(new Models.Module.ChannelModel
                {
                    Channel = item.Channel,
                    Id = item.Id
                });
            }
            return rs;
        }

        public static string RolesAuthor(RolesEnum[] roles)
        {
            var role = "";
            if (roles != null && roles.Length > 0)
            {
                foreach (var item in roles.Select((value, i) => new { i, value }))
                {
                    if (item.i == 0)
                        role = Enum.GetName(typeof(RolesEnum), item.value);
                    else
                        role += Enum.GetName(typeof(RolesEnum), item);
                }
            }
            return role;

        }

    }
}