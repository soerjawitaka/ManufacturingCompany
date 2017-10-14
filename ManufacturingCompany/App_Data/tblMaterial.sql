CREATE TABLE [dbo].Material
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [material_name] VARCHAR(50) NOT NULL, 
    [material_description] VARCHAR(MAX) NOT NULL, 
    [material_note] TEXT NULL, 
    [product_id] INT NULL,
	CONSTRAINT fk_material_product FOREIGN KEY (product_id)
	REFERENCES Product(id)
)
