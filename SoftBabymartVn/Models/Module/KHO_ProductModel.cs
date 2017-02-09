using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public class KHO_ProductModel
    {
        public int Id { get; set; }
        public int IdKho { get; set; }
        public int IdNPP { get; set; }
        public string TenNPP { get; set; }
        public string Kho { get; set; }
        public int Total { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Note { get; set; }
        public bool IsNewRecord { get; set; }
        public List<KHO_Product_DetailModel> ItemsKHO_Product_DetailModel { get; set; }
    }
    public class KHO_Product_DetailModel
    {
        public int Id { get; set; }
        public int Id_kho_product { get; set; }
        public long IdProduct { get; set; }
        public int SL { get; set; }
        public int Price { get; set; }
        public string masp { get; set; }
        public string soft_Barcode { get; set; }
        public string tensp { get; set; }
        public int soft_GiaM { get; set; }
    }
}