using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MesutWebd.Models;
using System.Web.Security;  

namespace MesutWebd.Areas.admin.Controllers
{
    public class loginController : Controller
    {
        // GET: admin/login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullanicilarformu,string ReturnUrl)
        {

            //if (ModelState.IsValid)
            //{
            //    return View("Index",kullanicilarformu);
            //}
            using (kahve2020Entities db=new kahve2020Entities())
            {
                var kullanicivarmi = db.kullanicilar.FirstOrDefault(x => x.kad == kullanicilarformu.kad && x.sifre == kullanicilarformu.sifre);
                if (kullanicivarmi!=null)
                {
                    FormsAuthentication.SetAuthCookie(kullanicivarmi.kad,kullanicilarformu.BeniHatirla);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }


                    else
                    {
                        return RedirectToAction("/Index", "Urunler");
                    }

                   

                }
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!!";
                return View("index");
                
                
            }
                
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}