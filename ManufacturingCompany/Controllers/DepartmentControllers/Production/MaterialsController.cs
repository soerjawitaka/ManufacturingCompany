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
    public class MaterialsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public MaterialsController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Material";
        }

        // GET: Materials
        public ActionResult Index()
        {
            var materials = db.Materials.Include(m => m.Product);
            return View(materials.ToList());
        }

        // GET: Materials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Detailed ";
            return View(material);
        }

        // GET: Materials/Create
        public ActionResult Create(int? productID)
        {
            if (productID != null)
            {
                return View(new Material() { product_id = productID, Product = db.Products.Find(productID) });
            }
            ViewBag.ActionTitle = "Create ";
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,material_name,material_description,material_note,product_id")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", material.product_id);
            ViewBag.ActionTitle = "Create ";
            return View(material);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id, int? productID, string removeFrom)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            if (productID != null)
            {
                material.product_id = productID;
                material.Product = db.Products.Find(productID);
            }
            if (removeFrom != null)
            {
                material.product_id = null;
                material.Product = null;
            }
            ViewBag.ActionTitle = "Edit ";
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,material_name,material_description,material_note,product_id")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", material.product_id);
            ViewBag.ActionTitle = "Edit ";
            return View(material);
        }

        [Authorize(Roles ="SuperUser, Manager, Supervisor")]
        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(material);
        }

        [Authorize(Roles = "SuperUser, Manager, Supervisor")]
        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Materials.Find(id);
            db.Materials.Remove(material);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Materials/RemoveProduct
        public ActionResult RemoveProduct(string removeFrom, int? optionalID)
        {
            if (optionalID != null)
            {
                return RedirectToAction(removeFrom, new { id = optionalID, removeFrom = removeFrom });
            }
            return RedirectToAction(removeFrom);
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
