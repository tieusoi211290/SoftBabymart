using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Enum
{
    [Serializable]
    [Flags]
    public enum RolesEnum
    {
        Administrator = 0,
        Read_OrderInput = 1,
        Create_OrderInput = 2
    }
}