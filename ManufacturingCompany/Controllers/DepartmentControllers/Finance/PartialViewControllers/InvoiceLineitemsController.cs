using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using System.Net;


namespace ManufacturingCompany.Controllers
{
    public class InvoiceLineitemsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: InvoiceLineitems
        public PartialViewResult Index(int? invoiceID)
        {
            var invoice_Lineitem = db.Invoice_Lineitem.Where(ili => ili.invoice_id == invoiceID).Include(i => i.Invoice).Include(i => i.Product_Inventory);
            return PartialView(invoice_Lineitem.ToList());
        }

        // GET: InvoiceLineitems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (invoice_Lineitem == null)
            {
                return HttpNotFound();
            }
            return PartialView(invoice_Lineitem);
        }

        // GET: InvoiceLineitems/Create
        public PartialViewResult Create()
        {
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id");
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id");
            return PartialView();
        }

        // POST: InvoiceLineitems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem)
        {
            if (ModelState.IsValid)
            {
                db.Invoice_Lineitem.Add(invoice_Lineitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id", invoice_Lineitem.invoice_id);
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", invoice_Lineitem.product_inventory_id);
            return PartialView(invoice_Lineitem);
        }

        // GET: InvoiceLineitems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (invoice_Lineitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id", invoice_Lineitem.invoice_id);
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", invoice_Lineitem.product_inventory_id);
            return PartialView(invoice_Lineitem);
        }

        // POST: InvoiceLineitems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_Lineitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id", invoice_Lineitem.invoice_id);
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", invoice_Lineitem.product_inventory_id);
            return PartialView(invoice_Lineitem);
        }

        // GET: InvoiceLineitems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (invoice_Lineitem == null)
            {
                return HttpNotFound();
            }
            return PartialView(invoice_Lineitem);
        }

        // POST: InvoiceLineitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            db.Invoice_Lineitem.Remove(invoice_Lineitem);
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