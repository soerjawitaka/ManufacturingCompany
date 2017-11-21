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
            if (invoiceID == null)
            {
                return PartialView("_Error");
            }
            var invoice_Lineitem = db.Invoice_Lineitem.Where(ili => ili.invoice_id == invoiceID).Include(i => i.Invoice).Include(i => i.Product_Inventory);
            foreach (var item in invoice_Lineitem) { item.CalculateTotal(item.product_inventory_id); }
            ViewBag.InvoiceID = Convert.ToInt32(invoiceID);
            return PartialView(invoice_Lineitem.ToList());
        }

        //// GET: InvoiceLineitems/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
        //    if (invoice_Lineitem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(invoice_Lineitem);
        //}

        // GET: InvoiceLineitems/Create
        public PartialViewResult Create(int? invoiceID, int? productInventoryID)
        {
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id");
            //ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id");

            var invoiceLineitem = new Invoice_Lineitem();
            if (Session["InvoiceLineitem"] != null) { invoiceLineitem = (Invoice_Lineitem)Session["ProductInventory"]; }

            if (productInventoryID != null) { invoiceLineitem.CalculateTotal(Convert.ToInt32(productInventoryID)); }

            Session["InvoiceLineitem"] = invoiceLineitem;
            ViewBag.InvoiceID = Convert.ToInt32(invoiceID);
            return PartialView(invoiceLineitem);
        }

        // POST: InvoiceLineitems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem, int? invoiceID)
        {
            if (ModelState.IsValid && invoiceID != null)
            {
                var invoiceLI = db.Invoice_Lineitem.SingleOrDefault(li => li.invoice_id == invoiceID && li.product_inventory_id == invoice_Lineitem.product_inventory_id);
                if (invoiceLI != null)
                {
                    invoiceLI.lineitem_unit_quantity += invoice_Lineitem.lineitem_unit_quantity;
                    db.Entry(invoiceLI).State = EntityState.Modified;
                }
                else
                {
                    db.Invoice_Lineitem.Add(invoice_Lineitem);
                }
                db.SaveChanges();
                return RedirectToAction("Edit", "Invoices", new { id = invoiceID });
            }

            return View("Error");
        }

        // GET: InvoiceLineitems/Edit/5
        public PartialViewResult Edit(int? id, int? invoiceID)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (invoice_Lineitem == null)
            {
                return PartialView("_Error");
            }
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id", invoice_Lineitem.invoice_id);
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", invoice_Lineitem.product_inventory_id);
            ViewBag.InvoiceID = Convert.ToInt32(invoiceID);
            return PartialView(invoice_Lineitem);
        }

        // POST: InvoiceLineitems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem, int? invoiceID)
        {
            if (ModelState.IsValid && invoiceID != null)
            {
                db.Entry(invoice_Lineitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Invoices", new { id = invoiceID });
            }
            return View("Error");
        }

        // GET: InvoiceLineitems/Delete/5
        public PartialViewResult Delete(int? id, int? invoiceID)
        {
            if (id == null)
            {
                return PartialView("_Error");
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (invoice_Lineitem == null)
            {
                return PartialView("_Error");
            }
            ViewBag.InvoiceID = Convert.ToInt32(invoiceID);
            return PartialView(invoice_Lineitem);
        }

        // POST: InvoiceLineitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int invoiceID)
        {
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            db.Invoice_Lineitem.Remove(invoice_Lineitem);
            db.SaveChanges();
            return RedirectToAction("Edit", "Invoices", new { id = invoiceID });
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