using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using AutoMapper;
using SoftBabymartVn.Models.Module;
using SoftBabymartVn.Models;
namespace SoftBabymartVn.Infractstructure
{
    public static class Mappers
    {
        public static void Boot()
        {
            babymart_vnEntities _context = new babymart_vnEntities();
            Mapper.CreateMap<shop_sanpham, ProductModel>();
            Mapper.CreateMap<ProductModel, shop_sanpham>();
            Mapper.CreateMap<shop_sanpham, ModelPropetiesProduct>();
            Mapper.CreateMap<ModelPropetiesProduct, shop_sanpham>();
            Mapper.CreateMap<soft_group_product, GroupProductModel>();
            Mapper.CreateMap<GroupProductModel, soft_group_product>();
            Mapper.CreateMap<soft_group_customer_product, GroupCustomerProductModel>();
            Mapper.CreateMap<GroupCustomerProductModel, soft_group_customer_product>();

            Mapper.CreateMap<KHO_OrderModel, soft_KHO_Order>();
            Mapper.CreateMap<soft_KHO_Order, KHO_OrderModel>();

            Mapper.CreateMap<KHO_Order_ChildModel, soft_KHO_Order_Child>();
            Mapper.CreateMap<soft_KHO_Order_Child, KHO_Order_ChildModel>();


            // Mapper.CreateMap<soft_KHO_Product, KHO_ProductModel>()
            //.ForMember(p => p.Kho, o => o.ResolveUsing(p =>
            //{
            //    var kho = _context.soft_Kho.Find(p.IdKho);
            //    if (kho != null)
            //        return kho.Kho;
            //    return string.Empty;
            //}))
            // .ForMember(p => p.TenNPP, o => o.ResolveUsing(p =>
            // {
            //     var npp = _context.soft_NPP.Find(p.IdNPP);
            //     if (npp != null)
            //         return npp.TenNPP;
            //     return string.Empty;
            // }))
            // .ForMember(p => p.ItemsKHO_Product_DetailModel, o => o.ResolveUsing(p =>
            // {
            //     List<KHO_Product_DetailModel> lstTmp = new List<KHO_Product_DetailModel>();
            //     foreach (var item in p.soft_KHO_Product_Detail)
            //     {
            //         var product = _context.shop_sanpham.Find(item.IdProduct);
            //         if (product != null)
            //         {
            //             var itemTmp = new KHO_Product_DetailModel
            //             {
            //                 Id = item.Id,
            //                 Id_kho_product = item.Id_kho_product,
            //                 IdProduct = item.IdProduct,
            //                 SL = item.SL,
            //                 Price = item.Price,
            //                 masp = product.masp,
            //                 soft_Barcode = product.soft_Barcode != null ? product.soft_Barcode : string.Empty,
            //                 soft_GiaM = product.soft_GiaM != null ? product.soft_GiaM.Value : 0,
            //                 tensp = product.tensp
            //             };
            //             lstTmp.Add(itemTmp);
            //         }
            //     }
            //     return lstTmp;
            // }));
            //Mapper.CreateMap<KHO_ProductModel, soft_KHO_Product>();

            //Mapper.CreateMap<soft_KHO_Product_Detail, KHO_Product_DetailModel>();
            //Mapper.CreateMap<KHO_Product_DetailModel, soft_KHO_Product_Detail>();
        }

    }
}