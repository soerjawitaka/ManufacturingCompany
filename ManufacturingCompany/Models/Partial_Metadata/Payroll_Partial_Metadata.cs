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
        private void SetTotalHours()
        {
            foreach (var i in this.Timesheets)
            {
                this.total_hours += i.GetTotalHours();
            }
        }

        private void SetSubtotal()
        {
            //this.subtotal = this.total_hours * this.AspNetUser.
        }

        private void SetFederalTax()
        {
            //this.federal_tax_witholding = this.subtotal * 
        }

        private void SetStateTax()
        {
            //this.state_tax_witholding = this.subtotal *
        }

        private void SetGrandTotal()
        {
            this.grand_total = this.subtotal - this.federal_tax_witholding - this.state_tax_witholding;
        }

        public void CalculatePayroll()
        {
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