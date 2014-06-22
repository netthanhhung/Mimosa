/**********************************************************************************
CHANGED OBJECT LIST
HT
[Booking] : Add Customer2Id column
[procListBooking] : alter
[procSaveBooking] : alter
[procListCustomer] : alter proc
[procListSiteBySiteGroup] : add

Equipment, Service : add Unit column
[procListEquipment]: alter
[procListService]: alter
[procSaveEquipment]: alter
[procSaveService]: alter
[procListRoomEquipment] : alter
[procListRoomService] : alter
[procListBookingRoomEquipment] : alter
[procListBookingRoomService] : alter
[BookingRoomEquipmentDetail] : new table
[BookingRoomServiceDetail] : new table
[procDeleteBookingRoomEquipmentDetail]: new
[procDeleteBookingRoomServiceDetail]: new
[procListBookingRoomEquipmentDetail]: new
[procListBookingRoomServiceDetail]: new
[procSaveBookingRoomEquipmentDetail]: new
[procSaveBookingRoomServiceDetail]: new

ContactInformation : add DoB, Visa, VisaValidFrom, VisaValidTo
[procListContactInformation]: alter
[procSaveContactInformation]: alter

[procListAspUser] : alter
[procUpdateAspUserOrganisationId] : new

[BookingPayment] : new table
[procSaveBookingPayment] : new proc
[ufnGetTotalEquipmentPrice] : new function
[ufnGetTotalServicePrice] : new function
[procListBookingPayment] : new proc

[procCheckExistBooking] : new proc
*************************************************************************************/


/***********************HT Start*******************************/

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomEquipment_RoomEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomEquipment]'))
ALTER TABLE [dbo].[BookingRoomEquipment] DROP CONSTRAINT [FK_BookingRoomEquipment_RoomEquipment]
GO

GO
EXEC sp_rename 'dbo.BookingRoomEquipment.RoomEquipmentId', 'EquipmentId', 'COLUMN';
GO

update BookingRoomEquipment Set EquipmentId = 1
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomEquipment_Equipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomEquipment]'))
ALTER TABLE [dbo].[BookingRoomEquipment] DROP CONSTRAINT [FK_BookingRoomEquipment_Equipment]
GO


ALTER TABLE [dbo].[BookingRoomEquipment]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomEquipment_Equipment] FOREIGN KEY([EquipmentId])
REFERENCES [dbo].[Equipment] ([EquipmentId])
GO

ALTER TABLE [dbo].[BookingRoomEquipment] CHECK CONSTRAINT [FK_BookingRoomEquipment_Equipment]
GO




IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomService_RoomService]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomService]'))
ALTER TABLE [dbo].[BookingRoomService] DROP CONSTRAINT [FK_BookingRoomService_RoomService]
GO

GO
EXEC sp_rename 'dbo.BookingRoomService.RoomServiceId', 'ServiceId', 'COLUMN';
GO

update BookingRoomService Set ServiceId = 1
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomService_Service]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomService]'))
ALTER TABLE [dbo].[BookingRoomService] DROP CONSTRAINT [FK_BookingRoomService_Service]
GO


ALTER TABLE [dbo].[BookingRoomService]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomService_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([ServiceId])
GO

ALTER TABLE [dbo].[BookingRoomService] CHECK CONSTRAINT [FK_BookingRoomService_Service]
GO




IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Booking' AND COLUMN_NAME = 'Customer2Id')
BEGIN
   ALTER TABLE Booking ADD Customer2Id int
END
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Booking_Customer2]') AND parent_object_id = OBJECT_ID(N'[dbo].[Booking]'))
ALTER TABLE [dbo].[Booking] DROP CONSTRAINT [FK_Booking_Customer2]
GO

ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Customer2] FOREIGN KEY([Customer2Id])
REFERENCES [dbo].[Customer] ([CustomerId])
GO

ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Customer2]
GO


ALTER PROCEDURE [dbo].[procListBooking]	
@OrganisationId int	
, @SiteId int = null
, @RoomId int = null
, @RoomName varchar(128) = null
, @BookingId int = null
, @BookingStatusIds varchar(128) = null
, @CustomerId int = null
, @CustomerName varchar(128) = null
, @BookDateStart smalldatetime = null
, @BookDateEnd smalldatetime = null
AS
BEGIN

	
	DECLARE @_BookingStatusId TABLE
	(
	BookingStatusId int
	, BookingStatus varchar(128)
	)
	IF @BookingStatusIds IS NULL 
	BEGIN
		INSERT INTO @_BookingStatusId
		SELECT BookingStatusId, Name
		FROM BookingStatus
	END ELSE BEGIN 
		INSERT INTO @_BookingStatusId
		SELECT BookingStatusId, Name
		FROM BookingStatus B
		INNER JOIN dbo.ufnSplitNumeric(',', @BookingStatusIds) N on B.BookingStatusId = N.Number	
	END
	
	SELECT TOP 1000
	B.[BookingId]
	,S.SiteID, S.Name as SiteName
	,R.RoomId, R.RoomName
	,B.[CustomerId]
	,C.FirstName
	,C.LastName
	,C.FirstName + ' ' + C.LastName as CustomerName
	,B.[Customer2Id]
	,C2.FirstName + ' ' + C2.LastName as Customer2Name
	,B.[BookDate]
	,B.[BookingStatusId]
	,BS.BookingStatus
	,B.[Description]
	,B.[RoomPrice]
	,B.[TotalPrice]
	,B.[ContractDateSign]
	,B.[ContractDateStart]
	,B.[ContractDateEnd]
	,B.[ContractTotalPrice]
	,B.[Concurrency]
	,B.[DateCreated]
	,B.[DateUpdated]
	,B.[CreatedBy]
	,B.[UpdatedBy]
	FROM [dbo].[Booking] B
	INNER JOIN Room R on B.RoomId = R.RoomId
	INNER JOIN Site S on R.SiteID = S.SiteID
	INNER JOIN @_BookingStatusId BS on B.BookingStatusId = BS.BookingStatusId
	LEFT OUTER JOIN Customer C on C.CustomerId = B.CustomerId
	LEFT OUTER JOIN Customer C2 on C2.CustomerId = B.Customer2Id
	WHERE S.OrganisationID = @OrganisationId
	AND (@SiteId IS NULL OR S.SiteID = @SiteId)
	AND (@BookingId IS NULL OR B.BookingId = @BookingId)
	AND (@SiteId IS NULL OR S.SiteID = @SiteId)
	ANd (@RoomName IS NULL OR (R.RoomName like '%' + @RoomName + '%'))
	AND (@CustomerId IS NULL OR B.CustomerId = @CustomerId OR B.Customer2Id = @CustomerId)
	ANd (@CustomerName IS NULL OR (C.FirstName like '%' + @CustomerName + '%')
		OR (C.LastName like '%' + @CustomerName + '%')
		OR (C2.FirstName like '%' + @CustomerName + '%')
		OR (C2.LastName like '%' + @CustomerName + '%'))
	AND (@BookDateStart IS NULL OR B.BookDate >= @BookDateStart)
	AND (@BookDateEnd IS NULL OR B.BookDate <= @BookDateEnd)
	ORDER BY B.BookDate DESC, S.DisplayIndex, R.RoomName, BS.BookingStatusId

	-- ======================================================================
	-- Change History
	-- ======================================================================
	-- 20-May-2014 HT
	-- Add Customer2Id
	
END
GO


/****** Object:  StoredProcedure [dbo].[procSaveBooking]    Script Date: 05/21/2014 10:09:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBooking]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBooking]
GO

CREATE PROCEDURE [dbo].[procSaveBooking]	
@BookingID int OUTPUT,
@RoomId int,
@CustomerId int,
@Customer2Id int,
@BookDate smalldatetime,	
@BookingStatusId int,
@Description varchar(max),
@RoomPrice decimal(20, 8),
@TotalPrice decimal(20, 8),
@ContractDateSign smalldatetime,
@ContractDateStart smalldatetime,
@ContractDateEnd smalldatetime,
@ContractTotalPrice decimal(20, 8),	
@CurrentUser varchar(128)
AS
BEGIN
	SET NOCOUNT ON

	IF @BookingID IS NULL BEGIN

		INSERT INTO Booking(
		RoomId,
		CustomerId,
		Customer2Id,
		BookDate,	
		BookingStatusId,
		Description,
		RoomPrice,
		TotalPrice,
		ContractDateSign,
		ContractDateStart,
		ContractDateEnd,
		ContractTotalPrice,	
		DateCreated,
		DateUpdated,
		CreatedBy,
		UpdatedBy
		) VALUES (
		@RoomId,
		@CustomerId,
		@Customer2Id,
		@BookDate,	
		@BookingStatusId,
		@Description,
		@RoomPrice,
		@TotalPrice,
		@ContractDateSign,
		@ContractDateStart,
		@ContractDateEnd,
		@ContractTotalPrice,	
		GETDATE(),
		GETDATE(),
		@CurrentUser,
		@CurrentUser
		)

		SET @BookingID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE Booking SET
		RoomId = @RoomId,
		CustomerId = @CustomerId,
		Customer2Id = @Customer2Id,
		BookDate = @BookDate,	
		BookingStatusId = @BookingStatusId,
		Description = @Description,
		RoomPrice = @RoomPrice,
		TotalPrice = @TotalPrice,
		ContractDateSign = @ContractDateSign,
		ContractDateStart = @ContractDateStart,
		ContractDateEnd = @ContractDateEnd,
		ContractTotalPrice = @ContractTotalPrice,	
		DateUpdated = GETDATE(),
		UpdatedBy = @CurrentUser

		WHERE BookingID = @BookingID

	END

	SELECT Concurrency FROM Booking WHERE BookingID = @BookingID
END

GO

/****** Object:  StoredProcedure [dbo].[procListCustomer]    Script Date: 05/30/2014 15:38:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListCustomer]
GO


CREATE PROCEDURE [dbo].[procListCustomer]	
@OrganisationId int = null
, @CustomerId int = null
, @FirstName varchar(125)
, @LastName varchar(125)
, @SiteId int
, @HasContracts bit
, @ContractDateStart smalldatetime = null
, @ContractDateEnd smalldatetime = null
, @ShowLegacy bit
AS
BEGIN
	SET NOCOUNT ON
	IF(@HasContracts = 1) 
	BEGIN
		SELECT top 1000	
			C.CustomerID
			, C.OrganisationId
			, S.Name as SiteName
			, R.RoomName
			, C.[FirstName]
			, C.[LastName]
			, C.[IsLegacy]
			, C.[Gender]
			, C.[Age]	
			, C.[ContactInformationId]
			, C.Concurrency, C.DateCreated, C.DateUpdated, C.CreatedBy, C.UpdatedBy
		FROM	Customer C
		INNER JOIN Booking B on (B.CustomerId = C.CustomerId OR B.Customer2Id = C.CustomerId)
		INNER JOIN Room R on R.RoomId = B.RoomId	
		INNER JOIN Site S on S.SiteID = R.SiteId	
		WHERE	(@OrganisationId IS NULL OR C.OrganisationId = @OrganisationId)
		AND (@CustomerId is null OR C.CustomerId = @CustomerId)
		AND	(@FirstName IS NULL OR C.FirstName like '%' + @FirstName + '%' OR C.LastName like '%' + @FirstName + '%')
		AND	(@LastName IS NULL OR C.LastName like '%' + @LastName + '%'OR C.FirstName like '%' + @LastName  + '%')
		AND (@ShowLegacy = 1 OR C.IsLegacy = 0)
		AND  B.BookingStatusId = 3 AND B.ContractDateStart IS NOT NULL
		AND (@SiteId is null OR R.SiteId = @SiteId)
		AND (@ContractDateStart IS NULL AND @ContractDateEnd IS NULL
			 OR @ContractDateStart IS NULL AND @ContractDateEnd IS NOT NULL AND @ContractDateEnd >= B.ContractDateStart
			 OR @ContractDateStart IS NOT NULL AND @ContractDateEnd IS NULL AND (B.ContractDateEnd IS NULL OR @ContractDateStart <= B.ContractDateEnd)
			 OR @ContractDateStart IS NOT NULL AND @ContractDateEnd IS NOT NULL 
				AND (@ContractDateEnd >= B.ContractDateStart AND (B.ContractDateEnd IS NULL OR @ContractDateStart <= B.ContractDateEnd))
			)				

		ORDER BY B.ContractDateStart DESC, C.[FirstName], C.[LastName]
	END
	ELSE
	BEGIN
	
		SELECT top 1000	
			CustomerID
			, OrganisationId
			, NULL as SiteName
			, NULL as RoomName
			, [FirstName]
			, [LastName]
			, [IsLegacy]
			, [Gender]
			, [Age]	
			, [ContactInformationId]
			, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
		FROM	Customer
		WHERE	(@OrganisationId IS NULL OR OrganisationId = @OrganisationId)
		AND (@CustomerId is null OR CustomerId = @CustomerId)
		AND	(@FirstName IS NULL OR FirstName like '%' + @FirstName + '%' OR LastName like '%' + @FirstName + '%')
		AND	(@LastName IS NULL OR LastName like '%' + @LastName + '%'OR FirstName like '%' + @LastName  + '%')
		AND (@ShowLegacy = 1 OR IsLegacy = 0)
		ORDER BY [FirstName], [LastName]
	END
	
	-- ======================================================================
	-- Change History
	-- ======================================================================
	-- 20-May-2014 HT
	-- Add OrganisationId
END
GO


/****** Object:  StoredProcedure [dbo].[procListSiteBySiteGroup]    Script Date: 05/29/2014 16:48:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListSiteBySiteGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListSiteBySiteGroup]
GO


CREATE PROCEDURE [dbo].[procListSiteBySiteGroup]	
	@SiteGroupId int
	, @ShowLegacy bit = null
AS
BEGIN
SET NOCOUNT ON

	SELECT S.[SiteID]
	,S.[OrganisationID]
	,S.[HotelID]
	,S.[Name]
	,S.[AbbreviatedName]
	,S.[ContactInformationID]
	,S.[LicenseKey]
	,S.[StarRating]
	,S.[PropCode]
	,S.[DisplayIndex]
	,S.[IsLegacy]
	,S.[Availability]
	,S.[Concurrency]
	,S.[DateCreated]
	,S.[DateUpdated]
	,S.[CreatedBy]
	,S.[UpdatedBy]
	FROM dbo.Site AS S
	INNER JOIN SiteGroupSite SGS on S.SiteID = SGS.SiteId
	WHERE (@SiteGroupId IS NULL OR @SiteGroupId = SGS.SiteGroupId)
	AND	(@ShowLegacy = 1 OR S.IsLegacy = 0)
	ORDER BY DisplayIndex	

-- ======================================================================
-- Change History
-- ======================================================================
-- 21-Feb-2014 HT
-- INIT
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Equipment' AND COLUMN_NAME = 'Unit')
BEGIN
   ALTER TABLE Equipment ADD Unit varchar(128)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Service' AND COLUMN_NAME = 'Unit')
BEGIN
   ALTER TABLE Service ADD Unit varchar(128)
END
GO


/****** Object:  StoredProcedure [dbo].[procListEquipment]    Script Date: 05/29/2014 17:01:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListEquipment]
GO


CREATE PROCEDURE [dbo].[procListEquipment]	
@OrganisationId int = null,
@EquipmentId int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.EquipmentId, RE.OrganisationId, RE.EquipmentName,
RE.IsLegacy, RE.Description, RE.RealPrice, RE.RentPrice, RE.Unit,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	Equipment RE
WHERE	(@EquipmentId is null OR RE.EquipmentId = @EquipmentId)
AND	(@OrganisationId is null OR RE.OrganisationId = @OrganisationId)
AND	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END

GO



/****** Object:  StoredProcedure [dbo].[procListService]    Script Date: 05/29/2014 17:01:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListService]
GO


CREATE PROCEDURE [dbo].[procListService]	
@OrganisationId int = null,
@ServiceId int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.ServiceId, RE.OrganisationId, RE.Name,
RE.IsLegacy, RE.Description, RE.Price, RE.Unit,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	Service RE
WHERE	(@ServiceId is null OR RE.ServiceId = @ServiceId)
AND	(@OrganisationId is null OR RE.OrganisationId = @OrganisationId)
AND	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END

GO




/****** Object:  StoredProcedure [dbo].[procSaveEquipment]    Script Date: 05/29/2014 17:02:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveEquipment]
GO


CREATE PROCEDURE [dbo].[procSaveEquipment]

@EquipmentId int OUTPUT,
@OrganisationId int,
@EquipmentName varchar(50),
@Description varchar(128),
@IsLegacy bit,
@RealPrice decimal(20,8),
@RentPrice decimal(20,8),
@Unit varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

	IF @EquipmentID IS NULL BEGIN

	INSERT INTO Equipment(
		OrganisationId,
		EquipmentName ,
		Description,
		IsLegacy,
		RealPrice,
		RentPrice,
		Unit,
		DateCreated,
		DateUpdated,
		CreatedBy,
		UpdatedBy
	) VALUES (
		@OrganisationId,
		@EquipmentName ,
		@Description,
		@IsLegacy,
		@RealPrice,
		@RentPrice,
		@Unit,
		GETDATE(),
		GETDATE(),
		@CurrentUser,
		@CurrentUser
	)

	SET @EquipmentId = SCOPE_IDENTITY()

	END ELSE BEGIN

	UPDATE Equipment SET
		OrganisationId = @OrganisationId,
		EquipmentName = @EquipmentName ,
		Description = @Description,
		IsLegacy = @IsLegacy,
		RealPrice = @RealPrice,
		RentPrice = @RentPrice,
		Unit = @Unit,
		DateUpdated = GETDATE(),
		UpdatedBy = @CurrentUser

	WHERE EquipmentId = @EquipmentId

END

SELECT Concurrency FROM Equipment WHERE EquipmentId = @EquipmentId
END

GO



/****** Object:  StoredProcedure [dbo].[procSaveService]    Script Date: 05/29/2014 17:03:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveService]
GO


CREATE PROCEDURE [dbo].[procSaveService]

@ServiceId int OUTPUT,
@OrganisationId int,
@Name varchar(50),
@Description varchar(128),
@IsLegacy bit,
@Price decimal(20,8),
@Unit varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

	IF @ServiceID IS NULL BEGIN

	INSERT INTO Service(
		OrganisationId,
		Name ,
		Description,
		IsLegacy,
		Price,
		Unit,
		DateCreated,
		DateUpdated,
		CreatedBy,
		UpdatedBy
	) VALUES (
		@OrganisationId,
		@Name ,
		@Description,
		@IsLegacy,
		@Price,
		@Unit,
		GETDATE(),
		GETDATE(),
		@CurrentUser,
		@CurrentUser
	)

	SET @ServiceId = SCOPE_IDENTITY()

	END ELSE BEGIN

	UPDATE Service SET
		OrganisationId = @OrganisationId,
		Name = @Name ,	
		Description = @Description,
		IsLegacy = @IsLegacy,
		Price = @Price,
		Unit = @Unit,
		DateUpdated = GETDATE(),
		UpdatedBy = @CurrentUser

	WHERE ServiceId = @ServiceId

END

SELECT Concurrency FROM Service WHERE ServiceId = @ServiceId
END

GO



/****** Object:  StoredProcedure [dbo].[procListRoomEquipment]    Script Date: 06/22/2014 15:28:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListRoomEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListRoomEquipment]
GO


CREATE PROCEDURE [dbo].[procListRoomEquipment]	
@RoomEquipmentId int = null,
@RoomId int = null
AS
BEGIN
SET NOCOUNT ON

	SELECT	RE.RoomEquipmentId, RE.RoomId, RE.EquipmentId, e.EquipmentName as Equipment,
		RE.Price, RE.Description, E.Unit,
		RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
	FROM	RoomEquipment RE
	INNER JOIN Equipment E on RE.EquipmentId = e.EquipmentId
	WHERE	(@RoomEquipmentId is null OR RE.RoomEquipmentId = @RoomEquipmentId)
	AND	(@RoomId is null OR RE.RoomId = @RoomId)

END

GO




/****** Object:  StoredProcedure [dbo].[procListRoomService]    Script Date: 05/29/2014 17:06:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListRoomService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListRoomService]
GO


CREATE PROCEDURE [dbo].[procListRoomService]	
@RoomServiceId int = null,
@RoomId int = null
AS
BEGIN
SET NOCOUNT ON

	SELECT	RE.RoomServiceId, RE.RoomId, RE.ServiceId, E.Name as Service,
		RE.Price, RE.Description, E.Unit,
		RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
	FROM	RoomService RE
	INNER JOIN Service E on RE.ServiceId = e.ServiceId
	WHERE	(@RoomServiceId is null OR RE.RoomServiceId = @RoomServiceId)
	AND	(@RoomId is null OR RE.RoomId = @RoomId)

END

GO



/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomEquipment]    Script Date: 06/05/2014 16:59:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBookingRoomEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBookingRoomEquipment]
GO

CREATE PROCEDURE [dbo].[procSaveBookingRoomEquipment]

@BookingRoomEquipmentId int OUTPUT,
@BookingId int,
@EquipmentId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
	BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM BookingRoomEquipment WHERE BookingId = @BookingId AND EquipmentId = @EquipmentId)
	BEGIN

		INSERT INTO BookingRoomEquipment(
		BookingId,
		EquipmentId,
		Price,
		Description,	
		DateCreated,
		DateUpdated,
		CreatedBy,
		UpdatedBy
		) VALUES (
		@BookingId,
		@EquipmentId,
		@Price,
		@Description,
		GETDATE(),
		GETDATE(),
		@CurrentUser,
		@CurrentUser
		)

		SET @BookingRoomEquipmentId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE BookingRoomEquipment SET
		BookingId = @BookingId,
		EquipmentId = @EquipmentId,
		Price = @Price,
		Description = @Description,	
		DateUpdated = GETDATE(),
		UpdatedBy = @CurrentUser

		WHERE BookingId = @BookingId AND EquipmentId = @EquipmentId

	END

	SELECT Concurrency FROM BookingRoomEquipment WHERE BookingId = @BookingId AND EquipmentId = @EquipmentId
END

GO


/****** Object:  StoredProcedure [dbo].[procListBookingRoomEquipment]    Script Date: 06/10/2014 16:32:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListBookingRoomEquipment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListBookingRoomEquipment]
GO


CREATE PROCEDURE [dbo].[procListBookingRoomEquipment]	
@BookingRoomEquipmentId int = null,
@BookingId int = null,
@EquipmentId int = null
AS
BEGIN
	SET NOCOUNT ON

	SELECT	BRE.BookingRoomEquipmentId, BRE.BookingId, BRE.EquipmentId, E.EquipmentName as Equipment, E.Unit,
		BRE.Price, BRE.Description,
		CASE WHEN (SELECT Count(*) FROM BookingRoomEquipmentDetail WHERE BookingRoomEquipmentId = BRE.BookingRoomEquipmentId) > 0
		THEN 0 ELSE 1 END as CanDelete,
		BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
	FROM	BookingRoomEquipment BRE
	INNER JOIN Equipment E on BRE.EquipmentId = E.EquipmentId
	WHERE	(@BookingRoomEquipmentId is null OR BRE.BookingRoomEquipmentId = @BookingRoomEquipmentId)
	AND	(@BookingId is null OR BRE.BookingId = @BookingId)
	AND	(@EquipmentId is null OR BRE.EquipmentId = @EquipmentId)

END
GO




/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomService]    Script Date: 06/05/2014 16:59:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBookingRoomService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBookingRoomService]
GO

CREATE PROCEDURE [dbo].[procSaveBookingRoomService]

@BookingRoomServiceId int OUTPUT,
@BookingId int,
@ServiceId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
	BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM BookingRoomService WHERE BookingId = @BookingId AND ServiceId = @ServiceId)
	BEGIN

		INSERT INTO BookingRoomService(
		BookingId,
		ServiceId,
		Price,
		Description,	
		DateCreated,
		DateUpdated,
		CreatedBy,
		UpdatedBy
		) VALUES (
		@BookingId,
		@ServiceId,
		@Price,
		@Description,
		GETDATE(),
		GETDATE(),
		@CurrentUser,
		@CurrentUser
		)

		SET @BookingRoomServiceId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE BookingRoomService SET
		BookingId = @BookingId,
		ServiceId = @ServiceId,
		Price = @Price,
		Description = @Description,	
		DateUpdated = GETDATE(),
		UpdatedBy = @CurrentUser

		WHERE BookingId = @BookingId AND ServiceId = @ServiceId

	END

	SELECT Concurrency FROM BookingRoomService WHERE BookingId = @BookingId AND ServiceId = @ServiceId
END

GO


/****** Object:  StoredProcedure [dbo].[procListBookingRoomService]    Script Date: 05/30/2014 10:53:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListBookingRoomService]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListBookingRoomService]
GO


CREATE PROCEDURE [dbo].[procListBookingRoomService]	
@BookingRoomServiceId int = null,
@BookingId int = null,
@ServiceId int = null
AS
BEGIN
	SET NOCOUNT ON

	SELECT	BRE.BookingRoomServiceId, BRE.BookingId, BRE.ServiceId, E.Name as Service, E.Unit,
		BRE.Price, BRE.Description,
		CASE WHEN (SELECT Count(*) FROM BookingRoomServiceDetail WHERE BookingRoomServiceId = BRE.BookingRoomServiceId) > 0
		THEN 0 ELSE 1 END as CanDelete,
		BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
	FROM	BookingRoomService BRE
	INNER JOIN Service E on BRE.ServiceId = E.ServiceId
	WHERE	(@BookingRoomServiceId is null OR BRE.BookingRoomServiceId = @BookingRoomServiceId)
	AND	(@BookingId is null OR BRE.BookingId = @BookingId)
	AND	(@ServiceId is null OR BRE.ServiceId = @ServiceId)

END

GO





IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomEquipmentDetail_BookingRoomEquipment]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomEquipmentDetail]'))
ALTER TABLE [dbo].[BookingRoomEquipmentDetail] DROP CONSTRAINT [FK_BookingRoomEquipmentDetail_BookingRoomEquipment]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomEquipmentDetail_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomEquipmentDetail] DROP CONSTRAINT [DF_BookingRoomEquipmentDetail_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomEquipmentDetail_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomEquipmentDetail] DROP CONSTRAINT [DF_BookingRoomEquipmentDetail_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomEquipmentDetail_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomEquipmentDetail] DROP CONSTRAINT [DF_BookingRoomEquipmentDetail_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomEquipmentDetail_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomEquipmentDetail] DROP CONSTRAINT [DF_BookingRoomEquipmentDetail_UpdatedBy]
END

GO


CREATE TABLE [dbo].[BookingRoomEquipmentDetail](
	[BookingRoomEquipmentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[BookingRoomEquipmentId] [int] NOT NULL,
	[Quantity] [decimal](20, 8) NULL,
	[DateStart] [smalldatetime] NULL,
	[DateEnd] [smalldatetime] NULL,
	[TotalPrice] [decimal](20, 8) NULL,
	[Payment] bit NOT NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingRoomEquipmentDetail] PRIMARY KEY CLUSTERED 
(
	[BookingRoomEquipmentDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomEquipmentDetail_BookingRoomEquipment] FOREIGN KEY([BookingRoomEquipmentId])
REFERENCES [dbo].[BookingRoomEquipment] ([BookingRoomEquipmentId])
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail] CHECK CONSTRAINT [FK_BookingRoomEquipmentDetail_BookingRoomEquipment]
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail] ADD  CONSTRAINT [DF_BookingRoomEquipmentDetail_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail] ADD  CONSTRAINT [DF_BookingRoomEquipmentDetail_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail] ADD  CONSTRAINT [DF_BookingRoomEquipmentDetail_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[BookingRoomEquipmentDetail] ADD  CONSTRAINT [DF_BookingRoomEquipmentDetail_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO




IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingRoomServiceDetail_BookingRoomService]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingRoomServiceDetail]'))
ALTER TABLE [dbo].[BookingRoomServiceDetail] DROP CONSTRAINT [FK_BookingRoomServiceDetail_BookingRoomService]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomServiceDetail_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomServiceDetail] DROP CONSTRAINT [DF_BookingRoomServiceDetail_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomServiceDetail_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomServiceDetail] DROP CONSTRAINT [DF_BookingRoomServiceDetail_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomServiceDetail_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomServiceDetail] DROP CONSTRAINT [DF_BookingRoomServiceDetail_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingRoomServiceDetail_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingRoomServiceDetail] DROP CONSTRAINT [DF_BookingRoomServiceDetail_UpdatedBy]
END

GO


CREATE TABLE [dbo].[BookingRoomServiceDetail](
	[BookingRoomServiceDetailId] [int] IDENTITY(1,1) NOT NULL,
	[BookingRoomServiceId] [int] NOT NULL,
	[Quantity] [decimal](20, 8) NULL,
	[DateStart] [smalldatetime] NULL,
	[DateEnd] [smalldatetime] NULL,
	[TotalPrice] [decimal](20, 8) NULL,
	[Payment] bit NOT NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingRoomServiceDetail] PRIMARY KEY CLUSTERED 
(
	[BookingRoomServiceDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomServiceDetail_BookingRoomService] FOREIGN KEY([BookingRoomServiceId])
REFERENCES [dbo].[BookingRoomService] ([BookingRoomServiceId])
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail] CHECK CONSTRAINT [FK_BookingRoomServiceDetail_BookingRoomService]
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail] ADD  CONSTRAINT [DF_BookingRoomServiceDetail_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail] ADD  CONSTRAINT [DF_BookingRoomServiceDetail_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail] ADD  CONSTRAINT [DF_BookingRoomServiceDetail_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[BookingRoomServiceDetail] ADD  CONSTRAINT [DF_BookingRoomServiceDetail_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO



/****** Object:  StoredProcedure [dbo].[procDeleteBookingRoomEquipmentDetail]    Script Date: 05/29/2014 17:55:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteBookingRoomEquipmentDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteBookingBookingRoomEquipmentDetailDetail]
GO


CREATE PROCEDURE [dbo].[procDeleteBookingRoomEquipmentDetail]
(
@BookingRoomEquipmenDetailId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM BookingRoomEquipmentDetail WHERE BookingRoomEquipmentDetailId = @BookingRoomEquipmenDetailId

END

GO



/****** Object:  StoredProcedure [dbo].[procDeleteBookingRoomServiceDetail]    Script Date: 05/29/2014 17:55:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procDeleteBookingRoomServiceDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procDeleteBookingBookingRoomServiceDetailDetail]
GO


CREATE PROCEDURE [dbo].[procDeleteBookingRoomServiceDetail]
(
@BookingRoomEquipmenDetailId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM BookingRoomServiceDetail WHERE BookingRoomServiceDetailId = @BookingRoomEquipmenDetailId

END

GO

/****** Object:  StoredProcedure [dbo].[procListBookingRoomEquipmentDetail]    Script Date: 05/29/2014 17:56:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListBookingRoomEquipmentDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListBookingRoomEquipmentDetail]
GO


CREATE PROCEDURE [dbo].[procListBookingRoomEquipmentDetail]	
@BookingRoomEquipmentDetailId int = null,
@BookingRoomEquipmentId int = null,
@BookingId int = null,
@DateStart smalldatetime = null,
@DateEnd smalldatetime = null
AS
BEGIN
SET NOCOUNT ON

	SELECT	D.BookingRoomEquipmentDetailId, D.BookingRoomEquipmentId, E.EquipmentId, E.EquipmentName as Equipment,
			D.DateStart, D.DateEnd, D.Quantity, BRE.Price, E.Unit, D.TotalPrice, D.Payment, D.Description,			
			BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
	FROM	BookingRoomEquipmentDetail D
	INNER JOIN BookingRoomEquipment BRE on BRE.BookingRoomEquipmentId = D.BookingRoomEquipmentId
	INNER JOIN Equipment E on BRE.EquipmentId = e.EquipmentId
	WHERE	(@BookingRoomEquipmentDetailId is null OR D.BookingRoomEquipmentDetailId = @BookingRoomEquipmentDetailId)
	AND		(@BookingRoomEquipmentId is null OR BRE.BookingRoomEquipmentId = @BookingRoomEquipmentId)
	AND		(@BookingId is null OR BRE.BookingId = @BookingId)
	AND     (@DateStart IS NULL OR (D.DateStart <= @DateEnd AND D.DateEnd >= @DateStart))
	ORDER BY D.DateStart DESC, D.DateEnd DESC
END

GO

/****** Object:  StoredProcedure [dbo].[procListBookingRoomServiceDetail]    Script Date: 05/29/2014 17:56:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListBookingRoomServiceDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListBookingRoomServiceDetail]
GO


CREATE PROCEDURE [dbo].[procListBookingRoomServiceDetail]	
@BookingRoomServiceDetailId int = null,
@BookingRoomServiceId int = null,
@BookingId int = null,
@DateStart smalldatetime = null,
@DateEnd smalldatetime = null
AS
BEGIN
SET NOCOUNT ON

	SELECT	D.BookingRoomServiceDetailId, D.BookingRoomServiceId, E.ServiceId, E.Name as Service,
			D.DateStart, D.DateEnd, D.Quantity, BRE.Price, E.Unit, D.TotalPrice, D.Payment, D.Description,			
			BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
	FROM	BookingRoomServiceDetail D
	INNER JOIN BookingRoomService BRE on BRE.BookingRoomServiceId = D.BookingRoomServiceId
	INNER JOIN dbo.Service E on BRE.ServiceId = e.ServiceId
	WHERE	(@BookingRoomServiceDetailId is null OR D.BookingRoomServiceDetailId = @BookingRoomServiceDetailId)
	AND		(@BookingRoomServiceId is null OR BRE.BookingRoomServiceId = @BookingRoomServiceId)
	AND		(@BookingId is null OR BRE.BookingId = @BookingId)
	AND     (@DateStart IS NULL OR (D.DateStart <= @DateEnd AND D.DateEnd >= @DateStart))
	ORDER BY D.DateStart DESC, D.DateEnd DESC
END

GO

/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomEquipmentDetail]    Script Date: 05/30/2014 09:34:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBookingRoomEquipmentDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBookingRoomEquipmentDetail]
GO


CREATE PROCEDURE [dbo].[procSaveBookingRoomEquipmentDetail]

@BookingRoomEquipmentDetailId int OUTPUT,
@BookingRoomEquipmentId int,
@Quantity decimal(20,8),
@DateStart smalldatetime,
@DateEnd smalldatetime,
@TotalPrice decimal(20,8),
@Payment bit,
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

	IF @BookingRoomEquipmentDetailId IS NULL 
	BEGIN

		INSERT INTO BookingRoomEquipmentDetail(
			BookingRoomEquipmentId,
			Quantity,
			DateStart,
			DateEnd,
			TotalPrice,
			Payment,
			Description,	
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@BookingRoomEquipmentId,
			@Quantity,
			@DateStart,
			@DateEnd,
			@TotalPrice,
			@Payment,
			@Description,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @BookingRoomEquipmentDetailId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE BookingRoomEquipmentDetail SET
			BookingRoomEquipmentId = @BookingRoomEquipmentId,
			Quantity = @Quantity,
			DateStart = @DateStart,
			DateEnd = @DateEnd,
			TotalPrice = @TotalPrice,
			Payment = @Payment,
			Description = @Description,	
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE BookingRoomEquipmentDetailId = @BookingRoomEquipmentDetailId

	END

	SELECT Concurrency FROM BookingRoomEquipmentDetail WHERE BookingRoomEquipmentDetailId = @BookingRoomEquipmentDetailId
END

GO




/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomServiceDetail]    Script Date: 05/30/2014 09:34:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBookingRoomServiceDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBookingRoomServiceDetail]
GO


CREATE PROCEDURE [dbo].[procSaveBookingRoomServiceDetail]

@BookingRoomServiceDetailId int OUTPUT,
@BookingRoomServiceId int,
@Quantity decimal(20,8),
@DateStart smalldatetime,
@DateEnd smalldatetime,
@TotalPrice decimal(20,8),
@Payment bit,
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

	IF @BookingRoomServiceDetailId IS NULL 
	BEGIN

		INSERT INTO BookingRoomServiceDetail(
			BookingRoomServiceId,
			Quantity,
			DateStart,
			DateEnd,
			TotalPrice,
			Payment,
			Description,	
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@BookingRoomServiceId,
			@Quantity,
			@DateStart,
			@DateEnd,
			@TotalPrice,
			@Payment,
			@Description,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @BookingRoomServiceDetailId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE BookingRoomServiceDetail SET
			BookingRoomServiceId = @BookingRoomServiceId,
			Quantity = @Quantity,
			DateStart = @DateStart,
			DateEnd = @DateEnd,
			TotalPrice = @TotalPrice,
			Payment = @Payment,
			Description = @Description,	
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE BookingRoomServiceDetailId = @BookingRoomServiceDetailId

	END

	SELECT Concurrency FROM BookingRoomServiceDetail WHERE BookingRoomServiceDetailId = @BookingRoomServiceDetailId
END

GO



IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'ContactInformation' AND COLUMN_NAME = 'DoB')
BEGIN
   ALTER TABLE ContactInformation ADD DoB datetime
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'ContactInformation' AND COLUMN_NAME = 'Visa')
BEGIN
   ALTER TABLE ContactInformation ADD Visa varchar(128)
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'ContactInformation' AND COLUMN_NAME = 'VisaValidFrom')
BEGIN
   ALTER TABLE ContactInformation ADD VisaValidFrom datetime
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'ContactInformation' AND COLUMN_NAME = 'VisaValidTo')
BEGIN
   ALTER TABLE ContactInformation ADD VisaValidTo datetime
END
GO



/****** Object:  StoredProcedure [dbo].[procListContactInformation]    Script Date: 05/30/2014 16:38:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListContactInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListContactInformation]
GO

CREATE PROCEDURE [dbo].[procListContactInformation] (
@ContactInformationId int = NULL
)

AS
BEGIN
	SET NOCOUNT ON

	SELECT ContactInformationID, ContactTypeId, FirstName, LastName, Address, Address2, District, City, State, Postcode, CountryId,
		PhoneNumber, FaxNumber, Email, DoB, Visa, VisaValidFrom, VisaValidTo,
		Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
	FROM ContactInformation
	WHERE (@ContactInformationID IS NULL OR ContactInformationId = @ContactInformationId)
END

GO


/****** Object:  StoredProcedure [dbo].[procSaveContactInformation]    Script Date: 05/30/2014 16:40:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveContactInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveContactInformation]
GO


CREATE PROCEDURE [dbo].[procSaveContactInformation]

@ContactInformationID int OUTPUT
,@ContactTypeId int
,@FirstName varchar(128)
,@LastName varchar(128)
,@Address varchar(255)
,@Address2 varchar(255)
,@District varchar(255)
,@City varchar(255)
,@State varchar(255)
,@Postcode varchar(255)
,@CountryId int
,@PhoneNumber varchar(128)
,@FaxNumber varchar(128)
,@Email varchar(255)
,@DoB datetime
,@Visa varchar(128)
,@VisaValidFrom datetime
,@VisaValidTo datetime
,@CurrentUser varchar(255)
AS
BEGIN
	SET NOCOUNT ON

	IF @ContactInformationID IS NULL BEGIN

	INSERT INTO ContactInformation(
			[ContactTypeId]
			,[FirstName]
			,[LastName]
			,[Address]
			,[Address2]
			,[District]
			,[City]
			,[State]
			,[Postcode]
			,[CountryId]
			,[PhoneNumber]
			,[FaxNumber]
			,[Email]
			,DoB
			,Visa
			,VisaValidFrom
			,VisaValidTo
			,DateCreated
			,DateUpdated
			,CreatedBy
			,UpdatedBy
		) VALUES (
			@ContactTypeId
			,@FirstName
			,@LastName
			,@Address
			,@Address2
			,@District
			,@City
			,@State
			,@Postcode
			,@CountryId
			,@PhoneNumber
			,@FaxNumber
			,@Email
			,@DoB
			,@Visa
			,@VisaValidFrom
			,@VisaValidTo
			,GETDATE()
			,GETDATE()
			,@CurrentUser
			,@CurrentUser
		)

		SET @ContactInformationID = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE ContactInformation SET
			ContactTypeId = @ContactTypeId,
			FirstName = @FirstName,
			LastName = @LastName,
			Address = @Address,
			Address2 = @Address2 ,
			District = @District,
			City = @City ,
			State = @State ,
			Postcode = @Postcode ,
			CountryID = @CountryID ,
			PhoneNumber = @PhoneNumber ,
			FaxNumber = @FaxNumber ,
			Email = @Email,
			DoB = @DoB,
			Visa = @Visa,
			VisaValidFrom = @VisaValidFrom,
			VisaValidTo = @VisaValidTo,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE ContactInformationID = @ContactInformationID

	END

	SELECT Concurrency FROM ContactInformation WHERE ContactInformationID = @ContactInformationID
END

GO




/****** Object:  StoredProcedure [dbo].[procListAspUser]    Script Date: 06/02/2014 15:34:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListAspUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListAspUser]
GO


CREATE PROCEDURE [dbo].[procListAspUser]	
@OrganisationId int = null	
, @UserID uniqueidentifier = null
, @IsLegacy bit = null
AS
BEGIN
	SET NOCOUNT ON
	SELECT distinct M.ApplicationId,M.UserId,Password,PasswordFormat,PasswordSalt,MobilePIN
		,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut
		,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate
		,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount
		,FailedPasswordAnswerAttemptWindowStart,NULL as Comment
		,U.UserName,U.LoweredUserName,U.MobileAlias, U.IsAnonymous,U.LastActivityDate, U.OrganisationID	
		,URA.SiteId
		,dbo.[ufnUserRoleMinLevel](M.UserId) as MinRoleLevel
	FROM aspnet_Membership M
	INNER JOIN aspnet_Users U on M.UserId = U.UserId	
	LEFT OUTER JOIN UserRoleAuth URA on U.UserId = URA.UserId
	WHERE (@OrganisationId is null or U.OrganisationId = @OrganisationId)
	AND (@UserID is null OR M.UserId = @UserID)
	ORDER BY U.UserName


	-- ======================================================================
	-- Change History
	-- ======================================================================
	-- 23-Feb-2013 HT
	-- INIT
END
GO


/****** Object:  StoredProcedure [dbo].[procUpdateAspUserOrganisationId]    Script Date: 06/02/2014 16:35:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procUpdateAspUserOrganisationId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procUpdateAspUserOrganisationId]
GO


CREATE PROCEDURE [dbo].[procUpdateAspUserOrganisationId]	
	@UserID uniqueidentifier
	, @OrganisationId int = null		
AS
BEGIN
	UPDATE aspnet_Users SET OrganisationId = @OrganisationId
	WHERE UserId = @UserId
END

GO


GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookingPayment_Booking]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookingPayment]'))
ALTER TABLE [dbo].[BookingPayment] DROP CONSTRAINT [FK_BookingPayment_Booking]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingPayment_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingPayment] DROP CONSTRAINT [DF_BookingPayment_DateCreated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingPayment_DateUpdated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingPayment] DROP CONSTRAINT [DF_BookingPayment_DateUpdated]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingPayment_CreatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingPayment] DROP CONSTRAINT [DF_BookingPayment_CreatedBy]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BookingPayment_UpdatedBy]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BookingPayment] DROP CONSTRAINT [DF_BookingPayment_UpdatedBy]
END
GO

/****** Object:  Table [dbo].[BookingPayment]    Script Date: 06/10/2014 16:57:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookingPayment]') AND type in (N'U'))
DROP TABLE [dbo].[BookingPayment]
GO

CREATE TABLE [dbo].[BookingPayment](
	[BookingPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
	[DateStart] [smalldatetime] NOT NULL,
	[DateEnd] [smalldatetime] NOT NULL,
	[RoomPrice] [decimal](20, 8) NOT NULL,
	[EquipmentPrice] [decimal](20, 8) NOT NULL,
	[ServicePrice] [decimal](20, 8) NOT NULL,
	[TotalPrice] [decimal](20, 8) NOT NULL,
	[CustomerPaid] [decimal](20, 8) NOT NULL,
	[Payment] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingPayment] PRIMARY KEY CLUSTERED 
(
	[BookingPaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BookingPayment]  WITH CHECK ADD  CONSTRAINT [FK_BookingPayment_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO

ALTER TABLE [dbo].[BookingPayment] CHECK CONSTRAINT [FK_BookingPayment_Booking]
GO

ALTER TABLE [dbo].[BookingPayment] ADD  CONSTRAINT [DF_BookingPayment_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[BookingPayment] ADD  CONSTRAINT [DF_BookingPayment_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO

ALTER TABLE [dbo].[BookingPayment] ADD  CONSTRAINT [DF_BookingPayment_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[BookingPayment] ADD  CONSTRAINT [DF_BookingPayment_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO



/****** Object:  StoredProcedure [dbo].[procSaveBookingPayment]    Script Date: 06/10/2014 17:10:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procSaveBookingPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procSaveBookingPayment]
GO


CREATE PROCEDURE [dbo].[procSaveBookingPayment]

@BookingPaymentId int OUTPUT,
@BookingId int,
@DateStart smalldatetime,
@DateEnd smalldatetime,
@RoomPrice decimal(20, 8),
@EquipmentPrice decimal(20, 8),
@ServicePrice decimal(20, 8),
@TotalPrice decimal(20,8),
@CustomerPaid decimal(20,8),
@Payment bit,
@CurrentUser varchar(128)
AS
	BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (SELECT * FROM BookingPayment WHERE BookingPaymentId = @BookingPaymentId)
	BEGIN

		INSERT INTO BookingPayment(
			BookingId,
			DateStart,
			DateEnd,
			RoomPrice,
			EquipmentPrice,
			ServicePrice,
			TotalPrice,
			CustomerPaid,
			Payment,
			DateCreated,
			DateUpdated,
			CreatedBy,
			UpdatedBy
		) VALUES (
			@BookingId,
			@DateStart,
			@DateEnd,
			@RoomPrice,
			@EquipmentPrice,
			@ServicePrice,
			@TotalPrice,
			@CustomerPaid,
			@Payment,
			GETDATE(),
			GETDATE(),
			@CurrentUser,
			@CurrentUser
		)

		SET @BookingPaymentId = SCOPE_IDENTITY()

	END ELSE BEGIN

		UPDATE BookingPayment SET
			BookingId = @BookingId,
			DateStart = @DateStart,
			DateEnd = @DateEnd,
			RoomPrice = @RoomPrice,
			EquipmentPrice = @EquipmentPrice,
			ServicePrice = @ServicePrice,
			TotalPrice = @TotalPrice,
			CustomerPaid = @CustomerPaid,
			Payment = @Payment,
			DateUpdated = GETDATE(),
			UpdatedBy = @CurrentUser

		WHERE BookingPaymentId = @BookingPaymentId

	END

	SELECT Concurrency FROM BookingPayment WHERE BookingPaymentId = @BookingPaymentId
END


GO




/****** Object:  UserDefinedFunction [dbo].[ufnGetTotalEquipmentPrice]    Script Date: 06/10/2014 17:51:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetTotalEquipmentPrice]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ufnGetTotalEquipmentPrice]
GO

CREATE FUNCTION [dbo].[ufnGetTotalEquipmentPrice]
(
@BookingId int,
@DateStart smalldatetime,
@DateEnd smalldatetime
)
RETURNS decimal(20,8)
AS
BEGIN
	DECLARE @Result decimal(20,8)

	SELECT @Result = SUM(TotalPrice)
	FROM BookingRoomEquipmentDetail D
	INNER JOIN BookingRoomEquipment E on D.BookingRoomEquipmentId = E.BookingRoomEquipmentId
	WHERE E.BookingId = @BookingId
	AND   D.DateStart <= @DateEnd
	AND   D.DateEnd >= @DateStart

	RETURN @Result

END

GO


/****** Object:  UserDefinedFunction [dbo].[ufnGetTotalServicePrice]    Script Date: 06/10/2014 17:51:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ufnGetTotalServicePrice]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[ufnGetTotalServicePrice]
GO

CREATE FUNCTION [dbo].[ufnGetTotalServicePrice]
(
@BookingId int,
@DateStart smalldatetime,
@DateEnd smalldatetime
)
RETURNS decimal(20,8)
AS
BEGIN
	DECLARE @Result decimal(20,8)

	SELECT @Result = SUM(TotalPrice)
	FROM BookingRoomServiceDetail D
	INNER JOIN BookingRoomService E on D.BookingRoomServiceId = E.BookingRoomServiceId
	WHERE E.BookingId = @BookingId
	AND   D.DateStart <= @DateEnd
	AND   D.DateEnd >= @DateStart

	RETURN @Result

END

GO


/****** Object:  StoredProcedure [dbo].[procListBookingPayment]    Script Date: 06/10/2014 17:21:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procListBookingPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procListBookingPayment]
GO


CREATE PROCEDURE [dbo].[procListBookingPayment]	
@OrganisationId int = null,
@SiteId int = null,
@RoomId int = null,
@BookingId int = null,
@BookingPaymentId int = null,
@DateStart smalldatetime,
@DateEnd smalldatetime,
@Payment int
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @_Contract TABLE
	(
	SiteId int
	, SiteName varchar(128)
	, BookingId int
	, RoomId int
	, RoomName varchar(128)
	, CustomerId int
	, Customer2Id int
	, CustomerName varchar(128)
	, Customer2Name varchar(128)
	, RoomPrice decimal(20,8)
	, ContractDateStart smalldatetime
	, ContractDateEnd smalldatetime
	)
	
	INSERT INTO @_Contract
	SELECT R.SiteId, S.Name, B.BookingId, R.RoomId, R.RoomName, B.CustomerId, B.Customer2Id,
			C1.FirstName + ' ' + C1.LastName as CustomerName,
			C2.FirstName + ' ' + C2.LastName as Customer2Name,
			B.ContractTotalPrice, B.ContractDateStart, B.ContractDateEnd
	FROM Booking B
	INNER JOIN Room R on B.RoomId = R.RoomId
	INNER JOIN Site S on S.SiteID = R.SiteId
	LEFT OUTER JOIN Customer C1 on B.CustomerId = C1.CustomerId
	LEFT OUTER JOIN Customer C2 on B.Customer2Id = C2.CustomerId
	WHERE (@OrganisationId IS NULL OR S.OrganisationID = @OrganisationId)
	AND (@SiteId IS NULL OR S.SiteID = @SiteId)
	AND (@RoomId IS NULL OR R.RoomId = @RoomId)
	AND (@BookingId IS NULL OR B.BookingId = @BookingId)
	AND (B.BookingStatusId = 3) -- Contract
	AND (B.ContractDateStart IS NOT NULL AND B.ContractDateStart <= @DateEnd)
	AND (B.ContractDateEnd IS NULL OR B.ContractDateEnd >= @DateStart)
	
	
	SELECT	C.SiteId, C.SiteName, C.BookingId, C.RoomId, C.RoomName, C.CustomerId, C.CustomerName, C.Customer2Id, C.Customer2Name, 
			BP.BookingPaymentId, 
			C.RoomPrice,
			[dbo].[ufnGetTotalEquipmentPrice](C.BookingId, @DateStart, @DateEnd) as EquipmentPrice,
			[dbo].[ufnGetTotalServicePrice](C.BookingId, @DateStart, @DateEnd) as ServicePrice, 
			BP.TotalPrice,				
			BP.CustomerPaid,
			CASE WHEN BP.BookingPaymentId IS NULL THEN @DateStart ELSE BP.DateStart END as DateStart, 
			CASE WHEN BP.BookingPaymentId IS NULL THEN @DateEnd ELSE BP.DateEnd END as DateEnd, 
			CASE WHEN BP.BookingPaymentId IS NULL THEN 0 ELSE BP.Payment END as Payment, 
			BP.Concurrency, BP.DateCreated, BP.DateUpdated, BP.CreatedBy, BP.UpdatedBy
	FROM	@_Contract C
	LEFT OUTER JOIN BookingPayment BP 
		on (C.BookingId = BP.BookingId 
			AND BP.DateStart <= @DateEnd
			AND BP.DateEnd >= @DateStart)
	WHERE	(@BookingPaymentId is null OR BP.BookingPaymentId = @BookingPaymentId)
	AND   (@Payment = 2 OR (@Payment = 1 AND BP.Payment = 1) OR (@Payment = 0 AND (BP.Payment IS NULL OR BP.Payment = 0)))
END

GO



/****** Object:  StoredProcedure [dbo].[procCheckExistBooking]    Script Date: 06/17/2014 11:20:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[procCheckExistBooking]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[procCheckExistBooking]
GO
CREATE PROCEDURE [dbo].[procCheckExistBooking] 
	@RoomId int, 
	@DateStart smalldatetime,
	@DateEnd smalldatetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT CASE WHEN EXISTS(SELECT * FROM Booking
							WHERE RoomId = @RoomId
							AND   ContractDateStart <= @DateEnd
							AND   (ContractDateEnd IS NULL OR ContractDateEnd >= @DateStart))
			THEN 1 ELSE 0 END
END

GO

/***********************HT End*******************************/
GO


PRINT 'Finished'
GO