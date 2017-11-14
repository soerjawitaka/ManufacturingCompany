using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Invoice_Partial_Metadata))]
    public partial class Invoice
    {
        [NotMapped]
        public List<Invoice_Lineitem> Lineitems { get; set; }

        [NotMapped]
        public int MyProperty { get; set; }
    }

    public class Invoice_Partial_Metadata
    {
        [Required]
        [Display(Name ="Invoice Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime invoice_date { get; set; }

        [Required]
        public string employee_id { get; set; }

        [Required]
        public int customer_id { get; set; }
    }
}