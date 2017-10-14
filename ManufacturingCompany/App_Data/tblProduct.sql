CREATE TABLE [dbo].Product
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [product_name] VARCHAR(50) NOT NULL, 
    [product_short_description] VARCHAR(256) NOT NULL, 
    [product_long_description] TEXT NOT NULL, 
    [product_note] TEXT NULL, 
    [product_unit_cost] MONEY NOT NULL, 
    [product_unit_price] MONEY NOT NULL, 
    [product_material_id] INT NOT NULL, 
    [product_category_id] INT NOT NULL,
	CONSTRAINT fk_product_category FOREIGN KEY (product_category_id)
	REFERENCES Product_Category(id)
)
