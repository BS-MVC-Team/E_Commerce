using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Commerce.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult ManageIndex()
        {
            return View();
        }

        public ActionResult Order()
        {
            OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
            ProductFormatRepository productFormatRepository = new ProductFormatRepository();
            ProductRepository productRepository = new ProductRepository();
            List<Detail> details = new List<Detail>();
            var orderDetails = orderDetailsRepository.GetAll();
            foreach(var item in orderDetails)
            {
                var productFormat = productFormatRepository.FindById(item.ProductFormatID);
                var product = productRepository.FindById(productFormat.ProductID);
                Detail detail = new Detail()
                {
                    OrderID = item.OrderID,
                    ProductID = productFormat.ProductID,
                    ProductFormatID = item.ProductFormatID,
                    ProductName = product.ProductName,
                    Color = productFormat.Color,
                    Size = productFormat.Size,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                details.Add(detail);
            }

            ViewBag.Detail = details;

            return View();
        }

        public ActionResult ProductInfoEdit()
        {
            return View();
        }

        public ActionResult ProductsInfo()
        {
            return View();
        }

        public ActionResult Tasks()
        {
            return View();
        }

        public ActionResult Logistics()
        {
            OrdersRepository repository = new OrdersRepository();
            var orders = repository.GetAll();
            ViewBag.Orders = orders;

            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Analysis()
        {
            return View();
        }
    }
}