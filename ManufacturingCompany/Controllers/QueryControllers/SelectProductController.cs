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
    public class SelectProductController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectProduct
        public ActionResult Index(string actionName, string controllerName)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            return View(db.Products.ToList());
        }

        // POST: SelectProduct/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch, string actionName, string controllerName)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;

            if (inputForUserSearch != "")
            {
                List<Product> products;
                switch (SearchBy)
                {
                    case "Name":
                        products = db.Products.Where(u => u.product_name.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Description":
                        products = db.Products.Where(u => u.product_short_description.Contains(inputForUserSearch) || u.product_long_description.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        products = new List<Product>();
                        break;
                }
                if (products.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(products);
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
