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
    
    public partial class soft_Product
    {
        public soft_Product()
        {
            this.soft_Branches_Product_Stock = new HashSet<soft_Branches_Product_Stock>();
            this.soft_Channel_Product_Price = new HashSet<soft_Channel_Product_Price>();
            this.soft_Order_Child = new HashSet<soft_Order_Child>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Catalog { get; set; }
        public Nullable<int> SuppliersId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string Barcode { get; set; }
        public string Code { get; set; }
        public Nullable<int> CodeType { get; set; }
        public string ProductName { get; set; }
        public string Detail_Info { get; set; }
        public Nullable<bool> Stop_Sale { get; set; }
        public Nullable<int> PriceBase { get; set; }
        public Nullable<int> PriceBase_Old { get; set; }
        public Nullable<int> PriceInput { get; set; }
        public int Stock_Total { get; set; }
        public int EmployeeCreate { get; set; }
        public System.DateTime DateCreate { get; set; }
        public Nullable<int> EmployeeUpdate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    
        public virtual ICollection<soft_Branches_Product_Stock> soft_Branches_Product_Stock { get; set; }
        public virtual ICollection<soft_Channel_Product_Price> soft_Channel_Product_Price { get; set; }
        public virtual ICollection<soft_Order_Child> soft_Order_Child { get; set; }
        public virtual soft_Suppliers soft_Suppliers { get; set; }
    }
}
