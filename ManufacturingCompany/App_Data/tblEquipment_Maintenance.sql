CREATE TABLE [dbo].[Equipment_Maintenance] (
    [Id]               INT            IDENTITY (1001, 1) NOT NULL,
    [equipment_id]     INT            NOT NULL,
    [maintenance_date] DATETIME       NOT NULL,
    [completion_eta]   DATETIME       NULL,
    [maintenance_cost] MONEY          NULL,
    [employee_id]      NVARCHAR (128) NULL,
    [maintenance_short_description] VARCHAR(256) NOT NULL, 
    [maintenance_long_description] TEXT NULL, 
    [maintenance_note] TEXT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_equipment_maintenance_equipment] FOREIGN KEY ([equipment_id]) REFERENCES [dbo].[Equipment] ([Id]),
    CONSTRAINT [fk_equipment_maintenance_aspnetusers] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

