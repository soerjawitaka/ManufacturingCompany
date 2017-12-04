using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers
{
    [Authorize(Roles = "SuperUser, Manager, Production")]
    public class ProductionController : Controller
    {
        public ProductionController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
        }

        // GET: Production
        public ActionResult Index()
        {
            return View();
        }
    }
}