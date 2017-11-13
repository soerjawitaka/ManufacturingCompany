CREATE TABLE [dbo].Product_Material_Assignment
(
	[product_id] INT NOT NULL , 
    [material_stock_id] INT NOT NULL, 
    CONSTRAINT pk_product_material_assignment PRIMARY KEY (product_id, material_stock_id),
	CONSTRAINT fk_pm_assignment_product FOREIGN KEY (product_id)
	REFERENCES Product(id),
	CONSTRAINT fk_pm_assignment_material_stock FOREIGN KEY (material_stock_id)
	REFERENCES Material_Stock(id)
)
