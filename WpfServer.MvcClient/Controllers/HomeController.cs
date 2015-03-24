using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WpfServer.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            return View();
        }

        //public ActionResult NoSocket()
        //{
        //    return View();
        //}


        public ActionResult About()
        {
            return View();
        }
    }
}
