using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models.Partial_Metadata
{
    [MetadataType(typeof(Equipment_Maintenance_Partial_Metadata))]
    public partial class Equipment_Maintenance { }

    public class Equipment_Maintenance_Partial_Metadata
    {
        [Required]
        [Display(Name = "Equipment Number")]
        public int equipment_id { get; set; }

        [Required]
        [Display(Name = "Maintenance Date")]
        public System.DateTime maintenance_date { get; set; }

        [Display(Name = "ETA for Completion")]
        public Nullable<System.DateTime> completion_eta { get; set; }

        [Display(Name = "Maintenance Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> maintenance_cost { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        public string employee_id { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string maintenance_short_description { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string maintenance_long_description { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string maintenance_note { get; set; }
    }
}