using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Paycheck_Partial_Metadata))]
    public partial class Paycheck
    {
        public enum PaycheckMode
        {
            Check,
            Deposit           
        }
        public PaycheckMode ModeOfPaycheck
        {
            get
            {
                this.ModeOfPaycheck = (PaycheckMode)Enum.Parse(typeof(PaycheckMode) , payment_type);
                return this.ModeOfPaycheck;
            }
            set
            {
                this.ModeOfPaycheck = value;
                this.payment_type = this.ModeOfPaycheck.ToString();
            }
        }
    }

    public class Paycheck_Partial_Metadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Paycheck Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime paycheck_date { get; set; }

        [Required]
        [Display(Name = "Payroll Number")]
        public int payroll_id { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string payment_type { get; set; }

        [Required]
        [Display(Name = "Check Number")]
        public string check_number { get; set; }

        [Required]
        [Display(Name = "Deposit Number")]
        public string direct_deposit_number { get; set; }

        [Required]
        [Display(Name = "Paycheck Amount")]
        public decimal payment_amount { get; set; }
    }
}