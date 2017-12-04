using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    [Authorize(Roles = "SuperUser, Manager, Supervisor")]
    public class SelectMaterialController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectMaterial
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
            return View(db.Materials.ToList());
        }

        // POST: SelectMaterial/Index
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
                List<Material> materials;
                switch (SearchBy)
                {
                    case "Name":
                        materials = db.Materials.Where(u => u.material_name.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Description":
                        materials = db.Materials.Where(u => u.material_description.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        materials = new List<Material>();
                        break;
                }
                if (materials.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(materials);
                }
                else
                {
                    ViewBag.ErrorString = "Products Not Found";
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