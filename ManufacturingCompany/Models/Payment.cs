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
    
    public partial class Payment
    {
        public int Id { get; set; }
        public int invoice_id { get; set; }
        public decimal payment_total { get; set; }
        public string payment_type { get; set; }
        public System.DateTime payment_date { get; set; }
        public string payment_note { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}
