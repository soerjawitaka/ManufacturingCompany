using ManufacturingCompany.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ManufacturingCompany.Controllers
{
    [Authorize]
    public class SelectUserController : Controller
    {
        private BusinessEntities db = new BusinessEntities();
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: SelectUser
        public ActionResult Index(string actionName, string controllerName, int? optionalID, string optionalDirection)
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
            if (optionalID != null)
            {
                ViewBag.OptionalID = optionalID;
            }
            if (optionalDirection != null)
            {
                ViewBag.OptionalDirection = optionalDirection;
            }
            return View();
        }

        // POST: SelectUser/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string SearchBy, string inputForUserSearch, string actionName, string controllerName, int? optionalID, string optionalDirection)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.SearchBy = new SelectList(searchBy);
            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
            if (optionalID != null)
            {
                ViewBag.OptionalID = optionalID;
            }
            if (optionalDirection != null)
            {
                ViewBag.OptionalDirection = optionalDirection;
            }

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
                    var duplicates = new List<AspNetUser>();
                    foreach (var u in users) { duplicates.Add(u); }
                    foreach (var i in duplicates)
                    {
                        // remove super users
                        if (await UserManager.IsInRoleAsync(i.Id, "SuperUser"))
                        {
                            users.Remove(i);
                        }
                        // remove users above own hierarchy
                        if (!User.IsInRole("Manager") && await UserManager.IsInRoleAsync(i.Id, "Manager"))
                        {
                            users.Remove(i);
                        }
                        if (!User.IsInRole("Manager") && !User.IsInRole("Supervisor") && await UserManager.IsInRoleAsync(i.Id, "Supervisor"))
                        {
                            users.Remove(i);
                        }
                    }
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