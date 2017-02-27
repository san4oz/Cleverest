create table GameRegistrationRequests(
	Id nvarchar(100) not null primary key,
	GameId nvarchar(100) not null,
	AccountId nvarchar(100) not null,
	[Date] datetime not null,
	foreign key(GameId) references Games(Id),
	foreign key(AccountId) references Accounts(Id)
);