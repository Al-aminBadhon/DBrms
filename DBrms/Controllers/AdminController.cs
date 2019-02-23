using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DBrms.Controllers
{
    public class AdminController : Controller
    {
        dbrmsEntities1 db = new dbrmsEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
           

            return View(db.Sliders.ToList());
        }

        [HttpGet]
        public ActionResult SliderAdd()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult SliderAdd([Bind(Include ="Name,Image,Details,IsActive")] Slider slider, Slider imagemodel)
        {
           if (imagemodel != null)
            {
                //String fileName = Path.GetFileNameWithoutExtension(imagemodel.ImageFile.FileName);
                //String extension = Path.GetExtension(imagemodel.ImageFile.FileName);
                //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //slider.Image = "~/Image/" + imagemodel;
                //fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                //imagemodel.ImageFile.SaveAs(fileName);

                //db.Sliders.Add(slider);

                //db.SaveChanges();
                //ModelState.Clear();
                //return RedirectToAction("Slider");

            }

            return View();
        }




        public ActionResult CategoryBuffet()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Newspanel()
        {
            

            return View();
        }


        [HttpGet]
        public ActionResult NewspanelAdd()
        {
           

            return View();
        }
        //[HttpPost]
        ////public ActionResult NewspanelAdd([Bind(Include = "")])
        ////{


        ////    return View();
        ////}
        ///
        public ActionResult TradingRestaurant()
        {


            return View();
        }

        public ActionResult Review()
        {


            return View();
        }

        public ActionResult Magazine()
        {
            return View();
        }
    }
}