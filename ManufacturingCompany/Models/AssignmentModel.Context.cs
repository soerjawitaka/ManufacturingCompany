﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AssignmentEntities : DbContext
    {
        public AssignmentEntities()
            : base("name=AssignmentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Invoice_Deliver_Assignment> Invoice_Deliver_Assignment { get; set; }
        public virtual DbSet<Payroll_Timesheet_Assignment> Payroll_Timesheet_Assignment { get; set; }
        public virtual DbSet<Product_Material_Assignment> Product_Material_Assignment { get; set; }
    }
}
