using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    public class Invoice_LineitemController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: Invoice_Lineitem
        public ActionResult Index()
        {
            var invoice_Lineitem = db.Invoice_Lineitem.Include(i => i.Invoice).Include(i => i.Product_Inventory);
            return View(invoice_Lineitem.ToList());
        }
        
        // GET: Invoice_Lineitem/PartialIndex
        public PartialViewResult PartialIndex(int? invoiceID)
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

        //// GET: Invoice_Lineitem/Details/5
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
        //    return View(invoice_Lineitem);
        //}

        // GET: Invoice_Lineitem/Create
        public ActionResult Create(int? invoiceID, int? id, int? Quantity)
        {
            var invoiceLineitem = new Invoice_Lineitem();
            if (Session["InvoiceLineitem"] != null) { invoiceLineitem = (Invoice_Lineitem)Session["InvoiceLineitem"]; }

            if (invoiceID != null)
            {
                invoiceLineitem.invoice_id = Convert.ToInt32(invoiceID);
            }
            
            if (id != null && Quantity != null)
            {
                invoiceLineitem.CalculateTotal(Convert.ToInt32(id));
                invoiceLineitem.lineitem_unit_quantity = Convert.ToInt32(Quantity);
            }
            ViewBag.OptionalID = invoiceID;
            Session["InvoiceLineitem"] = invoiceLineitem;
            return View(invoiceLineitem);
        }

        // POST: Invoice_Lineitem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem, int? optionalID)
        {
            if (ModelState.IsValid)
            {
                db.Invoice_Lineitem.Add(invoice_Lineitem);
                db.SaveChanges();
                Session["InvoiceLineitem"] = null;
                return RedirectToAction("Edit", "Invoices",  new { id = invoice_Lineitem.invoice_id});
            }
            ViewBag.OptionalID = optionalID;
            return View(invoice_Lineitem);
        }

        // GET: Invoice_Lineitem/Edit/5
        public ActionResult Edit(int? id, int? optionalID, int? productID, int? Quantity)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            if (productID != null && Quantity != null)
            {
                invoice_Lineitem.lineitem_unit_quantity = Convert.ToInt32(Quantity);
                invoice_Lineitem.product_inventory_id = Convert.ToInt32(productID);
            }
            invoice_Lineitem.CalculateTotal(invoice_Lineitem.product_inventory_id);
            if (invoice_Lineitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OptionalID = optionalID;
            ViewBag.LineitemID = id;
            return View(invoice_Lineitem);
        }

        // POST: Invoice_Lineitem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,invoice_id")] Invoice_Lineitem invoice_Lineitem, int? optionalID, int? lineitemID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_Lineitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "invoices", new { id = optionalID });
            }
            ViewBag.OptionalID = optionalID;
            ViewBag.LineitemID = lineitemID;
            return View(invoice_Lineitem);
        }

        //// GET: Invoice_Lineitem/Delete/5
        //public ActionResult Delete(int? id)
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
        //    return View(invoice_Lineitem);
        //}

        // GET: Invoice_Lineitem/Delete/5
        public PartialViewResult PartialDelete(int? id, int? optionalID)
        {
            if (id == null)
            {
                return PartialView(HttpStatusCode.BadRequest);
            }
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            invoice_Lineitem.CalculateTotal(invoice_Lineitem.product_inventory_id);
            if (invoice_Lineitem == null)
            {
                return PartialView("Error");
            }
            ViewBag.OptionalID = optionalID;
            return PartialView(invoice_Lineitem);
        }

        // POST: Invoice_Lineitem/Delete/5
        [HttpPost, ActionName("PartialDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? optionalID)
        {
            Invoice_Lineitem invoice_Lineitem = db.Invoice_Lineitem.Find(id);
            db.Invoice_Lineitem.Remove(invoice_Lineitem);
            db.SaveChanges();
            return RedirectToAction("Edit", "invoices", new { id = optionalID });
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
