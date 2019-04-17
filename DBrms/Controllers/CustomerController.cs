using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBrms.Models;
using PagedList;
using PagedList.Mvc;

namespace DBrms.Controllers
{
    public class CustomerController : Controller
    {
        dbrmsEntities db = new dbrmsEntities();
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
            
            if (id == null)
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
        [HttpPost]
        public ActionResult Edit([Bind(Include ="")] Customer customer)
        {
            return View();
        }
        public ActionResult CustomerOrderList()
        {

            return View();

        }
        public ActionResult CustomerReviewList(int? id,int? page)
        {
          
                id=Convert.ToInt32(Session["CustomerId"]);
               
                return View(db.Reviews.Where(x=> x.CustomerId == id).ToList().ToPagedList(page ?? 1,5));
           
        }
        
    }
}