CREATE TABLE [dbo].Timesheet
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [employee_id] NVARCHAR(128) NOT NULL, 
    [punch_in_time] DATETIME NOT NULL, 
    [punch_out_time] DATETIME NULL,
	CONSTRAINT fk_timesheet_aspnetusers FOREIGN KEY (employee_id)
	REFERENCES AspNetUsers(id)
)
