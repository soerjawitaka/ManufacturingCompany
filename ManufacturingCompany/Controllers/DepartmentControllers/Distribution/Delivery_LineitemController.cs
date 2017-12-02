using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Distribution
{
    public class Delivery_LineitemController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        //// GET: Delivery_Lineitem
        //public ActionResult Index()
        //{
        //    var delivery_Lineitem = db.Delivery_Lineitem.Include(d => d.Delivery_Schedule).Include(d => d.Invoice_Lineitem).Include(d => d.Product_Inventory);
        //    return View(delivery_Lineitem.ToList());
        //}

        // GET: Delivery_Lineitem/PartialIndex
        public PartialViewResult PartialIndex(int? deliveryScheduleID)
        {
            if (deliveryScheduleID == null)
            {
                return PartialView("Error");
            }
            var delivery_Lineitem = db.Delivery_Lineitem.Include(d => d.Delivery_Schedule)
                                                        .Include(d => d.Invoice_Lineitem)
                                                        .Include(d => d.Product_Inventory)
                                                        .Where(dl => dl.delivery_schedule_id == deliveryScheduleID)
                                                        .OrderBy(dl => dl.Invoice_Lineitem.invoice_id);
            foreach (var i in delivery_Lineitem) { i.CalculateTotal(i.product_inventory_id); }
            ViewBag.DeliveryScheduleID = deliveryScheduleID;
            return PartialView(delivery_Lineitem.ToList());
        }

        // GET: Delivery_Lineitem/SelectItem
        public ActionResult SelectItem(int? deliveryScheduleID)
        {
            if (deliveryScheduleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deliveryQ = db.Delivery_Lineitem.Where(dl => dl.delivery_schedule_id == null).OrderBy(dl => dl.Invoice_Lineitem.invoice_id).ToList();
            foreach (var i in deliveryQ) { i.CalculateTotal(i.product_inventory_id); }
            ViewBag.DeliveryScheduleID = deliveryScheduleID;
            ViewBag.ErrorMessage = "";
            return View(deliveryQ);
        }

        // POST: Delivery_Lineitem/SelectItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectItem(int? id, int? deliveryScheduleID, int? Quantity)
        {
            var newDeliveryLI = db.Delivery_Lineitem.Find(id);

            if (newDeliveryLI != null)
            {
                if (Quantity != null && Quantity > newDeliveryLI.lineitem_unit_quantity)
                {
                    ViewBag.ErrorMessage = "Please enter the quantity as equal or less than linvoice lineitem.";
                }
                else if (deliveryScheduleID != null && Quantity <= newDeliveryLI.lineitem_unit_quantity)
                {
                    newDeliveryLI.delivery_schedule_id = Convert.ToInt32(deliveryScheduleID);

                    // check quantity
                    if (Quantity < newDeliveryLI.lineitem_unit_quantity)
                    {
                        var secondDeliveryLI = new Delivery_Lineitem();
                        secondDeliveryLI.product_inventory_id = newDeliveryLI.product_inventory_id;
                        secondDeliveryLI.lineitem_unit_quantity = newDeliveryLI.lineitem_unit_quantity - Convert.ToInt32(Quantity);
                        secondDeliveryLI.invoice_lineitem_id = newDeliveryLI.invoice_lineitem_id;
                        db.Delivery_Lineitem.Add(secondDeliveryLI);
                    }
                    newDeliveryLI.lineitem_unit_quantity = Convert.ToInt32(Quantity);
                    db.Entry(newDeliveryLI).State = EntityState.Modified;
                    var result = db.SaveChanges();
                    if (result == 0)
                    {
                        return View("Error");
                    }
                    return RedirectToAction("Edit", "Delivery_Schedule", new { id = deliveryScheduleID });
                }
                else
                {
                    ViewBag.ErrorMessage = "Something went wrong. Please try again.";
                }
            }
            
            var deliveryQ = db.Delivery_Lineitem.Where(dl => dl.delivery_schedule_id == null).OrderBy(dl => dl.Invoice_Lineitem.invoice_id).ToList();
            foreach (var i in deliveryQ) { i.CalculateTotal(i.product_inventory_id); }
            ViewBag.DeliveryScheduleID = deliveryScheduleID;
            return View(deliveryQ);
        }

        // GET: Delivery_Lineitem/RemoveItem
        public ActionResult RemoveItem(int? id, int? deliveryScheduleID)
        {
            if (id == null || deliveryScheduleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deliveryLI = db.Delivery_Lineitem.Find(id);
            deliveryLI.delivery_schedule_id = null;
            db.Entry(deliveryLI).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", "Delivery_Schedule", new { id = deliveryScheduleID });
        }

        //// GET: Delivery_Lineitem/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Delivery_Lineitem delivery_Lineitem = db.Delivery_Lineitem.Find(id);
        //    if (delivery_Lineitem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(delivery_Lineitem);
        //}

        //// GET: Delivery_Lineitem/Create
        //public ActionResult Create()
        //{
        //    ViewBag.delivery_schedule_id = new SelectList(db.Delivery_Schedule, "Id", "warehouse_employee_id");
        //    ViewBag.invoice_lineitem_id = new SelectList(db.Invoice_Lineitem, "Id", "Id");
        //    ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id");
        //    return View();
        //}

        //// POST: Delivery_Lineitem/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,delivery_schedule_id,invoice_lineitem_id")] Delivery_Lineitem delivery_Lineitem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Delivery_Lineitem.Add(delivery_Lineitem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.delivery_schedule_id = new SelectList(db.Delivery_Schedule, "Id", "warehouse_employee_id", delivery_Lineitem.delivery_schedule_id);
        //    ViewBag.invoice_lineitem_id = new SelectList(db.Invoice_Lineitem, "Id", "Id", delivery_Lineitem.invoice_lineitem_id);
        //    ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", delivery_Lineitem.product_inventory_id);
        //    return View(delivery_Lineitem);
        //}

        //// GET: Delivery_Lineitem/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Delivery_Lineitem delivery_Lineitem = db.Delivery_Lineitem.Find(id);
        //    if (delivery_Lineitem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.delivery_schedule_id = new SelectList(db.Delivery_Schedule, "Id", "warehouse_employee_id", delivery_Lineitem.delivery_schedule_id);
        //    ViewBag.invoice_lineitem_id = new SelectList(db.Invoice_Lineitem, "Id", "Id", delivery_Lineitem.invoice_lineitem_id);
        //    ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", delivery_Lineitem.product_inventory_id);
        //    return View(delivery_Lineitem);
        //}

        //// POST: Delivery_Lineitem/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,product_inventory_id,lineitem_unit_quantity,delivery_schedule_id,invoice_lineitem_id")] Delivery_Lineitem delivery_Lineitem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(delivery_Lineitem).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.delivery_schedule_id = new SelectList(db.Delivery_Schedule, "Id", "warehouse_employee_id", delivery_Lineitem.delivery_schedule_id);
        //    ViewBag.invoice_lineitem_id = new SelectList(db.Invoice_Lineitem, "Id", "Id", delivery_Lineitem.invoice_lineitem_id);
        //    ViewBag.product_inventory_id = new SelectList(db.Product_Inventory, "Id", "Id", delivery_Lineitem.product_inventory_id);
        //    return View(delivery_Lineitem);
        //}

        //// GET: Delivery_Lineitem/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Delivery_Lineitem delivery_Lineitem = db.Delivery_Lineitem.Find(id);
        //    if (delivery_Lineitem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(delivery_Lineitem);
        //}

        //// POST: Delivery_Lineitem/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Delivery_Lineitem delivery_Lineitem = db.Delivery_Lineitem.Find(id);
        //    db.Delivery_Lineitem.Remove(delivery_Lineitem);
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
