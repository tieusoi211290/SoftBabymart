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
        public Nullable<int> StoreId { get; set; }
        public string masp { get; set; }
        public Nullable<int> maloai { get; set; }
        public Nullable<int> mahieu { get; set; }
        public Nullable<int> plantype { get; set; }
        public string tensp { get; set; }
        public string tensp_us { get; set; }
        public Nullable<int> typeshowhome { get; set; }
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
        public Nullable<bool> hide { get; set; }
        public string chitiet { get; set; }
        public string chitiet_us { get; set; }
        public Nullable<int> douutien { get; set; }
        public Nullable<bool> ischeckout { get; set; }
        public Nullable<bool> ischecksaleoff { get; set; }
        public Nullable<bool> ischeckgift { get; set; }
        public string gift { get; set; }
        public Nullable<int> countsale { get; set; }
        public Nullable<System.DateTime> timeend { get; set; }
        public Nullable<double> kg { get; set; }
        public Nullable<double> chieudai { get; set; }
        public Nullable<double> chieurong { get; set; }
        public Nullable<double> chieucao { get; set; }
        public Nullable<bool> showkg { get; set; }
        public Nullable<bool> showcm { get; set; }
        public Nullable<bool> showhome { get; set; }
        public Nullable<bool> showbanner { get; set; }
        public Nullable<bool> FromSoft { get; set; }
        public Nullable<int> soft_IdGroupProduct { get; set; }
        public Nullable<int> soft_IdNPP { get; set; }
        public Nullable<int> soft_IdKho { get; set; }
        public Nullable<int> soft_IdDvt { get; set; }
        public string soft_Barcode { get; set; }
        public Nullable<int> soft_GiaTC { get; set; }
        public Nullable<int> soft_GiaM { get; set; }


    }
}