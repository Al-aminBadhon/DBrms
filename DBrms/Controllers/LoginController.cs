using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBrms.Models;

namespace DBrms.Controllers
{
    public class LoginController : Controller
    {
        dbrmsEntities1 db = new dbrmsEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index (Login u )
        {
            //This action is for handle post (login)
            if(ModelState.IsValid)
            {
                var v =db.Logins.Where(a=>   a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)  ).FirstOrDefault();
                if(v != null)
                {
                    Session["LogedUserID"] = v.UserId.ToString();
                    //Session["UserRole"] = v.UserRole.ToString();
                    return RedirectToAction("Logged");
                }
            }
            return View();
        }
        public ActionResult Logged()
        {
            return View();
        }

    }
}