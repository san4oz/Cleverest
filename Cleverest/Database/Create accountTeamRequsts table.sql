create table AccountTeamRequests(
	Id nvarchar(100) not null primary key,
	FromId nvarchar(100) not null,
	ToId nvarchar(100) not null,
	TeamId nvarchar(100) not null,
	RequestType int not null,
	FOREIGN KEY (FromId) REFERENCES Accounts(Id),
	FOREIGN KEY (ToId) REFERENCES Accounts(Id),
	FOREIGN KEY (TeamId) REFERENCES Teams(Id)
);