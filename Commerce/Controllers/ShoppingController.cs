using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
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

            return View();
        }

        [HttpPost]
        public JsonResult ShoppingCart(string productid, string color, string size, string Quantity)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            var cookieName = "shoppingcar";
            var productrepository = new ProductRepository();
            var product = productrepository.FindProductFormatByProductID(int.Parse(productid));
            var quantity = product.FirstOrDefault((x) => x.Color == color && x.Size == size);
            var s = new Shopping()
            {
                ProductID = quantity.ProductID,
                ProductName = quantity.ProductName,
                ProductImage = quantity.ProductImage,
                UnitPrice = quantity.UnitPrice,
                Description = quantity.Description,
                StockQuantity = quantity.StockQuantity,
                Size = quantity.Size,
                Color = quantity.Color,
                Quantity = int.Parse(Quantity),
                ProductFormatID = quantity.ProductFormatID,
            };
            if (quantity.StockQuantity == 0)
            {
                return Json("NoProductQuantity");
            }
            else
            {
                if (Request.Cookies["shoppingcar"] == null)
                {
                    var token = Guid.NewGuid().ToString();
                    var shopping = new List<Shopping>();
                    shopping.Add(s);
                    //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                    string json = JSONSerializer.Serialize(shopping);
                    var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(json))
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(hc);
                }
                else
                {
                    string json = HttpUtility.UrlDecode(Request.Cookies["shoppingcar"].Value);
                    var shopping = JSONSerializer.Deserialize<List<Shopping>>(json);
                    shopping.Add(s);
                    //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                    string jsons = JSONSerializer.Serialize(shopping);
                    var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(jsons))
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(hc);
                }
            }
            return Json("Add Success");
        }

        public ActionResult NavBar()
        {
            return PartialView();
        }
    }
}