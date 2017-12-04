using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    [Authorize(Roles = "SuperUser, Manager, Supervisor")]
    public class SelectEquipmentController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectProduct
        public ActionResult Index(string actionName, string controllerName, int? optionalID)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.OptionalID = optionalID;
            return View(db.Equipments.ToList());
        }

        // POST: SelectProduct/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch, string actionName, string controllerName, int? optionalID)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.OptionalID = optionalID;

            if (inputForUserSearch != "")
            {
                List<Equipment> equipments;
                switch (SearchBy)
                {
                    case "Name":
                        equipments = db.Equipments.Where(u => u.equipment_name.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Description":
                        equipments = db.Equipments.Where(u => u.equipment_short_description.Contains(inputForUserSearch) || u.equipment_long_description.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        equipments = new List<Equipment>();
                        break;
                }
                if (equipments.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(equipments);
                }
                else
                {
                    ViewBag.ErrorString = "Equipments Not Found";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorString = "Please enter the search keyword";
                return View();
            }
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