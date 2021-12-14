use ADONET_TASKDB


GO
CREATE TABLE Providers (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    [Address] NVARCHAR(50) NOT NULL,
    [Years] INT NOT NULL,
    PRIMARY KEY (Id)
);


GO
CREATE TABLE Subscribers (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [Phone] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
    PRIMARY KEY (Id)
);


GO
CREATE TABLE Locations (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Region] NVARCHAR(50) NOT NULL,
    [Country] NVARCHAR(50) NOT NULL,
    [City] NVARCHAR(50) NOT NULL,
    [Zip] NVARCHAR(5) NOT NULL,
    [Latitude] DECIMAL(8,6) NOT NULL,
    [Longitude] DECIMAL(9,6) NOT NULL,
	[SubscriberId] INT,
    PRIMARY KEY (Id),
	CONSTRAINT FK_SubscriberLocation FOREIGN KEY (SubscriberId) REFERENCES Subscribers(Id) ON DELETE SET NULL
);


GO
CREATE TABLE ProviderAssignments (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Rank] INT NOT NULL,
    [Trade] NVARCHAR(50) NOT NULL,
	[ProviderId] INT,
	[LocationId] INT,
    PRIMARY KEY (Id),
	CONSTRAINT FK_AssignmentProvider FOREIGN KEY (ProviderId) REFERENCES Providers(Id) ON DELETE SET NULL,
	CONSTRAINT FK_LocationProvider FOREIGN KEY (LocationId) REFERENCES Locations(Id) ON DELETE SET NULL
);


GO
INSERT INTO Providers ([Name], [Address], [Years])
VALUES  
		('ProviderName1','Addres1', 1),
		('ProviderName2','Addres2', 5),
		('ProviderName3','Addres3', 8),
		('ProviderName4','Addres4', 10),
		('ProviderName5','Addres5', 20)


GO
INSERT INTO Subscribers ([FirstName], [LastName], [Email], [Phone])
VALUES  
		('Grady', 'Ross', 'ross123@gmail.com', '375291112233'),
		('Ulysses', 'Harris', 'uly123@gmail.com', '375294445566'),
		('Yusif', 'Wilson', 'yusif1999@gmail.com', '375298889911'),
		('Polina', 'Belyakova', 'polina44@gmail.com', '375297456622'),
		('Victoria', 'Lopez', 'victoria.lop@gmail.com', '375298467336')


GO
INSERT INTO Locations ([Region], [Country], [City], [Zip], [Latitude], [Longitude], [SubscriberId])
VALUES
		('PA', 'Luzerne', 'Hazleton', '18201', 40.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 41.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 43.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 44.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 45.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 46.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 47.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 48.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 49.951319, -75.272970, 1),
		('PA', 'Luzerne', 'Hazleton', '18201', 50.951319, -75.272970, 1),
		('NY', 'Suffolk County', 'West Islip', '11795', 40.951319, -75.972970, 2),
		('WA', 'Benton', 'Kennewick', '99337', 40.951319, -75.972970, 3),
		('AZ', 'Maricopa', 'Chandler', '85224', 40.951319, -75.972970, 4),
		('MD', 'Prince Georges', 'Beltsville', '20705', 40.951319, -75.972970, 5)


GO
INSERT INTO ProviderAssignments ([Rank], [Trade], [LocationId], [ProviderId])
VALUES 
		(1, 'Doors', 1, 1),
		(10, 'Doors', 1, 2),
		(11, 'Doors', 1, 3),
		(12, 'Doors', 1, 4),
		(15, 'Doors', 1, 5),

		(1, 'Roof Leaks', 2, 4),
		(2, 'Roof Leaks', 2, 5),
		(1, 'Dry Ice', 2, 5),

		(1, 'Rice', 3, 1),
		(1, 'Sugar', 3, 1),
		(1, 'Meat', 3, 1),

		(1, 'Iron', 3, 5),
		(1, 'Copper', 3, 5),
		(1, 'Nickel', 3, 5),

		(1, 'Paper', 5, 3),
		(1, 'Books', 5, 4)


GO
CREATE PROCEDURE sp_GetLocationById
		@LocationId INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT * 
	FROM Locations
	WHERE Id = @LocationId
END


GO
CREATE PROCEDURE sp_GetLocations
		@SubscriberId INT,
		@Page INT,
		@PageSize INT
AS
BEGIN
	SET NOCOUNT ON
	IF(@Page <=0)
		BEGIN
		SET @Page = 1;
		END
		IF(@PageSize <= 0)
		BEGIN
		SET @PageSize = 2147483647;
		END
		DECLARE @SkipRows int = (@Page - 1) * @PageSize;

	WITH OrderedSet AS
	(
		SELECT *, 
		ROW_NUMBER() OVER (ORDER BY [Id] DESC) as [Index]
		FROM Locations
	)

	SELECT * 
	FROM OrderedSet 
	ORDER BY [Index]
	OFFSET @SkipRows ROWS 
			FETCH NEXT @PageSize ROWS ONLY
END


GO
CREATE PROCEDURE sp_GetLocationAssigments
		@LocationId INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT *
	FROM ProviderAssignments
	WHERE LocationId = @LocationId
	ORDER BY Id
END


GO
CREATE PROCEDURE sp_GetLocationAssigmentForProvider
		@LocationId INT,
		@ProviderId INT
AS
BEGIN
	SET NOCOUNT ON
	SELECT *
	FROM ProviderAssignments
	WHERE LocationId = @LocationId AND ProviderId = @ProviderId
	ORDER BY Id
END


GO
CREATE PROCEDURE sp_CreateLocation
    @Region NVARCHAR(50),
    @Country NVARCHAR(50),
    @City NVARCHAR(50),
    @Zip NVARCHAR(5),
    @Latitude DECIMAL(8,6),
    @Longitude DECIMAL(9,6),
    @SubscriberId INT
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Locations([Region], [Country], [City], [Zip], [Latitude], [Longitude], [SubscriberId])
	VALUES (@Region, @Country, @City, @Zip, @Latitude, @Longitude, @SubscriberId)
END

GO
CREATE PROCEDURE sp_UpdateLocation
	@Id INT, 
    @Region NVARCHAR(50),
    @Country NVARCHAR(50),
    @City NVARCHAR(50),
    @Zip NVARCHAR(5),
    @Latitude DECIMAL(8,6),
    @Longitude DECIMAL(9,6),
    @SubscriberId INT
AS
BEGIN
	SET NOCOUNT ON
	UPDATE Locations
	SET Region = @Region, Country = @Country, City = @City, Zip = @Zip,
		Latitude = @Latitude, Longitude = @Longitude, SubscriberId = @SubscriberId
	WHERE Id = @Id
END


GO
CREATE PROCEDURE sp_DeleteLocation
		@Id INT
AS
BEGIN
	DELETE FROM Locations
	WHERE Id = @Id
END