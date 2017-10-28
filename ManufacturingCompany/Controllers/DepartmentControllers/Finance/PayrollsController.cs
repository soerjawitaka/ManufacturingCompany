using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Finance
{
    public class PayrollsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: Payrolls
        public ActionResult Index()
        {
            var payrolls = from pr in db.Payrolls
                           where !((from pc in db.Paychecks select pc.payroll_id).Contains(pr.Id))
                           select pr;
            return View(payrolls.ToList());
        }

        // GET: Payrolls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // GET: Payrolls/Create
        public ActionResult Create(string userID)
        {
            var payroll = new Payroll();
            payroll.employee_id = userID;
            ViewBag.employee_username = db.AspNetUsers.Find(userID).UserName;
            return View(payroll);
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,employee_id,period_begin,period_end,total_hours,subtotal,federal_tax_witholding,state_tax_witholding,grand_total")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                payroll.CalculatePayroll(); // calculate the rest of the fields
                db.Payrolls.Add(payroll);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    foreach (var i in payroll.assignedTimesheet)
                    {
                        var timesheet = db.Timesheets.Find(i.Id);
                        timesheet.is_in_payroll = true;
                        db.Entry(timesheet).State = EntityState.Modified;                        
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", payroll.employee_id);
            return View(payroll);
        }

        // GET: Payrolls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", payroll.employee_id);
            return View(payroll);
        }

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employee_id,period_begin,period_end,total_hours,subtotal,federal_tax_witholding,state_tax_witholding,grand_total")] Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", payroll.employee_id);
            return View(payroll);
        }

        // GET: Payrolls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payroll payroll = db.Payrolls.Find(id);
            if (payroll == null)
            {
                return HttpNotFound();
            }
            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            db.Payrolls.Remove(payroll);
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
