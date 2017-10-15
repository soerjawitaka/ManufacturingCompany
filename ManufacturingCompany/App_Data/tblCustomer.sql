CREATE TABLE [dbo].[Customer] (
    [Id]                   INT           IDENTITY (1001, 1) NOT NULL,
    [customer_company_name] VARCHAR (50)  NOT NULL,
    [customer_address1]    VARCHAR (100) NOT NULL,
    [customer_address2]    VARCHAR (100) NULL,
    [customer_city]        VARCHAR (100) NOT NULL,
    [customer_state]       VARCHAR (100) NOT NULL,
    [customer_zip]         VARCHAR (10)  NOT NULL,
    [customer_country]     VARCHAR (100) NOT NULL,
    [customer_phone]       NCHAR (10)    NULL,
    [customer_website]     VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

