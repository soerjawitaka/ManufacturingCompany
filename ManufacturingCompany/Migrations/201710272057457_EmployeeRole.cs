namespace ManufacturingCompany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeRoleViewModels",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeRoleViewModels");
        }
    }
}
