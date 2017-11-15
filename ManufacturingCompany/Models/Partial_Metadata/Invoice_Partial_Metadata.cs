using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Invoice_Partial_Metadata))]
    public partial class Invoice
    {
        [NotMapped]
        private const decimal Tax = .07m;

        [NotMapped]
        public List<Invoice_Lineitem> Lineitems { get; set; }
        
        [NotMapped]
        [Display(Name = "Subtotal")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal Subtotal { get; private set; }

        [NotMapped]
        [Display(Name = "Tax Amount")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal TaxAmount { get; private set; }

        [NotMapped]
        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal Total { get; private set; }

        public void CalculateTotal()
        {
            var db = new BusinessEntities();
            var invoiceLIs = db.Invoice_Lineitem.Where(li => li.invoice_id == this.Id);
            decimal subtotal = 0m;            
            foreach (var i in invoiceLIs)
            {
                i.CalculateTotal(i.product_inventory_id);
                i.ProductInventory.SetProduct();
                subtotal += i.LineitemTotal;
            }
            this.Lineitems = invoiceLIs.ToList();
            this.Subtotal = subtotal;
            this.TaxAmount = subtotal * Tax;
            this.Total = this.Subtotal + this.TaxAmount;
        }

    }

    public class Invoice_Partial_Metadata
    {
        [Required]
        [Display(Name ="Invoice Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime invoice_date { get; set; }

        [Required]
        public string employee_id { get; set; }

        [Required]
        public int customer_id { get; set; }
    }
}