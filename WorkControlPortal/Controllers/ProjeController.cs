using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;

namespace WorkControlPortal.Controllers
{
    public class ProjeController : Controller
    {
        // GET: Proje
        [Authorize(Roles = "admin")]
        public ActionResult Proje()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Projeler.ToList();
                return View(query);
            }
        }

        public ActionResult ProjeSil(int? projeId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {

                var query = context.Projeler.Find(projeId);
                context.Projeler.Remove(query);
                context.SaveChanges();

                return RedirectToAction("Proje");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult ProjeEkle()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                //DROPDOWNLIST
                List<SelectListItem> degerler = (from i in context.Departmanlar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.Departman,
                                                     Value = i.Referans.ToString()

                                                 }).ToList();

                degerler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.dgr = degerler;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ProjeEkle(Projeler projeler)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Where(p => p.Referans == projeler.DprtRef).SingleOrDefault();
                int deger = (int)Session["Referans"];


                Projeler p1 = new Projeler();
                p1.Kod = projeler.Kod;
                p1.Aciklama = projeler.Aciklama;
                p1.Icerik = projeler.Icerik;
                p1.BaTarih = projeler.BaTarih;
                p1.BiTarih = projeler.BiTarih;
                p1.KullaniciRef = deger;

                if (query != null) p1.DprtRef = query.Referans;

                context.Projeler.Add(p1);
                context.SaveChanges();

                return RedirectToAction("Proje");

            }
        }

        public ActionResult ProjeGetir(int projeId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Projeler.Find(projeId);

                List<SelectListItem> degerler = (from i in context.Departmanlar.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.Departman,
                                                     Value = i.Referans.ToString()
                                                 }).ToList();

                degerler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });

                ViewBag.dgr = degerler;

                return View("ProjeGetir", query);
            }
        }
        [HttpPost]
        public ActionResult ProjeGuncelle(Projeler projeler)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Projeler.Find(projeler.Referans);
                int deger = (int)Session["Referans"];

                query.Aciklama = projeler.Aciklama;
                query.BaTarih = projeler.BaTarih;
                query.BiTarih = projeler.BiTarih;
                query.Kod = projeler.Kod;
                query.Icerik = projeler.Icerik;
                query.KullaniciRef = deger;


                var result = context.Departmanlar.Where(p => p.Referans == projeler.DprtRef).FirstOrDefault();
                if (result != null) query.DprtRef = result.Referans;
                context.SaveChanges();

                return RedirectToAction("Proje");
            }

        }
    }
}