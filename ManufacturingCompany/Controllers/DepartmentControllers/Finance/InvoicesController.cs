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
    public class InvoicesController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public InvoicesController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
            ViewBag.ItemTitle = "Invoice";
        }

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.AspNetUser).Include(i => i.Customer);
            foreach (var i in invoices) { i.CalculateTotal(); }
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            invoice.CalculateTotal();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Detailed ";
            return View(invoice);
        }

        // GET: Invoices/SelectItems
        public ActionResult SelectItems(int? id, int? Quantity)
        {
            var invoiceItems = new List<Lineitem>();
            // load items from session
            if (Session["InvoiceItems"] != null) { invoiceItems = (List<Lineitem>)Session["InvoiceItems"]; }

            if (id != null)
            {
                if (invoiceItems.SingleOrDefault(ii => ii.product_inventory_id == id) == null)
                {
                    var li = new Lineitem();
                    li.lineitem_unit_quantity = Convert.ToInt32(Quantity);
                    li.CalculateTotal(Convert.ToInt32(id));
                    li.ProductInventory.Product = db.Products.Find(li.ProductInventory.product_id);
                    invoiceItems.Add(li);
                }
                else
                {
                    var li = invoiceItems.SingleOrDefault(ii => ii.product_inventory_id == id);
                    if (li != null)
                    {
                        li.lineitem_unit_quantity += Convert.ToInt32(Quantity);
                        li.CalculateTotal(Convert.ToInt32(id));
                        li.ProductInventory.Product = db.Products.Find(li.ProductInventory.product_id);
                    }
                }
            }

            Session["InvoiceItems"] = invoiceItems;

            ViewBag.ActionTitle = "Select Items for  ";
            return View(invoiceItems);
        }

        // GET: Invoices/RemoveItem
        public ActionResult RemoveItem(int? id)
        {
            var invoiceItems = new List<Lineitem>();
            // load items from session
            if (Session["InvoiceItems"] != null) { invoiceItems = (List<Lineitem>)Session["InvoiceItems"]; }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var li = invoiceItems.SingleOrDefault(item => item.product_inventory_id == id);
                if (li != null)
                {
                    invoiceItems.Remove(li);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            Session["InvoiceItems"] = invoiceItems;
            return RedirectToAction("SelectItems");
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name");
            ViewBag.ActionTitle = "Create ";
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,invoice_date,employee_id,customer_id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    var li = (List<Lineitem>)Session["InvoiceItems"];
                    foreach (var item in li)
                    {
                        var invoiceLI = new Invoice_Lineitem() { product_inventory_id = item.product_inventory_id, lineitem_unit_quantity = item.lineitem_unit_quantity};
                        invoiceLI.invoice_id = db.Invoices.Max(i => i.Id);
                        db.Invoice_Lineitem.Add(invoiceLI);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", invoice.employee_id);
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", invoice.customer_id);
            ViewBag.ActionTitle = "Create ";
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", invoice.employee_id);
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", invoice.customer_id);
            ViewBag.ActionTitle = "Edit ";
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,invoice_date,employee_id,customer_id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", invoice.employee_id);
            ViewBag.customer_id = new SelectList(db.Customers, "Id", "customer_company_name", invoice.customer_id);
            ViewBag.ActionTitle = "Edit ";
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region Lineitems

        // GET: InvoiceLineitems
        public PartialViewResult LineitemsIndex(int? invoiceID)
        {
            var invoice_Lineitem = db.Invoice_Lineitem.Where(ili => ili.invoice_id == invoiceID).Include(i => i.Invoice).Include(i => i.Product_Inventory);
            return PartialView("_LineitemsIndex", invoice_Lineitem.ToList());
        }

        // GET: InvoiceLineitems/Create
        public PartialViewResult LineitemsCreate()
        {
            ViewBag.invoice_id = new SelectList(db.Invoices, "Id", "employee_id");
            ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id");
            return PartialView("_LineitemsCreate");
        }

        #endregion

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
