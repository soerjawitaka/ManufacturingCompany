CREATE TABLE [dbo].Material_Stock
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [material_id] INT NOT NULL, 
    [material_unit_measure] NCHAR(10) NOT NULL, 
    [material_unit_quantity] INT NOT NULL, 
    [material_unit_cost] MONEY NOT NULL,
	CONSTRAINT fk_material_stock_material FOREIGN KEY (material_id)
	REFERENCES Material(id)
)
