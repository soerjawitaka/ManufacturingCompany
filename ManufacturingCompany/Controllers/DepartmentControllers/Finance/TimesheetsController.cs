using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Finance
{
    public class TimesheetsController : Controller
    {
        private BusinessEntities db = new BusinessEntities();

        // GET: Timesheets
        public ActionResult Index()
        {
            var timesheets = db.Timesheets.Include(t => t.AspNetUser);
            return View(timesheets.ToList());
        }

        // GET: Timesheets/SelectUser
        public ActionResult SelectUser()
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            return View();
        }

        // POST: Timesheets/SelectUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectUser(string SearchBy, string inputForUserSearch)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Email");
            searchBy.Add("Username");
            searchBy.Add("First Name");
            searchBy.Add("Last Name");
            ViewBag.SearchBy = new SelectList(searchBy);

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

        // GET: Timesheets/Create
        public ActionResult Create(string userID)
        {
            var timesheet = new Timesheet();
            timesheet.employee_id = userID;
            ViewBag.employee_username = db.AspNetUsers.Find(userID).UserName;
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

            ViewBag.employee_id = new SelectList(db.AspNetUsers, "Id", "Email", timesheet.employee_id);
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
