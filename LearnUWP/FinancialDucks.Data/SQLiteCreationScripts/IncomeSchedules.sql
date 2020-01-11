CREATE TABLE [IncomeSchedules]
(
	[ID] [int] NULL,
	[PaycheckID] [int] NOT NULL,
	[DepositBankID] [int] NOT NULL,
	[PayCycleID] [int] NOT NULL,
	[FirstPaymentDate] [datetime] NOT NULL,
	[LastPaymentDate] [datetime] NULL,
	LocalID INTEGER PRIMARY KEY NULL
)