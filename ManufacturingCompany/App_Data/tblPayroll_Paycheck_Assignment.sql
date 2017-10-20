CREATE TABLE [dbo].Payroll_Paycheck_Assignment
(
	[payroll_id] INT NOT NULL, 
    [paycheck_id] INT NOT NULL,
	CONSTRAINT pk_payroll_paycheck_assignment PRIMARY KEY (payroll_id, paycheck_id),
	CONSTRAINT fk_pp_assignment_payroll FOREIGN KEY (payroll_id)
	REFERENCES Payroll(id),
	CONSTRAINT fk_pp_assignment_paycheck FOREIGN KEY (paycheck_id)
	REFERENCES Paycheck(id)
)
