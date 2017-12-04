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
    [Authorize(Roles = "SuperUser, Manager, Production")]
    public class ProductInventoryController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public ProductInventoryController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Product Inventory";
        }

        // GET: ProductInventory
        public ActionResult Index()
        {
            var product_Inventory = db.Product_Inventory.Include(p => p.Product);
            return View(product_Inventory.ToList());
        }

        // GET: ProductInventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Inventory product_Inventory = db.Product_Inventory.Find(id);
            if (product_Inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Detailed ";
            return View(product_Inventory);
        }

        // GET: ProductInventory/Create
        public ActionResult Create(int? productID)
        {
            var inventory = new Product_Inventory() { product_id = Convert.ToInt32(productID), Product = db.Products.Find(productID) };
            ViewBag.ActionTitle = "Create ";
            return View(inventory);
        }

        // POST: ProductInventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,product_id,unit_quantity,unit_per_package,packaging_cost")] Product_Inventory product_Inventory)
        {
            if (ModelState.IsValid)
            {
                product_Inventory.CalculatePackage();
                db.Product_Inventory.Add(product_Inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", product_Inventory.product_id);
            ViewBag.ActionTitle = "Create ";
            return View(product_Inventory);
        }

        // GET: ProductInventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Inventory product_Inventory = db.Product_Inventory.Find(id);
            if (product_Inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", product_Inventory.product_id);
            ViewBag.ActionTitle = "Edit ";
            return View(product_Inventory);
        }

        // POST: ProductInventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,product_id,unit_quantity,unit_per_package,per_package_cost,packaging_cost,per_package_price")] Product_Inventory product_Inventory)
        {
            if (ModelState.IsValid)
            {
                product_Inventory.CalculatePackage();
                db.Entry(product_Inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", product_Inventory.product_id);
            ViewBag.ActionTitle = "Edit ";
            return View(product_Inventory);
        }

        [Authorize(Roles = "SuperUser, Manager")]
        // GET: ProductInventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Inventory product_Inventory = db.Product_Inventory.Find(id);
            if (product_Inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(product_Inventory);
        }

        [Authorize(Roles = "SuperUser, Manager")]
        // POST: ProductInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Inventory product_Inventory = db.Product_Inventory.Find(id);
            db.Product_Inventory.Remove(product_Inventory);
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
