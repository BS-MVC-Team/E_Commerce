using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        public ActionResult ProductInfoEdit()
        {
            return View();
        }

        public ActionResult ProductsInfo()
        {
            return View();
        }

        public ActionResult Tasks()
        {
            return View();
        }

        public ActionResult Logistics()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Analysis()
        {
            return View();
        }
    }
}