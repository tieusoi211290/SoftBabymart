//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftBabymartVn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sys_Role
    {
        public sys_Role()
        {
            this.sys_Employee_Role = new HashSet<sys_Employee_Role>();
        }
    
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public Nullable<int> Group_Id { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }
    
        public virtual soft_Channel soft_Channel { get; set; }
        public virtual ICollection<sys_Employee_Role> sys_Employee_Role { get; set; }
    }
}