CREATE TABLE [dbo].[Paycheck] (
    [Id]                    INT        IDENTITY (101, 1) NOT NULL,
    [payment_type]          NCHAR (10) NOT NULL,
    [check_number]          NCHAR (10) NULL,
    [direct_deposit_number] NCHAR (50) NULL,
    [payment_amount]        MONEY      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

