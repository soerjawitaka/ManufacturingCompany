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
        public void CalculatePackage()
        {
            SetProduct();
            this.per_package_cost = this.Product.product_unit_cost * this.unit_per_package;
            this.per_package_price = this.Product.product_unit_price * this.unit_per_package;
            if (this.packaging_cost != null)
            {
                this.per_package_price += Convert.ToDecimal(this.packaging_cost);
            }
            this.Product = null;
        }

        public void SetProduct()
        {
            var db = new BusinessEntities();
            if (this.Product == null)
            {
                this.Product = db.Products.Find(this.product_id);
            }
            db.Dispose();
        }
    }

    public class ProductInventory_Partial_Metadata
    {
        [Required]
        [Display(Name = "Unit Quantity")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:#,#}")]
        public int unit_quantity { get; set; }

        [Required]
        [Display(Name = "Unit per Package")]
        public int unit_per_package { get; set; }

        [Required]
        [Display(Name = "Cost per Package")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal per_package_cost { get; set; }

        [Display(Name = "Packaging Cost")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public Nullable<decimal> packaging_cost { get; set; }

        [Required]
        [Display(Name = "Price per Package")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal per_package_price { get; set; }
    }
}