using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public class OrderModel
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
        public int TypeOrder { get; set; }
        public int Total { get; set; }

        public int Id_From { get; set; }
        public int Id_To { get; set; }

        public string Name_From { get; set; }
        public string Name_To { get; set; }

        public bool IsShip { get; set; }

        public int EmployeeCreate { get; set; }
        public string EmployeeNameCreate { get; set; }
        public System.DateTime DateCreate { get; set; }

        public Nullable<int> EmployeeUpdate { get; set; }
        public string EmployeeNameUpdate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }

        public List<Order_DetialModel> Detail { get; set; }
        public CustomerModel Customer { get; set; }
    }
    public class Order_InputTmpModel
    {
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public string Code { get; set; }
    }
    public class Order_DetialModel
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int Total { get; set; }
        public int Price { get; set; }
        public ProductSampleModel Product { get; set; }
    }
    public class OrderChannel_GroupModel
    {
        public List<Order_DetialModel> products { get; set; }
        public SuppliersModel suppliers { get; set; }
    }

}