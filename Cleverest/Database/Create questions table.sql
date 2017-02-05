create table Questions(
	Id varchar(100) not null primary key,
	Question varchar(1000) not null,
	Answer varchar(1000) null,
	GameId nvarchar(100) not null,
	RoundNo int not null,
	ImageUrl varchar(300) null,
	SongUrl varchar(300) null
)