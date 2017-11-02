using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(ProductCategory_Partial_Metadata))]
    public partial class Product_Category { }

    public class ProductCategory_Partial_Metadata
    {
        [Required]
        [Display(Name = "Category Name")]
        public string category_name { get; set; }
    }
}