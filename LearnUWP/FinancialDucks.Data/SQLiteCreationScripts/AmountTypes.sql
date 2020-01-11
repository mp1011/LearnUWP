CREATE TABLE [AmountTypes](
	[ID] [int PRIMARY KEY] NOT NULL,
	[Name] [varchar](250) NOT NULL);

INSERT INTO AmountTypes VALUES(1,'Exact');
INSERT INTO AmountTypes VALUES(2,'Percent');