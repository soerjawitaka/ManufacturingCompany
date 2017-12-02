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
        public List<Delivery_Lineitem> DeliveryItems { get; set; }

        [NotMapped]
        [Display(Name = "Subtotal")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Subtotal { get; private set; }

        [NotMapped]
        [Display(Name = "Tax Amount")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal TaxAmount { get; private set; }

        [NotMapped]
        [Display(Name = "Total")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Total { get; private set; }

        [NotMapped]
        [Display(Name = "Delivery Status")]
        public string DeliveryStatus { get; set; }


        public void CalculateTotal()
        {
            // gathering lineitems info
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

            // gathering delivery items info
            var deliveryLIs = db.Delivery_Lineitem.Where(dl => dl.Invoice_Lineitem.invoice_id == this.Id && dl.Delivery_Schedule.is_delivered == true)
                                                  .OrderBy(dl => dl.Delivery_Schedule.delivery_date)
                                                  .ToList();
            foreach (var i in deliveryLIs) { i.CalculateTotal(i.product_inventory_id); }
            this.DeliveryItems = deliveryLIs;

            // determine delivery status
            bool isComplete = true;
            foreach (var i in this.Lineitems)
            {
                var matchingDeliveryItems = this.DeliveryItems.Where(di => di.invoice_lineitem_id == i.Id).ToList();
                if (matchingDeliveryItems != null)
                {
                    int qty = 0;
                    foreach (var d in matchingDeliveryItems) { qty += d.lineitem_unit_quantity; }
                    if (i.lineitem_unit_quantity != qty) { isComplete = false; }
                }
                else
                {
                    isComplete = false;
                }
            }
            if (isComplete == false) { this.DeliveryStatus = "Pending"; }
            else { this.DeliveryStatus = "Complete"; }

            this.Subtotal = subtotal;
            this.TaxAmount = subtotal * Tax;
            this.Total = this.Subtotal + this.TaxAmount;
        }

    }

    public class Invoice_Partial_Metadata
    {
        [Display(Name = "Invoice Number")]
        public int Id { get; set; }

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