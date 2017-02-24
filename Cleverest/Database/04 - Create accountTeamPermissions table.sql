create table AccountTeamPermissions(
	Id nvarchar(100) not null PRIMARY KEY,
	TeamId nvarchar(100) not null,
	AccountId nvarchar(100) not null,
	FOREIGN KEY (TeamId) REFERENCES Teams(Id),
	FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
);