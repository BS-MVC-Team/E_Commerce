using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildSchool.MvcSolution.OnlineStore.Repository;

namespace OrderDetailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            var orderDetail =  orderDetailsRepository.FindById(1);
            Console.WriteLine("OrderID  ProductFormatID  Quantity  UnitPrice");
            Console.WriteLine(orderDetail.OrderID + "        " + orderDetail.ProductFormatID + "                " 
                + orderDetail.Quantity + "        " + orderDetail.UnitPrice);
            Console.ReadLine();
        }
    }
}
