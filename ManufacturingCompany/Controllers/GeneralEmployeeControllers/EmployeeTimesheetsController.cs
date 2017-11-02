using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using Microsoft.AspNet.Identity;

namespace ManufacturingCompany.Controllers
{
    public class EmployeeTimesheetsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: EmployeeTimesheets
        public ActionResult Index()
        {
            var timesheets = db.Timesheets.Include(t => t.AspNetUser);
            var userID = User.Identity.GetUserId();
            return View(timesheets.Where(t => t.employee_id == userID && t.is_in_payroll == false).ToList());
        }

        // GET: EmployeeTimesheets/Create
        public ActionResult Create()
        {
            ViewBag.Username = User.Identity.GetUserName();
            return View();
        }

        // POST: EmployeeTimesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "punch_in_time,punch_out_time,timesheet_date")] Timesheet timesheet)
        {
            timesheet.employee_id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
            return View(timesheet);
        }

        // GET: EmployeeTimesheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
            return View(timesheet);
        }

        // POST: EmployeeTimesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employee_id,punch_in_time,punch_out_time,timesheet_date")] Timesheet timesheet)
        {
            timesheet.employee_id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
            return View(timesheet);
        }

        // GET: EmployeeTimesheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: EmployeeTimesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheet);
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
