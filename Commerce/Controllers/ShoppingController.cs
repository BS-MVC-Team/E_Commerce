using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Commerce.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult ShoppingCart()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;

            ShoppingCartRepository repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);

            List<ShoppingCartInformation> shoppingCarts = new List<ShoppingCartInformation>();
            decimal TotalPrice = 0;
            foreach (var item in Data)
            {
                ProductRepository productRepository = new ProductRepository();
                var productInformation = productRepository.FindById(item.ProductID);

                ProductFormatRepository productFormatRepository = new ProductFormatRepository();
                var productFormatInformation = productFormatRepository.FindById(item.ProductFormatID);

                ShoppingCartInformation shoppingCart = new ShoppingCartInformation()
                {
                    ShoppingCartID = item.ShoppingCartID,
                    ProductFormatID = item.ProductFormatID,
                    ProductName = productInformation.ProductName,
                    UnitPrice = productInformation.UnitPrice,
                    Color = productFormatInformation.Color,
                    Image = productFormatInformation.image,
                    Size = productFormatInformation.Size,
                    Quantity = item.Quantity
                };
                TotalPrice += productInformation.UnitPrice * item.Quantity;
                shoppingCarts.Add(shoppingCart);
            }

            ViewBag.ShoppingCart = shoppingCarts;
            ViewBag.TotalPrice = TotalPrice;

            return View();
        }

        [HttpPost]
        public JsonResult ShoppingCart(int ProductFormatID)
        {
            ProductFormatRepository repository = new ProductFormatRepository();
            var result = repository.FindById(ProductFormatID);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateCartItem(int ShoppingCartID, int Quantity)
        {
            ShoppingCartRepository repository = new ShoppingCartRepository();
            repository.Update(ShoppingCartID, Quantity);
            return Json("修改成功");
        }

        [HttpPost]
        public JsonResult DeleteCartItem(int ShoppingCartID)
        {
            ShoppingCartRepository repository = new ShoppingCartRepository();
            repository.Delete(ShoppingCartID);
            return Json("刪除成功");
        }

        [HttpPost]
        public JsonResult Order(string name, string phone, string address, string memberID, decimal totalPrice)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                return Json("填空區不可有空白");
            }
            else
            {
                if (Regex.Match(name, @"[\u3000-\u9FA5\x20]{2,4}").Success)
                {
                    if (Regex.Match(phone, @"(\(?\d{3,4}\)?)?[\s-]?\d{7,8}[\s-]?\d{0,4}").Success)
                    {
                        ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
                        OrdersRepository ordersRepository = new OrdersRepository();
                        EmployeesRepository employeesRepository = new EmployeesRepository();
                        ProductFormatRepository productFormatRepository = new ProductFormatRepository();
                        OrderDetailsRepository orderDetailsRepository = new OrderDetailsRepository();
                        ProductRepository productRepository = new ProductRepository();
                        var employees = employeesRepository.GetAll();
                        var randomNumber = new Random().Next(0, employees.Count());


                        Orders orders = new Orders()
                        {
                            EmployeeID = employees.ElementAt(randomNumber).EmployeeID,
                            MemberID = memberID,
                            ShipName = name,
                            ShipAddress = address,
                            ShipPhone = phone,
                            OrderDate = DateTime.Now,
                            Status = "未送貨",
                            TotalPrice = totalPrice
                        };

                        ordersRepository.Create(orders);
                        Procedure.Procedure procedure = new Procedure.Procedure();
                        var orderTempID = procedure.FindOrderID();

                        var shoppingCart = shoppingCartRepository.FindByMemberID(memberID);
                        Stack<OrderDetails> orderData = new Stack<OrderDetails>();
                        orderData.Clear();
                        foreach (var item in shoppingCart)
                        {
                            if(productFormatRepository.FindById(item.ProductFormatID).StockQuantity - item.Quantity >= 0)
                            {
                                OrderDetails orderDetails = new OrderDetails()
                                {
                                    OrderID = orderTempID.OrderID,
                                    ProductFormatID = item.ProductFormatID,
                                    Quantity = item.Quantity,
                                    UnitPrice = productRepository.FindById(item.ProductID).UnitPrice
                                };
                                orderData.Push(orderDetails);
                            }
                            else
                            {
                                var productFormatTemp = productFormatRepository.FindById(item.ProductFormatID);
                                var productTemp = productRepository.FindById(productFormatTemp.ProductID);


                                return Json("產品：" + productTemp.ProductName + Environment.NewLine + 
                                    "顏色：" + productFormatTemp.Color + Environment.NewLine + 
                                    "尺寸：" + productFormatTemp.Size + Environment.NewLine + 
                                    "剩餘數量：" + productFormatTemp.StockQuantity + Environment.NewLine +
                                    "請重新下訂！");
                            }                          
                        }

                        foreach(var orderItem in orderData)
                        {
                            orderDetailsRepository.Create(orderItem);
                        }

                        shoppingCartRepository.DeleteByMemberId(memberID);

                        return Json("下訂成功");
                    }
                    else
                    {
                        return Json("電話格式有誤");
                    }
                }
                else
                {
                    return Json("姓名不符合格式");
                }
            }
        }
    }
}