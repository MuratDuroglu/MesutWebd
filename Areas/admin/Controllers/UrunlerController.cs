using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MesutWebd.Controllers;
using MesutWebd.Models;

namespace MesutWebd.Areas.admin.Controllers
{
    public class UrunlerController : Controller
    {
        private string fotoAdi;

        // GET: admin/Urunler
        [Authorize]
        public ActionResult Index()
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }


        }
        public ActionResult Yeniurunekle()
        {
            var model = new urunler();
            return View("UrunForm", model);

        }
        public ActionResult Guncelle(int id)
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var model = db.urunler.Find(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);

            }

        }
        public ActionResult Kaydet(urunler gelenurun)
        {
            if (!ModelState.IsValid)
            {
                return View("UrunForm",gelenurun);
            }
            using (kahve2020Entities db = new kahve2020Entities())
            {
                if (gelenurun.id==0)
                {
                   if(gelenurun.fotofile==null)
                    {
                        ViewBag.HataFoto = "Lütfen resim yükleyiniz";
                        return View("Urunform",gelenurun);
                    }

                    string fotoadi = Seo.DosyaAdiDuzenle(gelenurun.fotofile.FileName);
                    gelenurun.foto = fotoadi;
                    db.urunler.Add(gelenurun);
                    //gelenurun.fotofile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"),Path.GetFileName(fotoAdi)));
                    TempData["urunEkle"] = "Ürun Başarılı eklendi";

                }
                else
                {
                    var guncellencek = db.urunler.Find(gelenurun.id);
                    if (gelenurun.fotofile !=null)
                    {
                        string fotoadi = Seo.DosyaAdiDuzenle(gelenurun.fotofile.FileName);
                        gelenurun.foto = fotoadi;
                        gelenurun.fotofile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"),Path.GetFileName(fotoAdi)));

                    }

                    db.Entry(guncellencek).CurrentValues.SetValues(gelenurun);
                    TempData["urunEkle"] = "Ürun Başarılı eklendi";
                }db.SaveChanges();
                return RedirectToAction("ındex");
            }
            
        }
        public ActionResult Sil(int id)
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var silinecek = db.urunler.Find(id);
                if(silinecek == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    db.urunler.Remove(silinecek);
                    db.SaveChanges();
                    TempData["urunEkle"] = "Ürun Başarıyla silindi";
                    return RedirectToAction("ındex");

                }
            }


        }

    }


}