using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using System.Net;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Distribution
{
    [Authorize(Roles = "SuperUser, Manager, Distribution")]
    public class DeliveryQueController : Controller
    {
        private BusinessEntities db;

        public DeliveryQueController()
        {
            db = new BusinessEntities();
            ViewBag.ViewHeaderPartial = "_Distribution";
            ViewBag.ItemTitle = "Delivery Queue";
        }

        //// GET: DeliveryQue
        //public ActionResult Index()
        //{
        //    var deliveryQ = db.Delivery_Lineitem.Where(dl => dl.delivery_schedule_id == null).OrderBy(dl => dl.Invoice_Lineitem.invoice_id).ToList();
        //    foreach (var i in deliveryQ) { i.CalculateTotal(i.product_inventory_id); }
        //    return View(deliveryQ);
        //}

        // GET: DeliveryQue
        public PartialViewResult PartialIndex()
        {
            var deliveryQ = db.Delivery_Lineitem.Where(dl => dl.delivery_schedule_id == null).OrderBy(dl => dl.Invoice_Lineitem.invoice_id).ToList();
            foreach (var i in deliveryQ) { i.CalculateTotal(i.product_inventory_id); }
            return PartialView(deliveryQ);
        }

        //// GET DeliveryQue/SelectItem
        //public PartialViewResult SelectItem()
        //{
        //    var invoiceLI = db.Invoice_Lineitem.ToList();
        //    var deliveryQ = new List<Invoice_Lineitem>();
        //    // check quantity between invoice Li and delivery Li
        //    foreach (var item in invoiceLI)
        //    {
        //        var deliveryLI = db.Delivery_Lineitem.Where(i => i.invoice_lineitem_id == item.Id).ToList();
        //        int quantity = 0;
        //        if (deliveryLI.Count() > 0) { foreach (var i in deliveryLI) { quantity += i.lineitem_unit_quantity; } }
        //        if (item.lineitem_unit_quantity > quantity)
        //        {
        //            item.lineitem_unit_quantity -= quantity;
        //            item.CalculateTotal(item.product_inventory_id);
        //            deliveryQ.Add(item);
        //        }
        //    }
        //    return PartialView(deliveryQ);
        //}

        //// POST DeliveryQue/SelectLineitem
        //public ActionResult SelectLineitem(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var deliveryLi = new Delivery_Lineitem();
        //    var li = db.Invoice_Lineitem.Find(id);
        //    deliveryLi.invoice_lineitem_id = li.Id;
        //    deliveryLi.lineitem_unit_quantity = li.lineitem_unit_quantity;
        //    deliveryLi.CalculateTotal(li.product_inventory_id);

        //    db.Delivery_Lineitem.Add(deliveryLi);
        //    var result = db.SaveChanges();
        //    if (result == 0)
        //    {
        //        ViewBag.ErrorMessage = "Unable to create the delivery queue. Please try again.";
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}