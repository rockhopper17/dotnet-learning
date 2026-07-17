-- CREATE TABLE UserProfile
-- (
--     UserId INT IDENTITY(1,1),
--     DisplayName NVARCHAR(100) not NULL CONSTRAINT DF_UserProfile_DisplayName DEFAULT 'Guest',
--     FirstName NVARCHAR(50) NOT NULL,
--     LastName NVARCHAR(50) NOT NULL,
--     Email NVARCHAR(100) NOT NULL,
--     AdObjId NVARCHAR(128) NOT NULL,
--     CONSTRAINT PK_UserProfile_UserId PRIMARY KEY (UserId)
-- )

use Learning
go

INSERT into UserProfile([DisplayName],[FirstName],[LastName],[Email],[AdObjId])
VALUES ('','Learn Smart','Coding','learning@gmail.com','asdjf')
go

SELECT * FROM UserProfile
go

UPDATE UserProfile SET DisplayName='Karthik' WHERE UserId=2
go