using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class PopualityProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string image { get; set; }
    }
}
