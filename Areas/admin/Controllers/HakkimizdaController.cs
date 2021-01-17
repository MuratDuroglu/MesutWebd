using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MesutWebd.Models;
using MesutWebd.Controllers;
using System.IO;

namespace MesutWebd.Areas.admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (kahve2020Entities db = new kahve2020Entities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
        }
        public ActionResult HakkimizdaGuncelle()
        {
            using (kahve2020Entities db=new kahve2020Entities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Kaydet(hakkimizda GelenVeri)
        {
            using(kahve2020Entities db=new kahve2020Entities())
            {
                var gullencekveri = db.hakkimizda.First();
                if (!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", GelenVeri);
                }
                if (GelenVeri.fotofile != null)
                {
                    GelenVeri.foto = Seo.AdresDuzenle(GelenVeri.fotofile.FileName);
                    GelenVeri.fotofile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(GelenVeri.fotofile.FileName)));
                }
                db.Entry(gullencekveri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                TempData["hakkimdaGuncelle"] = " ";
                return RedirectToAction("Index","Hakkimizda");
            }

        }



    }
}