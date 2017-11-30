CREATE TABLE [dbo].[Product_Category] (
    [Id]            INT            IDENTITY (1001, 1) NOT NULL,
    [category_name] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

