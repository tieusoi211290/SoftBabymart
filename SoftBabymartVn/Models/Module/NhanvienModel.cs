using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public class Nhanvien_RoleModel
    {
        public int Id { get; set; }
        public int IdRoel { get; set; }
        public int IdKho { get; set; }
        public DateTime DateUpdate { get; set; }

        //public string MaNV { get; set; }
        //public string TenNv { get; set; }
        //public string Sdt { get; set; }
        //public string Diachi { get; set; }
        //public string Kho { get; set; }
        //public string Role { get; set; }

        public NhanvienModel sys_nhanvien { get; set; }
        public Group_roleModel sys_group_role { get; set; }

    }
    public class NhanvienModel
    {
        public int Id { get; set; }
        public int IdRoel { get; set; }
        public int IdKho { get; set; }
        public string MaNV { get; set; }
        public string TenNv { get; set; }
        public string Sdt { get; set; }
        public string Diachi { get; set; }
        public string Kho { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public List<Group_roleModel> RoleNv { get; set; }
    }
    public class Group_roleModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }
        public bool IsCheck { get; set; }
    }
}