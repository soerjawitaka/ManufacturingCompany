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
    public class ProductCategoryController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public ProductCategoryController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Product Category";
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View(db.Product_Category.ToList());
        }

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            ViewBag.ActionTitle = "Create ";
            return View();
        }

        // POST: ProductCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,category_name")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                db.Product_Category.Add(product_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActionTitle = "Create ";
            return View(product_Category);
        }

        // GET: ProductCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category product_Category = db.Product_Category.Find(id);
            if (product_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Edit ";
            return View(product_Category);
        }

        // POST: ProductCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,category_name")] Product_Category product_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActionTitle = "Edit ";
            return View(product_Category);
        }

        //// GET: ProductCategory/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product_Category product_Category = db.Product_Category.Find(id);
        //    if (product_Category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ActionTitle = "Delete ";
        //    return View(product_Category);
        //}

        //// POST: ProductCategory/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product_Category product_Category = db.Product_Category.Find(id);
        //    db.Product_Category.Remove(product_Category);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
