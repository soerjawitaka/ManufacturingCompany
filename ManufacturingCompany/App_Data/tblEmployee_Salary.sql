CREATE TABLE [dbo].Employee_Salary
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(101, 1), 
    [payroll_id] INT NOT NULL, 
    [payment_type] NCHAR(10) NOT NULL, 
    [check_number] NCHAR(10) NULL, 
    [direct_deposit_number] NCHAR(50) NULL, 
    [payment_amount] MONEY NOT NULL,
	CONSTRAINT fk_employee_salary_payroll FOREIGN KEY (payroll_id)
	REFERENCES Payroll(id)
)
