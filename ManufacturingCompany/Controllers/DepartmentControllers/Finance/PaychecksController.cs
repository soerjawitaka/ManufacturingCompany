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
        private BusinessEntities dbBusiness = new BusinessEntities();

        // GET: Paychecks
        public ActionResult Index()
        {
            var paychecks = dbBusiness.Paychecks;
            List<PaycheckViewModel> paycheckModels = new List<PaycheckViewModel>();
            foreach (var i in paychecks)
            {
                var iModel = PaycheckViewModel.ToModel(i);
                paycheckModels.Add(iModel);
            }
            return View(paycheckModels);
        }

        // GET: Paychecks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaycheckViewModel paycheck = PaycheckViewModel.ToModel(dbBusiness.Paychecks.Find(id));
            if (paycheck == null)
            {
                return HttpNotFound();
            }
            return View(paycheck);
        }

        // GET: Paychecks/Create
        public ActionResult Create(int payrollid)
        {
            var paycheck = new PaycheckViewModel();
            paycheck.payroll_id = payrollid;
            var payroll = dbBusiness.Payrolls.Find(payrollid);
            payroll.CalculatePayroll();
            paycheck.Payroll = payroll;
            paycheck.payment_amount = payroll.grand_total;
            return View(paycheck);
        }

        // POST: Paychecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] PaycheckViewModel paycheck)
        {
            var newPaycheck = PaycheckViewModel.ToBase(paycheck);
            dbBusiness.Paychecks.Add(newPaycheck);
            var result = dbBusiness.SaveChanges();

            if (result > 0)
            {
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
            PaycheckViewModel paycheck = PaycheckViewModel.ToModel(dbBusiness.Paychecks.Find(id));
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
        public ActionResult Edit([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] PaycheckViewModel paycheck)
        {
            Paycheck modPaycheck = PaycheckViewModel.ToBase(paycheck);
            dbBusiness.Entry(modPaycheck).State = EntityState.Modified;
            var result = dbBusiness.SaveChanges();
            if (result > 0)
            {
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
            PaycheckViewModel paycheck = PaycheckViewModel.ToModel(dbBusiness.Paychecks.Find(id));
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
            Paycheck paycheck = dbBusiness.Paychecks.Find(id);
            dbBusiness.Paychecks.Remove(paycheck);
            dbBusiness.SaveChanges();
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
