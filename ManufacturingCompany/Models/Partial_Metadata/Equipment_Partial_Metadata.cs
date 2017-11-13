using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Equipment_Partial_Metadata))]
    public partial class Equipment { }

    public class Equipment_Partial_Metadata
    {
        [Required]
        [Display(Name = "Equipment Name")]
        public string equipment_name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string equipment_short_description { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string equipment_long_description { get; set; }

        [Display(Name = "Note")]
        public string equipment_note { get; set; }

        [Display(Name = "Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal equipment_cost { get; set; }

        [Display(Name = "Product Number")]
        public Nullable<int> product_id { get; set; }

        [Required]
        [Display(Name = "Is In Maintenance")]
        public bool in_maintenance { get; set; }
    }
}