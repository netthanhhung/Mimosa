USE [master]
GO
/****** Object:  Database [Mimosa]    Script Date: 04/19/2014 17:37:01 ******/
CREATE DATABASE [Mimosa] ON  PRIMARY 
( NAME = N'Mimosa', FILENAME = N'E:\Chanh\Database\Mimosa.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Mimosa_log', FILENAME = N'E:\Chanh\Database\Mimosa_log.ldf' , SIZE = 4224KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Mimosa] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Mimosa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Mimosa] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Mimosa] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Mimosa] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Mimosa] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Mimosa] SET ARITHABORT OFF
GO
ALTER DATABASE [Mimosa] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Mimosa] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Mimosa] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Mimosa] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Mimosa] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Mimosa] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Mimosa] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Mimosa] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Mimosa] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Mimosa] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Mimosa] SET  DISABLE_BROKER
GO
ALTER DATABASE [Mimosa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Mimosa] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Mimosa] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Mimosa] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Mimosa] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Mimosa] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Mimosa] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Mimosa] SET  READ_WRITE
GO
ALTER DATABASE [Mimosa] SET RECOVERY FULL
GO
ALTER DATABASE [Mimosa] SET  MULTI_USER
GO
ALTER DATABASE [Mimosa] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Mimosa] SET DB_CHAINING OFF
GO
USE [Mimosa]
GO
/****** Object:  Role [aspnet_Membership_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Membership_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Membership_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Membership_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Membership_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Personalization_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Personalization_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Personalization_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Profile_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Profile_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Profile_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Profile_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Roles_BasicAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Roles_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_Roles_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_Roles_ReportingAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Role [aspnet_WebEvent_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE ROLE [aspnet_WebEvent_FullAccess] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess] AUTHORIZATION [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Roles_FullAccess] AUTHORIZATION [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess] AUTHORIZATION [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess] AUTHORIZATION [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Profile_FullAccess] AUTHORIZATION [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess] AUTHORIZATION [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess] AUTHORIZATION [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess] AUTHORIZATION [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Membership_FullAccess] AUTHORIZATION [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 04/19/2014 17:37:01 ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess] AUTHORIZATION [aspnet_Membership_BasicAccess]
GO
/****** Object:  Table [dbo].[RoomStatus]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[RoomStatus](
	[RoomStatusId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoomStatus] PRIMARY KEY CLUSTERED 
(
	[RoomStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Language]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LanguageCulture] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageType]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImageType](
	[ImageTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ImageType] PRIMARY KEY CLUSTERED 
(
	[ImageTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Component]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Component](
	[ComponentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Component] PRIMARY KEY CLUSTERED 
(
	[ComponentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookingStatus]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[BookingStatus](
	[BookingStatusId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingStatus] PRIMARY KEY CLUSTERED 
(
	[BookingStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DistrictCode] [nvarchar](50) NOT NULL,
	[DistrictName] [nvarchar](250) NULL,
	[DistrictName_EN] [nvarchar](250) NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationID] [int] NOT NULL,
	[FirstName] [varchar](128) NOT NULL,
	[LastName] [varchar](128) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Gender] [int] NULL,
	[Age] [int] NULL,
	[ContactInformationId] [int] NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Concurrency] [timestamp] NULL,
	[DateCreated] [smalldatetime] NULL,
	[DateUpdated] [smalldatetime] NULL,
	[CreatedBy] [varchar](128) NULL,
	[UpdatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 04/19/2014 17:37:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactType](
	[ContactTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 04/19/2014 17:37:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications] 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 04/19/2014 17:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 04/19/2014 17:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name sysname,
        Group_id smallint,
        Users_in_group sysname,
        User_id smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 04/19/2014 17:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteGroup]    Script Date: 04/19/2014 17:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SiteGroup](
	[SiteGroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](128) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_SiteGroup] PRIMARY KEY CLUSTERED 
(
	[SiteGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[VisitorOfDay]    Script Date: 04/24/2014 16:42:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitorOfDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Day] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_VisitorOfDay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[VisitorOfDay] ON
INSERT [dbo].[VisitorOfDay] ([Id], [DateTime], [Day], [Month], [Year], [Count]) VALUES (1, CAST(0x0000A3170103545A AS DateTime), 24, 4, 2014, 2)
SET IDENTITY_INSERT [dbo].[VisitorOfDay] OFF


/****** Object:  Table [dbo].[UtilityDays]    Script Date: 04/19/2014 17:37:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UtilityDays](
	[DateEntry] [smalldatetime] NOT NULL,
	[DateWeekStarting]  AS (case datepart(weekday,[DateEntry]) when (2) then [DateEntry] when (3) then dateadd(day,(-1),[DateEntry]) when (4) then dateadd(day,(-2),[DateEntry]) when (5) then dateadd(day,(-3),[DateEntry]) when (6) then dateadd(day,(-4),[DateEntry]) when (7) then dateadd(day,(-5),[DateEntry]) when (1) then dateadd(day,(-6),[DateEntry])  end),
	[DateWeekEnding]  AS (case datepart(weekday,[DateEntry]) when (1) then [DateEntry] when (2) then dateadd(day,(6),[DateEntry]) when (3) then dateadd(day,(5),[DateEntry]) when (4) then dateadd(day,(4),[DateEntry]) when (5) then dateadd(day,(3),[DateEntry]) when (6) then dateadd(day,(2),[DateEntry]) when (7) then dateadd(day,(1),[DateEntry])  end),
	[DateMonthStarting]  AS (CONVERT([smalldatetime],(('01-'+CONVERT([varchar](3),[DateEntry],(109)))+'-')+CONVERT([varchar](4),datepart(year,[DateEntry]),(0)),(0))),
	[DateMonthEnding]  AS (dateadd(day,(-1),dateadd(month,(1),(('01-'+CONVERT([varchar](3),[DateEntry],(109)))+'-')+CONVERT([varchar](4),datepart(year,[DateEntry]),(0))))),
	[DateYear1] [smalldatetime] NULL,
	[DateYear2] [smalldatetime] NULL,
	[DateYear3] [smalldatetime] NULL,
	[DateYear4] [smalldatetime] NULL,
	[DateYear5] [smalldatetime] NULL,
	[DayOfWeek]  AS (datepart(weekday,[DateEntry])),
	[Day]  AS (datepart(day,[DateEntry])),
	[Month]  AS (datepart(month,[DateEntry])),
	[Quarter]  AS (datepart(quarter,[DateEntry])),
	[Year]  AS (datepart(year,[DateEntry])),
	[DateYear1Offset] [smalldatetime] NULL,
	[DateYear2Offset] [smalldatetime] NULL,
	[DateYear3Offset] [smalldatetime] NULL,
	[DateYear4Offset] [smalldatetime] NULL,
	[DateYear5Offset] [smalldatetime] NULL,
 CONSTRAINT [PK_UtilityDays] PRIMARY KEY CLUSTERED 
(
	[DateEntry] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[ufnSplit]    Script Date: 04/19/2014 17:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufnSplit](@sep char(1), @s varchar(MAX))
returns @temptable
TABLE
(
Id int NOT NULL IDENTITY (1, 1)
, Token varchar(MAX) NULL
)
AS BEGIN

    declare @idx int
    declare @split varchar(max)

    select @idx = 1
        if len(@s )<1 or @s is null return

    while @idx!= 0
    begin
        set @idx = charindex(@sep,@s)
        if @idx!=0
            set @split= left(@s,@idx - 1)
        else
            set @split= @s

        if(len(@split)>0)
            insert into @temptable(Token) values(ltrim(rtrim(@split)))

        set @s= right(@s,len(@s) - @idx)
        if len(@s) = 0 break
    end

return

-- ======================================================================
-- Change History
-- ======================================================================
-- 14-Mar-2014 HT
-- Init

END
GO
/****** Object:  UserDefinedFunction [dbo].[ufnGetRoleRank]    Script Date: 04/19/2014 17:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufnGetRoleRank]
(
@RoleId uniqueidentifier
)
RETURNS int
AS
BEGIN
DECLARE @Result int

SELECT @Result = CASE @RoleId
WHEN '0FEA5EAE-C34F-4E6B-817F-8ABA46697F19' THEN 1	-- Portal Admin
WHEN '0DA03E27-E0F9-419E-A5F1-3FA7A1219AFB' THEN 2	-- Organisation Admin
WHEN '4030222F-1655-42D7-9C2A-4278E105228C' THEN 3	-- Site Admin
ELSE 1000 -- others select * from aspnet_roles
END
RETURN @Result

END
GO
/****** Object:  Table [dbo].[TagVersion]    Script Date: 04/19/2014 17:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[TagVersion](
	[TagVersionID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](128) NULL,
	[Version] [varchar](128) NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NULL,
 CONSTRAINT [PK_TagVersion] PRIMARY KEY CLUSTERED 
(
	[TagVersionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procSaveSiteGroup]    Script Date: 04/19/2014 17:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveSiteGroup]	
@SiteGroupID int OUTPUT,	
@GroupName varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @SiteGroupID IS NULL BEGIN

INSERT INTO SiteGroup(
GroupName,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@GroupName,	
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @SiteGroupID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE SiteGroup SET
GroupName = @GroupName,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE SiteGroupID = @SiteGroupID

END

SELECT Concurrency FROM SiteGroup WHERE SiteGroupID = @SiteGroupID
END
GO
/****** Object:  UserDefinedFunction [dbo].[ufnSplitNumeric]    Script Date: 04/19/2014 17:37:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufnSplitNumeric] (@sep char(1), @s varchar(MAX))
RETURNS table
AS RETURN
SELECT
Id, convert(int, Token) AS Number
FROM
dbo.ufnSplit(@sep, @s)
GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
	[OrganisationID] [int] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users] 
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature nvarchar(128),
    @CompatibleSchemaVersion nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END
GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Roles_index1] ON [dbo].[aspnet_Roles] 
(
	[ApplicationId] ASC,
	[LoweredRoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature nvarchar(128),
    @CompatibleSchemaVersion nvarchar(128),
    @IsCurrentVersion bit,
    @RemoveIncompatibleSchema bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[ContactInformation](
	[ContactInformationId] [int] IDENTITY(1,1) NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[FirstName] [varchar](128) NULL,
	[LastName] [varchar](128) NULL,
	[Address] [varchar](255) NULL,
	[Address2] [varchar](255) NULL,
	[District] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[Postcode] [varchar](50) NULL,
	[CountryId] [int] NULL,
	[PhoneNumber] [varchar](128) NULL,
	[FaxNumber] [varchar](128) NULL,
	[Email] [varchar](255) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[ContactInformationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature nvarchar(128),
    @CompatibleSchemaVersion nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT *
                FROM dbo.aspnet_SchemaVersions
                WHERE Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName nvarchar(256),
    @ApplicationId uniqueidentifier OUTPUT
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
        END
        ELSE
     SET @TranStarted = 0

        SELECT @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT @ApplicationId = NEWID()
            INSERT dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END
GO
/****** Object:  Table [dbo].[Image]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageTypeId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[FileName] [varchar](128) NULL,
	[ImageContent] [varbinary](max) NULL,
	[ImageSmallContent] [varbinary](max) NULL,
	[DisplayIndex] [int] NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocaleStringResource]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocaleStringResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[ResourceName] [nvarchar](200) NOT NULL,
	[ResourceValue] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LocaleStringResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[procListCustomer]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListCustomer]	
@OrganisationId int = null
, @CustomerId int = null
, @FirstName varchar(125)
, @LastName varchar(125)
, @ShowLegacy bit
AS
BEGIN
	SET NOCOUNT ON

	SELECT top 1000	
		CustomerID
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

GO
/****** Object:  StoredProcedure [dbo].[procListCountry]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListCountry] (@CountryId int = NULL)

AS
BEGIN
SET NOCOUNT ON

SELECT CountryID, Name, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM Country
WHERE (@CountryID IS NULL OR CountryId = @CountryId)
END
GO
/****** Object:  StoredProcedure [dbo].[procListRoomStatus]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoomStatus]	
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RoomStatusId, RE.Name, RE.IsLegacy,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RoomStatus RE
WHERE	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END
GO
/****** Object:  StoredProcedure [dbo].[procListComponent]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListComponent]	
@ComponentId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	ComponentId, Name,
Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM	Component
WHERE	(@ComponentId is null OR ComponentId = @ComponentId)

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteSiteGroup]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteSiteGroup]
(
@SiteGroupId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM SiteGroup WHERE SiteGroupId = @SiteGroupId

END
GO
/****** Object:  StoredProcedure [dbo].[procGetLatestTagVersion]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procGetLatestTagVersion]
AS
BEGIN
SET NOCOUNT ON

SELECT TOP 1 TagVersionID, TagName, Version, DateCreated, CreatedBy
FROM TagVersion
ORDER BY TagVersionID DESC
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteCustomer]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteCustomer](@CustomerId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE Customer WHERE CustomerId = @CustomerId
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteCountry]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteCountry](@CountryId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE Country WHERE CountryId = @CountryId
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveCustomer]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveCustomer]
(
 @CustomerID int OUTPUT
, @OrganisationId int
, @FirstName varchar(128)
, @LastName varchar(128)
, @IsLegacy bit
, @Gender int = NULL
, @Age int = NULL
, @ContactInformationId int = NULL
, @CurrentUser varchar(128)
)
AS
BEGIN

SET NOCOUNT ON

IF @CustomerID IS NULL
BEGIN

INSERT INTO [Customer]
(
[OrganisationId]
, [FirstName]
, [LastName]
, [IsLegacy]
, [Gender]
, [Age]	
, [ContactInformationId]	
, [DateCreated]
, [DateUpdated]
, [CreatedBy]
, [UpdatedBy]
)
VALUES
(
@OrganisationId
, @FirstName
, @LastName
, @IsLegacy
, @Gender
, @Age
, @ContactInformationId
, GETDATE()
, GETDATE()
, @CurrentUser
, @CurrentUser
)

SET @CustomerID = SCOPE_IDENTITY()
END
ELSE
BEGIN

UPDATE
[Customer]
SET
[OrganisationId] = @OrganisationId
, [FirstName] = @FirstName
, LastName = @LastName
, IsLegacy = @IsLegacy
, Gender = @Gender
, Age = @Age
, ContactInformationId = @ContactInformationId
, [DateUpdated] = GETDATE()
, [UpdatedBy] = @CurrentUser
WHERE
[CustomerID] = @CustomerID

END

SELECT [Concurrency] FROM [Customer] WHERE [CustomerID] = @CustomerID

END

GO
/****** Object:  Table [dbo].[RoleComponentPermission]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoleComponentPermission](
	[RoleComponentPermissionId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ComponentId] [int] NOT NULL,
	[WriteRight] [bit] NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoleComponentPermission] PRIMARY KEY CLUSTERED 
(
	[RoleComponentPermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procSearchCustomer]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSearchCustomer]
(
@CustomerId int = NULL
, @SearchWord varchar(128)
, @ShowLegacy bit
)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT
C.[CustomerId]
, C.[FirstName]
, C.[LastName]
, C.[IsLegacy]
, C.[Gender]
, C.[Age]
, C.[ContactInformationId]
, C.IsLegacy
, I.[Address]
, I.[Address2]
, I.[District]
, I.[City]
, I.[State]
, I.[Postcode]
, I.[CountryId]
, CT.Name as Country
, I.[PhoneNumber]
, I.[FaxNumber]
, I.[Email]
, C.Concurrency
, C.DateCreated
, C.DateUpdated
, C.CreatedBy
, C.UpdatedBy
FROM
Customer AS C	
LEFT OUTER JOIN ContactInformation I on C.ContactInformationId = I.ContactInformationId
LEFT OUTER JOIN Country CT on I.CountryId = CT.CountryId
WHERE
(@CustomerId IS NULL OR C.CustomerID = @CustomerId)
AND (C.IsLegacy = 0 OR @ShowLegacy = 1)
--AND (CONTAINS(C.FirstName, @SearchWord)
-- OR CONTAINS(C.LastName, @SearchWord)
-- OR CONTAINS(I.FirstName, @SearchWord)
-- OR CONTAINS(I.LastName, @SearchWord)
-- OR CONTAINS(I.Address, @SearchWord)
-- OR CONTAINS(I.Address2, @SearchWord)
-- OR CONTAINS(I.City, @SearchWord)
-- OR CONTAINS(I.State, @SearchWord)
-- OR CONTAINS(I.Postcode, @SearchWord)
-- OR CONTAINS(CT.Name, @SearchWord)
-- OR CONTAINS(I.[Email], @SearchWord)
-- OR CONTAINS(I.PhoneNumber, @SearchWord)
-- OR CONTAINS(I.FaxNumber, @SearchWord)
--)

ORDER BY
C.FirstName, C.LastName, C.DateCreated DESC
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE ContactInformationID = @ContactInformationID

END

SELECT Concurrency FROM ContactInformation WHERE ContactInformationID = @ContactInformationID
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveImage]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveImage]

@ImageID int OUTPUT,
@ImageTypeId int,
@ItemId int,
@FileName varchar(128),
@ImageContent varbinary(MAX),
@ImageSmallContent varbinary(MAX),
@DisplayIndex int,
@Description varchar(max),
@CurrentUser varchar(128)
AS

SET NOCOUNT ON
BEGIN
IF (NOT EXISTS (SELECT * FROM Image WHERE ImageID = @ImageID)) BEGIN

INSERT INTO Image(
ImageTypeId
,ItemId
,[FileName]
,[ImageContent]
,[ImageSmallContent]
,[DisplayIndex]
,[Description]
,[DateCreated]
,[DateUpdated]
,[CreatedBy]
,[UpdatedBy]
) VALUES (
@ImageTypeId
,@ItemId
,@FileName
,@ImageContent
,@ImageSmallContent
,@DisplayIndex
,@Description
,GETDATE()
,GETDATE()
,@CurrentUser
,@CurrentUser
)

SET @ImageID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE Image SET

ImageTypeId = @ImageTypeId
,ItemId = @ItemId
,[FileName] = @FileName
,[ImageContent] = @ImageContent	
,[ImageSmallContent] = @ImageSmallContent
,[DisplayIndex] = @DisplayIndex
,[Description] = @Description
,DateUpdated = GETDATE()
,UpdatedBy = @CurrentUser

WHERE (ImageId = @ImageID)

END

SELECT Concurrency FROM Image WHERE ImageId = @ImageID
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteContactInformation](@ContactInformationId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE ContactInformation WHERE ContactInformationId = @ContactInformationId
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteImage]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteImage](@ImageId int)	
AS
BEGIN
SET NOCOUNT ON

DELETE Image WHERE ImageId = @ImageId
END
GO
/****** Object:  StoredProcedure [dbo].[procGetOrganisationIdForEmployee]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procGetOrganisationIdForEmployee]
(
@UserID uniqueidentifier
)
AS
BEGIN
SET NOCOUNT ON

SELECT OrganisationID
FROM aspnet_Users
WHERE UserId = @UserID

-- =================================================================================
-- Change History
-- =================================================================================
-- 14-Dec-2011 HT
-- use aspnet_Users to get Organisation

END
GO
/****** Object:  StoredProcedure [dbo].[procSaveAspRole]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveAspRole]	
@ApplicationName nvarchar(256),	
@RoleId uniqueidentifier,
@RoleName nvarchar(256),
@LoweredRoleName nvarchar(256),
@Description nvarchar(256),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON
DECLARE @ApplicationId uniqueidentifier
SELECT	@ApplicationId = ApplicationId
FROM	aspnet_Applications
WHERE	LoweredApplicationName = LOWER(@ApplicationName)

IF (@RoleId IS NULL OR @RoleId = '00000000-0000-0000-0000-000000000000') BEGIN	
INSERT INTO aspnet_Roles(
ApplicationId,
RoleId,
RoleName,
LoweredRoleName,
Description
) VALUES (
@ApplicationId,
newid(),
@RoleName,
@LoweredRoleName,
@Description
)	

END ELSE BEGIN

UPDATE aspnet_Roles SET
ApplicationId = @ApplicationId,
RoleId = @RoleId,
RoleName = @RoleName,
LoweredRoleName = @LoweredRoleName,
Description = @Description

WHERE RoleId = @RoleId

END

END
GO
/****** Object:  StoredProcedure [dbo].[procListContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListContactInformation] (
@ContactInformationId int = NULL
)

AS
BEGIN
SET NOCOUNT ON

SELECT ContactInformationID, ContactTypeId, FirstName, LastName, Address, Address2, District, City, State, Postcode, CountryId,
PhoneNumber, FaxNumber, Email, Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM ContactInformation
WHERE (@ContactInformationID IS NULL OR ContactInformationId = @ContactInformationId)
END
GO
/****** Object:  StoredProcedure [dbo].[procListImage]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListImage]
(
@ImageId int = NULL
,@ItemId int = NULL
,@ImageTypeId int = NULL
,@LoadType int -- 0 : no content, 1 : load big contect, 2 : load small conten, 3 : load all
)

AS
BEGIN
SET NOCOUNT ON

SELECT ImageID, ImageTypeId, ItemId, FileName,
CASE WHEN (@LoadType = 1 OR @LoadType = 3) THEN I.ImageContent ELSE NULL END AS ImageContent,
CASE WHEN (@LoadType = 2 OR @LoadType = 3) THEN I.ImageSmallContent ELSE NULL END AS ImageSmallContent,
DisplayIndex, Description,
Concurrency, DateCreated, DateUpdated, CreatedBy, UpdatedBy
FROM dbo.[Image] I
WHERE (@ImageID IS NULL OR ImageId = @ImageId)
AND	(@ItemId IS NULL OR ItemId = @ItemId)
AND (@ImageTypeId IS NULL OR ImageTypeId = @ImageTypeId)
END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteAspRole]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteAspRole]
(
@RoleId uniqueidentifier
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM aspnet_Roles WHERE RoleId = @RoleId

END
GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Organisation](
	[OrganisationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[ContactInformationID] [int] NOT NULL,
	[DisplayIndex] [int] NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[AuthorisationCode] [varchar](128) NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED 
(
	[OrganisationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName nvarchar(256),
    @RoleName nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId uniqueidentifier,
    @UserName nvarchar(256),
    @IsUserAnonymous bit,
    @LastActivityDate DATETIME,
    @UserId uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END
GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership] 
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName nvarchar(256),
    @RoleName nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
GO
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
GO
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Site](
	[SiteID] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationID] [int] NOT NULL,
	[HotelID] [int] NULL,
	[Name] [varchar](128) NOT NULL,
	[AbbreviatedName] [varchar](10) NULL,
	[ContactInformationID] [int] NULL,
	[LicenseKey] [uniqueidentifier] NULL,
	[StarRating] [decimal](2, 1) NULL,
	[PropCode] [varchar](50) NULL,
	[DisplayIndex] [int] NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Availability] [int] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[SiteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Service]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Price] [decimal](20, 8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL
    SELECT @NumTablesDeletedFrom = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
SET @TranStarted = 0

    DECLARE @ErrorCode int
    DECLARE @RowCount int

    SET @ErrorCode = 0
    SET @RowCount = 0

    SELECT @UserId = u.UserId
    FROM dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE u.LoweredUserName = LOWER(@UserName)
        AND u.ApplicationId = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName nvarchar(256),
    @RoleName nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId uniqueidentifier
    SELECT @RoleId = NULL
    SELECT @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserName_Custom]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserName_Custom]
@ApplicationName	nvarchar(256),
@UserName	nvarchar(256),
@NewUserName	nvarchar(256)
AS
BEGIN
DECLARE @UserId uniqueidentifier
DECLARE @ApplicationId uniqueidentifier
DECLARE @NewLoweredUserName nvarchar(256)
DECLARE @LastActivityDate datetime

SELECT @UserId = NULL
SELECT @UserId = u.UserId, @ApplicationId = a.ApplicationId
FROM dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
WHERE LoweredUserName = LOWER(@UserName) AND
u.ApplicationId = a.ApplicationId AND
LOWER(@ApplicationName) = a.LoweredApplicationName AND
u.UserId = m.UserId

IF (@UserId IS NULL)
BEGIN
SELECT 1
RETURN
END

SET @LastActivityDate = (getdate())
SET @NewLoweredUserName = LOWER(@NewUserName)

DECLARE @TranStarted bit
SET @TranStarted = 0

IF( @@TRANCOUNT = 0 )
BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
END
ELSE
SET @TranStarted = 0

UPDATE dbo.aspnet_Users WITH (ROWLOCK)
SET
UserName = @NewUserName,
LoweredUserName = @NewLoweredUserName,
LastActivityDate = @LastActivityDate
WHERE
@UserId = UserId

IF( @@ERROR <> 0 )
GOTO Cleanup

IF( @TranStarted = 1 )
BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
END

SELECT 0
RETURN

Cleanup:

IF( @TranStarted = 1 )
BEGIN
SET @TranStarted = 0
ROLLBACK TRANSACTION
END

SELECT -1
RETURN
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @IsPasswordCorrect bit,
    @UpdateLastLoginActivityDate bit,
    @MaxInvalidPasswordAttempts int,
    @PasswordAttemptWindow int,
    @CurrentTimeUtc datetime,
    @LastLoginDate datetime,
    @LastActivityDate datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @IsApproved bit
    DECLARE @IsLockedOut bit
    DECLARE @LastLockoutDate datetime
    DECLARE @FailedPasswordAttemptCount int
    DECLARE @FailedPasswordAttemptWindowStart datetime
    DECLARE @FailedPasswordAnswerAttemptCount int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
     SET @TranStarted = 0

    SELECT @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE dbo.aspnet_Users
        SET LastActivityDate = @LastActivityDate
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE dbo.aspnet_Membership
        SET LastLoginDate = @LastLoginDate
        WHERE UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
     ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @Email nvarchar(256),
    @Comment ntext,
    @IsApproved bit,
    @LastLoginDate datetime,
    @LastActivityDate datetime,
    @UniqueEmail int,
    @CurrentTimeUtc datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT @UserId = NULL
    SELECT @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email = @Email,
         LoweredEmail = LOWER(@Email),
         Comment = @Comment,
         IsApproved = @IsApproved,
         LastLoginDate = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
     ROLLBACK TRANSACTION
    END

    RETURN -1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL
    SELECT @UserId = u.UserId
    FROM dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @NewPassword nvarchar(128),
    @PasswordSalt nvarchar(128),
    @CurrentTimeUtc datetime,
    @PasswordFormat int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL
    SELECT @UserId = u.UserId
    FROM dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @NewPassword nvarchar(128),
    @MaxInvalidPasswordAttempts int,
    @PasswordAttemptWindow int,
    @PasswordSalt nvarchar(128),
    @CurrentTimeUtc datetime,
    @PasswordFormat int = 0,
    @PasswordAnswer nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut bit
    DECLARE @LastLockoutDate datetime
    DECLARE @FailedPasswordAttemptCount int
    DECLARE @FailedPasswordAttemptWindowStart datetime
    DECLARE @FailedPasswordAnswerAttemptCount int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId uniqueidentifier
    SET @UserId = NULL

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
     SET @TranStarted = 0

    SELECT @UserId = u.UserId
    FROM dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
     ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId uniqueidentifier,
    @CurrentTimeUtc datetime,
    @UpdateLastActivity bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE dbo.aspnet_Users
        SET LastActivityDate = @CurrentTimeUtc
        FROM dbo.aspnet_Users
        WHERE @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @CurrentTimeUtc datetime,
    @UpdateLastActivity bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE dbo.aspnet_Users
        SET LastActivityDate = @CurrentTimeUtc
        WHERE @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE @UserId = u.UserId AND u.UserId = m.UserId
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName nvarchar(256),
    @Email nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT u.UserName
        FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT u.UserName
        FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @UpdateLastLoginActivityDate bit,
    @CurrentTimeUtc datetime
AS
BEGIN
    DECLARE @IsLockedOut bit
    DECLARE @UserId uniqueidentifier
    DECLARE @Password nvarchar(128)
    DECLARE @PasswordSalt nvarchar(128)
    DECLARE @PasswordFormat int
    DECLARE @FailedPasswordAttemptCount int
    DECLARE @FailedPasswordAnswerAttemptCount int
    DECLARE @IsApproved bit
    DECLARE @LastActivityDate datetime
    DECLARE @LastLoginDate datetime

    SELECT @UserId = NULL

    SELECT @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
@FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET LastLoginDate = @CurrentTimeUtc
        WHERE UserId = @UserId

        UPDATE dbo.aspnet_Users
        SET LastActivityDate = @CurrentTimeUtc
        WHERE @UserId = UserId
    END


    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @MaxInvalidPasswordAttempts int,
    @PasswordAttemptWindow int,
    @CurrentTimeUtc datetime,
    @PasswordAnswer nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @PasswordFormat int
    DECLARE @Password nvarchar(128)
    DECLARE @passAns nvarchar(128)
    DECLARE @IsLockedOut bit
    DECLARE @LastLockoutDate datetime
    DECLARE @FailedPasswordAttemptCount int
    DECLARE @FailedPasswordAttemptWindowStart datetime
    DECLARE @FailedPasswordAnswerAttemptCount int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
     SET @TranStarted = 0

    SELECT @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
     ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName nvarchar(256),
    @MinutesSinceLastInActive int,
    @CurrentTimeUtc datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT @DateActive = DATEADD(minute, -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT @NumOnline = COUNT(*)
    FROM dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE u.ApplicationId = a.ApplicationId AND
            LastActivityDate > @DateActive AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName nvarchar(256),
    @PageIndex int,
    @PageSize int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName nvarchar(256),
    @UserNameToMatch nvarchar(256),
    @PageIndex int,
    @PageSize int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT @TotalRecords = COUNT(*)
    FROM #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName nvarchar(256),
    @EmailToMatch nvarchar(256),
    @PageIndex int,
    @PageSize int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT @TotalRecords = COUNT(*)
    FROM #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @Password nvarchar(128),
    @PasswordSalt nvarchar(128),
    @Email nvarchar(256),
    @PasswordQuestion nvarchar(256),
    @PasswordAnswer nvarchar(128),
    @IsApproved bit,
    @CurrentTimeUtc datetime,
    @CreateDate datetime = NULL,
    @UniqueEmail int = 0,
    @PasswordFormat int = 0,
    @UserId uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue int
    SET @ReturnValue = 0

    DECLARE @ErrorCode int
    SET @ErrorCode = 0

    DECLARE @TranStarted bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
    END
    ELSE
     SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM dbo.aspnet_Membership
                  WHERE @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET LastActivityDate = @CreateDate
        WHERE @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
SET @TranStarted = 0
COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
     ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @NewPasswordQuestion nvarchar(256),
    @NewPasswordAnswer nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL
    SELECT @UserId = u.UserId
    FROM dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE UserId=@UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END


    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 04/19/2014 17:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Equipment](
	[EquipmentId] [int] IDENTITY(1,1) NOT NULL,
	[OrganisationId] [int] NOT NULL,
	[EquipmentName] [varchar](200) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[RealPrice] [decimal](20, 8) NULL,
	[RentPrice] [decimal](20, 8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[EquipmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
@ApplicationName nvarchar(256),
@UserNames	nvarchar(4000),
@RoleNames	nvarchar(4000)
AS
BEGIN
DECLARE @AppId uniqueidentifier
SELECT @AppId = NULL
SELECT @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
IF (@AppId IS NULL)
RETURN(2)


DECLARE @TranStarted bit
SET @TranStarted = 0

IF( @@TRANCOUNT = 0 )
BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
END

DECLARE @tbNames table(Name nvarchar(256) NOT NULL PRIMARY KEY)
DECLARE @tbRoles table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
DECLARE @tbUsers table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
DECLARE @Num	int
DECLARE @Pos	int
DECLARE @NextPos int
DECLARE @Name	nvarchar(256)
DECLARE @CountAll int
DECLARE @CountU	int
DECLARE @CountR	int


SET @Num = 0
SET @Pos = 1
WHILE(@Pos <= LEN(@RoleNames))
BEGIN
SELECT @NextPos = CHARINDEX(N',', @RoleNames, @Pos)
IF (@NextPos = 0 OR @NextPos IS NULL)
SELECT @NextPos = LEN(@RoleNames) + 1
SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
SELECT @Pos = @NextPos+1

INSERT INTO @tbNames VALUES (@Name)
SET @Num = @Num + 1
END

INSERT INTO @tbRoles
SELECT RoleId
FROM dbo.aspnet_Roles ar, @tbNames t
WHERE LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
SELECT @CountR = @@ROWCOUNT

IF (@CountR <> @Num)
BEGIN
SELECT TOP 1 N'', Name
FROM @tbNames
WHERE LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar, @tbRoles r WHERE r.RoleId = ar.RoleId)
IF( @TranStarted = 1 )
ROLLBACK TRANSACTION
RETURN(2)
END


DELETE FROM @tbNames WHERE 1=1
SET @Num = 0
SET @Pos = 1


WHILE(@Pos <= LEN(@UserNames))
BEGIN
SELECT @NextPos = CHARINDEX(N',', @UserNames, @Pos)
IF (@NextPos = 0 OR @NextPos IS NULL)
SELECT @NextPos = LEN(@UserNames) + 1
SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
SELECT @Pos = @NextPos+1

INSERT INTO @tbNames VALUES (@Name)
SET @Num = @Num + 1
END

INSERT INTO @tbUsers
SELECT UserId
FROM dbo.aspnet_Users ar, @tbNames t
WHERE LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

SELECT @CountU = @@ROWCOUNT
IF (@CountU <> @Num)
BEGIN
SELECT TOP 1 Name, N''
FROM @tbNames
WHERE LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au, @tbUsers u WHERE u.UserId = au.UserId)

IF( @TranStarted = 1 )
ROLLBACK TRANSACTION
RETURN(1)
END

SELECT @CountAll = COUNT(*)
FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
WHERE ur.UserId = u.UserId AND ur.RoleId = r.RoleId

IF (@CountAll <> @CountU * @CountR)
BEGIN
SELECT TOP 1 UserName, RoleName
FROM	@tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
WHERE	u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
IF( @TranStarted = 1 )
ROLLBACK TRANSACTION
RETURN(3)
END

DELETE FROM dbo.aspnet_UsersInRoles
WHERE UserId IN (SELECT UserId FROM @tbUsers)
AND RoleId IN (SELECT RoleId FROM @tbRoles)
IF( @TranStarted = 1 )
COMMIT TRANSACTION
RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256),
    @RoleName nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT @RoleId = NULL

    SELECT @UserId = UserId
    FROM dbo.aspnet_Users
    WHERE LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT @RoleId = RoleId
    FROM dbo.aspnet_Roles
    WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName nvarchar(256),
    @RoleName nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT @RoleId = NULL

     SELECT @RoleId = RoleId
     FROM dbo.aspnet_Roles
     WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName nvarchar(256),
    @UserName nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT @UserId = NULL

    SELECT @UserId = UserId
    FROM dbo.aspnet_Users
    WHERE LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName nvarchar(256),
    @RoleName nvarchar(256),
    @UserNameToMatch nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT @ApplicationId = NULL
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT @RoleId = NULL

     SELECT @RoleId = RoleId
     FROM dbo.aspnet_Roles
     WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
@ApplicationName nvarchar(256),
@UserNames	nvarchar(4000),
@RoleNames	nvarchar(4000),
@CurrentTimeUtc datetime
AS
BEGIN
DECLARE @AppId uniqueidentifier
SELECT @AppId = NULL
SELECT @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
IF (@AppId IS NULL)
RETURN(2)
DECLARE @TranStarted bit
SET @TranStarted = 0

IF( @@TRANCOUNT = 0 )
BEGIN
BEGIN TRANSACTION
SET @TranStarted = 1
END

DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
DECLARE @Num	int
DECLARE @Pos	int
DECLARE @NextPos	int
DECLARE @Name	nvarchar(256)

SET @Num = 0
SET @Pos = 1
WHILE(@Pos <= LEN(@RoleNames))
BEGIN
SELECT @NextPos = CHARINDEX(N',', @RoleNames, @Pos)
IF (@NextPos = 0 OR @NextPos IS NULL)
SELECT @NextPos = LEN(@RoleNames) + 1
SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
SELECT @Pos = @NextPos+1

INSERT INTO @tbNames VALUES (@Name)
SET @Num = @Num + 1
END

INSERT INTO @tbRoles
SELECT RoleId
FROM dbo.aspnet_Roles ar, @tbNames t
WHERE LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

IF (@@ROWCOUNT <> @Num)
BEGIN
SELECT TOP 1 Name
FROM @tbNames
WHERE LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar, @tbRoles r WHERE r.RoleId = ar.RoleId)
IF( @TranStarted = 1 )
ROLLBACK TRANSACTION
RETURN(2)
END

DELETE FROM @tbNames WHERE 1=1
SET @Num = 0
SET @Pos = 1

WHILE(@Pos <= LEN(@UserNames))
BEGIN
SELECT @NextPos = CHARINDEX(N',', @UserNames, @Pos)
IF (@NextPos = 0 OR @NextPos IS NULL)
SELECT @NextPos = LEN(@UserNames) + 1
SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
SELECT @Pos = @NextPos+1

INSERT INTO @tbNames VALUES (@Name)
SET @Num = @Num + 1
END

INSERT INTO @tbUsers
SELECT UserId
FROM dbo.aspnet_Users ar, @tbNames t
WHERE LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

IF (@@ROWCOUNT <> @Num)
BEGIN
DELETE FROM @tbNames
WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au, @tbUsers u WHERE au.UserId = u.UserId)

INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
FROM @tbNames

INSERT INTO @tbUsers
SELECT UserId
FROM	dbo.aspnet_Users au, @tbNames t
WHERE LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
END

IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
BEGIN
SELECT TOP 1 UserName, RoleName
FROM	dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
WHERE	u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

IF( @TranStarted = 1 )
ROLLBACK TRANSACTION
RETURN(3)
END

INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
SELECT UserId, RoleId
FROM @tbUsers, @tbRoles

IF( @TranStarted = 1 )
COMMIT TRANSACTION
RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[procListRoleComponentPermission]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoleComponentPermission]	
@RoleId uniqueidentifier = null
, @ComponentId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	RCP.RoleComponentPermissionId, RCP.RoleId, R.RoleName, RCP.ComponentId,
C.Name as ComponentName, RCP.WriteRight,
RCP.Concurrency, RCP.DateCreated, RCP.DateUpdated, RCP.CreatedBy, RCP.UpdatedBy
FROM	RoleComponentPermission RCP
INNER JOIN Component C on RCP.ComponentId = C.ComponentId
INNER JOIN aspnet_Roles R on R.RoleId = RCP.RoleId
WHERE	(@RoleId is null OR RCP.RoleId = @RoleId)
AND	(@ComponentId is null OR C.ComponentId = @ComponentId)

END
GO
/****** Object:  StoredProcedure [dbo].[procSaveRoleComponentPermission]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveRoleComponentPermission]	
@RoleComponentPermissionID int OUTPUT,	
@RoleId uniqueidentifier,
@ComponentId int,
@WriteRight bit,
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @RoleComponentPermissionID IS NULL BEGIN

INSERT INTO RoleComponentPermission(
RoleId,
ComponentId,
WriteRight,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@RoleId,
@ComponentId,
@WriteRight,	
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @RoleComponentPermissionID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE RoleComponentPermission SET
RoleId = @RoleId,
ComponentId = @ComponentId,
WriteRight = @WriteRight,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoleComponentPermissionID = @RoleComponentPermissionID

END

SELECT Concurrency FROM RoleComponentPermission WHERE RoleComponentPermissionID = @RoleComponentPermissionID
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveOrganisation]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveOrganisation]

@OrganisationID int OUTPUT,
@Name varchar(128),
@ContactInformationID int,	
@DisplayIndex int,
@IsLegacy bit,
@AuthorisationCode VARCHAR(128),
@CurrentUser varchar(128)
AS BEGIN

SET NOCOUNT ON

-- Insert record
IF @OrganisationID IS NULL
BEGIN

INSERT INTO Organisation(
Name ,
ContactInformationID ,	
DisplayIndex ,
IsLegacy ,
AuthorisationCode,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@Name ,
@ContactInformationID ,	
@DisplayIndex ,
@IsLegacy ,
@AuthorisationCode,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @OrganisationID = SCOPE_IDENTITY()
END

-- Update record
ELSE
BEGIN
UPDATE Organisation SET

Name = @Name ,
ContactInformationID = @ContactInformationID ,	
DisplayIndex = @DisplayIndex ,
IsLegacy = @IsLegacy ,
AuthorisationCode = @AuthorisationCode,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE OrganisationID = @OrganisationID

END

-- Select concurrency
SELECT Concurrency FROM Organisation WHERE OrganisationID = @OrganisationID

-- ======================================================================
-- Change History
-- ======================================================================
-- 21-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveEquipment]

@EquipmentId int OUTPUT,
@OrganisationId int,
@EquipmentName varchar(50),
@Description varchar(128),
@IsLegacy bit,
@RealPrice decimal(20,8),
@RentPrice decimal(20,8),
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
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE EquipmentId = @EquipmentId

END

SELECT Concurrency FROM Equipment WHERE EquipmentId = @EquipmentId
END
GO
/****** Object:  StoredProcedure [dbo].[procGetOrganisationByAuthorisationCode]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procGetOrganisationByAuthorisationCode]

@AuthorisationCode varchar (128)

AS
BEGIN

SET NOCOUNT ON

SELECT
OrganisationID,
O.Name,
ContactInformationID,
DisplayIndex,
IsLegacy,
AuthorisationCode,
Concurrency,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy,
(CASE WHEN
(SELECT TOP 1 S.OrganisationID FROM Site S WHERE S.OrganisationID = O.OrganisationID) IS NOT NULL THEN 0
ELSE 1 END) As CanDelete
FROM
dbo.Organisation AS O
WHERE
AuthorisationCode = @AuthorisationCode

-- ======================================================================
-- Change History
-- ======================================================================
-- 23-Feb-2014 HT
-- INIT

END
GO
/****** Object:  StoredProcedure [dbo].[procGetOrganisation]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procGetOrganisation]
@OrganisationID int
AS
BEGIN

SET NOCOUNT ON

SELECT
OrganisationID,
O.Name,
ContactInformationID,
DisplayIndex,
IsLegacy,
AuthorisationCode,
Concurrency,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy,
(CASE WHEN
(SELECT TOP 1 S.OrganisationID FROM Site S WHERE S.OrganisationID = O.OrganisationID) IS NOT NULL THEN 0
ELSE 1 END) As CanDelete

FROM
dbo.Organisation AS O
WHERE
OrganisationID = @OrganisationID

-- ======================================================================
-- Change History
-- ======================================================================
-- 24-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procListEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListEquipment]	
@OrganisationId int = null,
@EquipmentId int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.EquipmentId, RE.OrganisationId, RE.EquipmentName,
RE.IsLegacy, RE.Description, RE.RealPrice, RE.RentPrice,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	Equipment RE
WHERE	(@EquipmentId is null OR RE.EquipmentId = @EquipmentId)
AND	(@OrganisationId is null OR RE.OrganisationId = @OrganisationId)
AND	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END
GO
/****** Object:  StoredProcedure [dbo].[procListOrganisation]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListOrganisation]

AS
BEGIN
SET NOCOUNT ON

SELECT
OrganisationID,
O.Name,
ContactInformationID,
DisplayIndex,
IsLegacy,
AuthorisationCode,
Concurrency,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy,
(CASE WHEN
(SELECT TOP 1 S.OrganisationID FROM Site S WHERE S.OrganisationID = O.OrganisationID) IS NOT NULL THEN 0
ELSE 1 END) As CanDelete	
FROM
dbo.Organisation AS O
-- ======================================================================
-- Change History
-- ======================================================================
-- 23-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procListSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListSite]
(
@OrganisationID int = null
, @SiteID int = null
, @ShowLegacy bit = null
)
AS
BEGIN
SET NOCOUNT ON

SELECT [SiteID]
,[OrganisationID]
,[HotelID]
,[Name]
,[AbbreviatedName]
,[ContactInformationID]
,[LicenseKey]
,[StarRating]
,[PropCode]
,[DisplayIndex]
,[IsLegacy]
,[Availability]
,[Concurrency]
,[DateCreated]
,[DateUpdated]
,[CreatedBy]
,[UpdatedBy]
FROM dbo.Site AS S
WHERE
(@OrganisationID IS NULL OR @OrganisationID = S.OrganisationID)
AND (@SiteID IS NULL OR @SiteID = S.OrganisationID)
AND	(@ShowLegacy = 1 OR S.IsLegacy = 0)
ORDER BY DisplayIndex	

-- ======================================================================
-- Change History
-- ======================================================================
-- 21-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procListService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListService]	
@OrganisationId int = null,
@ServiceId int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.ServiceId, RE.OrganisationId, RE.Name,
RE.IsLegacy, RE.Description, RE.Price,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	Service RE
WHERE	(@ServiceId is null OR RE.ServiceId = @ServiceId)
AND	(@OrganisationId is null OR RE.OrganisationId = @OrganisationId)
AND	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[IsLegacy] [bit] NOT NULL,
	[OrganisationId] [int] NULL,
	[SiteId] [int] NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[RoomTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoleAuth]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRoleAuth](
	[UserRoleAuthId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[WholeOrg] [bit] NULL,
	[SiteGroupId] [int] NULL,
	[SiteId] [int] NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_UserRoleAuth] PRIMARY KEY CLUSTERED 
(
	[UserRoleAuthId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[SiteGroupSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SiteGroupSite](
	[SiteGroupSiteId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[SiteGroupId] [int] NOT NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_SiteGroupSite] PRIMARY KEY CLUSTERED 
(
	[SiteGroupSiteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procSaveSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object: StoredProcedure [dbo].[procSaveSite1] Script Date: 04/03/2012 14:43:56 ******/
CREATE PROCEDURE [dbo].[procSaveSite]
@SiteID int OUTPUT,
@OrganisationID int,
@HotelId int,
@Name varchar(128),
@AbbreviatedName varchar (10),
@ContactInformationID int,
@LicenseKey uniqueidentifier,
@StarRating decimal(2,1),
@PropCode varchar (50),
@DisplayIndex int,
@IsLegacy bit,
@Availability int,
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @SiteID IS NULL BEGIN

INSERT INTO Site(
OrganisationID,
HotelId,
Name,
AbbreviatedName,
ContactInformationID,
LicenseKey,
StarRating,
PropCode,
DisplayIndex,
IsLegacy,
Availability,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@OrganisationID,
@HotelId,
@Name,
@AbbreviatedName,
@ContactInformationID,
@LicenseKey,
@StarRating,
@PropCode,
@DisplayIndex,
@IsLegacy,
@Availability,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)
SET @SiteID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE Site SET

OrganisationID = @OrganisationID ,
HotelID = @HotelId,
Name = @Name ,
AbbreviatedName = @AbbreviatedName,
ContactInformationID = @ContactInformationID,
LicenseKey = @LicenseKey,
StarRating = @StarRating,
PropCode = @PropCode,
DisplayIndex = @DisplayIndex,
IsLegacy = @IsLegacy,
Availability = @Availability,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE SiteID = @SiteID

END

SELECT Concurrency FROM Site WHERE SiteID = @SiteID

-- ======================================================================
-- Change History
-- ======================================================================
-- 27-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveService]

@ServiceId int OUTPUT,
@OrganisationId int,
@Name varchar(50),
@Description varchar(128),
@IsLegacy bit,
@Price decimal(20,8),
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
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE ServiceId = @ServiceId

END

SELECT Concurrency FROM Service WHERE ServiceId = @ServiceId
END
GO
/****** Object:  Table [dbo].[Room]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[RoomName] [varchar](200) NOT NULL,
	[RoomStatusId] [int] NOT NULL,
	[RoomTypeId] [int] NULL,
	[IsLegacy] [bit] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Width] [decimal](20, 8) NULL,
	[Height] [decimal](20, 8) NULL,
	[MeterSquare] [decimal](20, 8) NULL,
	[Floor] [int] NULL,
	[BasePrice] [decimal](20, 8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procSaveRoomType]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveRoomType]

@RoomTypeId int OUTPUT,
@Name varchar(128),
@OrganisationId int,
@SiteId int,
@Description varchar(128),
@IsLegacy bit,	
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @RoomTypeId IS NULL
BEGIN

INSERT INTO RoomType(
Name,
OrganisationId,
SiteId,
IsLegacy,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@Name,
@OrganisationId,
@SiteId,
@IsLegacy,
@Description,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @RoomTypeId = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE RoomType SET
Name = @Name,
OrganisationId = @OrganisationId,
SiteId = @SiteId,
Description = @Description,	
IsLegacy = @IsLegacy,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoomTypeId = @RoomTypeId

END

SELECT Concurrency FROM RoomType WHERE RoomTypeId = @RoomTypeId
END
GO
/****** Object:  UserDefinedFunction [dbo].[ufnUserRoleMinLevel]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufnUserRoleMinLevel]
(
@UserId uniqueidentifier
)
RETURNS int
AS
BEGIN
DECLARE @Result int

SELECT @Result = min(RoleRank)
FROM
(SELECT [dbo].[ufnGetRoleRank](RoleID) AS RoleRank	
FROM UserRoleAuth
WHERE UserId = @UserId
) A

IF(@Result is null)
SELECT @Result = 1000

RETURN @Result

END
GO
/****** Object:  StoredProcedure [dbo].[procListUserRoleAuth]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListUserRoleAuth]	
@OrganisationId int = null
, @UserId uniqueidentifier = null
, @RoleId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON

SELECT	URA.UserRoleAuthId, URA.UserId, U.UserName, URA.RoleId, R.RoleName, URA.WholeOrg,
URA.SiteGroupId, SG.GroupName as SiteGroup,
URA.SiteId, S.Name as Site,
URA.Concurrency, URA.DateCreated, URA.DateUpdated, URA.CreatedBy, URA.UpdatedBy
FROM	UserRoleAuth URA
INNER JOIN aspnet_Roles R on R.RoleId = URA.RoleId
INNER JOIN aspnet_Users U on U.UserId = URA.UserId
LEFT OUTER JOIN SiteGroup SG on SG.SiteGroupId = URA.SiteGroupId
LEFT OUTER JOIN Site S ON URA.SiteId is not null AND URA.SiteId = S.SiteId
WHERE	(@OrganisationId is null OR U.OrganisationId = @OrganisationId)
AND	(@RoleId is null OR URA.RoleId = @RoleId)
AND	(@UserId is null OR URA.UserId = @UserId)
ORDER BY U.UserName

END
GO
/****** Object:  StoredProcedure [dbo].[procListUserName]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListUserName]
(
@ApplicationName nvarchar(256),
@OrganisationId int = null,
@UserNameToMatch nvarchar(256)
)	
AS
BEGIN
SET NOCOUNT ON

DECLARE
@ApplicationId uniqueidentifier

    SELECT
@ApplicationId = NULL

    SELECT
@ApplicationId = ApplicationId
FROM
dbo.aspnet_Applications
WHERE
LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0
                
    SELECT
DISTINCT
UserName
    FROM
dbo.aspnet_Users U
INNER JOIN
UserRoleAuth URA ON URA.UserId = U.UserId
INNER JOIN
dbo.aspnet_Roles R ON R.RoleId = URA.RoleId
    WHERE
U.ApplicationId = @ApplicationId
AND (@OrganisationId is null OR U.OrganisationID = @OrganisationId)
AND U.LoweredUserName LIKE LOWER(@UserNameToMatch + '%')

    ORDER BY
UserName

-- ======================================================================
-- Change History
-- ======================================================================
-- 19-dec-2011 HT
-- User UserRoleAuth instead of aspnet_UsersInRoles
-- 15-Sep-2011 EJM
-- Added distinct as a workaround for the multi role workaround.

END
GO
/****** Object:  StoredProcedure [dbo].[procListSiteGroupSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListSiteGroupSite]	
@SiteGroupId int = null
, @SiteId int = null
AS
BEGIN
SET NOCOUNT ON
SELECT SGS.SiteGroupSiteId, SGS.SiteId, SGS.SiteGroupId, S.Name as SiteName, SG.GroupName,
SGS.Concurrency, SGS.DateCreated, SGS.DateUpdated, SGS.CreatedBy, SGS.UpdatedBy
FROM SiteGroupSite SGS
INNER JOIN Site S on (S.SiteId = SGS.SiteId)
INNER JOIN SiteGroup SG on (SGS.SiteGroupId = SGS.SiteGroupId)
WHERE (@SiteGroupId is null or SGS.SiteGroupId = @SiteGroupId)
AND (@SiteId is null or SGS.SiteId = @SiteId)
END
GO
/****** Object:  StoredProcedure [dbo].[procListSiteGroup]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListSiteGroup]	
@OrganisationId int = null	
, @SiteGroupId int = null
AS
BEGIN
SELECT distinct	SG.SiteGroupId, SG.GroupName, SG.Concurrency, SG.DateCreated, SG.DateUpdated, SG.CreatedBy, SG.UpdatedBy,
(CASE WHEN
(SELECT TOP 1 SGS.SiteGroupId FROM SiteGroupSite SGS WHERE SGS.SiteGroupId = SG.SiteGroupId) IS NOT NULL THEN 0
WHEN
(SELECT TOP 1 URA.SiteGroupId FROM UserRoleAuth URA WHERE URA.SiteGroupId = SG.SiteGroupId) IS NOT NULL THEN 0	
ELSE 1 END) As CanDelete
FROM	SiteGroup SG
LEFT OUTER JOIN SiteGroupSite SGS on SG.SiteGroupId = SGS.SiteGroupId
LEFT OUTER JOIN Site S on S.SiteId = SGS.SiteId
WHERE (@SiteGroupId is null OR SG.SiteGroupId = @SiteGroupId)
AND	(@OrganisationId is null OR S.OrganisationId is null OR S.OrganisationId = @OrganisationId)
ORDER BY SG.GroupName
END
GO
/****** Object:  StoredProcedure [dbo].[procListRoomType]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoomType]	
@OrganisationId int = null,
@SiteId int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RoomTypeId, RE.Name, RE.IsLegacy,
RE.Description, RE.OrganisationId, RE.SiteId,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RoomType RE
WHERE	(@OrganisationId is null OR RE.OrganisationId IS NULL OR RE.OrganisationId = @OrganisationId)
AND	(@SiteId is null OR RE.SiteId IS NULL OR RE.SiteId = @SiteId)
AND	(@ShowLegacy = 1 OR RE.IsLegacy = 0)

END
GO
/****** Object:  StoredProcedure [dbo].[procListRoleComponentPermissionByUser]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoleComponentPermissionByUser]	
@UserId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON

SELECT distinct	RCP.RoleComponentPermissionId, RCP.RoleId, R.RoleName, RCP.ComponentId,
C.Name as ComponentName, RCP.WriteRight,
RCP.Concurrency, RCP.DateCreated, RCP.DateUpdated, RCP.CreatedBy, RCP.UpdatedBy
FROM	RoleComponentPermission RCP
INNER JOIN Component C on RCP.ComponentId = C.ComponentId
INNER JOIN aspnet_Roles R on R.RoleId = RCP.RoleId
INNER JOIN UserRoleAuth URA on R.RoleId = RCP.RoleId
WHERE	(@UserId is null OR URA.UserId = @UserId)

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteSiteGroupSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteSiteGroupSite]
(
@SiteGroupSiteId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM SiteGroupSite WHERE SiteGroupSiteId = @SiteGroupSiteId

END
GO
/****** Object:  StoredProcedure [dbo].[procListAspRole]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListAspRole]	
@RoleId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON

SELECT	R.ApplicationId, R.RoleId, R.RoleName, R.LoweredRoleName, R.Description,
(CASE WHEN
(SELECT TOP 1 RCP.RoleId FROM RoleComponentPermission RCP WHERE RCP.RoleId = R.RoleId) IS NOT NULL THEN 0
WHEN
(SELECT TOP 1 URA.RoleId FROM UserRoleAuth URA WHERE URA.RoleId = R.RoleId) IS NOT NULL THEN 0	
ELSE 1 END) as CanDelete	
FROM	aspnet_Roles R	
WHERE	(@RoleId is null OR R.RoleId = @RoleId)

END
GO
/****** Object:  StoredProcedure [dbo].[procSaveUserRoleAuth]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveUserRoleAuth]	
@UserRoleAuthID int OUTPUT,	
@UserId uniqueidentifier,
@RoleId uniqueidentifier,
@WholeOrg bit,
@SiteGroupId int,
@SiteId int,
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @UserRoleAuthID IS NULL BEGIN

INSERT INTO UserRoleAuth(
UserId,
RoleId,
WholeOrg,
SiteGroupId,
SiteId,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@UserId,
@RoleId,
@WholeOrg,
@SiteGroupId,
@SiteId,	
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @UserRoleAuthID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE UserRoleAuth SET
UserId = @UserId,
RoleId = @RoleId,
WholeOrg = @WholeOrg,
SiteGroupId = @SiteGroupId,
SiteId = @SiteId,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE UserRoleAuthID = @UserRoleAuthID

END

SELECT Concurrency FROM UserRoleAuth WHERE UserRoleAuthID = @UserRoleAuthID
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveSiteGroupSite]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveSiteGroupSite]	
@SiteGroupSiteID int OUTPUT,	
@SiteGroupId int,
@SiteId int,
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @SiteGroupSiteID IS NULL BEGIN

INSERT INTO SiteGroupSite(
SiteGroupId,	
SiteId,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@SiteGroupId,	
@SiteId,	
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @SiteGroupSiteID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE SiteGroupSite SET
SiteGroupId = @SiteGroupId,
Siteid = @SiteId,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE SiteGroupSiteID = @SiteGroupSiteID

END

SELECT Concurrency FROM SiteGroupSite WHERE SiteGroupSiteID = @SiteGroupSiteID
END
GO
/****** Object:  Table [dbo].[RoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[RoomService](
	[RoomServiceId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Price] [decimal](20, 8) NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoomService] PRIMARY KEY CLUSTERED 
(
	[RoomServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[RoomEquipment](
	[RoomEquipmentId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[EquipmentId] [int] NOT NULL,
	[Price] [decimal](20, 8) NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_RoomEquipment] PRIMARY KEY CLUSTERED 
(
	[RoomEquipmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procListAspUser]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListAspUser]	
@OrganisationId int = null	
, @UserID uniqueidentifier = null
, @IsLegacy bit = null
AS
BEGIN
SET NOCOUNT ON
SELECT M.ApplicationId,M.UserId,Password,PasswordFormat,PasswordSalt,MobilePIN
,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut
,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate
,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount
,FailedPasswordAnswerAttemptWindowStart,Comment
,U.UserName,U.LoweredUserName,U.MobileAlias, U.IsAnonymous,U.LastActivityDate, U.OrganisationID	
,dbo.[ufnUserRoleMinLevel](M.UserId) as MinRoleLevel
FROM aspnet_Membership M
INNER JOIN aspnet_Users U on M.UserId = U.UserId	WHERE (@OrganisationId is null or U.OrganisationId = @OrganisationId)
AND (@UserID is null OR M.UserId = @UserID)
ORDER BY U.UserName


-- ======================================================================
-- Change History
-- ======================================================================
-- 23-Feb-2013 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procListRoom]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoom]	
@OrganisationId int,
@SiteId int = null,
@RoomId int = null,
@RoomName varchar(128) = null,
@RoomStatusIds varchar(128) = null,	
@RoomTypeIds varchar(128) = null,
@Floor int = null,
@ShowLegacy bit
AS
BEGIN
SET NOCOUNT ON

SELECT	R.RoomId, R.SiteId, R.RoomName, R.RoomStatusId, RS.Name as RoomStatus,
R.RoomTypeId, T.Name as RoomType, R.IsLegacy, R.Description,
R.Width, R.Height, R.MeterSquare, R.Floor, R.BasePrice,
R.Concurrency, R.DateCreated, R.DateUpdated, R.CreatedBy, R.UpdatedBy
FROM	Room R
INNER JOIN Site S on R.SiteId = S.SiteID
INNER JOIN RoomStatus RS on R.RoomStatusId = RS.RoomStatusId
INNER JOIN RoomType T on R.RoomTypeId = T.RoomTypeId
WHERE	S.OrganisationID = @OrganisationId
AND	(@SiteId is null OR S.SiteID = @SiteId)
AND	(@RoomId is null OR R.RoomId = @RoomId)
AND	(@RoomName IS NULL OR (R.RoomName like '%' + @RoomName + '%'))
AND	(@Floor IS NULL OR R.Floor = @Floor)
AND	(@RoomTypeIds IS NULL OR R.RoomTypeId in (SELECT ID FROM dbo.ufnSplitNumeric(',', @RoomTypeIds)))
AND	(@RoomStatusIds IS NULL OR R.RoomStatusId in (SELECT ID FROM dbo.ufnSplitNumeric(',', @RoomStatusIds)))
AND (@ShowLegacy = 1 OR R.IsLegacy = 0)

END
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[CustomerId] [int] NULL,
	[BookDate] [smalldatetime] NOT NULL,
	[BookingStatusId] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[RoomPrice] [decimal](20, 8) NULL,
	[TotalPrice] [decimal](20, 8) NULL,
	[ContractDateSign] [smalldatetime] NULL,
	[ContractDateStart] [smalldatetime] NULL,
	[ContractDateEnd] [smalldatetime] NULL,
	[ContractTotalPrice] [decimal](20, 8) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procSaveRoom]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object: StoredProcedure [dbo].[procSaveRoom1] Script Date: 04/03/2012 14:43:56 ******/
CREATE PROCEDURE [dbo].[procSaveRoom]
@RoomID int OUTPUT,
@SiteId int,
@RoomName varchar(128),
@RoomStatusId int,
@RoomTypeId int,
@IsLegacy bit,
@Description varchar(max),
@Width decimal(20,8),
@Height decimal(20,8),
@MeterSquare decimal(20,8),
@Floor int,
@BasePrice decimal(20,8),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF @RoomID IS NULL BEGIN

INSERT INTO Room(
SiteId,
RoomName,
RoomStatusId,
RoomTypeId,
IsLegacy,
Description,
Width,
Height,
MeterSquare,
[Floor],
BasePrice,
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@SiteId,
@RoomName,
@RoomStatusId,
@RoomTypeId,
@IsLegacy,
@Description,
@Width,
@Height,
@MeterSquare,
@Floor,
@BasePrice,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)
SET @RoomID = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE Room SET

SiteId = @SiteId,
RoomName = @RoomName,
RoomStatusId = @RoomStatusId,
RoomTypeId = @RoomTypeId,
IsLegacy = @IsLegacy,
Description = @Description,
Width = @Width,
Height = @Height,
MeterSquare = @MeterSquare,
[Floor] = @Floor,
BasePrice = @BasePrice,
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoomID = @RoomID

END

SELECT Concurrency FROM Room WHERE RoomID = @RoomID

-- ======================================================================
-- Change History
-- ======================================================================
-- 27-Feb-2014 HT
-- INIT
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveBooking]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveBooking]	
@BookingID int OUTPUT,
@RoomId int,
@CustomerId int,
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
/****** Object:  StoredProcedure [dbo].[procSaveRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveRoomService]

@RoomServiceId int OUTPUT,
@RoomId int,
@ServiceId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM RoomService WHERE RoomId = @RoomId AND ServiceId = @ServiceId)
BEGIN

INSERT INTO RoomService(
RoomId,
ServiceId,
Price,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@RoomId,
@ServiceId,
@Price,
@Description,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @RoomServiceId = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE RoomService SET
RoomId = @RoomId,
ServiceId = @ServiceId,
Price = @Price,
Description = @Description,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoomId = @RoomId AND ServiceId = @ServiceId

END

SELECT Concurrency FROM RoomService WHERE RoomId = @RoomId AND ServiceId = @ServiceId
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveRoomEquipment]

@RoomEquipmentId int OUTPUT,
@RoomId int,
@EquipmentId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM RoomEquipment WHERE RoomId = @RoomId AND EquipmentId = @EquipmentId)
BEGIN

INSERT INTO RoomEquipment(
RoomId,
EquipmentId,
Price,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@RoomId,
@EquipmentId,
@Price,
@Description,
GETDATE(),
GETDATE(),
@CurrentUser,
@CurrentUser
)

SET @RoomEquipmentId = SCOPE_IDENTITY()

END ELSE BEGIN

UPDATE RoomEquipment SET
RoomId = @RoomId,
EquipmentId = @EquipmentId,
Price = @Price,
Description = @Description,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE RoomId = @RoomId AND EquipmentId = @EquipmentId

END

SELECT Concurrency FROM RoomEquipment WHERE RoomId = @RoomId AND EquipmentId = @EquipmentId
END
GO
/****** Object:  Table [dbo].[BookingRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[BookingRoomService](
	[BookingRoomServiceId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
	[RoomServiceId] [int] NOT NULL,
	[Price] [decimal](20, 8) NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingRoomService] PRIMARY KEY CLUSTERED 
(
	[BookingRoomServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookingRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[BookingRoomEquipment](
	[BookingRoomEquipmentId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
	[RoomEquipmentId] [int] NOT NULL,
	[Price] [decimal](20, 8) NULL,
	[Description] [varchar](max) NULL,
	[Concurrency] [timestamp] NOT NULL,
	[DateCreated] [smalldatetime] NOT NULL,
	[DateUpdated] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](128) NOT NULL,
	[UpdatedBy] [varchar](128) NOT NULL,
 CONSTRAINT [PK_BookingRoomEquipment] PRIMARY KEY CLUSTERED 
(
	[BookingRoomEquipmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[procListRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoomService]	
@RoomServiceId int = null,
@RoomId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RoomServiceId, RE.RoomId, RE.ServiceId, E.Name as Service,
RE.Price, RE.Description,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RoomService RE
INNER JOIN Service E on RE.ServiceId = e.ServiceId
WHERE	(@RoomServiceId is null OR RE.RoomServiceId = @RoomServiceId)
AND	(@RoomId is null OR RE.RoomId = @RoomId)

END
GO
/****** Object:  StoredProcedure [dbo].[procListRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListRoomEquipment]	
@RoomEquipmentId int = null,
@RoomId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	RE.RoomEquipmentId, RE.RoomId, RE.EquipmentId, e.EquipmentName as Equipment,
RE.Price, RE.Description,
RE.Concurrency, RE.DateCreated, RE.DateUpdated, RE.CreatedBy, RE.UpdatedBy
FROM	RoomEquipment RE
INNER JOIN Equipment E on RE.EquipmentId = e.EquipmentId
WHERE	(@RoomEquipmentId is null OR RE.RoomEquipmentId = @RoomEquipmentId)
AND	(@RoomId is null OR RE.RoomId = @RoomId)

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteRoomService]
(
@RoomServiceId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM RoomService WHERE RoomServiceId = @RoomServiceId

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteRoomEquipment]
(
@RoomEquipmenId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM RoomEquipment WHERE RoomEquipmentId = @RoomEquipmenId

END
GO
/****** Object:  StoredProcedure [dbo].[procListBooking]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListBooking]	
@OrganisationId int	
, @SiteId int = null
, @RoomId int = null
, @RoomName varchar(128) = null
, @BookingId int = null
, @BookingStatusIds varchar(128) = null
, @CustomerName varchar(128) = null
, @BookDateStart smalldatetime = null
, @BookDateEnd smalldatetime = null
AS
BEGIN
SELECT TOP 1000
B.[BookingId]
,S.SiteID, S.Name as SiteName
,R.RoomId, R.RoomName
,B.[CustomerId]
,C.FirstName
,C.LastName
,C.FirstName + ' ' + C.LastName as CustomerName
,B.[BookDate]
,B.[BookingStatusId]
,BS.Name as BookingStatus
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
INNER JOIN BookingStatus BS on B.BookingStatusId = BS.BookingStatusId
LEFT OUTER JOIN Customer C on C.CustomerId = B.CustomerId
WHERE S.OrganisationID = @OrganisationId
AND (@SiteId IS NULL OR S.SiteID = @SiteId)
AND (@BookingId IS NULL OR B.BookingId = @BookingId)
AND (@SiteId IS NULL OR S.SiteID = @SiteId)
ANd (@RoomName IS NULL OR (R.RoomName like '%' + @RoomName + '%'))
ANd (@CustomerName IS NULL OR (C.FirstName like '%' + @CustomerName + '%')
OR (C.LastName like '%' + @CustomerName + '%'))
AND (@BookDateStart IS NULL OR B.BookDate >= @BookDateStart)
AND (@BookDateEnd IS NULL OR B.BookDate <= @BookDateEnd)
ORDER BY B.BookDate, S.DisplayIndex, R.RoomName, BS.BookingStatusId
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveBookingRoomService]

@BookingRoomServiceId int OUTPUT,
@BookingId int,
@RoomServiceId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM BookingRoomService WHERE BookingId = @BookingId AND RoomServiceId = @RoomServiceId)
BEGIN

INSERT INTO BookingRoomService(
BookingId,
RoomServiceId,
Price,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@BookingId,
@RoomServiceId,
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
RoomServiceId = @RoomServiceId,
Price = @Price,
Description = @Description,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE BookingId = @BookingId AND RoomServiceId = @RoomServiceId

END

SELECT Concurrency FROM BookingRoomService WHERE BookingId = @BookingId AND RoomServiceId = @RoomServiceId
END
GO
/****** Object:  StoredProcedure [dbo].[procSaveBookingRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procSaveBookingRoomEquipment]

@BookingRoomEquipmentId int OUTPUT,
@BookingId int,
@RoomEquipmentId int,
@Price decimal(20,8),
@Description varchar(128),
@CurrentUser varchar(128)
AS
BEGIN
SET NOCOUNT ON

IF NOT EXISTS (SELECT * FROM BookingRoomEquipment WHERE BookingId = @BookingId AND RoomEquipmentId = @RoomEquipmentId)
BEGIN

INSERT INTO BookingRoomEquipment(
BookingId,
RoomEquipmentId,
Price,
Description,	
DateCreated,
DateUpdated,
CreatedBy,
UpdatedBy
) VALUES (
@BookingId,
@RoomEquipmentId,
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
RoomEquipmentId = @RoomEquipmentId,
Price = @Price,
Description = @Description,	
DateUpdated = GETDATE(),
UpdatedBy = @CurrentUser

WHERE BookingId = @BookingId AND RoomEquipmentId = @RoomEquipmentId

END

SELECT Concurrency FROM BookingRoomEquipment WHERE BookingId = @BookingId AND RoomEquipmentId = @RoomEquipmentId
END
GO
/****** Object:  StoredProcedure [dbo].[procListBookingRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListBookingRoomService]	
@BookingRoomServiceId int = null,
@BookingId int = null,
@RoomServiceId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	BRE.BookingRoomServiceId, BRE.BookingId, BRE.RoomServiceId, RE.ServiceId, E.Name as Service,
BRE.Price, BRE.Description,
BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
FROM	BookingRoomService BRE
INNER JOIN RoomService RE on BRE.RoomServiceId = RE.RoomServiceId
INNER JOIN Service E on RE.ServiceId = e.ServiceId
WHERE	(@BookingRoomServiceId is null OR BRE.BookingRoomServiceId = @BookingRoomServiceId)
AND	(@BookingId is null OR BRE.BookingId = @BookingId)
AND	(@RoomServiceId is null OR BRE.RoomServiceId = @RoomServiceId)

END
GO
/****** Object:  StoredProcedure [dbo].[procListBookingRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procListBookingRoomEquipment]	
@BookingRoomEquipmentId int = null,
@BookingId int = null,
@RoomEquipmentId int = null
AS
BEGIN
SET NOCOUNT ON

SELECT	BRE.BookingRoomEquipmentId, BRE.BookingId, BRE.RoomEquipmentId, RE.EquipmentId, e.EquipmentName as Equipment,
BRE.Price, BRE.Description,
BRE.Concurrency, BRE.DateCreated, BRE.DateUpdated, BRE.CreatedBy, BRE.UpdatedBy
FROM	BookingRoomEquipment BRE
INNER JOIN RoomEquipment RE on BRE.RoomEquipmentId = RE.RoomEquipmentId
INNER JOIN Equipment E on RE.EquipmentId = e.EquipmentId
WHERE	(@BookingRoomEquipmentId is null OR BRE.BookingRoomEquipmentId = @BookingRoomEquipmentId)
AND	(@BookingId is null OR BRE.BookingId = @BookingId)
AND	(@RoomEquipmentId is null OR BRE.RoomEquipmentId = @RoomEquipmentId)

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteBookingRoomService]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteBookingRoomService]
(
@BookingRoomEquipmenId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM BookingRoomService WHERE BookingRoomServiceId = @BookingRoomEquipmenId

END
GO
/****** Object:  StoredProcedure [dbo].[procDeleteBookingRoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[procDeleteBookingRoomEquipment]
(
@BookingRoomEquipmenId int
)
AS
BEGIN
SET NOCOUNT ON

DELETE FROM BookingRoomEquipment WHERE BookingRoomEquipmentId = @BookingRoomEquipmenId

END
GO
/****** Object:  Default [DF_RoomStatus_IsLegacy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[RoomStatus] ADD  CONSTRAINT [DF_RoomStatus_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_RoomStatus_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[RoomStatus] ADD  CONSTRAINT [DF_RoomStatus_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoomStatus_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[RoomStatus] ADD  CONSTRAINT [DF_RoomStatus_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoomStatus_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[RoomStatus] ADD  CONSTRAINT [DF_RoomStatus_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoomStatus_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[RoomStatus] ADD  CONSTRAINT [DF_RoomStatus_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_ImageType_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ImageType_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ImageType_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_ImageType_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[ImageType] ADD  CONSTRAINT [DF_ImageType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Component_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Component] ADD  CONSTRAINT [DF_Component_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Component_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Component] ADD  CONSTRAINT [DF_Component_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Component_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Component] ADD  CONSTRAINT [DF_Component_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Component_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Component] ADD  CONSTRAINT [DF_Component_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_BookingStatus_IsLegacy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[BookingStatus] ADD  CONSTRAINT [DF_BookingStatus_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_BookingStatus_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[BookingStatus] ADD  CONSTRAINT [DF_BookingStatus_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_BookingStatus_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[BookingStatus] ADD  CONSTRAINT [DF_BookingStatus_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_BookingStatus_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[BookingStatus] ADD  CONSTRAINT [DF_BookingStatus_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_BookingStatus_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[BookingStatus] ADD  CONSTRAINT [DF_BookingStatus_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Customer_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Customer_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Customer_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Customer_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Country_DateCreated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Country_DateUpdated]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Country_CreatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Country_UpdatedBy]    Script Date: 04/19/2014 17:37:02 ******/
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_ContactType_DateCreated]    Script Date: 04/19/2014 17:37:03 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ContactType_DateUpdated]    Script Date: 04/19/2014 17:37:03 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ContactType_CreatedBy]    Script Date: 04/19/2014 17:37:03 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_ContactType_UpdatedBy]    Script Date: 04/19/2014 17:37:03 ******/
ALTER TABLE [dbo].[ContactType] ADD  CONSTRAINT [DF_ContactType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF__aspnet_Ap__Appli__51300E55]    Script Date: 04/19/2014 17:37:03 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD  DEFAULT (newid()) FOR [ApplicationId]
GO
/****** Object:  Default [DF_SiteGroup_DateCreated]    Script Date: 04/19/2014 17:37:16 ******/
ALTER TABLE [dbo].[SiteGroup] ADD  CONSTRAINT [DF_SiteGroup_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_SiteGroup_DateUpdated]    Script Date: 04/19/2014 17:37:16 ******/
ALTER TABLE [dbo].[SiteGroup] ADD  CONSTRAINT [DF_SiteGroup_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_VisitorOfDay_Count]    Script Date: 04/24/2014 16:42:25 ******/
ALTER TABLE [dbo].[VisitorOfDay] ADD  CONSTRAINT [DF_VisitorOfDay_Count]  DEFAULT ((0)) FOR [Count]
GO
/****** Object:  Default [DF_SiteGroup_CreatedBy]    Script Date: 04/19/2014 17:37:16 ******/
ALTER TABLE [dbo].[SiteGroup] ADD  CONSTRAINT [DF_SiteGroup_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_SiteGroup_UpdatedBy]    Script Date: 04/19/2014 17:37:16 ******/
ALTER TABLE [dbo].[SiteGroup] ADD  CONSTRAINT [DF_SiteGroup_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF__TagVersio__DateC__55F4C372]    Script Date: 04/19/2014 17:37:17 ******/
ALTER TABLE [dbo].[TagVersion] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__aspnet_Us__UserI__56E8E7AB]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (newid()) FOR [UserId]
GO
/****** Object:  Default [DF__aspnet_Us__Mobil__57DD0BE4]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (NULL) FOR [MobileAlias]
GO
/****** Object:  Default [DF__aspnet_Us__IsAno__58D1301D]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT ((0)) FOR [IsAnonymous]
GO
/****** Object:  Default [DF__aspnet_Ro__RoleI__59C55456]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Roles] ADD  DEFAULT (newid()) FOR [RoleId]
GO
/****** Object:  Default [DF_ContactInformation_FirstName]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FirstName]  DEFAULT ('') FOR [FirstName]
GO
/****** Object:  Default [DF_ContactInformation_LastName]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_LastName]  DEFAULT ('') FOR [LastName]
GO
/****** Object:  Default [DF_Table_1_AddressLineOne]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineOne]  DEFAULT ('') FOR [Address]
GO
/****** Object:  Default [DF_ContactInformation_Address2]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Address2]  DEFAULT ('') FOR [Address2]
GO
/****** Object:  Default [DF_Table_1_AddressLineTwo]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_Table_1_AddressLineTwo]  DEFAULT ('') FOR [City]
GO
/****** Object:  Default [DF_ContactInformation_State]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_State]  DEFAULT ('') FOR [State]
GO
/****** Object:  Default [DF_ContactInformation_Postcode]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Postcode]  DEFAULT ('') FOR [Postcode]
GO
/****** Object:  Default [DF_ContactInformation_Country]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Country]  DEFAULT ('') FOR [CountryId]
GO
/****** Object:  Default [DF_ContactInformation_PhoneNumber]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_PhoneNumber]  DEFAULT ('') FOR [PhoneNumber]
GO
/****** Object:  Default [DF_ContactInformation_FaxNumber]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_FaxNumber]  DEFAULT ('') FOR [FaxNumber]
GO
/****** Object:  Default [DF_ContactInformation_Email]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_Email]  DEFAULT ('') FOR [Email]
GO
/****** Object:  Default [DF_ContactInformation_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_ContactInformation_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_ContactInformation_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_ContactInformation_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation] ADD  CONSTRAINT [DF_ContactInformation_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Image_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Image_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Image_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Image_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Image] ADD  CONSTRAINT [DF_Image_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RoleComponentPermission_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission] ADD  CONSTRAINT [DF_RoleComponentPermission_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoleComponentPermission_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission] ADD  CONSTRAINT [DF_RoleComponentPermission_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoleComponentPermission_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission] ADD  CONSTRAINT [DF_RoleComponentPermission_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoleComponentPermission_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission] ADD  CONSTRAINT [DF_RoleComponentPermission_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Organisation_DisplayIndex]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Organisation] ADD  CONSTRAINT [DF_Organisation_DisplayIndex]  DEFAULT ((0)) FOR [DisplayIndex]
GO
/****** Object:  Default [DF_Organisation_IsLegacy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Organisation] ADD  CONSTRAINT [DF_Organisation_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Organisation_AuthorisationCode]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Organisation] ADD  CONSTRAINT [DF_Organisation_AuthorisationCode]  DEFAULT (replace(CONVERT([varchar](100),newid(),(0)),'-','')) FOR [AuthorisationCode]
GO
/****** Object:  Default [DF__aspnet_Me__Passw__73852659]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Membership] ADD  DEFAULT ((0)) FOR [PasswordFormat]
GO
/****** Object:  Default [DF__Site__LicenseKey__74794A92]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  DEFAULT ('cfeb48b3-aa4b-46c8-9948-cd6412243ab6') FOR [LicenseKey]
GO
/****** Object:  Default [DF_Site_DisplayIndex]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_DisplayIndex]  DEFAULT ((0)) FOR [DisplayIndex]
GO
/****** Object:  Default [DF_Site_IsLegacy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Site_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Site_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Site_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_Site_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Service_IsLegacy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Service_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Service_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Service_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Service_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Equipment_IsLegacy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [DF_Equipment_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Equipment_DateCreated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [DF_Equipment_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Equipment_DateUpdated]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [DF_Equipment_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Equipment_CreatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [DF_Equipment_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Equipment_UpdatedBy]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment] ADD  CONSTRAINT [DF_Equipment_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RoomType_IsLegacy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_RoomType_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoomType_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoomType_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoomType_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType] ADD  CONSTRAINT [DF_RoomType_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_UserRoleAuth_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth] ADD  CONSTRAINT [DF_UserRoleAuth_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_UserRoleAuth_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth] ADD  CONSTRAINT [DF_UserRoleAuth_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_UserRoleAuth_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth] ADD  CONSTRAINT [DF_UserRoleAuth_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_UserRoleAuth_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth] ADD  CONSTRAINT [DF_UserRoleAuth_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_SiteGroupSite_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite] ADD  CONSTRAINT [DF_SiteGroupSite_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_SiteGroupSite_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite] ADD  CONSTRAINT [DF_SiteGroupSite_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_SiteGroupSite_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite] ADD  CONSTRAINT [DF_SiteGroupSite_CreatedBy]  DEFAULT ('') FOR [CreatedBy]
GO
/****** Object:  Default [DF_SiteGroupSite_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite] ADD  CONSTRAINT [DF_SiteGroupSite_UpdatedBy]  DEFAULT ('') FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Room_IsLegacy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_IsLegacy]  DEFAULT ((0)) FOR [IsLegacy]
GO
/****** Object:  Default [DF_Room_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Room_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Room_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Room_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room] ADD  CONSTRAINT [DF_Room_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RoomService_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService] ADD  CONSTRAINT [DF_RoomService_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoomService_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService] ADD  CONSTRAINT [DF_RoomService_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoomService_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService] ADD  CONSTRAINT [DF_RoomService_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoomService_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService] ADD  CONSTRAINT [DF_RoomService_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_RoomEquipment_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment] ADD  CONSTRAINT [DF_RoomEquipment_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_RoomEquipment_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment] ADD  CONSTRAINT [DF_RoomEquipment_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_RoomEquipment_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment] ADD  CONSTRAINT [DF_RoomEquipment_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_RoomEquipment_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment] ADD  CONSTRAINT [DF_RoomEquipment_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_Booking_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Booking_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_Booking_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_Booking_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking] ADD  CONSTRAINT [DF_Booking_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_BookingRoomService_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService] ADD  CONSTRAINT [DF_BookingRoomService_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_BookingRoomService_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService] ADD  CONSTRAINT [DF_BookingRoomService_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_BookingRoomService_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService] ADD  CONSTRAINT [DF_BookingRoomService_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_BookingRoomService_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService] ADD  CONSTRAINT [DF_BookingRoomService_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  Default [DF_BookingRoomEquipment_DateCreated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment] ADD  CONSTRAINT [DF_BookingRoomEquipment_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_BookingRoomEquipment_DateUpdated]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment] ADD  CONSTRAINT [DF_BookingRoomEquipment_DateUpdated]  DEFAULT (getdate()) FOR [DateUpdated]
GO
/****** Object:  Default [DF_BookingRoomEquipment_CreatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment] ADD  CONSTRAINT [DF_BookingRoomEquipment_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO
/****** Object:  Default [DF_BookingRoomEquipment_UpdatedBy]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment] ADD  CONSTRAINT [DF_BookingRoomEquipment_UpdatedBy]  DEFAULT (suser_sname()) FOR [UpdatedBy]
GO
/****** Object:  ForeignKey [FK__aspnet_Us__Appli__28ED12D1]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__Appli__29E1370A]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Ro__Appli__2AD55B43]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK_ContactInformation_ContactType]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation]  WITH CHECK ADD  CONSTRAINT [FK_ContactInformation_ContactType] FOREIGN KEY([ContactTypeId])
REFERENCES [dbo].[ContactType] ([ContactTypeId])
GO
ALTER TABLE [dbo].[ContactInformation] CHECK CONSTRAINT [FK_ContactInformation_ContactType]
GO
/****** Object:  ForeignKey [FK_ContactInformation_Country]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[ContactInformation]  WITH CHECK ADD  CONSTRAINT [FK_ContactInformation_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[ContactInformation] CHECK CONSTRAINT [FK_ContactInformation_Country]
GO

/****** Object:  ForeignKey [FK_Customer_Organisation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([OrganisationID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Organisation]
GO

/****** Object:  ForeignKey [FK_Image_ImageType]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_ImageType] FOREIGN KEY([ImageTypeId])
REFERENCES [dbo].[ImageType] ([ImageTypeId])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_ImageType]
GO
/****** Object:  ForeignKey [FK_LocaleStringResource_Language]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[LocaleStringResource]  WITH CHECK ADD  CONSTRAINT [FK_LocaleStringResource_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[LocaleStringResource] CHECK CONSTRAINT [FK_LocaleStringResource_Language]
GO
/****** Object:  ForeignKey [FK_RoleComponentPermission_Component]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission]  WITH CHECK ADD  CONSTRAINT [FK_RoleComponentPermission_Component] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[Component] ([ComponentId])
GO
ALTER TABLE [dbo].[RoleComponentPermission] CHECK CONSTRAINT [FK_RoleComponentPermission_Component]
GO
/****** Object:  ForeignKey [FK_RoleComponentPermission_Role]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[RoleComponentPermission]  WITH CHECK ADD  CONSTRAINT [FK_RoleComponentPermission_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RoleComponentPermission] CHECK CONSTRAINT [FK_RoleComponentPermission_Role]
GO
/****** Object:  ForeignKey [FK_Organisation_ContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Organisation]  WITH CHECK ADD  CONSTRAINT [FK_Organisation_ContactInformation] FOREIGN KEY([ContactInformationID])
REFERENCES [dbo].[ContactInformation] ([ContactInformationId])
GO
ALTER TABLE [dbo].[Organisation] CHECK CONSTRAINT [FK_Organisation_ContactInformation]
GO
/****** Object:  ForeignKey [FK__aspnet_Me__Appli__32767D0B]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
/****** Object:  ForeignKey [FK__aspnet_Me__UserI__336AA144]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__RoleI__345EC57D]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
/****** Object:  ForeignKey [FK__aspnet_Us__UserI__3552E9B6]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
/****** Object:  ForeignKey [FK_Site_ContactInformation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK_Site_ContactInformation] FOREIGN KEY([ContactInformationID])
REFERENCES [dbo].[ContactInformation] ([ContactInformationId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FK_Site_ContactInformation]
GO
/****** Object:  ForeignKey [FK_Site_Organisation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FK_Site_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([OrganisationID])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FK_Site_Organisation]
GO
/****** Object:  ForeignKey [FK_Service_Organisation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationID])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Organisation]
GO
/****** Object:  ForeignKey [FK_Equipment_Organisation]    Script Date: 04/19/2014 17:37:18 ******/
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationID])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FK_Equipment_Organisation]
GO
/****** Object:  ForeignKey [FK_RoomType_Organisation]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType]  WITH CHECK ADD  CONSTRAINT [FK_RoomType_Organisation] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationID])
GO
ALTER TABLE [dbo].[RoomType] CHECK CONSTRAINT [FK_RoomType_Organisation]
GO
/****** Object:  ForeignKey [FK_RoomType_Site]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomType]  WITH CHECK ADD  CONSTRAINT [FK_RoomType_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteID])
GO
ALTER TABLE [dbo].[RoomType] CHECK CONSTRAINT [FK_RoomType_Site]
GO
/****** Object:  ForeignKey [FK_UserRoleAuth_Role]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleAuth_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoleAuth] CHECK CONSTRAINT [FK_UserRoleAuth_Role]
GO
/****** Object:  ForeignKey [FK_UserRoleAuth_Site]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleAuth_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteID])
GO
ALTER TABLE [dbo].[UserRoleAuth] CHECK CONSTRAINT [FK_UserRoleAuth_Site]
GO
/****** Object:  ForeignKey [FK_UserRoleAuth_SiteGroup]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleAuth_SiteGroup] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[SiteGroup] ([SiteGroupId])
GO
ALTER TABLE [dbo].[UserRoleAuth] CHECK CONSTRAINT [FK_UserRoleAuth_SiteGroup]
GO
/****** Object:  ForeignKey [FK_UserRoleAuth_User]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[UserRoleAuth]  WITH CHECK ADD  CONSTRAINT [FK_UserRoleAuth_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[UserRoleAuth] CHECK CONSTRAINT [FK_UserRoleAuth_User]
GO
/****** Object:  ForeignKey [FK_SiteGroupSite_Site]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite]  WITH CHECK ADD  CONSTRAINT [FK_SiteGroupSite_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteID])
GO
ALTER TABLE [dbo].[SiteGroupSite] CHECK CONSTRAINT [FK_SiteGroupSite_Site]
GO
/****** Object:  ForeignKey [FK_SiteGroupSite_SiteGroup]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[SiteGroupSite]  WITH CHECK ADD  CONSTRAINT [FK_SiteGroupSite_SiteGroup] FOREIGN KEY([SiteGroupId])
REFERENCES [dbo].[SiteGroup] ([SiteGroupId])
GO
ALTER TABLE [dbo].[SiteGroupSite] CHECK CONSTRAINT [FK_SiteGroupSite_SiteGroup]
GO
/****** Object:  ForeignKey [FK_Room_RoomStatus]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomStatus] FOREIGN KEY([RoomStatusId])
REFERENCES [dbo].[RoomStatus] ([RoomStatusId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomStatus]
GO
/****** Object:  ForeignKey [FK_Room_RoomType]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomType] FOREIGN KEY([RoomTypeId])
REFERENCES [dbo].[RoomType] ([RoomTypeId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomType]
GO
/****** Object:  ForeignKey [FK_Room_Site]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteID])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Site]
GO
/****** Object:  ForeignKey [FK_RoomService_Room]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService]  WITH CHECK ADD  CONSTRAINT [FK_RoomService_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[RoomService] CHECK CONSTRAINT [FK_RoomService_Room]
GO
/****** Object:  ForeignKey [FK_RoomService_Service]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomService]  WITH CHECK ADD  CONSTRAINT [FK_RoomService_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([ServiceId])
GO
ALTER TABLE [dbo].[RoomService] CHECK CONSTRAINT [FK_RoomService_Service]
GO
/****** Object:  ForeignKey [FK_RoomEquipment_Equipment]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment]  WITH CHECK ADD  CONSTRAINT [FK_RoomEquipment_Equipment] FOREIGN KEY([EquipmentId])
REFERENCES [dbo].[Equipment] ([EquipmentId])
GO
ALTER TABLE [dbo].[RoomEquipment] CHECK CONSTRAINT [FK_RoomEquipment_Equipment]
GO
/****** Object:  ForeignKey [FK_RoomEquipment_Room]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[RoomEquipment]  WITH CHECK ADD  CONSTRAINT [FK_RoomEquipment_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[RoomEquipment] CHECK CONSTRAINT [FK_RoomEquipment_Room]
GO
/****** Object:  ForeignKey [FK_Booking_BookingStatus]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_BookingStatus] FOREIGN KEY([BookingStatusId])
REFERENCES [dbo].[BookingStatus] ([BookingStatusId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_BookingStatus]
GO
/****** Object:  ForeignKey [FK_Booking_Customer]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Customer]
GO
/****** Object:  ForeignKey [FK_Booking_Room]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Room]
GO
/****** Object:  ForeignKey [FK_BookingRoomService_Booking]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomService_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO
ALTER TABLE [dbo].[BookingRoomService] CHECK CONSTRAINT [FK_BookingRoomService_Booking]
GO
/****** Object:  ForeignKey [FK_BookingRoomService_RoomService]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomService]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomService_RoomService] FOREIGN KEY([RoomServiceId])
REFERENCES [dbo].[RoomService] ([RoomServiceId])
GO
ALTER TABLE [dbo].[BookingRoomService] CHECK CONSTRAINT [FK_BookingRoomService_RoomService]
GO
/****** Object:  ForeignKey [FK_BookingRoomEquipment_Booking]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomEquipment_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO
ALTER TABLE [dbo].[BookingRoomEquipment] CHECK CONSTRAINT [FK_BookingRoomEquipment_Booking]
GO
/****** Object:  ForeignKey [FK_BookingRoomEquipment_RoomEquipment]    Script Date: 04/19/2014 17:37:19 ******/
ALTER TABLE [dbo].[BookingRoomEquipment]  WITH CHECK ADD  CONSTRAINT [FK_BookingRoomEquipment_RoomEquipment] FOREIGN KEY([RoomEquipmentId])
REFERENCES [dbo].[RoomEquipment] ([RoomEquipmentId])
GO
ALTER TABLE [dbo].[BookingRoomEquipment] CHECK CONSTRAINT [FK_BookingRoomEquipment_RoomEquipment]
GO
