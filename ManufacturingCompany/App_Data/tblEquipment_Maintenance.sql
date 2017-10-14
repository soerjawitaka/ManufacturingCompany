CREATE TABLE [dbo].Equipment_Maintenance
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [equipment_id] INT NOT NULL, 
    [maintenance_date] DATETIME NOT NULL, 
    [completion_eta] DATETIME NULL, 
    [maintenance_cost] MONEY NULL, 
    [employee_id] NVARCHAR(128) NULL,
	CONSTRAINT fk_equipment_maintenance_equipment FOREIGN KEY (equipment_id)
	REFERENCES Equipment(id),
	CONSTRAINT fk_equipment_maintenance_aspnetusers FOREIGN KEY (employee_id)
	REFERENCES AspNetUsers(id)
)
