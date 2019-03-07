using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBrms.Models;

namespace DBrms.Controllers
{
    public class CustomerController : Controller
    {
        dbrmsEntities1 db = new dbrmsEntities1();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            id = Convert.ToInt32(Session["CustomerId"]);
            return View(db.Customers.Where(x=> x.CustomerId == id).FirstOrDefault());
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var edit = db.Customers.Find(id);
            if(edit == null)
            {
                return HttpNotFound();
            }
            return View(edit);
        }
        
    }
}