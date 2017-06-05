using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{

    public class ProductModel
    {
        public int id { get; set; }
        public string masp { get; set; }
        public string tensp { get; set; }
        public int soft_IdGroupProduct { get; set; }
        public int soft_IdNPP { get; set; }
        public int soft_IdKho { get; set; }
        public int soft_IdDvt { get; set; }

        public string soft_Barcode { get; set; }
        public int soft_GiaTC { get; set; }
        public int soft_GiaM { get; set; }

    }
    public class ModelPropetiesProduct
    {

        public int id { get; set; }
        public int StoreId { get; set; }
        public string masp { get; set; }
        public int maloai { get; set; }
        public int mahieu { get; set; }
        public int plantype { get; set; }
        public string tensp { get; set; }
        public string tensp_us { get; set; }
        public int typeshowhome { get; set; }
        public string thongtin { get; set; }
        public string thongtin_us { get; set; }
        public string spurl { get; set; }
        public string spurl_us { get; set; }
        public string sptitle { get; set; }
        public string sptitle_us { get; set; }
        public string spdescription { get; set; }
        public string spdescription_us { get; set; }
        public string spkeywords_us { get; set; }
        public string spkeywords { get; set; }
        public bool hide { get; set; }
        public string chitiet { get; set; }
        public string chitiet_us { get; set; }
        public int douutien { get; set; }
        public bool ischeckout { get; set; }
        public bool ischecksaleoff { get; set; }
        public bool ischeckgift { get; set; }
        public string gift { get; set; }
        public int countsale { get; set; }
        public System.DateTime timeend { get; set; }
        public double kg { get; set; }
        public double chieudai { get; set; }
        public double chieurong { get; set; }
        public double chieucao { get; set; }
        public bool showkg { get; set; }
        public bool showcm { get; set; }
        public bool showhome { get; set; }
        public bool showbanner { get; set; }
        public bool FromSoft { get; set; }
        public int soft_IdGroupProduct { get; set; }
        public int soft_IdNPP { get; set; }
        public int soft_IdKho { get; set; }
        public int soft_IdDvt { get; set; }
        public string soft_Barcode { get; set; }
        public int soft_GiaTC { get; set; }
        public int soft_GiaM { get; set; }


    }

    public class ProductSampleModel
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public int UnitId { get; set; }
        public string Barcode { get; set; }
        public string Code { get; set; }
        public int CodeType { get; set; }
        public string ProductName { get; set; }
        public string Detail_Info { get; set; }
        public bool Stop_Sale { get; set; }
        public int PriceBase { get; set; }
        public int PriceBase_Old { get; set; }
        public int PriceInput { get; set; }
        public int Stock_Total { get; set; }
        public int EmployeeCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public int EmployeeUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool HasInChannel { get; set; }
        public SuppliersModel soft_Suppliers { get; set; }
    }

    public class Prodcut_Branches_PriceChannel
    {
        public Product_PriceModel product_price { get; set; }
        public Product_StockModel product_stock { get; set; }
        public ProductSampleModel product { get; set; }
    }

    public class Product_PriceModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ChannelId { get; set; }
        public int Price { get; set; }
        public int PriceChange { get; set; }
        public string Note { get; set; }
        public int EmployeeCreate { get; set; }
        public System.DateTime DateCreate { get; set; }
        public string EmployeeUpdate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    }

    public class Product_StockModel
    {
        public int BranchesId { get; set; }
        public int ProductId { get; set; }
        public int Stock_Total { get; set; }
        public string Note { get; set; }
        public int EmployeeCreate { get; set; }
        public System.DateTime DateCreate { get; set; }
        public Nullable<int> EmployeeUpdate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    }
}