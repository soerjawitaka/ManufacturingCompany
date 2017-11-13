CREATE TABLE [dbo].Payroll_Timesheet_Assignment
(
	[payroll_id] INT NOT NULL, 
    [timesheet_id] INT NOT NULL,
	CONSTRAINT pk_payroll_timesheet_assignment PRIMARY KEY (payroll_id, timesheet_id),
	CONSTRAINT fk_pt_assignment_payroll FOREIGN KEY (payroll_id)
	REFERENCES Payroll(id),
	CONSTRAINT fk_pt_assignment_timesheet FOREIGN KEY (timesheet_id)
	REFERENCES Timesheet(id)
)
