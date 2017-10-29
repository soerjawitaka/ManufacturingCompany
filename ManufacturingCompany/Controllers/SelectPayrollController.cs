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
    public class SelectPayrollController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectPayroll
        public ActionResult Index(string userID)
        {
            var payrolls = db.Payrolls.Where(p => p.employee_id == userID).OrderByDescending(p => p.period_end).ToList();
            return View(payrolls);
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
