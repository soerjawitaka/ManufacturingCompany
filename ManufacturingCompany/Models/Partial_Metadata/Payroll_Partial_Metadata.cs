using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Payroll_Partial_Metadata))]
    public partial class Payroll
    {
        BusinessEntities db = new BusinessEntities();
        public List<Timesheet> assignedTimesheet { get; private set; }
        private const decimal fedTax = .07m;
        private const decimal stTax = .014m;
        private const int biweekly = 26;
        private const int monthly = 12;
        private const int twiceAmonth = 24;

        private void SetTimesheets()
        {
            this.assignedTimesheet = db.Timesheets.Where(t => t.timesheet_date >= this.period_begin && t.timesheet_date <= this.period_end && t.is_in_payroll == false && t.employee_id == this.employee_id).ToList();
        }

        public int InitializeTimesheets()
        {
            this.assignedTimesheet = db.Timesheets.Where(t => t.timesheet_date >= this.period_begin && t.timesheet_date <= this.period_end && t.employee_id == this.employee_id).ToList();
            foreach (var i in this.assignedTimesheet)
            {
                var timesheet = db.Timesheets.Find(i.Id);
                timesheet.is_in_payroll = false;
                db.Entry(timesheet).State = System.Data.Entity.EntityState.Modified;                
            }
            return db.SaveChanges();
        }

        public int ChangeTimesheetsStatus()
        {
            this.assignedTimesheet = db.Timesheets.Where(t => t.timesheet_date >= this.period_begin && t.timesheet_date <= this.period_end && t.employee_id == this.employee_id).ToList();
            foreach (var i in this.assignedTimesheet)
            {
                var timesheet = db.Timesheets.Find(i.Id);
                timesheet.is_in_payroll = true;
                db.Entry(timesheet).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }

        public bool ThereAreAvailableTimesheets()
        {
            var availableTimesheets = db.Timesheets.Where(t => t.timesheet_date >= this.period_begin && t.timesheet_date <= this.period_end && t.is_in_payroll == false && t.employee_id == this.employee_id).ToList();
            if (availableTimesheets.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetTotalHours()
        {
            if (db.AspNetUsers.Find(this.employee_id).ModeOfWage == ((int)ApplicationUser.WageMode.Hourly))
            {
                foreach (var i in this.assignedTimesheet)
                {
                    this.total_hours += Convert.ToDouble(i.GetTotalHours());
                }
            }            
        }

        private void SetSubtotal()
        {
            if (db.AspNetUsers.Find(this.employee_id).ModeOfWage == ((int)ApplicationUser.WageMode.Hourly))
            {
                this.subtotal = Convert.ToDecimal(this.total_hours) * db.AspNetUsers.Find(this.employee_id).WageAmount;
            }
            else
            {
                this.subtotal = db.AspNetUsers.Find(this.employee_id).WageAmount / biweekly;
            }
        }

        private void SetFederalTax()
        {
            this.federal_tax_witholding = this.subtotal * fedTax;
        }

        private void SetStateTax()
        {
            this.state_tax_witholding = this.subtotal * stTax;
        }

        private void SetGrandTotal()
        {
            this.grand_total = this.subtotal - this.federal_tax_witholding - this.state_tax_witholding;
        }

        public void CalculatePayroll()
        {
            this.total_hours = 0.0D;
            SetTimesheets();
            SetTotalHours();
            SetSubtotal();
            SetFederalTax();
            SetStateTax();
            SetGrandTotal();
        }
    }

    public class Payroll_Partial_Metadata
    {
        [Required]
        [Display(Name = "Period Begin")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime period_begin { get; set; }

        [Required]
        [Display(Name = "Period End")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime period_end { get; set; }

        [Display(Name = "Total Hours")]
        public decimal total_hours { get; set; }

        [Display(Name = "Subtotal")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal subtotal { get; set; }

        [Display(Name = "Federal Tax")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal federal_tax_witholding { get; set; }

        [Display(Name = "State Tax")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal state_tax_witholding { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal grand_total { get; set; }
    }
}