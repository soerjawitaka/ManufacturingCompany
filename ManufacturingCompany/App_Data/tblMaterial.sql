CREATE TABLE [dbo].[Material] (
    [Id]                   INT           IDENTITY (1001, 1) NOT NULL,
    [material_name]        VARCHAR (50)  NOT NULL,
    [material_description] VARCHAR (MAX) NOT NULL,
    [material_note]        TEXT          NULL,
    [product_id]           INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_material_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[Product] ([Id])
);

