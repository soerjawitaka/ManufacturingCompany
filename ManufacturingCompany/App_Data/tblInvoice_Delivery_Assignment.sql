CREATE TABLE [dbo].Invoice_Deliver_Assignment
(
	[invoice_id] INT NOT NULL, 
    [delivery_schedule_id] INT NOT NULL,
	CONSTRAINT pk_invoice_delivery_assignment PRIMARY KEY (invoice_id, delivery_schedule_id),
	CONSTRAINT fk_id_assignment_invoice FOREIGN KEY (invoice_id)
	REFERENCES Invoice(id),
	CONSTRAINT fk_id_assignment_delivery_schedule FOREIGN KEY (delivery_schedule_id)
	REFERENCES Delivery_Schedule(id)
)
