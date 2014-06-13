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