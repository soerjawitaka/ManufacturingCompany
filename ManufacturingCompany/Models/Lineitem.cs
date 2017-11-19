using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingCompany.Models
{
    public class Lineitem
    {
        [Required]
        public int product_inventory_id { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int lineitem_unit_quantity { get; set; }

        [NotMapped]
        [Display(Name = "Number of Packages")]
        public int PackageQuantity { get; set; }

        [NotMapped]
        [Display(Name = "Total Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal LineitemTotal { get; set; }

        [NotMapped]
        public Product_Inventory ProductInventory { get; set; }

        private void SetProductInventory(int productInventoryID)
        {
            var db = new BusinessEntities();
            this.product_inventory_id = productInventoryID;
            this.ProductInventory = db.Product_Inventory.Find(productInventoryID);
            this.ProductInventory.CalculatePackage();
            this.ProductInventory.SetProduct();
            db.Dispose();
        }

        public void CalculateTotal(int productInventoryID)
        {
            SetProductInventory(productInventoryID);
            this.PackageQuantity = this.lineitem_unit_quantity / this.ProductInventory.unit_per_package;
            this.LineitemTotal = this.PackageQuantity * this.ProductInventory.per_package_price;
        }
    }
}