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
    
    public partial class shop_loai
    {
        public shop_loai()
        {
            this.shop_danhmuc = new HashSet<shop_danhmuc>();
            this.support_silder = new HashSet<support_silder>();
            this.support_textlink = new HashSet<support_textlink>();
            this.shop_thuonghieu = new HashSet<shop_thuonghieu>();
        }
    
        public int maloai { get; set; }
        public string tenloai { get; set; }
        public string title { get; set; }
        public string metadescription { get; set; }
        public string metakeywords { get; set; }
        public string banner_top_con { get; set; }
        public string link { get; set; }
        public string hinh { get; set; }
        public Nullable<bool> km { get; set; }
        public string banner_top { get; set; }
    
        public virtual ICollection<shop_danhmuc> shop_danhmuc { get; set; }
        public virtual ICollection<support_silder> support_silder { get; set; }
        public virtual ICollection<support_textlink> support_textlink { get; set; }
        public virtual ICollection<shop_thuonghieu> shop_thuonghieu { get; set; }
    }
}