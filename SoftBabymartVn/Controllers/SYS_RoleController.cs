using AutoMapper;
using SoftBabymartVn.Models;
using SoftBabymartVn.Models.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBabymartVn.Controllers
{
    public class SYS_RoleController : Controller
    {
        //
        // GET: /SYS_Role/

        private babymart_vnEntities _context;
        public SYS_RoleController()
        {
            _context = new babymart_vnEntities();
        }
        public ActionResult RenderView()
        {
            return PartialView("~/Views/Shared/Partial/module/SYS/_Role.cshtml");
        }
        //public JsonResult GetListRole()
        //{
        //    var lstRole = Mapper.Map<List<Group_roleModel>>(_context.sys_group_role.ToList());
        //    return Json(lstRole, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult AddOrUpdate(Group_roleModel model)
        //{
        //    var Messaging = new RenderMessaging();
        //    try
        //    {
        //        if (model.Id == 0)
        //        {
        //            var obj = Mapper.Map<sys_group_role>(model);
        //            _context.sys_group_role.Add(obj);
        //            _context.SaveChanges();
        //            Messaging.success = true;
        //            Messaging.messaging = "Thêm phân quyền thành công";
        //        }
        //        else
        //        {
        //            var obj = Mapper.Map<sys_group_role>(model);
        //            var data = _context.sys_group_role.Find(obj.Id);
        //            _context.Entry(data).CurrentValues.SetValues(obj);// = EntityState.Modified;
        //            _context.SaveChanges();
        //            Messaging.success = true;
        //            Messaging.messaging = "Chỉnh sửa phân quyền thành công";
        //        }
        //    }
        //    catch
        //    {
        //        Messaging.isError = true;
        //        Messaging.messaging = "Do sự cố mạng, vui lòng thử lại !";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);
        //}
        //[HttpDelete]
        //public JsonResult Remove(int id)
        //{
        //    var Messaging = new RenderMessaging();

        //    try
        //    {
        //        var item = _context.sys_group_role.Find(id);
        //        _context.sys_group_role.Remove(item);
        //        _context.SaveChanges();
        //        Messaging.success = true;
        //        Messaging.messaging = "Xóa phân quyền thành công";
        //    }
        //    catch
        //    {
        //        Messaging.isError = true;
        //        Messaging.messaging = "Phân quyền có tính ràng buộc, vui lòng thử lại !";
        //    }
        //    return Json(Messaging, JsonRequestBehavior.AllowGet);

        //}
    }
}
