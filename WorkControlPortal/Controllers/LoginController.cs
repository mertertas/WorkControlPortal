using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;
using System.Web.Security;

namespace WorkControlPortal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanicilar users)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Kullanicilar.FirstOrDefault(x => x.MailAdres == users.MailAdres && x.Sifre == users.Sifre);
                if (query != null)
                {
                    FormsAuthentication.SetAuthCookie(query.MailAdres, false);
                    Session["Referans"] = query.Referans;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
         
        }
        public ActionResult LogOut()
        {

            return RedirectToAction("Login");
        }
    }
}