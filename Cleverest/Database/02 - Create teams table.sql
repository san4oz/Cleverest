create table Teams(
	Id nvarchar(100) not null primary key,
	Name nvarchar(250) not null,
	[Description] nvarchar(max) null,
	OwnerId nvarchar(100) not null
);