﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Models.Module
{
	public class CustomerModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pwd { get; set; }
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        
        public string NameShip { get; set; }
        public string AddressShip { get; set; }
        public string PhoneShip { get; set; }
        public int DistrictIdShip { get; set; }
        public int ProvinceIdShip { get; set; }
    }
}