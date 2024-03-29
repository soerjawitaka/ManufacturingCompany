﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.CustomerContacts = db.Customer_Contact.Where(cc => cc.customer_id == id).ToList();
            ViewBag.ActionTitle = "Detailed ";
            return View(customer);
        }

        #region CustomerContact
        // GET: Customers/CreateContact
        public ActionResult CreateContact(int id)
        {
            var contact = new Customer_Contact() { customer_id = id };
            contact.Customer = db.Customers.Find(id);
            ViewBag.ActionTitle = "Create ";
            ViewBag.CustomerName = db.Customers.Find(id).customer_company_name;
            return View(contact);
        }

        // POST: Customers/CreateContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact([Bind(Include = "Id,customer_id,first_name,last_name,work_phone,mobile_phone,fax,contact_email")] Customer_Contact customer_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Contact.Add(customer_Contact);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = customer_Contact.customer_id });
            }

            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", customer_Contact.customer_id);
            ViewBag.ActionTitle = "Create ";
            ViewBag.CustomerName = db.Customers.Find(customer_Contact.customer_id).customer_company_name;
            return View(customer_Contact);
        }

        // GET: Customers/EditContact/5
        public ActionResult EditContact(int? id)
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
            ViewBag.ActionTitle = "Edit ";
            ViewBag.CustomerName = db.Customers.Find(customer_Contact.customer_id).customer_company_name;
            return View(customer_Contact);
        }

        // POST: Customers/EditContact/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact([Bind(Include = "Id,customer_id,first_name,last_name,work_phone,mobile_phone,fax,contact_email")] Customer_Contact customer_Contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = customer_Contact.customer_id });
            }
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", customer_Contact.customer_id);
            ViewBag.ActionTitle = "Edit ";
            ViewBag.CustomerName = db.Customers.Find(customer_Contact.customer_id).customer_company_name;
            return View(customer_Contact);
        }

        // GET: Customers/DeleteContact/5
        public ActionResult DeleteContact(int? id)
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
            ViewBag.ActionTitle = "Delete ";
            ViewBag.CustomerName = db.Customers.Find(customer_Contact.customer_id).customer_company_name;
            return View(customer_Contact);
        }

        // POST: Customers/DeleteContact/5
        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContactConfirmed(int id)
        {
            Customer_Contact customer_Contact = db.Customer_Contact.Find(id);
            db.Customer_Contact.Remove(customer_Contact);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = customer_Contact.customer_id });
        }

        #endregion

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            ViewBag.CountryList = new SelectList(XmlHelper.GetCountries(Server, Url), "Value", "Text");
            ViewBag.ActionTitle = "Create ";
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,customer_company_name,customer_address1,customer_address2,customer_city,customer_state,customer_zip,customer_country,customer_phone,customer_website")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            ViewBag.CountryList = new SelectList(XmlHelper.GetCountries(Server, Url), "Value", "Text");
            ViewBag.ActionTitle = "Create ";
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
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

            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            ViewBag.CountryList = new SelectList(XmlHelper.GetCountries(Server, Url), "Value", "Text");
            ViewBag.ActionTitle = "Edit ";
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,customer_company_name,customer_address1,customer_address2,customer_city,customer_state,customer_zip,customer_country,customer_phone,customer_website")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            ViewBag.CountryList = new SelectList(XmlHelper.GetCountries(Server, Url), "Value", "Text");
            ViewBag.ActionTitle = "Edit ";
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
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
            ViewBag.ActionTitle = "Delete ";
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
