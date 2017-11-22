using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    public class SelectProductInventoryController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectProductInventory
        public ActionResult Index(string actionName, string controllerName, int? optionalID, int? lineitemID)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.OptionalID = optionalID;
            ViewBag.LineitemID = lineitemID;
            return View(db.Product_Inventory.ToList());
        }

        // POST: SelectProductInventory/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch, string actionName, string controllerName, int? optionalID, int? lineitemID)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.OptionalID = optionalID;
            ViewBag.LineitemID = lineitemID;

            if (inputForUserSearch != "")
            {
                List<Product_Inventory> productInventories;
                switch (SearchBy)
                {
                    case "Name":
                        productInventories = db.Product_Inventory.Where(u => u.Product.product_name.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Description":
                        productInventories = db.Product_Inventory.Where(u => u.Product.product_short_description.Contains(inputForUserSearch) || u.Product.product_long_description.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        productInventories = new List<Product_Inventory>();
                        break;
                }
                if (productInventories.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(productInventories);
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