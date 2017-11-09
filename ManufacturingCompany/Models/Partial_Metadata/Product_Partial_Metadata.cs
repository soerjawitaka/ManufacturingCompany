using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Product_Partial_Metadata))]
    public partial class Product { }

    public class Product_Partial_Metadata
    {
        [Required]
        [Display(Name = "Product Name")]
        public string product_name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string product_short_description { get; set; }

        [Required]
        [Display(Name = "Details")]
        public string product_long_description { get; set; }

        [Display(Name = "Note")]
        public string product_note { get; set; }

        [Required]
        [Display(Name = "Unit Measure")]
        public string product_unit_measure { get; set; }

        [Required]
        [Display(Name = "Unit Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal product_unit_cost { get; set; }

        [Required]
        [Display(Name = "Unit Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal product_unit_price { get; set; }
    }
}