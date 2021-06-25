using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;

namespace WorkControlPortal.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult User()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Kullanicilar.ToList();
                return View(query);
            }
        }
        [HttpGet]
        public ActionResult UserEkle()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                List<SelectListItem> departmanlar = (from i in context.Departmanlar.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.Departman,
                                                         Value = i.Referans.ToString()

                                                     }).ToList();

                departmanlar.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.departman = departmanlar;

                List<SelectListItem> roles = new List<SelectListItem>();
                roles.Add(new SelectListItem
                {
                    Text = "Admin",
                    Value = "Admin"

                });
                roles.Add(new SelectListItem
                {
                    Text = "User",
                    Value = "User"

                });


                ViewBag.roles = roles;

                //List<SelectListItem> statu = new List<SelectListItem>();
                //roles.Add(new SelectListItem
                //{
                //    Text = "Aktif",
                //    Value = "true"

                //});
                //roles.Add(new SelectListItem
                //{
                //    Text = "Pasif",
                //    Value = "false"

                //});

                //ViewBag.statu = statu;
                return View();
            }

        }
        [HttpPost]
        public ActionResult UserEkle(Kullanicilar kullanicilar)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Where(p => p.Referans == kullanicilar.Departmanlar.Referans).SingleOrDefault();

                Kullanicilar k1 = new Kullanicilar();
                k1.Isim = kullanicilar.Isim;
                k1.MailAdres = kullanicilar.MailAdres;
                k1.Sifre = kullanicilar.Sifre;
                k1.Role = kullanicilar.Role;
                k1.Tel = kullanicilar.Tel;
                if (query != null) k1.DepartmanRef = query.Referans;
                context.Kullanicilar.Add(k1);
                context.SaveChanges();
            }
            return RedirectToAction("User");
        }

        public ActionResult UserGetir(int userId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Kullanicilar.Find(userId);
                return View(query);
            }
        }

        public ActionResult UserGuncelle(Kullanicilar kullanicilar)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Kullanicilar.Find(kullanicilar.Referans);
                query.Isim = kullanicilar.Isim;
                query.MailAdres = kullanicilar.MailAdres;
                query.Tel = kullanicilar.Tel;
                if (query != null) query.DepartmanRef = query.Referans;
                return RedirectToAction("User");
        }

    }
}