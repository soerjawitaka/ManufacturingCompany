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
    
    public partial class BusinessEntities : DbContext
    {
        public BusinessEntities()
            : base("name=BusinessEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customer_Contact> Customer_Contact { get; set; }
        public virtual DbSet<Delivery_Lineitem> Delivery_Lineitem { get; set; }
        public virtual DbSet<Delivery_Schedule> Delivery_Schedule { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Equipment_Maintenance> Equipment_Maintenance { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoice_Lineitem> Invoice_Lineitem { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Material_Stock> Material_Stock { get; set; }
        public virtual DbSet<Paycheck> Paychecks { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Category> Product_Category { get; set; }
        public virtual DbSet<Product_Inventory> Product_Inventory { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
    }
}
