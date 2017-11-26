using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers.DepartmentControllers
{
    public class DistributionController : Controller
    {
        public DistributionController()
        {
            ViewBag.ViewHeaderPartial = "_Distribution";
        }

        // GET: Distribution
        public ActionResult Index()
        {
            return View();
        }
    }
}