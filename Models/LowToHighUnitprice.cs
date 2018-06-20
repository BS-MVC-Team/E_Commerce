using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class LowToHighUnitprice
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public string image { get; set; }

        public string CategoryName { get; set; }
    }
}
