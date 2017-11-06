using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(ProductInventory_Partial_Metadata))]
    public partial class Product_Inventory
    {
        public void SetCostPerPackage()
        {
            this.per_package_cost = this.Product.product_unit_cost * this.unit_per_package;
        }
    }

    public class ProductInventory_Partial_Metadata
    {
        [Required]
        [Display(Name = "Unit Quantity")]
        public int unit_quantity { get; set; }

        [Required]
        [Display(Name = "Unit per Package")]
        public int unit_per_package { get; set; }

        [Required]
        [Display(Name = "Cost per Package")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal per_package_cost { get; set; }

        [Required]
        [Display(Name = "Price per Package")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal per_package_price { get; set; }
    }
}