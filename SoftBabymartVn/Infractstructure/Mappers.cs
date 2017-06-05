using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using AutoMapper;
using SoftBabymartVn.Models.Module;
using SoftBabymartVn.Models;
using SoftBabymartVn.Infractstructure.Security;
using SoftBabymartVn.Models.Enum;

namespace SoftBabymartVn.Infractstructure
{
    public static class Mappers
    {
        public static void Boot()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ChannelModel, soft_Channel>();
                cfg.CreateMap<soft_Channel, ChannelModel>();

                cfg.CreateMap<Channel_Product_PriceModel, soft_Channel_Product_Price>();
                cfg.CreateMap<soft_Channel_Product_Price, Channel_Product_PriceModel>();

                cfg.CreateMap<soft_Channel_Product_Price, Product_PriceModel>();
                cfg.CreateMap<soft_Branches_Product_Stock, Product_StockModel>();


                cfg.CreateMap<soft_Branches, BranchesModel>();

                cfg.CreateMap<BranchesModel, soft_Branches>();

                cfg.CreateMap<soft_Order, OrderModel>()
                .ForMember(a => a.Detail, b => b.MapFrom(c => c.soft_Order_Child))
                 .ForMember(a => a.StatusName, b => b.ResolveUsing(c =>
                 {
                     if (c.TypeOrder == (int)TypeOrder.Sale)
                     {
                         switch (c.Status)
                         {
                             case (int)StatusOrderSale.Done:
                                 return "Done";
                             case (int)StatusOrderSale.Shipped:
                                 return "Shipped";
                             default:
                                 return "Pending";
                         }
                     }
                     else
                     {
                         switch (c.Status)
                         {
                             case (int)StatusOrderProduct.Done:
                                 return "Done";
                             default:
                                 return "Pending";
                         }
                     }

                     return null;
                 }));
                //.ForMember(a => a.ChannelsFrom, b => b.ResolveUsing(c =>
                //{
                //    if (c.Id_From.HasValue)
                //    {
                //        return new ChannelModel
                //        {
                //            Id = (int)c.Id_From.Value,
                //            Channel = c.soft_Channel.Channel
                //        };
                //    }
                //    return null;
                //}))
                //  .ForMember(a => a.ChannelsTo, b => b.ResolveUsing(c =>
                //  {
                //      if (c.Id_To.HasValue)
                //      {
                //          return new ChannelModel
                //          {
                //              Id = (int)c.Id_To.Value,
                //              Channel = c.soft_Channel1.Channel
                //          };
                //      }
                //      return null;
                //  }));
                cfg.CreateMap<soft_Order_Child, Order_DetialModel>()
                .ForMember(a => a.Product, b => b.MapFrom(c => c.soft_Product));


                cfg.CreateMap<OrderModel, soft_Order>()
                 .ForMember(a => a.soft_Order_Child, b => b.MapFrom(c => c.Detail));

                cfg.CreateMap<Order_DetialModel, soft_Order_Child>();

                cfg.CreateMap<soft_Product, ProductSampleModel>();
                cfg.CreateMap<ProductSampleModel, soft_Product>();

                cfg.CreateMap<RoleModel, sys_Role>();
                cfg.CreateMap<sys_Role, RoleModel>();


                cfg.CreateMap<sys_Employee_Role, Employee_RoleModel>();
                cfg.CreateMap<Employee_RoleModel, sys_Employee_Role>();


                cfg.CreateMap<EmployeeModel, sys_Employee>();
                cfg.CreateMap<sys_Employee, EmployeeModel>();

                cfg.CreateMap<SuppliersModel, soft_Suppliers>();
                cfg.CreateMap<soft_Suppliers, SuppliersModel>();


                cfg.CreateMap<CustomPrincipal, Config_UserModel>();
                cfg.CreateMap<soft_Customer, CustomerModel>();
                cfg.CreateMap<CustomerModel, soft_Customer>();
                


            });



        }

    }
}