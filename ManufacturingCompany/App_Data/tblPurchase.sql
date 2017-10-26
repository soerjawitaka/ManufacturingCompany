﻿CREATE TABLE [dbo].[Purchase] (
    [purchase_id]               INT            IDENTITY (1001, 1) NOT NULL,
    [purchase_item_name]        VARCHAR (50)   NOT NULL,
    [purchase_item_description] VARCHAR (MAX)  NOT NULL,
    [purchase_item_unit_cost]   MONEY          NOT NULL,
    [employee_id]               NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([purchase_id] ASC),
    CONSTRAINT [fk_purchase_aspnetusers] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

