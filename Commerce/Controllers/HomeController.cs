using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "首頁";

            return View();
        }

        public ActionResult Login_Member()
        {
            ViewBag.Title = "登入";

            return View();
        }

        [HttpPost]
        public JsonResult Login_Member(string memberid, string memberpassword)
        {
            Account account = new Account
            {
                memberid = memberid,
                memberpassword = memberpassword
            };

            MemberRepository repository = new MemberRepository();
            var member = repository.FindById(memberid);
            if (member == null)
            {
                return Json("");
            }
            else
            {
                if (memberpassword == member.Password)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1, "MemberId", DateTime.Now, DateTime.Now.AddMinutes(1), false, memberid);

                    var ticketData = FormsAuthentication.Encrypt(ticket);

                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketData);
                    cookie.Expires = ticket.Expiration;
                    Response.Cookies.Add(cookie);
                    return Json(account);
                }
                else
                {
                    return Json("");
                }
            }

        }

        public ActionResult Home()
        {
            ViewBag.Title = "首頁";
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

        public ActionResult SignUp()
        {
            ViewBag.Title = "註冊會員";

            return View();
        }

        public ActionResult Category()
        {
            ViewBag.Title = "目錄";

            return View();
        }

        public ActionResult PageModel()
        {
            ViewBag.Title = "型號";

            return View();
        }

        public ActionResult Login_Employee()
        {
            ViewBag.Title = "系統管理員登入";

            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
        public ActionResult ProductInterface(string productid)
        {
            ViewBag.Title = "產品介面";
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            var productrepository = new ProductRepository();
            var product = productrepository.FindProductFormatByProductID(int.Parse(productid));
            List<string> productcolor = new List<string>();
            List<string> productsize = new List<string>();
            foreach (var item in product)
            {
                ViewData["productid"] = item.ProductID;
                ViewData["productname"] = item.ProductName;
                ViewData["productprice"] = item.UnitPrice.ToString("#0.0");
                ViewData["Description"] = item.Description;
                ViewData["productimage"] = item.ProductImage;
                productcolor.Add(item.Color);
                productsize.Add(item.Size);
            }
            ViewData["productcolor"] = productcolor.Distinct();
            ViewData["productsize"] = productsize.Distinct();
            ViewData["product"] = JSONSerializer.Serialize(product);
            
            return View();
        }

        public ActionResult Quantity(string color, string size, string productjson)
        {
            JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();
            var products = JSONSerializer.Deserialize<List<FindProductFormatByProductID>>(productjson);
            var product = products.FirstOrDefault((x) => x.Color == color && x.Size == size);
            if (product == null)
            {
                ViewData["quantity"] = "0";
            }
            else
            {
                ViewData["quantity"] = product.StockQuantity.ToString();
            }
            return PartialView();
        }

        public ActionResult EnterShoppingcar(string productid, string color, string size, string Quantity)
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
                return View("NoProductQuantity");
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
            return RedirectToAction("Index", "Home");



        }

    }
}