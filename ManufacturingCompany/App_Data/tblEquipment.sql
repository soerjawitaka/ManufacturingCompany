CREATE TABLE [dbo].Equipment
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [equipment_name] VARCHAR(50) NOT NULL, 
    [equipment_short_description] VARCHAR(100) NOT NULL, 
    [equipment_long_description] VARCHAR(256) NOT NULL, 
    [equipment_note] TEXT NULL, 
    [equipment_cost] MONEY NOT NULL, 
    [product_id] INT NULL,
	CONSTRAINT fk_equipment_product FOREIGN KEY (product_id)
	REFERENCES Product(id)
)
