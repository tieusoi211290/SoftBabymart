using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public class KHO_OrderModel
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public System.DateTime DateOrder { get; set; }
        public Nullable<double> Total { get; set; }
    }
    public class KHO_Order_ChildModel
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public ModelPropetiesProduct product { get; set; }
        public int IdNPP { get; set; }
        public string TenNPP { get; set; }
        public Nullable<int> Total_Day { get; set; }
        public Nullable<int> Total_Product { get; set; }
        public Nullable<int> Total_Order { get; set; }
        public Nullable<double> Total_Money { get; set; }
        public string Note { get; set; }
    }
    public class soft_KHO_Order_Input
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> DateInput { get; set; }
        public string Note { get; set; }
        public Nullable<double> Total { get; set; }
    }
    public class soft_KHO_Order_Input_Child
    {
        public int Id_Order_Input { get; set; }
        public int IdProduct { get; set; }
        public Nullable<int> Total_Input { get; set; }
        public Nullable<double> Total_Money { get; set; }
        public Nullable<double> Total { get; set; }
        public string Note { get; set; }
    }
}