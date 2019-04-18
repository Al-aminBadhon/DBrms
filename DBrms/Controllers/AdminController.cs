﻿using DBrms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class AdminController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider(int? page)
        {


            return View(db.Sliders.ToList().ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult SliderAdd()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderAdd([Bind(Include = "SliderId,Name,Image,Details,IsActive")] Slider slider, HttpPostedFileBase ImageFile)

        {

            String filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            String extension = Path.GetExtension(ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            slider.Image = "/Image/" + filename;
            filename = Path.Combine(Server.MapPath("/Image/"), filename);
            

            if (ModelState.IsValid)
            {
                ImageFile.SaveAs(filename);
                db.Sliders.Add(slider);
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("Slider");

            }
            return View();
        }




        public ActionResult CategoryBuffet()
        {


            return View();
        }
        public ActionResult Newspanel(int? page)
        {


            return View(db.Magazines.ToList().ToPagedList(page ?? 1, 3));
        }


        [HttpGet]
        public ActionResult NewspanelEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Magazine newspanelEdit = db.Magazines.Find(id);
            if (newspanelEdit == null)
            {
                return HttpNotFound();
            }
            return View(newspanelEdit);

        }
        [HttpPost]
        public ActionResult NewspanelEdit([Bind(Include = "MagazineId,Name,Image,Details,IsActive")] Magazine newspanel)
        {
            if (ModelState.IsValid)
            {

                db.Entry(newspanel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Newspanel");
            }
            return View();
        }



        public ActionResult TradingRestaurant(int? page)
        {


            return View(db.Restaurants.ToList().ToPagedList(page ?? 1, 3));
        }


        [HttpGet]
        public ActionResult TradingRestaurantEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant tradingrestaurantEdit = db.Restaurants.Find(id);
            if (tradingrestaurantEdit == null)
            {
                return HttpNotFound();
            }
            return View(tradingrestaurantEdit);

        }
        [HttpPost]
        public ActionResult TradingRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive,Visible")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {

                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TradingRestaurant");
            }
            return View();
        }
        public ActionResult Review(int? page)
        {


            return View(db.Reviews.ToList().ToPagedList(page ?? 1, 5));
        }

        public ActionResult Magazine(int? page)
        {
            return View(db.Magazines.ToList().ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult MagazineAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MagazineAdd([Bind(Include = "Name,Image,Details")] Magazine magazine, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                magazine.Image = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Magazines.Add(magazine);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Magazine");
            }
            return View();
        }

        public ActionResult ManageRsetaurant(int? page)
        {

            return View(db.Restaurants.ToList().ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult RestaurantDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult RestaurantDelete(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("ManageRsetaurant");
        }



        [HttpGet]

        public ActionResult RestaurantAdd()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RestaurantAdd([Bind(Include = "Name,Address,Phone,picture,LocationId,CostPerOrder,Cuisine")] Restaurant restaurant, HttpPostedFileBase ImageFile, int? LocationId)
        {
            //int er = 0;
            //if (LocationId == null)
            //{
            //    er++;

            //}
            //if (er > 0)
            //{
            //    return View("Index");
            //}
            String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            String extension = Path.GetExtension(ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            restaurant.Picture = "/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
            ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ManageRestaurant");

            }
            return View();
        }


        [HttpGet]
        public ActionResult ManageRestaurantEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Restaurant managerestaurantEdit = db.Restaurants.Find(id);
            if (managerestaurantEdit == null)
            {
                return HttpNotFound();
            }
            return View(managerestaurantEdit);


        }
        [HttpPost]
        public ActionResult ManageRestaurantEdit([Bind(Include = "RestaurantId,Name,Address,Phone,Picture,Location,PopularMenu,CostPerOrder,Time,Cuisine,Extra,Discount,Username,Password,IsActive,Visible")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                //Restaurant res = db.Restaurants.FirstOrDefault(x=> x.RestaurantId == restaurant.RestaurantId);
                //res.IsActive = restaurant.IsActive;
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageRestaurant");
            }
            return View();
        }

        public ActionResult ManageCustomer(int? page)
        {
            return View(db.Customers.ToList().ToPagedList(page ?? 1, 3));
        }


        [HttpGet]
        public ActionResult CustomerAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAdd([Bind(Include = "Name,Address,Image,Phone,Username,Password")] Customer customer, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                String fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                String extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                customer.Image = "/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("/Image/"), fileName);
                ImageFile.SaveAs(fileName);

                db.Customers.Add(customer);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ManageCustomer");
            }
            return View();
        }

        [HttpGet]
        public ActionResult CustomerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult CustomerEdit([Bind(Include = "Name,Address,Image,Phone,Username,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //Restaurant res = db.Restaurants.FirstOrDefault(x=> x.RestaurantId == restaurant.RestaurantId);
                //res.IsActive = restaurant.IsActive;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCustomer");
            }
            return View();
        }

    }
}