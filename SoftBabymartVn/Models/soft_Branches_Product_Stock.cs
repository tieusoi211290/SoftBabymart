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
    
    public partial class soft_Branches_Product_Stock
    {
        public int BranchesId { get; set; }
        public int ProductId { get; set; }
        public int Stock_Total { get; set; }
        public string Note { get; set; }
        public int EmployeeCreate { get; set; }
        public System.DateTime DateCreate { get; set; }
        public Nullable<int> EmployeeUpdate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    
        public virtual soft_Branches soft_Branches { get; set; }
        public virtual soft_Product soft_Product { get; set; }
    }
}
