using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using WorkControlPortal.Models;
using System.Threading;

namespace WorkControlPortal.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        [Authorize(Roles = "Admin")]
        public ActionResult Cari(/*int sayfa=1*/)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                //Sayfalama
                               var query = context.Cariler.ToList();
                //var query = context.Cariler.ToList().ToPagedList(sayfa,15);
                return View(query);
            }

        }

        public ActionResult CariSil(int? cariId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                try
                {
                    if (cariId != null)
                    {
                        var query = context.Cariler.Find(cariId);
                        context.Cariler.Remove(query);
                        context.SaveChanges();

                    }
                }
                catch (Exception)
                {


                }

                return RedirectToAction("Cari");
            }
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                //DROPDOWNLIST
                List<SelectListItem> degerler = (from i in context.Departmanlar.ToList()
                                                 select new SelectListItem 
                                                 {
                                                 Text=i.Departman,
                                                 Value=i.Referans.ToString()
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
        public ActionResult CariEkle( Cariler cariler)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Departmanlar.Where(m => m.Referans == cariler.Departmanlar.Referans).FirstOrDefault();
                //cariler.Departmanlar = query;
                Cariler c1 = new Cariler();
               
                c1.Kod = cariler.Kod;
                c1.Unvan = cariler.Unvan;
                c1.Ilgili1 = cariler.Ilgili1;
                c1.Ilgili1_Telefon1 = cariler.Ilgili1_Telefon1;
                c1.Ilgili1_Email = cariler.Ilgili1_Email;
               if(query!=null) c1.DprtRef = Convert.ToInt32(query.Referans);

                context.Cariler.Add(c1);
                context.SaveChanges();
             
               return RedirectToAction("Cari");
            }
        }

        public ActionResult CariGetir(int cariId)
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                var query = context.Cariler.Find(cariId);
                //Dropdownlist Doldur
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
                return View("CariGetir", query);
            }
        }
        [HttpPost]
        public ActionResult CariGuncelle(Cariler cariler)
        {
            using (EMSEYCRMDBEntities context =new EMSEYCRMDBEntities())
            { //Kayıt Bul
                var query = context.Cariler.Find(cariler.Referans);
                query.Kod = cariler.Kod;
                query.Unvan = cariler.Unvan;
                query.Ilgili1 = cariler.Ilgili1;
                query.Ilgili1_Email = cariler.Ilgili1_Email;
                query.Ilgili1_Telefon1 = cariler.Ilgili1_Telefon1;

               //Departman Bul
                var result = context.Departmanlar.Where(m => m.Referans == cariler.Departmanlar.Referans).FirstOrDefault();
                if (result != null) query.DprtRef = result.Referans;

                context.SaveChanges();
                return RedirectToAction("Cari");
            }

        }
    }
}