using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
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

        [NoCache]
        [HttpPost]
        public JsonResult UpdateShippedDate(int OrderID)
        {
            OrdersRepository repository = new OrdersRepository();
            var order = repository.FindById(OrderID);
            if(order.Status == "未送貨")
            {
                repository.UpdateShippedDateAndStatus(OrderID);
                return Json("");
            }
            else
            {
                return Json("點取無效");
            }           
        }

        [NoCache]
        [HttpPost]
        public JsonResult DeleteShippedDate(int OrderID)
        {
            OrdersRepository repository = new OrdersRepository();
            var order = repository.FindById(OrderID);

            if(order.Status == "送貨中")
            {
                repository.DeleteShippedDateAndStatus(OrderID);
                return Json("");
            }
            else
            {
                return Json("無效點取");
            }
        }

        [NoCache]
        [HttpPost]
        public JsonResult UpdateReceiptedDate(int OrderID)
        {
            OrdersRepository repository = new OrdersRepository();
            var order = repository.FindById(OrderID);
            if(order.Status=="送貨中")
            {
                repository.UpdateReceiptedDateAndStatus(OrderID);
                return Json("");
            }
            else
            {
                return Json("無效點取");
            }
        }

        [NoCache]
        [HttpPost]
        public JsonResult DeleteReceiptedDate(int OrderID)
        {
            OrdersRepository repository = new OrdersRepository();
            var order = repository.FindById(OrderID);
            if (order.Status == "已送達")
            {
                repository.DeleteReceiptedDateAndStatus(OrderID);
                return Json("");
            }
            else
            {
                return Json("無效點取");
            }
        }
    }
}