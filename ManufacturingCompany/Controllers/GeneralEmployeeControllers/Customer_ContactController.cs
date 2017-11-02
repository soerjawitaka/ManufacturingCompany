using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers
{
    public class Customer_ContactController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: Customer_Contact
        public ActionResult Index()
        {
            var customer_Contact = db.Customer_Contact.Include(c => c.Customer);
            return View(customer_Contact.ToList());
        }

        // GET: Customer_Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Contact customer_Contact = db.Customer_Contact.Find(id);
            if (customer_Contact == null)
            {
                return HttpNotFound();
            }
            return View(customer_Contact);
        }

        // GET: Customer_Contact/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name");
            return View();
        }

        // POST: Customer_Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,customer_id,first_name,last_name,work_phone,mobile_phone,fax,contact_email")] Customer_Contact customer_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Contact.Add(customer_Contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", customer_Contact.customer_id);
            return View(customer_Contact);
        }

        // GET: Customer_Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Contact customer_Contact = db.Customer_Contact.Find(id);
            if (customer_Contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", customer_Contact.customer_id);
            return View(customer_Contact);
        }

        // POST: Customer_Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,customer_id,first_name,last_name,work_phone,mobile_phone,fax,contact_email")] Customer_Contact customer_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", customer_Contact.customer_id);
            return View(customer_Contact);
        }

        // GET: Customer_Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Contact customer_Contact = db.Customer_Contact.Find(id);
            if (customer_Contact == null)
            {
                return HttpNotFound();
            }
            return View(customer_Contact);
        }

        // POST: Customer_Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Contact customer_Contact = db.Customer_Contact.Find(id);
            db.Customer_Contact.Remove(customer_Contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
