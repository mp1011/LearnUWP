CREATE TABLE [PaymentSchedules]
(
	[ID] [int] NULL,
	[BankAccountID] [int] NOT NULL,
	[ExpenseID] [int] NOT NULL,
	[RecurrenceTypeID] [int] NOT NULL,
	[FirstPaymentDate] [datetime] NOT NULL,
	[LastPaymentDate] [datetime] NOT NULL,
	[Amount] [decimal](10, 2) NULL,
	[AmountTypeID] [int] NOT NULL,
	[Description] [varchar](250) NOT NULL,
	LocalID INTEGER PRIMARY KEY NULL
)