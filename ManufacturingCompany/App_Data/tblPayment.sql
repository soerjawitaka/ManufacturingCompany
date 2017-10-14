CREATE TABLE [dbo].Payment
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [invoice_id] INT NOT NULL, 
    [payment_total] MONEY NOT NULL, 
    [payment_type] NCHAR(10) NOT NULL, 
    [payment_date] DATETIME NOT NULL,
	CONSTRAINT fk_payment_invoice FOREIGN KEY (invoice_id)
	REFERENCES Invoice(id)
)
