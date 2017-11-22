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
        public ActionResult Create(string userID, int? equipmentID)
        {
            var eqMaintenance = new Equipment_Maintenance();
            // load info in session
            if (Session["EquipmentMaintenance"] != null)
            {
                eqMaintenance = (Equipment_Maintenance)Session["EquipmentMaintenance"];
            }

            // if changing employee
            if (userID != null)
            {
                eqMaintenance.employee_id = userID;
                eqMaintenance.AspNetUser = db.AspNetUsers.Find(userID);
            }
            //  if changing equipment
            if (equipmentID != null)
            {
                eqMaintenance.equipment_id = Convert.ToInt32(equipmentID);
                eqMaintenance.Equipment = db.Equipments.Find(equipmentID);
            }
            Session["EquipmentMaintenance"] = eqMaintenance;
            return View(eqMaintenance);
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
                Session["EquipmentMaintenance"] = null;
                return RedirectToAction("Index");
            }

            equipment_Maintenance.AspNetUser = db.AspNetUsers.Find(equipment_Maintenance.employee_id);
            equipment_Maintenance.Equipment = db.Equipments.Find(equipment_Maintenance.equipment_id);
            return View(equipment_Maintenance);
        }

        // GET: EquipmentMaintenance/Edit/5
        public ActionResult Edit(int? id, string userID, int? equipmentID)
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
            // load info in session
            if (Session["EquipmentMaintenance"] != null)
            {
                equipment_Maintenance = (Equipment_Maintenance)Session["EquipmentMaintenance"];

                // if changing employee
                if (userID != null)
                {
                    equipment_Maintenance.employee_id = userID;
                    equipment_Maintenance.AspNetUser = db.AspNetUsers.Find(userID);
                }
                //  if changing equipment
                if (equipmentID != null)
                {
                    equipment_Maintenance.equipment_id = Convert.ToInt32(equipmentID);
                    equipment_Maintenance.Equipment = db.Equipments.Find(equipmentID);
                }
            }
            else
            {
                equipment_Maintenance.AspNetUser = db.AspNetUsers.Find(equipment_Maintenance.employee_id);
                equipment_Maintenance.Equipment = db.Equipments.Find(equipment_Maintenance.equipment_id);
            }
            
            Session["EquipmentMaintenance"] = equipment_Maintenance;
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
                Session["EquipmentMaintenance"] = null;
                return RedirectToAction("Index");
            }
            equipment_Maintenance.AspNetUser = db.AspNetUsers.Find(equipment_Maintenance.employee_id);
            equipment_Maintenance.Equipment = db.Equipments.Find(equipment_Maintenance.equipment_id);
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
