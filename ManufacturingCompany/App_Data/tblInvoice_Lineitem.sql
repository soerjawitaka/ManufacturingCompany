CREATE TABLE [dbo].Invoice_Lineitem
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [product_inventory_id] INT NOT NULL, 
    [lineitem_unit_quantity] INT NOT NULL,
	[invoice_id] INT NOT NULL, 
    CONSTRAINT fk_invoice_lineitem_product_inventory FOREIGN KEY (product_inventory_id)
	REFERENCES Product_Inventory(id),
	CONSTRAINT fk_invoice_lineitem_invoice FOREIGN KEY (invoice_id)
	REFERENCES Invoice(id)
)
