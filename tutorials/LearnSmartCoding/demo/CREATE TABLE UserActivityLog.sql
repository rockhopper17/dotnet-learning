CREATE TABLE UserActivityLog
(
LogId INT IDENTITY(1,1),
UserId INT,
ActivityType NVARCHAR(50) NOT NULL,
ActivityDescription NVARCHAR(MAX),
LogDate DATETIME NOT NULL,
CONSTRAINT PK_UserActivityLog_LogId PRIMARY KEY (LogId),
CONSTRAINT FK_UserActivityLog_UserProfile FOREIGN KEY (UserId) REFERENCES UserProfile(UserId)
)