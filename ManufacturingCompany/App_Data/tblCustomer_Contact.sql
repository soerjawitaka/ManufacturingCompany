CREATE TABLE [dbo].Customer_Contact
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1001, 1), 
    [customer_id] INT NOT NULL, 
    [first_name] VARCHAR(50) NOT NULL, 
    [last_name] VARCHAR(50) NOT NULL, 
    [work_phone] NCHAR(10) NULL, 
    [mobile_phone] NCHAR(10) NULL, 
    [fax] NCHAR(10) NULL, 
    [contact_email] VARCHAR(100) NULL,
	CONSTRAINT fk_customer_contact_customer FOREIGN KEY (customer_id)	
	REFERENCES Customer(id)
)
