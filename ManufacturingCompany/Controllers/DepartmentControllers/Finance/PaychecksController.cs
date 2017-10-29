using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using ManufacturingCompany.Classes;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Finance
{
    public class PaychecksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Paychecks
        public ActionResult Index()
        {
            var paychecks = db.Paychecks;
            List<PaycheckViewModel> paycheckModels = new List<PaycheckViewModel>();
            foreach (var i in paychecks) { paycheckModels.Add(i as PaycheckViewModel); }
            return View(paycheckModels);
        }

        // GET: Paychecks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paycheck paycheck = db.Paychecks.Find(id);
            if (paycheck == null)
            {
                return HttpNotFound();
            }
            return View(paycheck);
        }

        // GET: Paychecks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paychecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] Paycheck paycheck)
        {
            if (ModelState.IsValid)
            {
                db.Paychecks.Add(paycheck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paycheck);
        }

        // GET: Paychecks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paycheck paycheck = db.Paychecks.Find(id);
            if (paycheck == null)
            {
                return HttpNotFound();
            }
            return View(paycheck);
        }

        // POST: Paychecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] Paycheck paycheck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paycheck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paycheck);
        }

        // GET: Paychecks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paycheck paycheck = db.Paychecks.Find(id);
            if (paycheck == null)
            {
                return HttpNotFound();
            }
            return View(paycheck);
        }

        // POST: Paychecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paycheck paycheck = db.Paychecks.Find(id);
            db.Paychecks.Remove(paycheck);
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
