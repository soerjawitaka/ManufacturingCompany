namespace ManufacturingCompany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaycheckModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Paychecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        paycheck_date = c.DateTime(nullable: false),
                        payroll_id = c.Int(nullable: false),
                        payment_type = c.String(nullable: false),
                        check_number = c.String(nullable: false),
                        direct_deposit_number = c.String(nullable: false),
                        payment_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModeOfPaycheck = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payrolls", t => t.payroll_id)
                .Index(t => t.payroll_id);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        employee_id = c.String(),
                        period_begin = c.DateTime(nullable: false),
                        period_end = c.DateTime(nullable: false),
                        total_hours = c.Double(),
                        subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        federal_tax_witholding = c.Decimal(nullable: false, precision: 18, scale: 2),
                        state_tax_witholding = c.Decimal(nullable: false, precision: 18, scale: 2),
                        grand_total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AspNetUser_Id = c.String(maxLength: 128),
                        Timesheet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paychecks", "Payroll_Id", "dbo.Payrolls");
            DropTable("dbo.Payrolls");
            DropTable("dbo.Paychecks");
        }
    }
}
