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
    
    public partial class Delivery_Schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delivery_Schedule()
        {
            this.Delivery_Lineitem = new HashSet<Delivery_Lineitem>();
        }
    
        public int Id { get; set; }
        public string warehouse_employee_id { get; set; }
        public string driver_employee_id { get; set; }
        public System.DateTime delivery_date { get; set; }
        public Nullable<decimal> delivery_cost { get; set; }
        public bool is_delivered { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery_Lineitem> Delivery_Lineitem { get; set; }
    }
}
