using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Home()
        {
            ViewBag.Title = "首頁";

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
    }
}