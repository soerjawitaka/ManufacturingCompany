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
    public class EquipmentsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public EquipmentsController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Equipment";
        }

        // GET: Equipments
        public ActionResult Index()
        {
            var equipments = db.Equipments.Include(e => e.Product);
            return View(equipments.ToList());
        }

        // GET: Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create(int? productID)
        {
            if (productID != null)
            {
                var eq = new Equipment() { product_id = productID, Product = db.Products.Find(productID) };
                return View(eq);
            }
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,equipment_name,equipment_short_description,equipment_long_description,equipment_note,equipment_cost,product_id,in_maintenance")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", equipment.product_id);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id, int? productID, string action)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            if (productID != null)
            {
                equipment.product_id = Convert.ToInt16(productID);
                equipment.Product = db.Products.Find(productID);
            }
            if (action == "Edit")
            {
                equipment.product_id = null;
                equipment.Product = null;
            }
            
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,equipment_name,equipment_short_description,equipment_long_description,equipment_note,equipment_cost,product_id,in_maintenance")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "Id", "product_name", equipment.product_id);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Equipments/RemoveProduct
        public ActionResult RemoveProduct(string actionName, int? optionalID)
        {
            if (actionName == "Create")
            {
                return RedirectToAction(actionName);
            }
            return RedirectToAction(actionName, new { id = optionalID });
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
