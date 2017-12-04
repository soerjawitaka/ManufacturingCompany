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

namespace ManufacturingCompany.Controllers
{
    [Authorize(Roles = "SuperUser, Manager, Finance")]
    public class PaychecksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private BusinessEntities dbBusiness = new BusinessEntities();

        public PaychecksController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
            ViewBag.ItemTitle = "Paycheck";
        }

        // GET: Paychecks
        public ActionResult Index()
        {
            var paychecks = dbBusiness.Paychecks.OrderByDescending(p => p.paycheck_date);
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
            ViewBag.ActionTitle = "Detailed ";
            return View(paycheck);
        }

        // GET: Paychecks/Create
        public ActionResult Create(int payrollid)
        {
            var paycheck = new PaycheckModeModel();
            paycheck.SetPayroll(payrollid);
            ViewBag.ActionTitle = "Create ";
            return View(paycheck);
        }

        // POST: Paychecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] PaycheckModeModel paycheck)
        {
            var newPaycheck = PaycheckModeModel.ToBase(paycheck);
            dbBusiness.Paychecks.Add(newPaycheck);
            var result = dbBusiness.SaveChanges();

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ActionTitle = "Create ";
            return View(paycheck);
        }

        // GET: Paychecks/Edit/5
        public ActionResult Edit(int? id, int? payrollid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaycheckModeModel paycheck = PaycheckModeModel.ToModel(dbBusiness.Paychecks.Find(id));
            if (payrollid != null)
            {
                paycheck.SetPayroll(Convert.ToInt32(payrollid));
            }
            
            if (paycheck == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Edit ";
            return View(paycheck);
        }

        //// GET: Paychecks/Edit/EditPayroll
        //public ActionResult EditPayroll(int id, int payrollid)
        //{            
        //    PaycheckModeModel paycheck = PaycheckModeModel.ToModel(dbBusiness.Paychecks.Find(id));
        //    if (paycheck == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    paycheck.SetPayroll(payrollid);
        //    ViewBag.ActionTitle = "Edit Payroll for ";
        //    return View(paycheck);
        //}

        // POST: Paychecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ModeOfPaycheck,paycheck_date,payroll_id,payment_type,check_number,direct_deposit_number,payment_amount")] PaycheckModeModel paycheck)
        {
            // set old paycheck
            paycheck.SetPayroll(paycheck.payroll_id);
            Paycheck oldPaycheck = PaycheckModeModel.ToBase(paycheck);

            // assign fields to new paycheck
            var newPaycheck = dbBusiness.Paychecks.Find(oldPaycheck.Id);
            newPaycheck.paycheck_date = oldPaycheck.paycheck_date;
            newPaycheck.payroll_id = oldPaycheck.payroll_id;
            newPaycheck.payment_type = oldPaycheck.payment_type;
            newPaycheck.check_number = oldPaycheck.check_number;
            newPaycheck.direct_deposit_number = oldPaycheck.direct_deposit_number;
            newPaycheck.payment_amount = oldPaycheck.payment_amount;

            dbBusiness.Entry(newPaycheck).State = EntityState.Modified;
            var result = dbBusiness.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ActionTitle = "Edit ";
            return View(paycheck);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
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
            ViewBag.ActionTitle = "Delete ";
            return View(paycheck);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
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
