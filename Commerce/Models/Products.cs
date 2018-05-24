using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Commerce.Models
{
    public class Products
    {
        public string ProductName  { get; set; }
        public decimal UnitPrice   { get; set; }
        public string ProductImage { get; set; }
    }
}