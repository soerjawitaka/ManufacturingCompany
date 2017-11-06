using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;
using System.Threading.Tasks;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Finance
{
    public class TimesheetsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        public TimesheetsController()
        {
            ViewBag.ViewHeaderPartial = "_Finance";
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.SearchBy = new SelectList(searchBy);
        }

        // GET: Timesheets
        public ActionResult Index()
        {
            var timesheets = db.Timesheets.Include(t => t.AspNetUser);
            ViewBag.ErrorString = "";
            return View(timesheets.Where(t => t.is_in_payroll == false).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch)
        {
            if (inputForUserSearch != "")
            {
                List<Timesheet> timesheets;
                switch (SearchBy)
                {
                    case "Email":
                        timesheets = db.Timesheets.Where(u => u.AspNetUser.Email.Contains(inputForUserSearch) && u.is_in_payroll == false).ToList();
                        break;
                    case "Username":
                        timesheets = db.Timesheets.Where(u => u.AspNetUser.UserName.Contains(inputForUserSearch) && u.is_in_payroll == false).ToList();
                        break;
                    case "First Name":
                        timesheets = db.Timesheets.Where(u => u.AspNetUser.FirstName.Contains(inputForUserSearch) && u.is_in_payroll == false).ToList();
                        break;
                    case "Last Name":
                        timesheets = db.Timesheets.Where(u => u.AspNetUser.LastName.Contains(inputForUserSearch) && u.is_in_payroll == false).ToList();
                        break;
                    default:
                        timesheets = new List<Timesheet>();
                        break;
                }
                if (timesheets.Count > 0 || timesheets == null)
                {
                    ViewBag.ErrorString = "";
                    return View(timesheets);
                }
                else
                {
                    ViewBag.ErrorString = "No timesheets found with the specified criteria";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorString = "Please enter the search keyword";
                return View(db.Timesheets.Include(t => t.AspNetUser).Where(t => t.is_in_payroll == false).ToList());
            }
        }

        // GET: Timesheets/Create
        public ActionResult Create(string userID)
        {
            var timesheet = new Timesheet();
            timesheet.employee_id = userID;
            ViewBag.employee_username = db.AspNetUsers.Find(userID).UserName;
            ViewBag.ActionTitle = "Create ";
            return View(timesheet);
        }

        // POST: Timesheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,employee_id,punch_in_time,punch_out_time,timesheet_date")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Timesheets.Add(timesheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employee_username = db.AspNetUsers.Find(timesheet.employee_id).UserName;
            ViewBag.ActionTitle = "Create ";
            return View(timesheet);
        }

        // GET: Timesheets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
            ViewBag.ActionTitle = "Edit ";
            return View(timesheet);
        }

        // POST: Timesheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,employee_id,punch_in_time,punch_out_time,timesheet_date")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
            ViewBag.ActionTitle = "Edit ";
            return View(timesheet);
        }

        // GET: Timesheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.Timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActionTitle = "Delete ";
            return View(timesheet);
        }

        // POST: Timesheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.Timesheets.Find(id);
            db.Timesheets.Remove(timesheet);
            db.SaveChanges();
            return RedirectToAction("Index");
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
