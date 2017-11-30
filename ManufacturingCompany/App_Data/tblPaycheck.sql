CREATE TABLE [dbo].[Paycheck] (
    [Id]                    INT        IDENTITY (101, 1) NOT NULL,
    [paycheck_date]         DATETIME   NOT NULL,
    [payroll_id]            INT        NOT NULL,
    [payment_type]          NCHAR (10) NOT NULL,
    [check_number]          NCHAR (10) NULL,
    [direct_deposit_number] NCHAR (50) NULL,
    [payment_amount]        MONEY      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_paycheck_payroll] FOREIGN KEY ([payroll_id]) REFERENCES [dbo].[Payroll] ([Id])
);