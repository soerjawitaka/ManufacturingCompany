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
    [Authorize(Roles = "SuperUser, Manager, Finance")]
    public class PaymentsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public PaymentsController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
            ViewBag.ItemTitle = "Payment";
        }

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Invoice);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            payment.Invoice.CalculateTotal();
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Detailed ";
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create(int? invoiceID)
        {
            var newPayment = new Payment();
            if (invoiceID != null)
            {
                var selectedInvoice = db.Invoices.Find(invoiceID);
                selectedInvoice.CalculateTotal();
                newPayment.invoice_id = Convert.ToInt32(invoiceID);
                newPayment.Invoice = selectedInvoice;
            }
            ViewBag.ActionTitle = "Create ";
            return View(newPayment);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,invoice_id,payment_total,payment_type,payment_date,payment_note")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ActionTitle = "Create ";
            return View(payment);
        }

        // partial view to select an invoice
        public PartialViewResult PartialSelectInvoice(string actionName, int? optionalID)
        {
            // setting for query options
            List<string> searchBy = new List<string>();
            searchBy.Add("Invoice ID");
            searchBy.Add("Customer Name");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.OptionalID = optionalID;

            var invoices = db.Invoices.OrderByDescending(i => i.Id).Take(10).ToList();
            foreach (var i in invoices)
            {
                i.CalculateTotal();
            }
            return PartialView(invoices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult PartialSelectInvoice(string actionName, string SearchBy, string inputForUserSearch, int? optionalID)
        {
            // setting for query options
            List<string> searchBy = new List<string>();
            searchBy.Add("Invoice ID");
            searchBy.Add("Customer Name");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.OptionalID = optionalID;

            List<Invoice> invoices = null;

            if (inputForUserSearch != null)
            {
                switch (SearchBy)
                {
                    case "Invoice ID":
                        invoices = db.Invoices.Where(i => i.Id.ToString().Contains(inputForUserSearch)).OrderByDescending(i => i.Id).Take(10).ToList();
                        break;
                    case "Customer Name":
                        invoices = db.Invoices.Where(i => i.Customer.customer_company_name.Contains(inputForUserSearch)).OrderByDescending(i => i.Id).Take(10).ToList();
                        break;
                    default:
                        invoices = new List<Invoice>();
                        break;
                }
            }
            string errorString = "";
            if (invoices != null)
            {
                foreach (var i in invoices)
                {
                    i.CalculateTotal();
                }
            }
            else
            {
                errorString = "Invoices not found.";
            }
            ViewBag.ErrorString = errorString;
            return PartialView(invoices);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id, int? invoiceID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            Invoice selectedInvoice;
            if (invoiceID != null)
            {
                selectedInvoice = db.Invoices.Find(invoiceID);
                if (selectedInvoice == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                selectedInvoice = db.Invoices.Find(payment.invoice_id);
            }
            selectedInvoice.CalculateTotal();
            payment.invoice_id = selectedInvoice.Id;
            payment.Invoice = selectedInvoice;
            ViewBag.ActionTitle = "Edit ";
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,invoice_id,payment_total,payment_type,payment_date,payment_note")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActionTitle = "Edit ";
            return View(payment);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            payment.Invoice.CalculateTotal();
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(payment);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
