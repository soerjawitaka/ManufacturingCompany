using ManufacturingCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers
{
    public class SelectUserController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: SelectUser
        public ActionResult Index(string actionName, string controllerName)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            return View();
        }

        // POST: SelectUser/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch, string actionName, string controllerName)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;

            if (inputForUserSearch != "")
            {
                List<AspNetUser> users;
                switch (SearchBy)
                {
                    case "Email":
                        users = db.AspNetUsers.Where(u => u.Email.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Username":
                        users = db.AspNetUsers.Where(u => u.UserName.Contains(inputForUserSearch)).ToList();
                        break;
                    case "First Name":
                        users = db.AspNetUsers.Where(u => u.FirstName.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Last Name":
                        users = db.AspNetUsers.Where(u => u.LastName.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        users = new List<AspNetUser>();
                        break;
                }
                if (users.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(users);
                }
                else
                {
                    ViewBag.ErrorString = "User Not Found";
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