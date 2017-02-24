create table Questions(
	Id nvarchar(100) not null primary key,
	QuestionBody nvarchar(max) not null,
	GameId nvarchar(100) not null,
	RoundNo int not null,
	OrderNo int not null,
	CorrectAnswer nvarchar(max) not null
	foreign key(GameId) references Games(Id)
);