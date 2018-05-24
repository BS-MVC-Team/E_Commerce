using BuildSchool.MvcSolution.OnlineStore.Repository;
using Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public JsonResult SignUp(string MemberId,string MemberPassword,string Name,string Phone,string Email,string Address)
        {
            return Json("");
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
    }
}