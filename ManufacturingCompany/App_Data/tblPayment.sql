CREATE TABLE [dbo].[Payment] (
    [Id]            INT        IDENTITY (1001, 1) NOT NULL,
    [invoice_id]    INT        NOT NULL,
    [payment_total] MONEY      NOT NULL,
    [payment_type]  NCHAR (10) NOT NULL,
    [payment_date]  DATETIME   NOT NULL,
    [payment_note] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_payment_invoice] FOREIGN KEY ([invoice_id]) REFERENCES [dbo].[Invoice] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Invoice_Id]
    ON [dbo].[Payment]([invoice_id] ASC);

