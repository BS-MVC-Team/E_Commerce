using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            CategoryRepository categoryRepository = new CategoryRepository();
            ViewBag.Categories = categoryRepository.GetAll();

            return View();
        }

        public ActionResult ProductsInfo()
        {
            ProductRepository repository = new ProductRepository();
            ViewBag.Products = repository.GetAll();

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
            CategoryRepository repository = new CategoryRepository();
            ViewBag.Categories = repository.GetAll();

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

        [NoCache]
        [HttpPost]
        public JsonResult CreateNewProduct(string ProductName, string UnitPrice, string Description, string SelectCategory)
        {
            if(string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(UnitPrice) 
                || string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(SelectCategory))
            {
                return Json("填空區不可為空白");
            }
            else
            {
                if(decimal.TryParse(UnitPrice,out decimal result))
                {
                    if(result < 0)
                    {
                        return Json("價格不可為負值");
                    }
                    else
                    {
                        if(result == 0)
                        {
                            return Json("不符合價錢格式");
                        }
                        else
                        {
                            ProductRepository repository = new ProductRepository();
                            BuildSchool.MvcSolution.OnlineStore.Models.Products products = new BuildSchool.MvcSolution.OnlineStore.Models.Products()
                            {
                                ProductName = ProductName,
                                UnitPrice = result,
                                Description = Description,
                                CategoryID = int.Parse(SelectCategory),
                                ShelfDate = DateTime.Now
                            };

                            repository.Create(products);
                            return Json("");
                        }                       
                    }
                }
                else
                {
                    return Json("不符合價錢格式");
                }
            }
        }
    }
}