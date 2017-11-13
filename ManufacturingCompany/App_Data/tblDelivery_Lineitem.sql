CREATE TABLE [dbo].Delivery_Lineitem
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [product_inventory_id] INT NOT NULL, 
    [lineitem_unit_quantity] INT NOT NULL, 
    [delivery_schedule_id] INT NOT NULL,
	CONSTRAINT fk_delivery_lineitem_delivery_schedule FOREIGN KEY (delivery_schedule_id)
	REFERENCES Delivery_Schedule(id),
	CONSTRAINT fk_delivery_lineitem_product_inventory FOREIGN KEY (product_inventory_id)
	REFERENCES Product_Inventory(id)
)
