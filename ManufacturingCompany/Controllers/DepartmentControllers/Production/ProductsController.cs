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
    public class ProductsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public ProductsController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Product";
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.Include(p => p.Product_Category).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Detailed ";
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.product_category_id = new SelectList(db.Product_Category, "Id", "category_name");
            ViewBag.ActionTitle = "Create ";
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,product_name,product_short_description,product_long_description,product_note,product_unit_measure,product_unit_cost,product_unit_price,product_material_id,product_category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_category_id = new SelectList(db.Product_Category, "Id", "category_name", product.product_category_id);
            ViewBag.ActionTitle = "Create ";
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_category_id = new SelectList(db.Product_Category, "Id", "category_name", product.product_category_id);
            ViewBag.ActionTitle = "Edit ";
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,product_name,product_short_description,product_long_description,product_note,product_unit_measure,product_unit_cost,product_unit_price,product_material_id,product_category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_category_id = new SelectList(db.Product_Category, "Id", "category_name", product.product_category_id);
            ViewBag.ActionTitle = "Edit ";
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
