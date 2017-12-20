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
    [Authorize(Roles = "SuperUser, Manager, Finance")]
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
                    if (Quantity > db.Product_Inventory.Find(id).unit_quantity)
                    {
                        return RedirectToAction("Index", "SelectProductInventory", new { actionName = "SelectItems", controllerName = "Invoices", message = "Quantity requested is not available." });
                    }
                    var li = new Lineitem();
                    li.lineitem_unit_quantity = Convert.ToInt32(Quantity);
                    li.CalculateTotal(Convert.ToInt32(id)); // assign product id and calculate total
                    li.ProductInventory.Product = db.Products.Find(li.ProductInventory.product_id); // assign product
                    invoiceItems.Add(li);
                }
                else
                {
                    var li = invoiceItems.SingleOrDefault(ii => ii.product_inventory_id == id);
                    if (li != null)
                    {
                        if ((Quantity + li.lineitem_unit_quantity) > db.Product_Inventory.Find(id).unit_quantity)
                        {
                            return RedirectToAction("Index", "SelectProductInventory", new { actionName = "SelectItems", controllerName = "Invoices", message = "Quantity requested is not available." });
                        }
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
        public ActionResult Create(string userID, int? customerID)
        {
            var currentInvoice = new Invoice();
            if (Session["CurrentInvoice"] != null) { currentInvoice = (Invoice)Session["CurrentInvoice"]; }

            // assign employee if employee is not manager or superuser
            if (!(User.IsInRole("Manager") || User.IsInRole("SuperUser")))
            {
                currentInvoice.employee_id = db.AspNetUsers.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            }

            // assigning employee
            if (userID != null) 
            {
                currentInvoice.employee_id = userID;
                currentInvoice.AspNetUser = db.AspNetUsers.Find(userID);
            }

            // assigning customer
            if (customerID != null) 
            {
                currentInvoice.customer_id = Convert.ToInt32(customerID);
                currentInvoice.Customer = db.Customers.Find(customerID);
            }
            Session["CurrentInvoice"] = currentInvoice;
            ViewBag.EmployeeError = "";
            ViewBag.CustomerError = "";
            ViewBag.ActionTitle = "Create ";
            return View(currentInvoice);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,invoice_date,employee_id,customer_id")] Invoice invoice)
        {
            if (ModelState.IsValid && invoice.employee_id != null && invoice.customer_id != 0)
            {
                db.Invoices.Add(invoice);
                var result = db.SaveChanges();
                if (result > 0)
                {
                    var li = (List<Lineitem>)Session["InvoiceItems"];
                    foreach (var item in li)
                    {
                        // create invoice lineitem
                        var invoiceLI = new Invoice_Lineitem() { product_inventory_id = item.product_inventory_id, lineitem_unit_quantity = item.lineitem_unit_quantity};
                        invoiceLI.invoice_id = db.Invoices.Max(i => i.Id);
                        db.Invoice_Lineitem.Add(invoiceLI);
                        db.SaveChanges();
                        // create delivery lineitem queue
                        var deliveryLI = new Delivery_Lineitem() { product_inventory_id = item.product_inventory_id, lineitem_unit_quantity = item.lineitem_unit_quantity };
                        deliveryLI.invoice_lineitem_id = db.Invoice_Lineitem.Max(i => i.Id);
                        db.Delivery_Lineitem.Add(deliveryLI);
                        db.SaveChanges();
                        // update product inventory quantity
                        var productInventory = db.Product_Inventory.Find(item.product_inventory_id);
                        productInventory.unit_quantity -= item.lineitem_unit_quantity;
                        db.Entry(productInventory).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                Session["InvoiceItems"] = null;
                Session["CurrentInvoice"] = null;
                return RedirectToAction("Index");
            }

            if (invoice.employee_id != null || invoice.employee_id != "") { invoice.AspNetUser = db.AspNetUsers.Find(invoice.employee_id); }
            if (invoice.customer_id != 0 ) { invoice.Customer = db.Customers.Find(invoice.customer_id); }
            var employeeError = "";
            var customerError = "";
            if (invoice.employee_id == null) { employeeError = "Please select an employee."; }
            if (invoice.customer_id == 0) { customerError = "Please select a customer."; }
            ViewBag.EmployeeError = employeeError;
            ViewBag.CustomerError = customerError;
            ViewBag.ActionTitle = "Create ";
            Session["CurrentInvoice"] = invoice;
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id, string userID, int? customerID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            invoice.CalculateTotal();
            if (userID != null)
            {
                invoice.employee_id = userID;
                invoice.AspNetUser = db.AspNetUsers.Find(userID);
            }
            else
            {
                invoice.AspNetUser = db.AspNetUsers.Find(invoice.employee_id);
            }
            if (customerID != null)
            {
                invoice.customer_id = Convert.ToInt32(customerID);
                invoice.Customer = db.Customers.Find(customerID);
            }
            else
            {
                invoice.Customer = db.Customers.Find(invoice.customer_id);
            }
            invoice.Customer = db.Customers.Find(invoice.customer_id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
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
            invoice.AspNetUser = db.AspNetUsers.Find(invoice.employee_id);
            invoice.Customer = db.Customers.Find(invoice.customer_id);
            ViewBag.ActionTitle = "Edit ";
            return View(invoice);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
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
            ViewBag.ActionTitle = "Delete ";
            return View(invoice);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);

            // delete line items
            var invoiceLI = db.Invoice_Lineitem.Where(iLI => iLI.invoice_id == id).ToList(); // invoice lineitems
            foreach (var i in invoiceLI)
            {
                var deliveryLI = db.Delivery_Lineitem.Where(dLI => dLI.invoice_lineitem_id == i.Id).ToList();
                foreach (var di in deliveryLI)
                {
                    db.Delivery_Lineitem.Remove(di); // remove delivery lineitem
                }
                //db.SaveChanges();
                db.Invoice_Lineitem.Remove(i); // remove invoice lineitem
            }
            //db.SaveChanges();
            db.Invoices.Remove(invoice); // remove invoice
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
