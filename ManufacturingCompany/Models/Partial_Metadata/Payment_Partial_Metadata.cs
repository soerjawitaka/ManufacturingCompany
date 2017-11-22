using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Payment_Partial_Metadata))]
    public partial class Payment { }

    public class Payment_Partial_Metadata
    {
        [Required]
        [Display(Name = "Invoice Number")]
        public int invoice_id { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal payment_total { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string payment_type { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime payment_date { get; set; }

        [Display(Name = "Payment Notes")]
        public string payment_note { get; set; }
    }
}