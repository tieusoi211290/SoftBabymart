//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftBabymartVn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sys_nhanbangtin
    {
        public sys_nhanbangtin()
        {
            this.sys_bangtin_conhang = new HashSet<sys_bangtin_conhang>();
        }
    
        public int id { get; set; }
        public string hoten { get; set; }
        public string email { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }
    
        public virtual ICollection<sys_bangtin_conhang> sys_bangtin_conhang { get; set; }
    }
}
