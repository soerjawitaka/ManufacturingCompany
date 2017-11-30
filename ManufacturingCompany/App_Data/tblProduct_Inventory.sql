CREATE TABLE [dbo].[Product_Inventory] (
    [Id]                INT   IDENTITY (1001, 1) NOT NULL,
    [product_id]        INT   NOT NULL,
    [unit_quantity]     INT   NOT NULL,
    [unit_per_package]  INT   NOT NULL,
    [per_package_cost]  MONEY NOT NULL,
    [packaging_cost]    MONEY NULL,
    [per_package_price] MONEY NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_product_inventory_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[Product] ([Id])
);
