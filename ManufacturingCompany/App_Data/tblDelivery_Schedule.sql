CREATE TABLE [dbo].[Delivery_Schedule] (
    [Id]                    INT            IDENTITY (1001, 1) NOT NULL,
    [warehouse_employee_id] NVARCHAR (128) NOT NULL,
    [driver_employee_id]    NVARCHAR (128) NULL,
    [delivery_date]         DATETIME       NOT NULL,
    [delivery_cost]         MONEY          NULL,
    [is_delivered]          BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_delivery_schedule_warehouse_aspnetusers] FOREIGN KEY ([warehouse_employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [fk_delivery_schedule_driver_aspnetusers] FOREIGN KEY ([driver_employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

