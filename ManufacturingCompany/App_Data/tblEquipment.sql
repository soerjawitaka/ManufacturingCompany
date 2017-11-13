CREATE TABLE [dbo].[Equipment] (
    [Id]                          INT           IDENTITY (1001, 1) NOT NULL,
    [equipment_name]              VARCHAR (50)  NOT NULL,
    [equipment_short_description] VARCHAR (100) NOT NULL,
    [equipment_long_description]  VARCHAR (256) NOT NULL,
    [equipment_note]              TEXT          NULL,
    [equipment_cost]              MONEY         NOT NULL,
    [product_id]                  INT           NULL,
    [in_maintenance] BIT NOT NULL DEFAULT ((0)), 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_equipment_product] FOREIGN KEY ([product_id]) REFERENCES [dbo].[Product] ([Id])
);

