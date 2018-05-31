using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
using Procedure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
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
            /*var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;

            ViewBag.UserName = ticket.UserData;*/
            var productrepository = new ProductRepository();
            var products = productrepository.FindIndexProducts();
            ViewData["products"] = products;
            


            return View();
        }

        /*[HttpPost]
        public JsonResult Index(string id)
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
           
                Procedure.Procedure procedure = new Procedure.Procedure();
                var ProductFormat = procedure.GetFormatByProductID(int.Parse(id));
                ViewData["ProductFormat"] = ProductFormat;
            return Json(ProductFormat);
            
         
            
        }*/

        public ActionResult SignIn()
        {
            ViewBag.Title = "登入";

            return View();
        }


        public ActionResult ProductDetail()
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

        public ActionResult Contact()
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

            ViewBag.Title = "聯絡我們";

            return View();
        }

        [HttpPost]
        public JsonResult SignIn(string memberid, string memberpassword)
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
                        1, "MemberId", DateTime.Now, DateTime.Now.AddMinutes(5), false, memberid);

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

        public ActionResult SignUp()
        {
            ViewBag.Title = "註冊會員";

            return View();
        }

        [HttpPost]
        public JsonResult SignUp(string MemberId,string MemberPassword, string MemberCheckPassword, string Name,string Phone,string Email,string Address)
        {
            if(string.IsNullOrWhiteSpace(MemberId) || string.IsNullOrWhiteSpace(MemberPassword) || string.IsNullOrWhiteSpace(MemberCheckPassword) ||
                string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Address))
            {
                return Json("填空區不可有空白");
            }
            else
            {
                if(Regex.Match(Name, @"[\u3000-\u9FA5\x20]{2,4}").Success && Regex.Match(MemberId, @"[\w\-]{8,12}").Success)
                {
                    if(Regex.Match(MemberPassword, @"[\x21-\x7E]{8,12}").Success)
                    {
                        if (MemberPassword != MemberCheckPassword)
                        {
                            return Json("確認密碼有誤");
                        }
                        else
                        {
                            if (IsValidEmail(Email) && Regex.Match(Phone, @"(\(?\d{3,4}\)?)?[\s-]?\d{7,8}[\s-]?\d{0,4}").Success)
                            {
                                MemberRepository repository = new MemberRepository();
                                if (repository.FindById(MemberId) == null)
                                {
                                    Members members = new Members()
                                    {
                                        MemberID = MemberId,
                                        Password = MemberPassword,
                                        Name = Name,
                                        Phone = Phone,
                                        Email = Email,
                                        Address = Address
                                    };

                                    repository.Create(members);
                                    return Json("註冊成功");
                                }
                                else
                                {
                                    return Json("此帳號已擁有");
                                }

                            }
                            else
                            {
                                return Json("電話或是電子信箱格式有誤");
                            }
                        }
                    }
                    else
                    {
                        return Json("密碼需8~12碼或是格式不符合");
                    }                   
                }
                else
                {
                    return Json("姓名不符合格式或是帳戶需8~12碼");
                }                
            }           
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

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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
                productcolor.Add(item.Color);
                productsize.Add(item.Size);
            }
            ViewData["productcolor"] = productcolor.Distinct();
            ViewData["productsize"] = productsize.Distinct();
            ViewData["product"] = JSONSerializer.Serialize(product);
            
            return View();
        }

        public ActionResult Modal(string productid)
        {
            Procedure.Procedure procedure = new Procedure.Procedure();
            var ProductFormat = procedure.GetFormatByProductID(int.Parse(productid));
            ViewData["ProductFormat"] = ProductFormat;
            var imagegroup = ProductFormat.Select((x)=>x.Image).Distinct();
            ViewData["ImageGroup"] = imagegroup;
            var productName = ProductFormat.Select((x) => x.ProductName).Distinct();
            ViewData["ProductName"] = productName;
            var Description = ProductFormat.Select((x) => x.Description).Distinct();
            ViewData["Description"] = Description;
            var UnitPrice = ProductFormat.Select((x) => x.UnitPrice).Distinct();
            ViewData["UnitPrice"] = UnitPrice;
            var Color = ProductFormat.Select((x) => x.Color).Distinct();
            ViewData["Color"] = Color;
            var Size = ProductFormat.Select((x) => x.Size).Distinct();
            ViewData["Size"] = Size;
            ViewData["ProductID"] = productid;
            return PartialView();
        }


        private int stock;
        private int productformatid;
        private string image;
        private string productName;
        private decimal unitPrice;
        [HttpPost]
        public bool ModaltoCart(string productid, string size,string color,string quantity)
        {
            
            
            
            Procedure.Procedure procedure = new Procedure.Procedure();
            var ProductFormat = procedure.GetFormatIDByProductIDCS(int.Parse(productid), size, color);
            foreach (var item in ProductFormat)
            {
                productformatid = item.ProductFormatID;
                stock = item.StockQuantity;
                image = item.Image;
                productName = item.ProductName;
                unitPrice = item.UnitPrice;
            }
            if (int.Parse(quantity)< stock)
            {
                ShoppingCart shoppingCart = new ShoppingCart {
                    MemberID = "123",
                    ProductFormatID = productformatid,
                    Image = image,
                    Size = size,
                    Color = color,
                    ProductID = int.Parse(productid),
                    ProductName = productName,
                    Quantity = int.Parse(quantity),
                    UnitPrice = unitPrice
                };
                var repository = new ShoppingCartRepository();
                repository.Create(shoppingCart);
                var isempty = true;
                return isempty;
            }
            else
            {
                var isempty = false;
                return isempty;
            }
            
        }
    }
}