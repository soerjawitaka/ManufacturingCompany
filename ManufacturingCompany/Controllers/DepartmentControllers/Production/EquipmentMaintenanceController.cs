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
    public class EquipmentMaintenanceController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: EquipmentMaintenance
        public ActionResult Index()
        {
            var equipment_Maintenance = db.Equipment_Maintenance.Include(e => e.AspNetUser).Include(e => e.Equipment);
            return View(equipment_Maintenance.ToList());
        }

        // GET: EquipmentMaintenance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Maintenance equipment_Maintenance = db.Equipment_Maintenance.Find(id);
            if (equipment_Maintenance == null)
            {
                return HttpNotFound();
            }
            return View(equipment_Maintenance);
        }

        // GET: EquipmentMaintenance/Create
        public ActionResult Create()
        {
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.equipment_id = new SelectList(db.Equipments, "Id", "equipment_name");
            return View();
        }

        // POST: EquipmentMaintenance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,equipment_id,maintenance_date,completion_eta,maintenance_cost,employee_id,maintenance_short_description,maintenance_long_description,maintenance_note")] Equipment_Maintenance equipment_Maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Equipment_Maintenance.Add(equipment_Maintenance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", equipment_Maintenance.employee_id);
            ViewBag.equipment_id = new SelectList(db.Equipments, "Id", "equipment_name", equipment_Maintenance.equipment_id);
            return View(equipment_Maintenance);
        }

        // GET: EquipmentMaintenance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Maintenance equipment_Maintenance = db.Equipment_Maintenance.Find(id);
            if (equipment_Maintenance == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", equipment_Maintenance.employee_id);
            ViewBag.equipment_id = new SelectList(db.Equipments, "Id", "equipment_name", equipment_Maintenance.equipment_id);
            return View(equipment_Maintenance);
        }

        // POST: EquipmentMaintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,equipment_id,maintenance_date,completion_eta,maintenance_cost,employee_id,maintenance_short_description,maintenance_long_description,maintenance_note")] Equipment_Maintenance equipment_Maintenance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment_Maintenance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", equipment_Maintenance.employee_id);
            ViewBag.equipment_id = new SelectList(db.Equipments, "Id", "equipment_name", equipment_Maintenance.equipment_id);
            return View(equipment_Maintenance);
        }

        // GET: EquipmentMaintenance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment_Maintenance equipment_Maintenance = db.Equipment_Maintenance.Find(id);
            if (equipment_Maintenance == null)
            {
                return HttpNotFound();
            }
            return View(equipment_Maintenance);
        }

        // POST: EquipmentMaintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment_Maintenance equipment_Maintenance = db.Equipment_Maintenance.Find(id);
            db.Equipment_Maintenance.Remove(equipment_Maintenance);
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
