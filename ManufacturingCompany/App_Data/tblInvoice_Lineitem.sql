CREATE TABLE [dbo].[Invoice_Lineitem] (
    [Id]                     INT IDENTITY (1001, 1) NOT NULL,
    [product_inventory_id]   INT NOT NULL,
    [lineitem_unit_quantity] INT NOT NULL,
    [invoice_id]             INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_invoice_lineitem_product_inventory] FOREIGN KEY ([product_inventory_id]) REFERENCES [dbo].[Product_Inventory] ([Id]),
    CONSTRAINT [fk_invoice_lineitem_invoice] FOREIGN KEY ([invoice_id]) REFERENCES [dbo].[Invoice] ([Id])
);