CREATE TABLE [dbo].[Invoice] (
    [Id]           INT            IDENTITY (1001, 1) NOT NULL,
    [invoice_date] DATETIME       NOT NULL,
    [employee_id]  NVARCHAR (128) NOT NULL,
    [customer_id]  INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_invoice_aspnetusers] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [fk_invoice_customer] FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customer] ([Id])
);
