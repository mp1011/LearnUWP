CREATE TABLE [RecurrenceTypes]
(
	[ID] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL
);

insert into recurrencetypes values(1,	'OneTime');
insert into recurrencetypes values(2,	'Weekly');
insert into recurrencetypes values(3,	'Biweekly');
insert into recurrencetypes values(4,	'Monthly');
insert into recurrencetypes values(5,	'Annual');