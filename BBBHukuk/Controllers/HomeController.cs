using BBBHukuk.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BBBHukuk.Models.Model;

namespace BBBHukuk.Controllers
{
    public class HomeController : Controller
    {
        private BBBHukukBurosuDBContext db = new BBBHukukBurosuDBContext();
        // GET: Home
        public ActionResult Index()
        {

            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            return View();
        }

        public ActionResult SliderPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderId));
        }
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Hakkimizda.SingleOrDefault());

        }
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null, string email=null , string konu = null, string mesaj=null)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            if (adsoyad!=null && email!=null)
            {
                
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.SmtpUseDefaultCredentials = false;
                WebMail.UserName = "mail";
                WebMail.Password = "mail app password";
                WebMail.SmtpPort = 587;
                WebMail.Send("bulentkipcak04@gmail.com",konu,"Gönderen: "+adsoyad+ " Eposta: "+ email + " Mesaj: "+mesaj);
                ViewBag.Uyari = "Mesajınız gönderildi!";
            }
            else
            {
                ViewBag.Uyari = "Hatalı gönderim, lütfen kontrol ediniz.";
            }
            return View(db.Iletisim.SingleOrDefault());
        }

        public ActionResult Blog(int Sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();


            return View(db.Blog.Include("Kategori").OrderByDescending(x=>x.BlogId).ToPagedList(Sayfa,5));

        }

        public ActionResult KategoriBlog(int id,int Sayfa=1) {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Sayfa,5);
            return View(b);

        }
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);


        }

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik,int blogid )
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            if (icerik==null)
            {

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogid,Onay=false});
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);

        }



        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));    
        }
        public ActionResult BlogKayitPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return PartialView(db.Blog.ToList().OrderBy(x=>x.Baslik));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            return PartialView();
        }

    }
    
}
