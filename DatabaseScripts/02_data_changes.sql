BEGIN TRY

	BEGIN TRANSACTION

	DECLARE 
        @C_CurrentUser varchar (128)
        , @C_CurrentDate datetime 
    SELECT 
        @C_CurrentUser = 'Mimosa IT - Manual'
        , @C_CurrentDate = getDate()

	IF NOT EXISTS (SELECT * FROM Component Where ComponentId = 6)
	BEGIN
		SET IDENTITY_INSERT [Component] ON
		INSERT INTO [dbo].[Component]
			   ([ComponentId]
			   ,[Name]
			   ,[DateCreated]
			   ,[DateUpdated]
			   ,[CreatedBy]
			   ,[UpdatedBy])
		SELECT  6, 'Equipment Admin ', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION ALL SELECT  7, 'Service Admin', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION ALL SELECT  8, 'Room Admin', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION ALL SELECT  101, 'Booking Admin', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION ALL SELECT  201, 'Customer Admin', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION ALL SELECT  202, 'Monthly Payment', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		SET IDENTITY_INSERT [Component] OFF
	END
	
	IF NOT EXISTS (SELECT * FROM [RoleComponentPermission])
	BEGIN
		INSERT INTO [dbo].[RoleComponentPermission]
			   ([RoleId]
			   ,[ComponentId]
			   ,[WriteRight]
			   ,[DateCreated]
			   ,[DateUpdated]
			   ,[CreatedBy]
			   ,[UpdatedBy])
	    SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 1, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 2, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 3, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 4, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 5, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 6, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 7, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 8, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 101, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 201, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB', 202, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 3, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 4, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 6, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 7, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 8, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 101, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 201, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    UNION ALL SELECT '4030222F-1655-42D7-9C2A-4278E105228C', 202, 1, @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser       
	    
	END 
	
	IF NOT EXISTS (SELECT * FROM [City])
	BEGIN
		SET IDENTITY_INSERT [City] ON
		INSERT INTO [dbo].[City]
           ([CityId]
           ,[CountryId]
           ,[Name]
           ,[DateCreated]
           ,[DateUpdated]
           ,[CreatedBy]
           ,[UpdatedBy])
		SELECT 1, 232, 'Ho Chi Minh', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION SELECT 2, 232, 'Ha Noi', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION SELECT 3, 232, 'Hue', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 4, 232, 'Da Nang', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 5, 232, 'Ha Long', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 6, 232, 'Nha Trang', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 7, 232, 'Phan Thiet', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 8, 232, 'Da Lat', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 9, 232, 'Dong Nai', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 10, 232, 'Vung Tau', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 11, 232, 'Ba Ria', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 12, 232, 'Binh Duong', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 13, 232, 'Long An', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
           
		SET IDENTITY_INSERT [City] OFF      				
	END
	
	IF NOT EXISTS (SELECT * FROM [District])
	BEGIN
		SET IDENTITY_INSERT [District] ON
		INSERT INTO [dbo].[District]
           ([DistrictId]
           ,[CityId]
           ,[Name]
           ,[DateCreated]
           ,[DateUpdated]
           ,[CreatedBy]
           ,[UpdatedBy])
		SELECT 1, 1, '1', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION SELECT 2, 1, '2', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser
		UNION SELECT 3, 1, '3', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 4, 1, '4', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 5, 1, '5', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 6, 1, '6', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 7, 1, '7', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 8, 1, '8', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 9, 1, '9', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 10, 1, '10', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 11, 1, '11', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 12, 1, '12', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 13, 1, 'Phu Nhuan', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 14, 1, 'Tan Binh', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 15, 1, 'Tan Phu', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 16, 1, 'Go Vap', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 17, 1, 'Thu Duc', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 18, 1, 'Hoc Mon', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 19, 1, 'Binh Chanh', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
        UNION SELECT 20, 1, 'Nha Be', @C_CurrentDate, @C_CurrentDate, @C_CurrentUser, @C_CurrentUser		           
           
		SET IDENTITY_INSERT [District] OFF      	
	END
	                            	        
    COMMIT TRANSACTION 
	PRINT 'EXECUTED SUCCESSFULLY'

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION 
	PRINT	'ERROR_NUMBER ' + CONVERT(NVARCHAR(50),ERROR_NUMBER())
	PRINT	'ERROR_SEVERITY '+			CONVERT(NVARCHAR(50),ERROR_SEVERITY())
	PRINT	'ERROR_LINE	' +CONVERT(NVARCHAR(50),ERROR_LINE())
	PRINT	'ERROR_MESSAGE '+	ERROR_MESSAGE()
	PRINT 'EXECUTED UNSUCCESSFULLY'
END CATCH
GO

SELECT TOP 1 Version FROM dbo.TagVersion ORDER BY DateCreated DESC, Version DESC
GO