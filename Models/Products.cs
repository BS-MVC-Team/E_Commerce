﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string ProductImage { get; set; }
    }
}