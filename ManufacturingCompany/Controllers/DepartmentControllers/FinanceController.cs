using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers
{
    public class FinanceController : Controller
    {
        public FinanceController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
        }
        // GET: Finance
        public ActionResult Index()
        {
            return View();
        }
    }
}