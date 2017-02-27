create table Games(
	Id nvarchar(100) not null primary key,
	Title nvarchar(250) not null,
	GameDate datetime null,
	Location nvarchar(150) null,
	MaxTeamCount int null	
)