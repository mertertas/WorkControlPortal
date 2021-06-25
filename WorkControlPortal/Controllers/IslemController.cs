using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Web.UI;

namespace WorkControlPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IslemController : Controller
    {
        // GET: Islem
       
        public ActionResult Islem(DateTime? Tarih)
        {
            //using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            //{

            EMSEYCRMDBEntities context = new EMSEYCRMDBEntities();


            if (Tarih is null)
            {
                var result = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == DateTime.Now.Year && p.BaTarih.Value.Month == DateTime.Now.Month && p.BaTarih.Value.Day == DateTime.Now.Day).ToList();

                return View(result);
            }

            var query = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == Tarih.Value.Year && p.BaTarih.Value.Month == Tarih.Value.Month && p.BaTarih.Value.Day == Tarih.Value.Day).ToList();

            return View(query);
            //}

        }

       
        [HttpGet]
        public ActionResult IslemEkle()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())

            {
               

                List<SelectListItem> cariler = (from i in context.Cariler.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.Unvan,
                                                    Value = i.Referans.ToString()

                                                }).ToList();

                cariler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.cariValue = cariler;


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
                ViewBag.departmanValue = departmanlar;

                List<SelectListItem> projeler = (from i in context.Projeler.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.Aciklama,
                                                     Value = i.Referans.ToString()

                                                 }).ToList();

                projeler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.projeValue = projeler;


                List<SelectListItem> hizmetler = (from i in context.Hizmetler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.Aciklama,
                                                      Value = i.Referans.ToString()

                                                  }).ToList();

                hizmetler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.hizmetValue = hizmetler;

                return View();

            }
        }
        [HttpPost]
        public ActionResult IslemEkle(HizmetHareketleri hizmetHareketleri)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                //var departman = context.Departmanlar.Where(p => p.Referans == hizmetHareketleri).SingleOrDefault();
                var hizmet = context.Hizmetler.Where(p => p.Referans == hizmetHareketleri.Hizmetler.Referans).SingleOrDefault();
                var proje = context.Projeler.Where(p => p.Referans == hizmetHareketleri.Projeler.Referans).SingleOrDefault();
                var cari = context.Cariler.Where(p => p.Referans == hizmetHareketleri.Cariler.Referans).SingleOrDefault();

                int deger = (int)Session["Referans"];
               

                HizmetHareketleri h1 = new HizmetHareketleri();
                h1.BaTarih = hizmetHareketleri.BaTarih;
                h1.BiTarih = hizmetHareketleri.BiTarih;
                h1.Aciklama = hizmetHareketleri.Aciklama;
                h1.Ilgili = hizmetHareketleri.Ilgili;
                h1.Ekleyen = deger;/// KONTROL
                h1.DprtRef = cari.Departmanlar.Referans;
                h1.Ucret = hizmetHareketleri.Ucret;
                h1.MailBil = hizmetHareketleri.MailBil;
                h1.Kapama = hizmetHareketleri.Kapama;
                if (hizmet != null) h1.HizmetRef = hizmet.Referans;
                if (proje != null) h1.ProjeRef = proje.Referans;
                if (cari != null) h1.CariRef = cari.Referans;

                context.HizmetHareketleri.Add(h1);
                context.SaveChanges();

                return RedirectToAction("Islem");
            }

        }

        public ActionResult IslemSil(int? IslemId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                try
                {
                    if (IslemId != null)
                    {
                        var query = context.HizmetHareketleri.Find(IslemId);
                        context.HizmetHareketleri.Remove(query);
                        context.SaveChanges();
                    }



                }
                catch (Exception)
                {

                    throw;
                }
              
                return RedirectToActionPermanent("Islem");

            }
        }

        public ActionResult IslemGetir(int IslemId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.HizmetHareketleri.Find(IslemId);

                List<SelectListItem> cariler = (from i in context.Cariler.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.Unvan,
                                                    Value = i.Referans.ToString()

                                                }).ToList();

                cariler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.cariValue = cariler;


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
                ViewBag.departmanValue = departmanlar;

                List<SelectListItem> projeler = (from i in context.Projeler.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.Aciklama,
                                                     Value = i.Referans.ToString()

                                                 }).ToList();

                projeler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.projeValue = projeler;


                List<SelectListItem> hizmetler = (from i in context.Hizmetler.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.Aciklama,
                                                      Value = i.Referans.ToString()

                                                  }).ToList();

                hizmetler.Insert(0, new SelectListItem()
                {
                    Value = "0",
                    Text = "-Please Select-"
                });
                ViewBag.hizmetValue = hizmetler;

                return View("IslemGetir", query);
            }

        }

        public ActionResult IslemGuncelle(HizmetHareketleri hizmetHareketleri)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                try
                {
                    var query = context.HizmetHareketleri.Find(hizmetHareketleri.Referans);
                    int deger = (int)Session["Referans"];

                    if (query != null)
                    {
                        query.BaTarih = hizmetHareketleri.BaTarih;
                        query.BiTarih = hizmetHareketleri.BiTarih;
                        query.Aciklama = hizmetHareketleri.Aciklama;
                        query.Ilgili = hizmetHareketleri.Ilgili;
                        query.Ekleyen = deger;/// KONTROL
                        query.Ucret = hizmetHareketleri.Ucret;
                        query.Kapama = hizmetHareketleri.Kapama;
                        query.MailBil = hizmetHareketleri.MailBil;


                        var hizmet = context.Hizmetler.Where(p => p.Referans == hizmetHareketleri.Hizmetler.Referans).SingleOrDefault();
                        var proje = context.Projeler.Where(p => p.Referans == hizmetHareketleri.Projeler.Referans).SingleOrDefault();
                        var cari = context.Cariler.Where(p => p.Referans == hizmetHareketleri.Cariler.Referans).SingleOrDefault();

                        if (hizmet != null) query.HizmetRef = hizmet.Referans;
                        if (proje != null) query.ProjeRef = proje.Referans;
                        if (cari != null) query.CariRef = cari.Referans;
                        context.SaveChanges();


                    }
                    return RedirectToAction("Islem");
                }
                catch (Exception)
                {

                    return RedirectToAction("Login");
                }
                
            }
        }
    }
}