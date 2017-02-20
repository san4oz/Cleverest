IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
where TABLE_CATALOG = 'Cleverest'and
TABLE_NAME = 'Teams')
  alter table Teams
  add [Description] nvarchar(500) null  
ELSE
create table Teams(
	Id nvarchar(100) not null primary key,
	Name nvarchar(250) not null,
	[Description] nvarchar(500) null,
	OwnerId nvarchar(100) not null
)