using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
        public int MemberID { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipPhone { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ReceiptedDate { get; set; }
        public string Status { get; set; }
        public decimal Discount { get; set; }
    }
}
