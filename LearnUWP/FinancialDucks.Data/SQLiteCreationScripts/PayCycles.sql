CREATE TABLE [PayCycles]
(
	[ID] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL
);

INSERT INTO PayCycles Values(1,	'Weekly');
INSERT INTO PayCycles Values(2,	'Biweekly');
INSERT INTO PayCycles Values(3,	'FirstOfTheMonth');
INSERT INTO PayCycles Values(4,	'FirstAndFifteenthOfTheMonth');
INSERT INTO PayCycles Values(5,	'EndOfTheMonth');