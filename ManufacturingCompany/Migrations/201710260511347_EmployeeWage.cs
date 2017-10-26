namespace ManufacturingCompany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeWage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ModeOfWage", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "WageAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "WageAmount");
            DropColumn("dbo.AspNetUsers", "ModeOfWage");
        }
    }
}
