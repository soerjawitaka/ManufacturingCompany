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
    
    public partial class Payroll
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payroll()
        {
            this.Paychecks = new HashSet<Paycheck>();
        }
    
        public int Id { get; set; }
        public string employee_id { get; set; }
        public System.DateTime period_begin { get; set; }
        public System.DateTime period_end { get; set; }
        public Nullable<double> total_hours { get; set; }
        public decimal subtotal { get; set; }
        public decimal federal_tax_witholding { get; set; }
        public decimal state_tax_witholding { get; set; }
        public decimal grand_total { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paycheck> Paychecks { get; set; }
    }
}
