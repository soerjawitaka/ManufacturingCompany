using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Paycheck_Partial_Metadata))]
    public partial class Paycheck { }

    public class Paycheck_Partial_Metadata
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Paycheck Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime paycheck_date { get; set; }

        [Required]
        [Display(Name = "Payroll Number")]
        public int payroll_id { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string payment_type { get; set; }
        
        [Display(Name = "Check Number")]
        public string check_number { get; set; }
        
        [Display(Name = "Deposit Number")]
        public string direct_deposit_number { get; set; }

        [Required]
        [Display(Name = "Paycheck Amount")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal payment_amount { get; set; }
    }
}