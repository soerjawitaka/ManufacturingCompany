namespace ManufacturingCompany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCategory : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Paycheck", new[] { "Payroll_Id" });
            DropIndex("dbo.Delivery_Lineitem", new[] { "Delivery_Schedule_Id" });
            DropIndex("dbo.Delivery_Lineitem", new[] { "Product_Inventory_Id" });
            DropIndex("dbo.Product_Inventory", new[] { "Product_Id" });
            DropIndex("dbo.Invoice_Lineitem", new[] { "Invoice_Id" });
            DropIndex("dbo.Invoice_Lineitem", new[] { "Product_Inventory_Id" });
            DropIndex("dbo.Invoice", new[] { "Customer_Id" });
            DropIndex("dbo.Customer_Contact", new[] { "Customer_Id" });
            DropIndex("dbo.Payment", new[] { "Invoice_Id" });
            DropIndex("dbo.Product", new[] { "Product_Category_Id" });
            DropIndex("dbo.Equipment_Maintenance", new[] { "Equipment_Id" });
            DropIndex("dbo.Material_Stock", new[] { "Material_Id" });
            AlterColumn("dbo.Paycheck", "Payroll_Id", c => c.Int());
            AlterColumn("dbo.Paycheck", "payroll_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Delivery_Lineitem", "Delivery_Schedule_Id", c => c.Int());
            AlterColumn("dbo.Delivery_Lineitem", "Product_Inventory_Id", c => c.Int());
            AlterColumn("dbo.Delivery_Lineitem", "delivery_schedule_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Delivery_Lineitem", "product_inventory_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Product_Inventory", "Product_Id", c => c.Int());
            AlterColumn("dbo.Product_Inventory", "product_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice_Lineitem", "Invoice_Id", c => c.Int());
            AlterColumn("dbo.Invoice_Lineitem", "Product_Inventory_Id", c => c.Int());
            AlterColumn("dbo.Invoice_Lineitem", "invoice_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice_Lineitem", "product_inventory_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Invoice", "customer_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer_Contact", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Customer_Contact", "customer_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Payment", "Invoice_Id", c => c.Int());
            AlterColumn("dbo.Payment", "invoice_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "Product_Category_Id", c => c.Int());
            AlterColumn("dbo.Product", "product_category_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Equipment_Maintenance", "Equipment_Id", c => c.Int());
            AlterColumn("dbo.Equipment_Maintenance", "equipment_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Material_Stock", "Material_Id", c => c.Int());
            AlterColumn("dbo.Material_Stock", "material_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Product_Category", "category_name", c => c.String(nullable: false));
            CreateIndex("dbo.Paycheck", "Payroll_Id");
            CreateIndex("dbo.Delivery_Lineitem", "Delivery_Schedule_Id");
            CreateIndex("dbo.Delivery_Lineitem", "Product_Inventory_Id");
            CreateIndex("dbo.Product_Inventory", "Product_Id");
            CreateIndex("dbo.Invoice_Lineitem", "Invoice_Id");
            CreateIndex("dbo.Invoice_Lineitem", "Product_Inventory_Id");
            CreateIndex("dbo.Invoice", "Customer_Id");
            CreateIndex("dbo.Customer_Contact", "Customer_Id");
            CreateIndex("dbo.Payment", "Invoice_Id");
            CreateIndex("dbo.Product", "Product_Category_Id");
            CreateIndex("dbo.Equipment_Maintenance", "Equipment_Id");
            CreateIndex("dbo.Material_Stock", "Material_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Material_Stock", new[] { "Material_Id" });
            DropIndex("dbo.Equipment_Maintenance", new[] { "Equipment_Id" });
            DropIndex("dbo.Product", new[] { "Product_Category_Id" });
            DropIndex("dbo.Payment", new[] { "Invoice_Id" });
            DropIndex("dbo.Customer_Contact", new[] { "Customer_Id" });
            DropIndex("dbo.Invoice", new[] { "Customer_Id" });
            DropIndex("dbo.Invoice_Lineitem", new[] { "Product_Inventory_Id" });
            DropIndex("dbo.Invoice_Lineitem", new[] { "Invoice_Id" });
            DropIndex("dbo.Product_Inventory", new[] { "Product_Id" });
            DropIndex("dbo.Delivery_Lineitem", new[] { "Product_Inventory_Id" });
            DropIndex("dbo.Delivery_Lineitem", new[] { "Delivery_Schedule_Id" });
            DropIndex("dbo.Paycheck", new[] { "Payroll_Id" });
            AlterColumn("dbo.Product_Category", "category_name", c => c.String());
            AlterColumn("dbo.Material_Stock", "material_id", c => c.Int());
            AlterColumn("dbo.Material_Stock", "Material_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Equipment_Maintenance", "equipment_id", c => c.Int());
            AlterColumn("dbo.Equipment_Maintenance", "Equipment_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "product_category_id", c => c.Int());
            AlterColumn("dbo.Product", "Product_Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Payment", "invoice_id", c => c.Int());
            AlterColumn("dbo.Payment", "Invoice_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customer_Contact", "customer_id", c => c.Int());
            AlterColumn("dbo.Customer_Contact", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice", "customer_id", c => c.Int());
            AlterColumn("dbo.Invoice", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice_Lineitem", "product_inventory_id", c => c.Int());
            AlterColumn("dbo.Invoice_Lineitem", "invoice_id", c => c.Int());
            AlterColumn("dbo.Invoice_Lineitem", "Product_Inventory_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoice_Lineitem", "Invoice_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Product_Inventory", "product_id", c => c.Int());
            AlterColumn("dbo.Product_Inventory", "Product_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Delivery_Lineitem", "product_inventory_id", c => c.Int());
            AlterColumn("dbo.Delivery_Lineitem", "delivery_schedule_id", c => c.Int());
            AlterColumn("dbo.Delivery_Lineitem", "Product_Inventory_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Delivery_Lineitem", "Delivery_Schedule_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Paycheck", "payroll_id", c => c.Int());
            AlterColumn("dbo.Paycheck", "Payroll_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Material_Stock", "Material_Id");
            CreateIndex("dbo.Equipment_Maintenance", "Equipment_Id");
            CreateIndex("dbo.Product", "Product_Category_Id");
            CreateIndex("dbo.Payment", "Invoice_Id");
            CreateIndex("dbo.Customer_Contact", "Customer_Id");
            CreateIndex("dbo.Invoice", "Customer_Id");
            CreateIndex("dbo.Invoice_Lineitem", "Product_Inventory_Id");
            CreateIndex("dbo.Invoice_Lineitem", "Invoice_Id");
            CreateIndex("dbo.Product_Inventory", "Product_Id");
            CreateIndex("dbo.Delivery_Lineitem", "Product_Inventory_Id");
            CreateIndex("dbo.Delivery_Lineitem", "Delivery_Schedule_Id");
            CreateIndex("dbo.Paycheck", "Payroll_Id");
        }
    }
}
