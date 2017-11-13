using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Finance"))
            {
                return RedirectToAction("Index", "Finance");
            }
            else if (User.IsInRole("Production"))
            {
                return RedirectToAction("Index", "Production");
            }
            else if (User.IsInRole("Distribution"))
            {
                return RedirectToAction("Index", "Distribution");
            }
            else if (User.IsInRole("Administration"))
            {
                return RedirectToAction("Index", "Administration");
            }
            else
            {
                return View();
            }            
        }
    }
}