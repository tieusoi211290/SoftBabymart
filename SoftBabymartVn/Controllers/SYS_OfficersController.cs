using AutoMapper;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using SoftBabymartVn.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class SYS_OfficersController : Controller
    {
        ////
        //// GET: /Officers/
        //private babymart_vnEntities _context;
        //public SYS_OfficersController()
        //{
        //    _context = new babymart_vnEntities();
        //}
        //public ActionResult RenderView()
        //{
        //    return PartialView("~/Views/Shared/Partial/module/SYS/_Officers.cshtml");
        //}
        //public ActionResult ChangePWDRenderView()
        //{
        //    return PartialView("~/Views/Shared/Partial/module/SYS/_PWDOfficers.cshtml");
        //}
        //public JsonResult GetListNV()
        //{
        //    var listNv = Mapper.Map<List<NhanvienModel>>(_context.sys_nhanvien.ToList());
        //    foreach (var it in listNv)
        //    {
        //        // it.Kho = it.soft_KHO.Kho;
        //    }
        //    return Json(listNv, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetNV(int id)
        //{
        //    var nhanvien = Mapper.Map<NhanvienModel>(_context.sys_nhanvien.Find(id));
        //    var role = _context.sys_nhanvien_Role.Where(o => o.IdNv.Equals(id)).ToList();
        //    nhanvien.RoleNv = new List<Group_roleModel>();
        //    foreach (var item in role)
        //    {
        //        if (item.sys_group_role != null)
        //        {
        //            var mapobj = Mapper.Map<Group_roleModel>(item.sys_group_role);
        //            mapobj.IsCheck = true;
        //            nhanvien.RoleNv.Add(mapobj);
        //        }
        //    }
        //    return Json(nhanvien, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult AddOrUpdate(NhanvienModel model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        var obj = Mapper.Map<sys_nhanvien>(model);
        //        foreach (var item in model.RoleNv)
        //        {
        //            if (item.IsCheck == true)
        //            {
        //                var role = new sys_nhanvien_Role();
        //                role.IdNv = model.Id;
        //                role.IdRole = item.Id;
        //                role.DateUpdate = DateTime.Now;
        //                obj.sys_nhanvien_Role.Add(role);
        //            }
        //        }
        //        if (model.Id == 0)
        //        {
        //            //var kho = _context.soft_KHO.Find(model.IdKho);
        //            //obj.soft_KHO = kho;
        //            _context.sys_nhanvien.Add(obj);
        //            _context.SaveChanges();
        //            Messaging.success = true;
        //            Messaging.messaging = "Thêm nhân viên thành công";
        //        }
        //        else
        //        {
        //            var nvien = _context.sys_nhanvien.Find(model.Id);
        //            var nvRole = _context.sys_nhanvien_Role.Where(o => o.IdNv == model.Id).ToList();
        //            foreach (var item in nvRole)
        //                _context.sys_nhanvien_Role.Remove(item);
        //            nvien.TenNv = obj.TenNv;
        //            nvien.Sdt = obj.Sdt;
        //            nvien.Diachi = obj.Diachi;
        //            // nvien.IdKho = obj.IdKho;
        //            //nvien.MaNV = obj.MaNV;
        //            _context.Entry(nvien).State = EntityState.Modified;
        //            foreach (var it in obj.sys_nhanvien_Role)
        //                _context.sys_nhanvien_Role.Add(it);
        //            _context.SaveChanges();
        //            Messaging.success = true;
        //            Messaging.messaging = "Cập nhật nhân viên thành công";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Messaging.success = true;
        //        Messaging.messaging = "Có lỗi xảy ra";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}

    }
}
