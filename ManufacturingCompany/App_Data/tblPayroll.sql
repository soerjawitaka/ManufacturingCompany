CREATE TABLE [dbo].Payroll
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [employee_id] NVARCHAR(128) NOT NULL, 
    [period_begin] DATETIME NOT NULL, 
    [period_end] DATETIME NOT NULL, 
    [total_hours] DECIMAL(3, 2) NOT NULL, 
    [subtotal] MONEY NOT NULL, 
    [federal_tax_witholding] MONEY NOT NULL, 
    [state_tax_witholding] MONEY NOT NULL, 
    [grand_total] MONEY NOT NULL,
	CONSTRAINT fk_payroll_aspnetusers FOREIGN KEY (employee_id)
	REFERENCES AspNetUsers(id)
)
