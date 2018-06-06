using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach(var item in Data)
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
        public JsonResult UpdateCartItem(int ShoppingCartID,int Quantity)
        {
            ShoppingCartRepository repository = new ShoppingCartRepository();
            repository.Update(ShoppingCartID,Quantity);
            return Json("修改成功");
        }

        [HttpPost]
        public JsonResult DeleteCartItem(int ShoppingCartID)
        {
            ShoppingCartRepository repository = new ShoppingCartRepository();
            repository.Delete(ShoppingCartID);
            return Json("刪除成功");
        }

        public ActionResult NavBar()
        {
            return PartialView();
        }

        public ActionResult SideCart()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return PartialView();
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
            
            ViewData["ShoppingCart"] = shoppingCarts;
            ViewData["TotalPrice"] = TotalPrice;
            return PartialView();
        }
    }
}