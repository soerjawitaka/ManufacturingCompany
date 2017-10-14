CREATE TABLE [dbo].Delivery_Schedule
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [warehouse_employee_id] NVARCHAR(128) NOT NULL, 
    [driver_employee_id] NVARCHAR(128) NULL, 
    [delivery_date] DATETIME NOT NULL, 
    [delivery_cost] MONEY NULL,
	CONSTRAINT fk_delivery_schedule_warehouse_aspnetusers FOREIGN KEY (warehouse_employee_id)
	REFERENCES AspNetUsers(id),
	CONSTRAINT fk_delivery_schedule_driver_aspnetusers FOREIGN KEY (driver_employee_id)
	REFERENCES AspnetUsers(id)
)
