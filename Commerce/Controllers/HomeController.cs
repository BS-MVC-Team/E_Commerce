using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
using Procedure;
using System;
using System.Collections.Generic;
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


        [HttpPost]
        public ActionResult shoppingcar1(string productid, string color, string size, string Quantity)
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
            return View();
        }

        public ActionResult shoppingcar1()
        {

            return View();
        }

    }
}