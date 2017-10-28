using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ManufacturingCompany.Models;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;

namespace ManufacturingCompany.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private BusinessEntities businessDB = new BusinessEntities();
        private ApplicationRoleManager _roleManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                LoggedInUser = businessDB.AspNetUsers.Find(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/ChangeProfile
        public ActionResult ChangeProfile()
        {
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            return View((ApplicationUser)(UserManager.FindById(User.Identity.GetUserId())));
        }

        //
        // POST: /Manage/ChangeProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile([Bind(Include = "UserName, Email, FirstName, LastName, Address, City, State, ZipCode, PhoneNumber")]ApplicationUser user)
        {
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var updatedUser = db.Users.Find(User.Identity.GetUserId());
            updatedUser.UserName = user.UserName;
            updatedUser.Email = user.Email;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Address = user.Address;
            updatedUser.City = user.City;
            updatedUser.State = user.State;
            updatedUser.ZipCode = user.ZipCode;
            updatedUser.PhoneNumber = user.PhoneNumber;

            db.Entry(updatedUser).State = EntityState.Modified;
            int result = db.SaveChanges();
            if (result == 0)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Manage/ChangeProfile
        public ActionResult ChangeEmployeeProfile(string userID)
        {
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            return View((ApplicationUser)(UserManager.FindById(userID)));
        }

        //
        // POST: /Manage/ChangeProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeEmployeeProfile([Bind(Include = "Id, UserName, Email, FirstName, LastName, Address, City, State, ZipCode, PhoneNumber, ModeOfWage, WageAmount")]ApplicationUser user)
        {
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var updatedUser = db.Users.Find(user.Id);
            updatedUser.UserName = user.UserName;
            updatedUser.Email = user.Email;
            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Address = user.Address;
            updatedUser.City = user.City;
            updatedUser.State = user.State;
            updatedUser.ZipCode = user.ZipCode;
            updatedUser.PhoneNumber = user.PhoneNumber;
            updatedUser.ModeOfWage = user.ModeOfWage;
            updatedUser.WageAmount = user.WageAmount;

            db.Entry(updatedUser).State = EntityState.Modified;
            int result = db.SaveChanges();
            if (result == 0)
            {
                return View(user);
            }
            return RedirectToAction("ManageEmployee", new { userID = updatedUser.Id });
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        #region RoleManager
        // Role manager****************************************************************************
        public ActionResult Roles()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles.Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name }));
        }

        public ActionResult CreateRole()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole([Bind(Include = "Name")] RoleViewModel model)
        {
            // validation for if role exist
            if (RoleManager.RoleExists(model.Name))
            {
                ViewBag.ErrorMessage = "Role name already exist";
                return View();
            }

            var newRole = new IdentityRole(model.Name);
            var result = RoleManager.Create(newRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return View(model);
        }

        public ActionResult EditRole(string id)
        {
            var roleViewModel = new RoleViewModel();
            var roleModel = RoleManager.Roles.Where(r => r.Id == id).SingleOrDefault();
            roleViewModel.Id = roleModel.Id;
            roleViewModel.Name = roleModel.Name;
            ViewBag.ErrorMessage = "";
            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "Id, Name")]RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = RoleManager.Update(new IdentityRole() { Id = model.Id, Name = model.Name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
            }

            ViewBag.ErrorMessage = "Unable to edit Role. Please try again.";
            return View(model);
        }
        #endregion

        #region EmployeeManager
        [Authorize(Roles = "SuperUser, Manager")]
        public ActionResult ManageEmployee(string userID)
        {
            var employee = UserManager.FindById(userID);
            var employeeRoles = UserManager.GetRoles(userID);
            var allRoles = RoleManager.Roles.ToList();

            List<string> availableRoles = new List<string>();
            List<string> allRolesText = new List<string>();
            foreach (var i in allRoles) { availableRoles.Add(i.Name); allRolesText.Add(i.Name); }
            // filter for available roles
            foreach (var er in employeeRoles)
            {
                foreach (var ar in allRolesText)
                {
                    if (er.Equals(ar))
                    {
                        availableRoles.Remove(ar);
                    }
                }
            }
            //var model = new ManageEmployeeModel();
            //model.Employee = UserManager.FindById(userID);

            ViewBag.EmployeeRoles = (List<string>)employeeRoles;
            ViewBag.StateList = new SelectList(XmlHelper.GetStates(Server, Url), "Value", "Text");
            ViewBag.ErrorMessage = "";
            return View(employee);
        }

        [Authorize(Roles = "SuperUser, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManageEmployee(ManageEmployeeModel model)
        {
            // updating profile
            var resultUser = await UserManager.UpdateAsync(model.Employee);
            if (resultUser.Succeeded)
            {
                // assigning user to role here
                var resultRole = await this.UserManager.AddToRoleAsync(model.UserID, model.RoleToBeAssigned);
                if (!resultRole.Succeeded)
                {
                    ViewBag.ErrorMessage = "Cannot assign Role";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Cannot update employee's profile";
            }

            return View(model);
        }

        // GET: /Manage/ChangeEmployeeRoles
        [Authorize(Roles = "SuperUser, Manager")]
        public ActionResult ChangeEmployeeRoles(string userID)
        {
            ViewBag.Username = UserManager.FindById(userID).UserName;
            ViewBag.ErrorMessage = "";

            var employeeRoles = UserManager.GetRoles(userID);
            var allRoles = RoleManager.Roles.ToList();
            List<string> availableRoles = new List<string>();
            List<string> allRolesText = new List<string>();
            foreach (var i in allRoles) { availableRoles.Add(i.Name); allRolesText.Add(i.Name); }
            // filter for available roles
            foreach (var er in employeeRoles)
            {
                foreach (var ar in allRolesText)
                {
                    if (er.Equals(ar))
                    {
                        availableRoles.Remove(ar);
                    }
                }
            }
            ViewBag.EmployeeRoles = (List<string>)employeeRoles;
            ViewBag.AvailableRoles = availableRoles;
            var roles = new EmployeeRoleViewModel() { UserID = userID };
            return View(roles);
        }

        [Authorize(Roles = "SuperUser, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeEmployeeRoles(EmployeeRoleViewModel model)
        {
            var result = UserManager.AddToRole(model.UserID, model.Role);
            if (result.Succeeded)
            {
                return RedirectToAction("ManageEmployee", new { userID = model.UserID });
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to assign role. Please try again.";
                return View(model);
            }
        }

        public ActionResult RemoveUserRole(string userID, string role)
        {
            var result = UserManager.RemoveFromRole(userID, role);
            if (result.Succeeded)
            {
                return RedirectToAction("ManageEmployee", new { userID = userID });
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to remove role. Please try again.";
                return RedirectToAction("ChangeEmployeeRoles", new { userID = userID });
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}