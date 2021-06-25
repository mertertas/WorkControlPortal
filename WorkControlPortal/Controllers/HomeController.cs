using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkControlPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            string MailAdres = (string)Session["MailAdres"];
            ViewBag.dgr = MailAdres;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}


