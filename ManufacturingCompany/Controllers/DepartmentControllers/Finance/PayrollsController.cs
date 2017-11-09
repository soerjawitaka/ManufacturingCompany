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

        public PayrollsController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
            ViewBag.ItemTitle = "Payroll";
        }

        // GET: Payrolls
        public ActionResult Index()
        {
            var payrolls = (from pr in db.Payrolls
                           where !((from pc in db.Paychecks select pc.payroll_id).Contains(pr.Id))
                           select pr).OrderByDescending(p => p.period_end).ToList();
            return View(payrolls);
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
            ViewBag.ActionTitle = "Detailed ";
            return View(payroll);
        }

        // GET: Payrolls/Create
        public ActionResult Create(string userID)
        {
            var payroll = new Payroll();
            payroll.employee_id = userID;
            ViewBag.employee_username = db.AspNetUsers.Find(userID).UserName;
            ViewBag.ActionTitle = "Create ";
            return View(payroll);
        }

        // POST: Payrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,employee_id,period_begin,period_end,total_hours,subtotal,federal_tax_witholding,state_tax_witholding,grand_total")] Payroll payroll)
        {
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                if (payroll.ThereAreAvailableTimesheets())
                {
                    payroll.CalculatePayroll(); // calculate the rest of the fields
                    db.Payrolls.Add(payroll);
                    var result = db.SaveChanges();
                    if (result > 0)
                    {
                        if (payroll.ChangeTimesheetsStatus() > 0) // change timesheet status and check result
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            db.Payrolls.Remove(payroll);
                            db.SaveChanges();
                            errorMessage = "Unable to create this payroll. Timesheet updates failed.";
                        }
                    }
                    errorMessage = "Unable to save this payroll.";
                }
                errorMessage = "There are no available timesheets for this period.";
            }
            ViewBag.ErrorMessage = errorMessage;
            ViewBag.employee_username = db.AspNetUsers.Find(payroll.employee_id).UserName;
            ViewBag.ActionTitle = "Create ";
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
            ViewBag.EmployeeUsername = db.AspNetUsers.Find(payroll.employee_id).UserName;
            ViewBag.ActionTitle = "Edit ";
            return View(payroll);
        }

        // POST: Payrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employee_id,period_begin,period_end")] Payroll payroll)
        {
            string errorMessage = "";
            var oldPayroll = db.Payrolls.Find(payroll.Id);
            oldPayroll.CalculatePayroll();

            if (ModelState.IsValid)
            {
                oldPayroll.InitializeTimesheets();
                var newPayroll = db.Payrolls.Find(payroll.Id);
                newPayroll.period_begin = payroll.period_begin;
                newPayroll.period_end = payroll.period_end;
                newPayroll.total_hours = 0;
                newPayroll.subtotal = 0m;
                newPayroll.CalculatePayroll();
                db.Entry(newPayroll).State = EntityState.Modified;
                var result = db.SaveChanges();

                if (result > 0)
                {
                    newPayroll.ChangeTimesheetsStatus();
                    return RedirectToAction("Index");
                }
                else
                {
                    oldPayroll.ChangeTimesheetsStatus();
                    errorMessage = "Unable to modify this payroll. Please try again.";
                }
            }

            ViewBag.ErrorMessage = errorMessage;
            ViewBag.ActionTitle = "Edit ";
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
            ViewBag.ActionTitle = "Delete ";
            return View(payroll);
        }

        // POST: Payrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payroll payroll = db.Payrolls.Find(id);
            payroll.CalculatePayroll();
            payroll.InitializeTimesheets();
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
