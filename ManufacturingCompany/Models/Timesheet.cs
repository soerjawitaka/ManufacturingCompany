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
    
    public partial class Timesheet
    {
        public int Id { get; set; }
        public string employee_id { get; set; }
        public System.TimeSpan punch_in_time { get; set; }
        public Nullable<System.TimeSpan> punch_out_time { get; set; }
        public System.DateTime timesheet_date { get; set; }
        public bool is_in_payroll { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
