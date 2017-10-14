CREATE TABLE [dbo].Invoice
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [invoice_date] DATETIME NOT NULL, 
    [employee_id] NVARCHAR(128) NOT NULL, 
    [customer_id] INT NOT NULL,
	CONSTRAINT fk_invoice_aspnetusers FOREIGN KEY (employee_id)
	REFERENCES AspNetUsers(id),
	CONSTRAINT fk_invoice_customer FOREIGN KEY (customer_id)
	REFERENCES Customer(id)
)
