create table UserProfile
(
    UserId INT IDENTITY(1,1),
    DisplayName NVARCHAR(100) not null CONSTRAINT DF_UserProfile_DisplayName DEFAULT 'Guest',
    FirstName NVARCHAR(50) not null,
    LastName NVARCHAR(50) not null,
    Email NVARCHAR(100) not null,
    AdObjId NVARCHAR(128) NOT NULL,
    CONSTRAINT PK_UserProfile_UserId PRIMARY KEY (UserId)
);
