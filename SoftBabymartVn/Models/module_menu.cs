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
    
    public partial class module_menu
    {
        public module_menu()
        {
            this.module_detail = new HashSet<module_detail>();
            this.module_group = new HashSet<module_group>();
        }
    
        public int id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string tenloai { get; set; }
        public string tenloai_us { get; set; }
        public Nullable<int> typemodule { get; set; }
        public string url { get; set; }
        public string des { get; set; }
        public string keyword { get; set; }
    
        public virtual ICollection<module_detail> module_detail { get; set; }
        public virtual ICollection<module_group> module_group { get; set; }
    }
}
