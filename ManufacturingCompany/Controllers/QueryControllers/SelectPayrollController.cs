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
    [Authorize]
    public class SelectPayrollController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectPayroll
        //public ActionResult Index(string userID, string actionName, int? paycheckID)
        public ActionResult Index(string actionName, string controllerName, int? paycheckID)
        {
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            ViewBag.PaycheckID = paycheckID;

            var payrolls = (from pr in db.Payrolls
                            where /*pr.employee_id == userID &&*/
                            !((from pc in db.Paychecks select pc.payroll_id).Contains(pr.Id))
                            select pr).OrderByDescending(p => p.period_end).ToList();

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
