using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Distribution
{
    [Authorize(Roles = "SuperUser, Manager, Distribution")]
    public class Delivery_ScheduleController : Controller
    {
        public Delivery_ScheduleController()
        {
            ViewBag.ViewHeaderPartial = "_Distribution";
            ViewBag.ItemTitle = "Delivery Schedule";
        }

        private BusinessEntities db = new BusinessEntities();

        // GET: Delivery_Schedule
        public ActionResult Index()
        {
            var delivery_Schedule = db.Delivery_Schedule.Include(d => d.AspNetUser).Include(d => d.AspNetUser1);
            return View(delivery_Schedule.ToList());
        }
        
        // GET: Delivery_Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Schedule delivery_Schedule = db.Delivery_Schedule.Find(id);
            if (delivery_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(delivery_Schedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // GET: Delivery_Schedule/Create
        public ActionResult Create(string userID, string optionalDirection)
        {
            var deliverySchedule = new Delivery_Schedule();
            if (Session["DeliverySchedule"] != null) { deliverySchedule = (Delivery_Schedule)Session["DeliverySchedule"]; }
            if (userID != null)
            {
                switch(optionalDirection)
                {
                    case "Warehouse":
                        deliverySchedule.warehouse_employee_id = userID;
                        deliverySchedule.AspNetUser = db.AspNetUsers.Find(userID);
                        break;
                    case "Driver":
                        deliverySchedule.driver_employee_id = userID;
                        deliverySchedule.AspNetUser1 = db.AspNetUsers.Find(userID);
                        break;
                }
            }
            Session["DeliverySchedule"] = deliverySchedule;
            return View(deliverySchedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Delivery_Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,warehouse_employee_id,driver_employee_id,delivery_date,delivery_cost,is_delivered,invoice_id")] Delivery_Schedule delivery_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Delivery_Schedule.Add(delivery_Schedule);
                db.SaveChanges();
                Session["DeliverySchedule"] = null;
                return RedirectToAction("Index");
            }
            
            return View(delivery_Schedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // GET: Delivery_Schedule/Edit/5
        public ActionResult Edit(int? id, string userID, string optionalDirection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Schedule delivery_Schedule = db.Delivery_Schedule.Find(id);
            if (delivery_Schedule == null)
            {
                return HttpNotFound();
            }
            if (Session["DeliverySchedule"] != null)
            {
                delivery_Schedule = (Delivery_Schedule)Session["DeliverySchedule"];
            }
            if (userID != null)
            {
                switch (optionalDirection)
                {
                    case "Warehouse":
                        delivery_Schedule.warehouse_employee_id = userID;
                        delivery_Schedule.AspNetUser = db.AspNetUsers.Find(userID);
                        break;
                    case "Driver":
                        delivery_Schedule.driver_employee_id = userID;
                        delivery_Schedule.AspNetUser1 = db.AspNetUsers.Find(userID);
                        break;
                }
            }
            Session["DeliverySchedule"] = delivery_Schedule;
            return View(delivery_Schedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Delivery_Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,warehouse_employee_id,driver_employee_id,delivery_date,delivery_cost,is_delivered,invoice_id")] Delivery_Schedule delivery_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery_Schedule).State = EntityState.Modified;
                db.SaveChanges();
                Session["DeliverySchedule"] = null;
                return RedirectToAction("Index");
            }
            return View(delivery_Schedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // GET: Delivery_Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_Schedule delivery_Schedule = db.Delivery_Schedule.Find(id);
            if (delivery_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(delivery_Schedule);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Delivery_Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery_Schedule delivery_Schedule = db.Delivery_Schedule.Find(id);
            db.Delivery_Schedule.Remove(delivery_Schedule);
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
