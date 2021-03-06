﻿using BuildSchool.MvcSolution.OnlineStore.Models;
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
        [NoCache]
        public ActionResult Index()
        {
            ViewBag.Title = "首頁";


            var productrepository = new ProductRepository();
            var products = productrepository.FindIndexProducts();
            ViewData["products"] = products;

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();
            return View();
        }

        [HttpPost]
        public int NewProductItemCount()
        {
            var productrepository = new ProductRepository();
            var newProducts = productrepository.NewProduct();
            return newProducts.Count();
        }

        [HttpPost]
        public int HighToLowUnitpriceItemCount()
        {
            var productrepository = new ProductRepository();
            var HighToLowUnitprice = productrepository.HighToLowUnitprice();
            return HighToLowUnitprice.Count();
        }

        [HttpPost]
        public int LowToHighUnitpriceItemCount()
        {
            var productrepository = new ProductRepository();
            var LowToHighUnitprice = productrepository.LowToHighUnitprice();
            return LowToHighUnitprice.Count();
        }

        [HttpPost]
        public int ColorFilterItemCount(string Colors)
        {
            Procedure.Procedure procedure = new Procedure.Procedure();
            var Color = procedure.ColorFilters(Colors);
            return Color.Count();
        }

        public ActionResult NewProduct()
        {
            ViewBag.Title = "新商品";

            var productrepository = new ProductRepository();
            var newProducts = productrepository.NewProduct();
            ViewData["newProducts"] = newProducts;
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();

            return View();
        }

        public ActionResult HighToLowUnitprice()
        {
            ViewBag.Title = "價錢";

            var productrepository = new ProductRepository();
            var hightolow = productrepository.HighToLowUnitprice();
            ViewData["hightolow"] = hightolow;
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();

            return View();
        }

        public ActionResult LowToHighUnitprice()
        {
            ViewBag.Title = "價錢";

            var productrepository = new ProductRepository();
            var lowtohigh = productrepository.LowToHighUnitprice();
            ViewData["lowtohigh"] = lowtohigh;
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();

            return View();
        }


        [NoCache]
        public ActionResult SignIn()
        {
            ViewBag.Title = "登入";

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

        [NoCache]
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

        [NoCache]
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

        [NoCache]
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
                        1, "MemberId", DateTime.Now, DateTime.Now.AddMinutes(30), false, memberid);

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

        [NoCache]
        public ActionResult SignUp()
        {
            ViewBag.Title = "註冊會員";

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

        [NoCache]
        [HttpPost]
        public JsonResult SignUp(string MemberId, string MemberPassword, string MemberCheckPassword, string Name, string Phone, string Email, string Address)
        {
            if (string.IsNullOrWhiteSpace(MemberId) || string.IsNullOrWhiteSpace(MemberPassword) || string.IsNullOrWhiteSpace(MemberCheckPassword) ||
                string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Address))
            {
                return Json("填空區不可有空白");
            }
            else
            {
                if (Regex.Match(Name, @"[\u3000-\u9FA5\x20]{2,4}").Success && Regex.Match(MemberId, @"[\w\-]{8,12}").Success)
                {
                    if (Regex.Match(MemberPassword, @"[\x21-\x7E]{8,12}").Success)
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

        [NoCache]
        [Route("Logout")]
        public void Logout()
        {
            FormsAuthentication.SignOut();
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now;
            Response.Cookies.Add(cookie);
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

        [NoCache]
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

        [NoCache]
        public ActionResult Modal(string productid)
        {
            Procedure.Procedure procedure = new Procedure.Procedure();
            var ProductFormat = procedure.GetFormatByProductID(int.Parse(productid));
            ViewData["ProductFormat"] = ProductFormat;
            var imagegroup = ProductFormat.Select((x) => x.Image).Distinct();
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
        [NoCache]
        [HttpPost]
        public int ModaltoCart(string productid, string size, string color, string quantity)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return 1;
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewData["UserName"] = ticket.UserData;

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

            var repeat = procedure.SearchRepeatCart(ticket.UserData, productformatid);
            if (repeat != null)
            {
                var isempty = 4;
                return isempty;
            }
            else
            {
                if (int.Parse(quantity) <= stock)
                {
                    ShoppingCart shoppingCart = new ShoppingCart
                    {
                        MemberID = ticket.UserData,
                        ProductFormatID = productformatid,
                        ProductID = int.Parse(productid),
                        Quantity = int.Parse(quantity),
                    };
                    var repository = new ShoppingCartRepository();
                    repository.Create(shoppingCart);
                    var isempty = 2;
                    CartIconNumber();
                    return isempty;
                }
                else
                {
                    var isempty = 3;
                    return isempty;
                }
            }
        }

        [NoCache]
        public ActionResult Quantity(string color, string size, string productid)
        {
            int quantitynumber = 0;
            Procedure.Procedure procedure = new Procedure.Procedure();
            var ProductFormat = procedure.GetFormatIDByProductIDCS(int.Parse(productid), size, color);
            foreach (var item in ProductFormat)
            {
                quantitynumber = item.StockQuantity;
            };
            if (ProductFormat == null)
            {
                ViewData["quantity"] = "0";
            }
            else
            {
                ViewData["quantity"] = quantitynumber.ToString();
            }
            return PartialView();
        }

        public ActionResult ColorFilter(string Colors)
        {

            Procedure.Procedure procedure = new Procedure.Procedure();
            ViewData["color"] = procedure.ColorFilters(Colors);

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();
            return View();
        }

        [NoCache]
        public ActionResult CartIconNumber()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                ViewData["iconcount"] = "0";
                return PartialView();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["iconcount"] = Data.Count().ToString();
            return PartialView();
        }


        [NoCache]
        public ActionResult PriceBetween(string lower,string higher,int active)
        {
            ViewData["active"] = active;
            var procedure = new Procedure.Procedure();
            var priceproducts = procedure.FindMoneyBetween(decimal.Parse(lower), decimal.Parse(higher));
            ViewData["priceproducts"] = priceproducts;

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();
            return View();
        }

        public ActionResult PopualityIndex()
        {
            ViewBag.Title = "熱銷產品介面";
            var Popuality = new ProductRepository();
            var FindPopualityProduct = Popuality.PopularityProduct();


            //new list
            List<string> productcolor = new List<string>();
            List<string> productsize = new List<string>();
            ViewBag.FindPopualityProduct = FindPopualityProduct;
            // ViewData["FindPopualityProduct"] = FindPopualityProduct;

            var popualitylist = new List<PopualityProduct>();
            foreach (var item in FindPopualityProduct)
            {
                PopualityProduct popuality = new PopualityProduct()
                {
                    image = item.image,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    Color = item.Color,
                    StockQuantity = item.StockQuantity,
                    Description = item.Description,
                    UnitPrice = item.UnitPrice,
                    CategoryName = item.CategoryName
                };
                popualitylist.Add(popuality);
            }
            //ViewData.Add();
            ViewData["popualitylist"] = popualitylist;
            ViewData["count"] = popualitylist.Count();
            ViewData["productcolor"] = productcolor.Distinct();
            ViewData["productsize"] = productsize.Distinct();

            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();
            return View();
        }

        [HttpGet]
        //[Route("Search")]
        public ActionResult Search(string productname)
        {

            Procedure.Procedure procedure = new Procedure.Procedure();
            var SearchProduct = procedure.Search(productname);
            ViewData["SearchProduct"] = SearchProduct;
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                ViewBag.IsAuthenticated = false;
                return View();
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            ViewBag.IsAuthenticated = true;
            ViewBag.UserName = ticket.UserData;
            var repository = new ShoppingCartRepository();
            var Data = repository.FindByMemberID(ticket.UserData);
            ViewData["count"] = Data.Count().ToString();
            return View();
        }


    }
}
