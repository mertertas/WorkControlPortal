using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;


namespace WorkControlPortal.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman


        [Authorize(Roles = "Admin")]
        public ActionResult Departman()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.ToList();
                return View(query);
            }

        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departmanlar departmanlar)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                //Departmanlar d1 = new Departmanlar();
                //d1.Departman = departman;
                context.Departmanlar.Add(departmanlar);
                context.SaveChanges();

                return RedirectToAction("Departman");
            }
        }

        public ActionResult DepartmanSil(int? departmanId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                try
                {
                    if (departmanId != null)
                    {
                        var query = context.Departmanlar.Find(departmanId);
                        context.Departmanlar.Remove(query);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    ViewBag.sonuc = "Departman Kullanıımda Silinemez.";
                }


                return RedirectToAction("Departman");
            }
        }


        public ActionResult DepartmanGetir(int departmanId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Find(departmanId);
                return View("DepartmanGetir", query);
            }

        }

        [HttpPost]
        public ActionResult DepartmanGuncelle(Departmanlar departmanlar)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Find(departmanlar.Referans);
                query.Departman = departmanlar.Departman;
                context.SaveChanges();
                return RedirectToAction("Departman");
            }
        }
    }
}