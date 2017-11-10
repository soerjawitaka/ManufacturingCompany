using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Production
{
    public class MaterialStockController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: MaterialStock
        public ActionResult Index()
        {
            var material_Stock = db.Material_Stock.Include(m => m.Material);
            return View(material_Stock.ToList());
        }

        // GET: MaterialStock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material_Stock material_Stock = db.Material_Stock.Find(id);
            if (material_Stock == null)
            {
                return HttpNotFound();
            }
            return View(material_Stock);
        }

        // GET: MaterialStock/Create
        public ActionResult Create(int? materialID)
        {
            if (materialID != null)
            {
                var material = db.Materials.Find(materialID);
                var newStock = new Material_Stock() { material_id = Convert.ToInt32(materialID), Material = material };
                return View(newStock);
            }
            
            return View();
        }

        // POST: MaterialStock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,material_id,material_unit_measure,material_unit_quantity,material_unit_cost")] Material_Stock material_Stock)
        {
            if (ModelState.IsValid)
            {
                db.Material_Stock.Add(material_Stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.material_id = new SelectList(db.Materials, "Id", "material_name", material_Stock.material_id);
            return View(material_Stock);
        }

        // GET: MaterialStock/Edit/5
        public ActionResult Edit(int? id, int? materialID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material_Stock material_Stock = db.Material_Stock.Find(id);
            if (material_Stock == null)
            {
                return HttpNotFound();
            }
            if (materialID != null)
            {
                material_Stock.material_id = Convert.ToInt32(materialID);
                material_Stock.Material = db.Materials.Find(materialID);
            }
            ViewBag.material_id = new SelectList(db.Materials, "Id", "material_name", material_Stock.material_id);
            return View(material_Stock);
        }

        // POST: MaterialStock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,material_id,material_unit_measure,material_unit_quantity,material_unit_cost")] Material_Stock material_Stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(material_Stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.material_id = new SelectList(db.Materials, "Id", "material_name", material_Stock.material_id);
            return View(material_Stock);
        }

        // GET: MaterialStock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material_Stock material_Stock = db.Material_Stock.Find(id);
            if (material_Stock == null)
            {
                return HttpNotFound();
            }
            return View(material_Stock);
        }

        // POST: MaterialStock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material_Stock material_Stock = db.Material_Stock.Find(id);
            db.Material_Stock.Remove(material_Stock);
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
