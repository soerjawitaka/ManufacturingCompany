CREATE TABLE [dbo].[Product] (
    [Id]                        INT           IDENTITY (1001, 1) NOT NULL,
    [product_name]              VARCHAR (50)  NOT NULL,
    [product_short_description] VARCHAR (256) NOT NULL,
    [product_long_description]  TEXT          NOT NULL,
    [product_note]              TEXT          NULL,
	[product_unit_measure]      VARCHAR(20)   NULL,
    [product_unit_cost]         MONEY         NOT NULL,
    [product_unit_price]        MONEY         NOT NULL,
    [product_category_id]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_product_category] FOREIGN KEY ([product_category_id]) REFERENCES [dbo].[Product_Category] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Product_Category_Id]
    ON [dbo].[Product]([product_category_id] ASC);

