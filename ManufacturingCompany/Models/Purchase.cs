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
    
    public partial class Purchase
    {
        public int purchase_id { get; set; }
        public string purchase_item_name { get; set; }
        public string purchase_item_description { get; set; }
        public decimal purchase_item_unit_cost { get; set; }
        public string department_id { get; set; }
        public string employee_id { get; set; }
        public System.DateTime purchase_date { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
