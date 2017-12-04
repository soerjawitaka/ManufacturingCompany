using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers
{
    [Authorize(Roles ="SuperUser, Manager, Supervisor")]
    public class PartialSelectInvoiceController : Controller
    {
        private BusinessEntities db;

        public PartialSelectInvoiceController()
        {
            db = new BusinessEntities();
        }

        // GET: PartialSelectInvoice
        public PartialViewResult Index(string actionName, string controllerName, int? optionalID, string contentID)
        {
            // setting for query options
            List<string> searchBy = new List<string>();
            searchBy.Add("Invoice ID");
            searchBy.Add("Customer Name");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.OptionalID = optionalID;
            ViewBag.ControllerName = controllerName;
            ViewBag.ContentID = contentID;

            var invoices = db.Invoices.OrderByDescending(i => i.Id).Take(10).ToList();
            foreach (var i in invoices)
            {
                i.CalculateTotal();
            }
            return PartialView(invoices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Index(string actionName, string controllerName, string SearchBy, string inputForUserSearch, int? optionalID, string contentID)
        {
            // setting for query options
            List<string> searchBy = new List<string>();
            searchBy.Add("Invoice ID");
            searchBy.Add("Customer Name");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.OptionalID = optionalID;
            ViewBag.ContentID = contentID;

            List<Invoice> invoices = null;

            if (inputForUserSearch != null)
            {
                switch (SearchBy)
                {
                    case "Invoice ID":
                        invoices = db.Invoices.Where(i => i.Id.ToString().Contains(inputForUserSearch)).OrderByDescending(i => i.Id).Take(10).ToList();
                        break;
                    case "Customer Name":
                        invoices = db.Invoices.Where(i => i.Customer.customer_company_name.Contains(inputForUserSearch)).OrderByDescending(i => i.Id).Take(10).ToList();
                        break;
                    default:
                        invoices = new List<Invoice>();
                        break;
                }
            }
            string errorString = "";
            if (invoices != null)
            {
                foreach (var i in invoices)
                {
                    i.CalculateTotal();
                }
            }
            else
            {
                errorString = "Invoices not found.";
            }
            ViewBag.ErrorString = errorString;
            return PartialView(invoices);
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