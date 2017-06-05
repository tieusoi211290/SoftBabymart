using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
    public partial class RoleModel
    {
        public int Id { get; set; }
        public string ChannelId { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }
        public int EmployeeCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public int EmployeeUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public ChannelModel soft_Channel { get; set; }
        public List<Employee_RoleModel> sys_Employee_Role { get; set; }
    }
    public class Employee_RoleModel
    {
        public int RoleId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public int EmployeeUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public sys_Employee sys_Employee { get; set; }
        public RoleModel sys_Role { get; set; }
    }
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int EmployeeCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public int EmployeeUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public List<Employee_RoleModel> sys_Employee_Role { get; set; }
    }
}