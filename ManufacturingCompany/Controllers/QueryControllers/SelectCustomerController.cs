using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using ManufacturingCompany.Classes;

namespace ManufacturingCompany.Controllers
{
    public class SelectCustomerController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectUser
        public ActionResult Index(string actionName, string controllerName, int? optionalID)
        {
            ViewBag.ErrorString = "";
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            if (optionalID != null)
            {
                ViewBag.OptionalID = optionalID;
            }
            return View();
        }

        // POST: SelectUser/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string inputForUserSearch, string actionName, string controllerName, int? optionalID)
        {
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            if (optionalID != null)
            {
                ViewBag.OptionalID = optionalID;
            }

            if (inputForUserSearch != "")
            {
                var customers = db.Customers.Where(u => u.customer_company_name.Contains(inputForUserSearch)).ToList();
                
                if (customers.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(customers);
                }
                else
                {
                    ViewBag.ErrorString = "Customer Not Found";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorString = "Please enter the search keyword";
                return View();
            }
        }
    }
}