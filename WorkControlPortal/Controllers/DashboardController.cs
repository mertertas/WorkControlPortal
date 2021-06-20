using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkControlPortal.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace WorkControlPortal.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize(Roles = "admin")]
        public ActionResult Dashboard()
        {
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {

                var deger5 = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == DateTime.Now.Year && p.BaTarih.Value.Month == DateTime.Now.Month && p.BaTarih.Value.Day == DateTime.Now.Day && p.Ucret == 0).Select(p => p.Zaman).Sum();
                ViewBag.d5 = deger5 == null ? 0 : deger5;


                var deger6 = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == DateTime.Now.Year && p.BaTarih.Value.Month == DateTime.Now.Month && p.BaTarih.Value.Day == DateTime.Now.Day && p.Ucret == 1).Select(p => p.Zaman).Sum();
                ViewBag.d6 = deger6 == null ? 0 : deger6;

                int user = (int)Session["Referans"];
                var deger7 = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == DateTime.Now.Year && p.BaTarih.Value.Month == DateTime.Now.Month && p.BaTarih.Value.Day == DateTime.Now.Day && p.Ekleyen == user).Count();
                ViewBag.d7 = deger7 == null ? 0 : deger7;

                var deger8 = context.HizmetHareketleri.Where(p => p.BaTarih.Value.Year == DateTime.Now.Year && p.BaTarih.Value.Month == DateTime.Now.Month && p.BaTarih.Value.Day == DateTime.Now.Day).Count();
                ViewBag.d8 = deger8 == null ? 0 : deger8;


                return View();
            }
        }
        public ActionResult VisualizeBarResult()
        {
            return Json(BarHareketListesi(), JsonRequestBehavior.AllowGet);
        }

        public List<GrafikModel> BarHareketListesi()
        {
            List<GrafikModel> grafik = new List<GrafikModel>();
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                grafik = context.HizmetHareketleri.Where(a=> a.BaTarih.Value.Year == DateTime.Now.Year && a.BaTarih.Value.Month == DateTime.Now.Month).GroupBy(a => a.Kullanicilar.Isim).Select(a => new GrafikModel
                {
                    Kullanici = a.Key,
                    Zaman = a.Sum(z => (int)z.Zaman)
                }).ToList();

                return grafik;
            }

        }

        public ActionResult VisualizePieResult()
        {
            return Json(PieHareketListesi(), JsonRequestBehavior.AllowGet);
        }

        public List<GrafikModel> PieHareketListesi()
        {
            
            int hafta = GetWeekNumber(DateTime.Now);
            List<GrafikModel> grafik = new List<GrafikModel>();
            using (EMSEYCRMDBEntities context = new EMSEYCRMDBEntities())
            {
                grafik = context.HizmetHareketleri.Where(a => a.BaTarih.Value.Year == DateTime.Now.Year && a.BaTarih.Value.Month == DateTime.Now.Month && a.Hafta==hafta ).GroupBy(a => a.Kullanicilar.Isim).Select(a => new GrafikModel
                {
                    Kullanici = a.Key,
                    Zaman = a.Sum(z => (int)z.Zaman)
                }).ToList();
                
                return grafik;
            }

        }
        public int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;

        }

    }
}