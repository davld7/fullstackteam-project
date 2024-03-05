CREATE TABLE [BusinessData].[Transactions]
(
	[TransactionUUID] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL ,
	[BusinessId] INT NOT NULL,
	[NTrasaction] NVARCHAR(50) NOT NULL,
	[SubTotal] DECIMAL(18,2) NOT NULL,
	[Iva] DECIMAL(5,2) NOT NULL,
	[AmountTotal] DECIMAL(18,2) NOT NULL,
	[PaymentTypeId] INT NOT NULL,
	[CurrencyId] INT NOT NULL,
	[TransactionDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
)
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_Business] ON [BusinessData].[Transactions] ([BusinessId]);
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_NTransaction] ON [BusinessData].[Transactions] ([NTrasaction]);
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_PaymentType] ON [BusinessData].[Transactions] ([PaymentTypeId]);
GO
CREATE NONCLUSTERED INDEX [IX_Transaction_TransactionDate] ON [BusinessData].[Transactions] ([TransactionDate]);

