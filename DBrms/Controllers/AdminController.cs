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
        dbrmsEntities1 bd = new dbrmsEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
           

            return View();
        }

        [HttpGet]
        public ActionResult SliderAdd()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult SliderAdd([Bind(Include ="SliderId,Name,Details")] Slider slider, Slider imagemodel)
        {
           if (imagemodel != null)
            {
                String filename = Path.GetFileNameWithoutExtension(imagemodel.ImageFile.FileName);
                String extension = Path.GetExtension(imagemodel.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                slider.Image = "~/Image" + imagemodel;
                filename = Path.Combine(Server.MapPath("~/Image"), filename);
                imagemodel.ImageFile.SaveAs(filename);
                db.Sliders.Add(slider);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Slider");

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
        [HttpPost]
        public ActionResult NewspanelAdd([Bind(Include = "")])
        {
          

            return View();
        }


    }
}