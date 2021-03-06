create table Accounts(
	[Id] nvarchar(100) not null primary key,
	[TeamId] nvarchar(100) null,
	[Email] nvarchar(250) not null,
	[Name] nvarchar(250) not null,
	[PhoneNumber] nvarchar(250),
	[SocialNetworkLink] nvarchar(150),
	[Password] nvarchar(250) not null,
	[PasswordSalt] nvarchar(150) not null,
	FOREIGN KEY (TeamId) REFERENCES Teams(Id)
);