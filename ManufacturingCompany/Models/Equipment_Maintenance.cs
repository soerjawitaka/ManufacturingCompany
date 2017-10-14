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
    
    public partial class Equipment_Maintenance
    {
        public int Id { get; set; }
        public int equipment_id { get; set; }
        public System.DateTime maintenance_date { get; set; }
        public Nullable<System.DateTime> completion_eta { get; set; }
        public Nullable<decimal> maintenance_cost { get; set; }
        public string employee_id { get; set; }
        public string maintenance_short_description { get; set; }
        public string maintenance_long_description { get; set; }
        public string maintenance_note { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
