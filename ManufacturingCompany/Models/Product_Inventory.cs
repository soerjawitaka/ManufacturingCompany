//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManufacturingCompany.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Inventory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Inventory()
        {
            this.Delivery_Lineitem = new HashSet<Delivery_Lineitem>();
            this.Invoice_Lineitem = new HashSet<Invoice_Lineitem>();
        }
    
        public int Id { get; set; }
        public int product_id { get; set; }
        public int unit_quantity { get; set; }
        public int unit_per_package { get; set; }
        public decimal per_package_cost { get; set; }
        public Nullable<decimal> packaging_cost { get; set; }
        public decimal per_package_price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery_Lineitem> Delivery_Lineitem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice_Lineitem> Invoice_Lineitem { get; set; }
        public virtual Product Product { get; set; }
    }
}
