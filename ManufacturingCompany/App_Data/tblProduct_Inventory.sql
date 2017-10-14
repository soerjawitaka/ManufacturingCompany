CREATE TABLE [dbo].Product_Inventory
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [product_id] INT NOT NULL, 
    [unit_quantity] INT NOT NULL, 
    [unit_per_package] INT NOT NULL, 
    [per_package_cost] MONEY NOT NULL, 
    [per_package_price] MONEY NOT NULL,
	CONSTRAINT fk_product_inventory_product FOREIGN KEY (Product_id)
	REFERENCES Product(id)
)
