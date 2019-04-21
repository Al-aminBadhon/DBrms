using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DBrms.Models;

namespace DBrms.Controllers
{
    public class LoginController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Index (string username, string password)
        {
            var admin = db.Logins.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();

            if (admin == null)
            {
                var Login = db.Restaurants.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

                if (Login == null)
                {
                    var cus = db.Customers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                    if (cus == null)
                    {
                        ViewBag.message = "Login Fail";
                        return View();
                    }
                    else
                    {
                        Session["username"] = cus.Name.ToString();
                        Session["CustomerId"] = cus.CustomerId.ToString();
                        return RedirectToAction("Index", "Customer");


                    }
                }
                else
                {
                    Session["username"] = Login.Name.ToString();
                    Session["RestaurantsId"] = Login.RestaurantId.ToString();
                    return RedirectToAction("Index", "Restaurants");
                }
            }  
           else
            {
                Session["UserId"] = admin.UserId.ToString();
                return RedirectToAction("Index","Admin");
            }
           
           
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
       
    }
}