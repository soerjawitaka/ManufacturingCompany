using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Material_Partial_Metadata))]
    public partial class Material { }

    public class Material_Partial_Metadata
    {
        [Required]
        [Display(Name = "Material Name")]
        public string material_name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string material_description { get; set; }

        [Display(Name = "Notes")]
        public string material_note { get; set; }
    }
}