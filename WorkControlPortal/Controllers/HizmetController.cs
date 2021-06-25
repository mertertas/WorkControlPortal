using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;

namespace WorkControlPortal.Controllers
{
    public class HizmetController : Controller
    {
        // GET: Hizmet
        [Authorize(Roles = "Admin")]
        public ActionResult Hizmet()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Hizmetler.ToList();
                return View(query);
            }
        }
        public ActionResult HizmetSil(int? hizmetId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                try
                {
                    if (hizmetId != null)
                    {
                        var query = context.Hizmetler.Find(hizmetId);
                        context.Hizmetler.Remove(query);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    ViewBag.sonuc = "Hizmet Kullanımda Silinemez.";
                }
                return RedirectToAction("Hizmet");
            }
        }
        [HttpGet]
        public ActionResult HizmetEkle()
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
        public ActionResult HizmetEkle(Hizmetler hizmetler)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Where(p => p.Referans == hizmetler.Departmanlar.Referans).SingleOrDefault();

                Hizmetler h1 = new Hizmetler();
                h1.Aciklama = hizmetler.Aciklama;
                if(query!=null) h1.DprtRef = query.Referans;

                context.Hizmetler.Add(h1);
                context.SaveChanges();

                return RedirectToAction("Hizmet");
            }
        }

        public ActionResult HizmetGetir(int hizmetId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Hizmetler.Find(hizmetId);

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

                return View("HizmetGetir",query);
            }
        }
        public ActionResult HizmetGuncelle(Hizmetler hizmetler)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Hizmetler.Find(hizmetler.Referans);
                query.Aciklama = hizmetler.Aciklama;

                var result = context.Departmanlar.Where(p=>p.Referans==hizmetler.Departmanlar.Referans).FirstOrDefault();
                if (result != null)  query.DprtRef = result.Referans;

                context.SaveChanges();
                return RedirectToAction("Hizmet");
            }
        }
    }
}