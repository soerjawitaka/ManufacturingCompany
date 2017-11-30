CREATE TABLE [dbo].[Payroll] (
    [Id]                     INT            IDENTITY (1001, 1) NOT NULL,
    [employee_id]            NVARCHAR (128) NOT NULL,
    [period_begin]           DATETIME       NOT NULL,
    [period_end]             DATETIME       NOT NULL,
    [total_hours]            FLOAT (53)     NULL,
    [subtotal]               MONEY          NOT NULL,
    [federal_tax_witholding] MONEY          NOT NULL,
    [state_tax_witholding]   MONEY          NOT NULL,
    [grand_total]            MONEY          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_payroll_aspnetusers] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

