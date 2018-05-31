﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class ShoppingCart
    {
        public string MemberID { get; set; }
        public int ProductID { get; set; }
        public int ProductFormatID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        
    }
}
