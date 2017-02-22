CREATE TABLE Scores(
	Id nvarchar(100) not null primary key,
	GameId nvarchar(100) not null,
	TeamId nvarchar(100) not null,
	RoundNo int not null,
	Score decimal not null
);	
