using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Purchase_Partial_Metadata))]
    public partial class Purchase { }

    public class Purchase_Partial_Metadata
    {
        [Key]
        public int purchase_id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string purchase_item_name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string purchase_item_description { get; set; }

        [Required]
        [Display(Name = "Cost")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal purchase_item_unit_cost { get; set; }

        [Required]
        [Display(Name = "Department")]
        public string department_id { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime purchase_date { get; set; }
    }
}