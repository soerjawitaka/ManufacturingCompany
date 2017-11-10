using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Material_Stock_Partial_Metadata))]
    public partial class Material_Stock { }

    public class Material_Stock_Partial_Metadata
    {

        public int material_id { get; set; }

        [Required]
        [Display(Name = "Unit Measure")]
        public string material_unit_measure { get; set; }

        [Required]
        [Display(Name = "Unit Quantity")]
        public int material_unit_quantity { get; set; }

        [Required]
        [Display(Name = "Unit Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal material_unit_cost { get; set; }

    }
}