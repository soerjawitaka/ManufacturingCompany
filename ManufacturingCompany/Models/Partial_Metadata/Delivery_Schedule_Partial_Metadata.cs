using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Delivery_Schedule_Partial_Metadata))]
    public partial class Delivery_Schedule { }

    public class Delivery_Schedule_Partial_Metadata
    {
        [Display(Name ="Delivery Number")]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Delivery Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime delivery_date { get; set; }

        [Display(Name = "Delivery Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> delivery_cost { get; set; }

        [Display(Name = "Is Delivered")]
        public bool is_delivered { get; set; }

        [Display(Name = "Invoice Number")]
        public int invoice_id { get; set; }
    }
}