CREATE TABLE [dbo].[Delivery_Lineitem] (
    [Id]                     INT IDENTITY (1001, 1) NOT NULL,
    [product_inventory_id]   INT NOT NULL,
    [lineitem_unit_quantity] INT NOT NULL,
    [delivery_schedule_id]   INT NULL,
    [invoice_lineitem_id]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_delivery_lineitem_product_inventory] FOREIGN KEY ([product_inventory_id]) REFERENCES [dbo].[Product_Inventory] ([Id]),
    CONSTRAINT [fk_delivery_lineitem_invoice_lineitem] FOREIGN KEY ([invoice_lineitem_id]) REFERENCES [dbo].[Invoice_Lineitem] ([Id]),
    CONSTRAINT [fk_delivery_lineitem_delivery_schedule] FOREIGN KEY ([delivery_schedule_id]) REFERENCES [dbo].[Delivery_Schedule] ([Id])
);
