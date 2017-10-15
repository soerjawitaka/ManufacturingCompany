CREATE TABLE [dbo].[Timesheet] (
    [Id]             INT            IDENTITY (1001, 1) NOT NULL,
    [employee_id]    NVARCHAR (128) NOT NULL,
    [punch_in_time]  TIME       NOT NULL,
    [punch_out_time] TIME       NULL,
    [timesheet_date] DATETIME NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_timesheet_aspnetusers] FOREIGN KEY ([employee_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

