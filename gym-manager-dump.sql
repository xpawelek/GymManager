USE [master]
GO
/****** Object:  Database [GymManagerDb]    Script Date: 11/06/2025 16:41:42 ******/
CREATE DATABASE [GymManagerDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GymManagerDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER05\MSSQL\DATA\GymManagerDb.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GymManagerDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER05\MSSQL\DATA\GymManagerDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GymManagerDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GymManagerDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GymManagerDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GymManagerDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GymManagerDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GymManagerDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GymManagerDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [GymManagerDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GymManagerDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GymManagerDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GymManagerDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GymManagerDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GymManagerDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GymManagerDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GymManagerDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GymManagerDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GymManagerDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GymManagerDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GymManagerDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GymManagerDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GymManagerDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GymManagerDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GymManagerDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GymManagerDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GymManagerDb] SET RECOVERY FULL 
GO
ALTER DATABASE [GymManagerDb] SET  MULTI_USER 
GO
ALTER DATABASE [GymManagerDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GymManagerDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GymManagerDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GymManagerDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GymManagerDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GymManagerDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GymManagerDb', N'ON'
GO
ALTER DATABASE [GymManagerDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [GymManagerDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GymManagerDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/06/2025 16:41:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipments]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](300) NOT NULL,
	[Notes] [nvarchar](300) NULL,
	[PhotoPath] [nvarchar](max) NULL,
 CONSTRAINT [PK_Equipments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[MembershipCardNumber] [nvarchar](10) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Memberships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[MembershipTypeId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Memberships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembershipTypes]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[DurationInDays] [int] NOT NULL,
	[IncludesPersonalTrainer] [bit] NOT NULL,
	[PersonalTrainingsPerMonth] [int] NULL,
	[AllowTrainerSelection] [bit] NOT NULL,
	[IncludesProgressTracking] [bit] NOT NULL,
	[IsVisible] [bit] NOT NULL,
 CONSTRAINT [PK_MembershipTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageContent] [nvarchar](1000) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[MemberId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[SentByMember] [bit] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgressPhotos]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgressPhotos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[IsPublic] [bit] NOT NULL,
 CONSTRAINT [PK_ProgressPhotos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceRequests]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceProblemTitle] [nvarchar](50) NOT NULL,
	[ProblemNote] [nvarchar](250) NOT NULL,
	[EquipmentId] [int] NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[IsResolved] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainerAssignments]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainerAssignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_TrainerAssignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainers]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[PhotoPath] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Trainers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrainingSessions]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingSessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrainerId] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[IsGroupSession] [bit] NOT NULL,
	[MemberId] [int] NULL,
	[DurationInMinutes] [int] NOT NULL,
 CONSTRAINT [PK_TrainingSessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutNotes]    Script Date: 11/06/2025 16:41:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrentWeight] [float] NULL,
	[CurrentHeight] [float] NULL,
	[WorkoutInfo] [nvarchar](500) NULL,
	[WorkoutStartTime] [datetime2](7) NOT NULL,
	[MemberId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[TrainingSessionId] [int] NOT NULL,
 CONSTRAINT [PK_WorkoutNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250514192908_InitialCreate', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250517103653_RemoveWeeklySchedule', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250518133023_FixCascade', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250521214558_FixTrainerCascadeConflict', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250521214827_AddedEquipmentToServiceRequest', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250522202606_IsActiveToBeInDatabaseTrainerAssignment', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250522211436_SentByWhoMessage', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250523100524_ChangedTrainingSessionDurationDataType', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250523113756_IsActiveToDbFromMembership', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530143549_TrainingSessionWorkoutNoteCascade', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530143841_FixCascadeDeleteWorkoutNote', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530144723_FixCascadeInWorkoutNote', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530145652_Fix', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250530150321_AddWorkoutNoteTrainingSessionFk', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250531120948_AddedDateToServiceRequest', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250603124358_AddPhotoPathToEquipment', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250603210108_AllowNullPhotoPath', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250603224251_AllowNullPhotoPathTrainer', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250606192357_UpdateServiceRequestFields', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250606195832_UpdateServiceRequestFields2', N'8.0.5')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250608231503_AddWorkoutNoteFK', N'8.0.5')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'213417f4-f855-4b18-9ccf-4713624c5e3a', N'Member', N'MEMBER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'76a0dc23-ff43-4363-94c5-cb92e004787f', N'Receptionist', N'RECEPTIONIST', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9e1887e1-1ff6-462c-890d-f44bb2590191', N'Trainer', N'TRAINER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd2b41d63-a59b-47df-ba75-689146e774a8', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'02ca0993-1402-4770-bca6-7e112079d46e', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'080f715c-9bbe-4f11-b5f2-009585291b4b', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0cf95a78-afb5-4d24-bc1c-4240c4d3565f', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0dc490d5-f466-4a64-8c77-99f32a59af93', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17654199-4c10-4b5e-87d8-e9f4ad62cdfc', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17cd82a0-b8d7-4dcd-a3cb-b6e502bac0b9', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1f1fb704-7287-4d9a-81e4-adba74d43d6e', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'251a5fcd-00c1-41f9-a7d9-d999ed8e1eea', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'27cf8ffb-8e8b-4906-bd07-b28c5c3d6cbe', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30fb1037-53d4-4e6d-813b-7d413d8eabf3', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'312a8137-f9b7-494c-8809-9831628836ee', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3bb15303-5f86-4471-bf2b-a4daa2553a6f', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3c4d3abf-d1ce-4449-a7cb-9e9576f8ed57', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'45392617-2029-4f28-a3b5-74b45ebb33de', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4fd174f3-de3e-4536-898f-12b60a5a5be0', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'60930438-70c2-4bf0-9f16-575d91259880', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6cf6b090-5178-485f-9089-61868256733e', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'79c42384-7459-4490-b0ad-79b59db09d81', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7dd391d0-7493-4690-91fc-975cb06132db', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'84fc4144-ee29-4d6e-b0da-43b363566f86', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'939a3f07-d3c2-479c-8bd5-e7b373672d2a', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9a36a710-3f38-4312-b5ad-9fa0cf84793d', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9b2a6145-a148-4a38-87d0-7d02a7c5cd49', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a22ac8f2-c4b4-4cd7-99c1-753f777d7848', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b3c5dd54-8748-46b2-a7b5-c4c436c5507d', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e6df278b-9062-4ed6-bc05-9a098484feec', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e9c4fa1d-4abe-4572-8830-64b5371abda5', N'213417f4-f855-4b18-9ccf-4713624c5e3a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0023dbe6-979e-4da6-962b-fca98ba2edcb', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'26a9652f-6e11-475e-9464-cba05feef31c', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'272b69e9-00f0-4447-92e3-1428015294a4', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2cae5779-424a-4e23-a479-604e06cdfea9', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2e95a081-e9fa-439c-969a-7b161794d4a2', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5e10d96c-e2d5-4fa6-8f7d-c58cc92cbfb6', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'aa5d7d71-c1a9-4540-a478-016fa52e5558', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c7db0523-f23e-46dd-bd4e-f766aa3ca0e6', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd2019b42-d4aa-4134-8252-0e95ceff759f', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd9b496b1-b946-4f30-b494-55089ae29764', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'df167dd4-870b-48d0-842d-6b6e851080e1', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc661842-578c-42c2-b1dd-61075379e9a1', N'9e1887e1-1ff6-462c-890d-f44bb2590191')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ada2675a-a8ec-4fbd-a71e-950ca8de5bdf', N'd2b41d63-a59b-47df-ba75-689146e774a8')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0023dbe6-979e-4da6-962b-fca98ba2edcb', N'trainer4@gmail.com', N'TRAINER4@GMAIL.COM', N'trainer4@gmail.com', N'TRAINER4@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAED72aDB1PqgFvV5XRluFCGpz14uaOwZOmnFN56ClN8BBZy2VcmeO69M5DRz9Kv/SOA==', N'2QUKDFUS4ETKQDQ44RIJ7Z6EY6AFGIXO', N'5e0748c5-99f9-4ba5-9578-39e589f86bd6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'02ca0993-1402-4770-bca6-7e112079d46e', N'member13@gmail.com', N'MEMBER13@GMAIL.COM', N'member13@gmail.com', N'MEMBER13@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEO9k7P+T3iaPWl7bb1XYvCPGTnpKEDMsKweXxAefyvAuOnONl8HaaEvx1hFDm1ghuA==', N'RULNRQM2F5YVLMFIBSAPVKR6RJM2NPHE', N'7ec246ea-8735-49b5-80b3-ddd8a4fd8a90', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'080f715c-9bbe-4f11-b5f2-009585291b4b', N'member18@gmail.com', N'MEMBER18@GMAIL.COM', N'member18@gmail.com', N'MEMBER18@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEKyne4FXKvRT700GWj/rG7+nYLWqcrbBP4lP6vOJhQ6NJtULmvBpkgofsd3eoOZ88w==', N'4U2N7EKS2K3RU4X6SJ6RXUO4BGRJ66B6', N'4a167c30-981d-4204-a291-98b5df34fe38', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0cf95a78-afb5-4d24-bc1c-4240c4d3565f', N'member7@gmail.com', N'MEMBER7@GMAIL.COM', N'member7@gmail.com', N'MEMBER7@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPgNhXEimP119j9L17skZN2Hxd/15V1EQGYjw7nr5Kz+fjNI9t+lyqiwRMQ8MvLcRQ==', N'C445JSDTN3W2OVSNHOMEOKJHZXL6W5HN', N'3b07715e-c124-45b2-b975-7ce447bb901e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0dc490d5-f466-4a64-8c77-99f32a59af93', N'yin.yang@gmail.com', N'YIN.YANG@GMAIL.COM', N'yin.yang@gmail.com', N'YIN.YANG@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEDd5GBc8wIxwfHXwtXbHSvfBc4Fx09guBnvOBxxLL0a7M2LMxo56ESbt269NPIn4dA==', N'EWKIDODNYFX3JD7RZLDV5HQ7GKTLDXOX', N'fc74903c-b483-4057-b375-0ff10988745d', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'17654199-4c10-4b5e-87d8-e9f4ad62cdfc', N'member8@gmail.com', N'MEMBER8@GMAIL.COM', N'member8@gmail.com', N'MEMBER8@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAENdVYhdUriMp0zdxRgEamLNACIduW5j/1XUr9I+twYF5mm+4GWc+OQW0EnfgTSMx+w==', N'ZSKRX4UAX435RVOS6POJBUZKUDRQSSJN', N'5ace94c7-c6eb-422e-b7e5-9226e4d81f6b', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'17cd82a0-b8d7-4dcd-a3cb-b6e502bac0b9', N'member4@gmail.com', N'MEMBER4@GMAIL.COM', N'member4@gmail.com', N'MEMBER4@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEDZDvmtaHB22AkFj4vqDYsAru99/G/AEvYK7KoDaEEX0uSpVNkKbKCBGIB0qcCbUow==', N'QUGAMDHGHTW7UCCNXDSA5FFWYOBXA6IP', N'151a029d-5d26-4fb1-a9f1-0fc55bb4989c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1f1fb704-7287-4d9a-81e4-adba74d43d6e', N'pawel.nowak@gmail.com', N'PAWEL.NOWAK@GMAIL.COM', N'pawel.nowak@gmail.com', N'PAWEL.NOWAK@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEMP2Z4Wr7rbQ4/yL4OW3kiDFnW0oYQO6pzeUJFUor4vkC5N53Hqa/fGWJNQia5kjlg==', N'CEW2NTBYKFP7UCF5MH3O3UEA5RBDQ4RF', N'e6bf50ba-d46d-476f-895a-4520070ea2c4', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'251a5fcd-00c1-41f9-a7d9-d999ed8e1eea', N'member9@gmail.com', N'MEMBER9@GMAIL.COM', N'member9@gmail.com', N'MEMBER9@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAELYKBIryrTZdEncuXvdZYJIR29uOaJ09jxxy3LCJN3HJLiTPfhjzzAUZZJMrkbjzTw==', N'K4BLEP5QGN4FJL4VH7I2F6KKJ54POETC', N'64984366-b5eb-4124-bd13-3df68b1f3ae9', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'26a9652f-6e11-475e-9464-cba05feef31c', N'ojojojtrainer123@gmail.com', N'OJOJOJTRAINER123@GMAIL.COM', N'ojojojtrainer123@gmail.com', N'OJOJOJTRAINER123@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJBV6JTqSWkGh4yhEDqbHA1pV7zPlxdWbugnLEH3ssvPPTp2ACAmUVuOGsJhQ+u99A==', N'HHKFSESRFI6YJB44HLM24UZ6J7MM3ULC', N'e9d67819-e920-41c4-9581-e35d27516d8f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'272b69e9-00f0-4447-92e3-1428015294a4', N'as@sf', N'AS@SF', N'as@sf', N'AS@SF', 1, N'AQAAAAIAAYagAAAAEJ2b+982Cvq7xu9HPhvxC1YMoXM0oOV4u4RTclnTsKREjyBGSX7wBwdXIN6oz0FBuQ==', N'V353MHY4U3IIGAPPLM24GLOIBIL5JRAF', N'fd1d0a0f-fa3d-48b6-bc61-9b45a2e942c2', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'27cf8ffb-8e8b-4906-bd07-b28c5c3d6cbe', N'member21@gmail.com', N'MEMBER21@GMAIL.COM', N'member21@gmail.com', N'MEMBER21@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGA9FlBB4OibuH01KXfFm+F7hbB6vbAhQsUJ6yjb+Ad7CsHQQFZWshTsDfVMaxgnLQ==', N'FBKZ3ZQ3BL45QMU5C2DJ3WTZIH4NAOQD', N'3709fab7-520c-4c8e-8585-17afd1059ab2', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2cae5779-424a-4e23-a479-604e06cdfea9', N'trainer6@gmail.com', N'TRAINER6@GMAIL.COM', N'trainer6@gmail.com', N'TRAINER6@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEM8u4AxnsglSVSE5nZWJNsyPyudjph+zitQiLsizQsl9/Om3WPsLMFL0bWhPEANRHw==', N'Z3CHAY7VTDMGTQOWPLULG3FY5TMGKPLY', N'414d9fc8-7b37-4e77-8d12-ebcef2c4b990', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2e95a081-e9fa-439c-969a-7b161794d4a2', N'trainer@gmail.com', N'TRAINER@GMAIL.COM', N'trainer@gmail.com', N'TRAINER@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAELs6yki2UHhqCb2r49gXpeZoyB9aBio9+O1iUK/F1mj+L5tH8okOUu00pWrqcSS0PA==', N'SS66MO5NYRDYVJ75E6XMUYUDBQZK6EIT', N'feaee0d1-eed2-4b1d-9d1c-f3f1baedc0a3', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'30fb1037-53d4-4e6d-813b-7d413d8eabf3', N'member5@gmail.com', N'MEMBER5@GMAIL.COM', N'member5@gmail.com', N'MEMBER5@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEKbFuVaHvPIr7qZHYR2/y6e4VYaeKkIiwIllE5pks75QuLSFQfsmyqmICJpdWPgTsg==', N'PLUDKFCTAZZFEEGAT7HXHATBFCEEPJZ3', N'225efb08-cefa-4d32-ab48-d88fc9c8a1d4', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'312a8137-f9b7-494c-8809-9831628836ee', N'member23@gmail.com', N'MEMBER23@GMAIL.COM', N'member23@gmail.com', N'MEMBER23@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAGYHMjH5Tq7I+nPyH27PifM+I0AQL+kriVkGgYEwHjjTou1jfL6KeAu2WtCiqrpbg==', N'OPCVNKVWVF2EH2EMME6GE66NCG4LHKFL', N'db2793dc-e55b-4f72-86af-9a66b35047c9', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3bb15303-5f86-4471-bf2b-a4daa2553a6f', N'member28@gmail.com', N'MEMBER28@GMAIL.COM', N'member28@gmail.com', N'MEMBER28@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEC4z77E4pGS7AZry0DPp/rD4hf6b2d5LnB0tvpzSxQDH539T5z5qkDuHQu32ErMb3Q==', N'O4TDI4I7SV32B6BM3E4B5W6EDGOMOIZU', N'd968105b-244f-441f-b086-0bc31c38cc63', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3c4d3abf-d1ce-4449-a7cb-9e9576f8ed57', N'member1@gmail.com', N'MEMBER1@GMAIL.COM', N'member1@gmail.com', N'MEMBER1@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAECqPjFwPtKZ3nes6e8Us4WLcjGCjFCF5cBW64ARrLc41DIBy1Mgjjgy5vib1a0Wopw==', N'CD3AMLFEXAPZSHSFMMY4YVHQ2QIPMJQC', N'108397ca-a22e-4656-99b4-988955a366ad', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'45392617-2029-4f28-a3b5-74b45ebb33de', N'asia.krol@gmail.com', N'ASIA.KROL@GMAIL.COM', N'asia.krol@gmail.com', N'ASIA.KROL@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJpswmYda/7g1wbZO0QvBTFymzJ4sQGRQeuoPmS/5jSHrfyUJ8vf2JBk7TnZrsrkKg==', N'PV5T2DYTDB5SIGI2HRBQJ4RSZDW5W4PU', N'2cafc56f-08b1-4c83-b299-c714cf24597f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4fd174f3-de3e-4536-898f-12b60a5a5be0', N'member15@gmail.com', N'MEMBER15@GMAIL.COM', N'member15@gmail.com', N'MEMBER15@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEF4M/OqmYpr2wMMY1D/l51QoN9rLv22AY+3vSuS2TL3rYqMb8UE9Jx9LyB40nzgRhg==', N'SA5IHGXIXTNAKFVSASHJB5AHVWNSEGAD', N'e2555d9c-c96d-4dfc-9dbb-9c9fdf49c0e9', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5e10d96c-e2d5-4fa6-8f7d-c58cc92cbfb6', N'trainer3@gmail.com', N'TRAINER3@GMAIL.COM', N'trainer3@gmail.com', N'TRAINER3@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAELs5uxPjuElNh+zd/veTKRXEYFd6Hyp9Q/MeFSwJWhjn73xAegP8iwWOqwW85fg24Q==', N'XTPQ2JP3EGUEKPDWFBJLEGL4JYIIYXRQ', N'e6e0f622-f159-4da2-b4ac-2cd45156b2d0', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'60930438-70c2-4bf0-9f16-575d91259880', N'ola@gmail.com', N'OLA@GMAIL.COM', N'ola@gmail.com', N'OLA@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAWKnSC8RaoHOwphvGYSZTTX3vq8xeUhrnzPu2Tx02M+h4q9JmvYj1KLBLryBFEJwg==', N'2MQZZRYH44YURPTXCMB4ZOQJE3WFRQGD', N'5b023fb3-cead-42ac-b96c-35543ee29caf', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6cf6b090-5178-485f-9089-61868256733e', N'member11@gmail.com', N'MEMBER11@GMAIL.COM', N'member11@gmail.com', N'MEMBER11@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAENWCcesO7t90UXIIIcg6r4JhDk7H2u5ndlhbilYnajr1nxFUC11EFQLz3EG694zd5w==', N'2FHOEHKR2LNMZP57ZXRL4DGER276XUSQ', N'1ac80364-1bea-45f0-b650-0149a818c46c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'79c42384-7459-4490-b0ad-79b59db09d81', N'member12@gmail.com', N'MEMBER12@GMAIL.COM', N'member12@gmail.com', N'MEMBER12@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAztvWzyGVisMjq8Av8Ho30SbfZ6SU8Y8zyNha7WAfV/DDBASiRqHrY1zPhbgEfwFg==', N'P5XW5VQHV6LYR4RZKBUTO7KOJZAGBSKC', N'e2d76985-88f5-4fd5-8e93-7b628e17ec41', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7dd391d0-7493-4690-91fc-975cb06132db', N'member27@gmail.com', N'MEMBER27@GMAIL.COM', N'member27@gmail.com', N'MEMBER27@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPuMMotmKZSv+zNlLlkI6R0U4yxAL7lubEYb7Nl8mjiRm0C9Fak6cNQORAuOV/nomw==', N'PAOQ3RKTVUCRW7HENBZ6CTGGWDSHF24Z', N'9c249276-a34e-426d-94f9-2341a8609c61', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'84fc4144-ee29-4d6e-b0da-43b363566f86', N'member14@gmail.com', N'MEMBER14@GMAIL.COM', N'member14@gmail.com', N'MEMBER14@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEE75sLi+VA3gayKmtbOZc59EXbCg107descGsFP1EtAMqUxilBsK2FtyAsEig+xoRA==', N'I4GVNFWGCEFCG3X2PLIMWNZBVILMX5TO', N'dd87359a-4c95-424c-a438-01b8d9d63ee6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'939a3f07-d3c2-479c-8bd5-e7b373672d2a', N'olek.olek@gmail.com', N'OLEK.OLEK@GMAIL.COM', N'olek.olek@gmail.com', N'OLEK.OLEK@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAi665FBmfFeCn7L+1qqZvze/1B2ngp6+D4OgRjoEpw7zy2vTXYuDrqpN78cbb0DpA==', N'RQIE36C5L3Q4C5QIE3KYY2RTI3JJ7NTG', N'121a9b3e-3428-49ac-b75f-2294736c7a72', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9a36a710-3f38-4312-b5ad-9fa0cf84793d', N'oma@gmail.com', N'OMA@GMAIL.COM', N'oma@gmail.com', N'OMA@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEDc4ysUOuyakXljDKvrC5RqDHAD+EsIIJsAT5DPuZHGLCbYn9GGWmfFigp3Fkon+3Q==', N'UFDPCLDPW7HSJ454TO4TW7VS26HKGV6V', N'cb8ea330-c065-4e3c-9558-3d6a2f14fd08', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9b2a6145-a148-4a38-87d0-7d02a7c5cd49', N'member20@gmail.com', N'MEMBER20@GMAIL.COM', N'member20@gmail.com', N'MEMBER20@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAR4E7VkkeKTiI6YWJfvdYi4wFwSPFpdUjARB25eMLOqbg+qjoxFhvAbNvdhrLI7hQ==', N'3AFZDZ6K5DVS46DTMR7PAQMYUAC2Q5LC', N'438945aa-9e5a-4f6a-833e-49382fb5fddf', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a22ac8f2-c4b4-4cd7-99c1-753f777d7848', N'test@gmail.com', N'TEST@GMAIL.COM', N'test@gmail.com', N'TEST@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEB+sSMboZdq5dEeUMSpcq9zUq9xBVltvaMeQFv7H9XVMeY49hTEFBKPB8v1+4HwTTw==', N'BW5TXGFOU4ZN3QVRHSXAWNMGG2ORSTWJ', N'2c6ed658-dcc1-469f-a5b5-d3bf3980750e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'aa5d7d71-c1a9-4540-a478-016fa52e5558', N'trainer1@gmail.com', N'TRAINER1@GMAIL.COM', N'trainer1@gmail.com', N'TRAINER1@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJplTf9w4v4o1NZOoJhy4S0AFP2YOwcoGWdR5wiswnh9pUYFFINs1n4W/h2e0YfmBQ==', N'W64GS2X5L72F7ZDNYPPABQHKF26QILAM', N'25e5c394-b65b-45fb-95b7-177c162ed1dd', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ada2675a-a8ec-4fbd-a71e-950ca8de5bdf', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEIE5V0/j1S64WXox4Aas8JKbQkwIoF8XuOtMvUcS+0KpsaJTE4J9DNo43F1vsHJ4Hw==', N'GQ2UOANCQOGXZRVUDBYRZIPSLYE5Q3GB', N'b0905c7a-c9d7-455c-885b-ab42fc9f38c7', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b3c5dd54-8748-46b2-a7b5-c4c436c5507d', N'member29@gmail.com', N'MEMBER29@GMAIL.COM', N'member29@gmail.com', N'MEMBER29@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAELR8Nf4qSaJ+5CFJ/qABgHT6god+bzb6lswxmikYSiIdEUCA7tbCM5d9b1TuJiKPPA==', N'ZFFTCRMPCEAP4QBAF2SCPKOFPNKCCYAK', N'e5a4a348-d466-42a5-a86c-b8a501e47b86', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c7db0523-f23e-46dd-bd4e-f766aa3ca0e6', N'ejeje@gmail.com', N'EJEJE@GMAIL.COM', N'ejeje@gmail.com', N'EJEJE@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEK6N2OczBOLOL0aDOxUTVT542JK/WcG8oLiTbFmQveTwcCNWGfRXk5IlHkGshXS/qg==', N'CBQGU6RTNXOH2UZRWGCFOCGQ6NEG2HJ6', N'ca5286fc-3b1d-4026-996b-4542611857ca', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd2019b42-d4aa-4134-8252-0e95ceff759f', N'trainer12@gmail.com', N'TRAINER12@GMAIL.COM', N'trainer12@gmail.com', N'TRAINER12@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEIXkc2tXQeWux9USqA9j/cpqatcF0O3aYPu6dp0yyL5Wk4Q3JEV3Mx7jFsWlcB81ag==', N'IULV7WJCHGANKSVLNMFIN4QL5HRZJTUT', N'1e75bf6c-9b23-43e1-97f4-9c0777e52622', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd9b496b1-b946-4f30-b494-55089ae29764', N'trainer11@gmail.com', N'TRAINER11@GMAIL.COM', N'trainer11@gmail.com', N'TRAINER11@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPxlHmRPkVEq7l0AGkJQrW52ZGy6M3ZM8ArbdE7Revcmq8cX3Hbu0rBD7X1m8uRzvg==', N'DMD4NRO3CGQQU5GBH624YO4TCOGE7MGP', N'2581fd89-a52a-4700-b2c4-ae1af4364c49', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'df167dd4-870b-48d0-842d-6b6e851080e1', N'trainer2@gmail.com', N'TRAINER2@GMAIL.COM', N'trainer2@gmail.com', N'TRAINER2@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEAFHBfKObd+xha8QpSYHch6z1zENu1ERbDZUC+Z2wq/e0tNigMM7BrHqeyNDwWaQMg==', N'FNQQCK6UFOJRNBUQ3FD7YJ6HD6QDBYSA', N'96158d4d-83a6-4e64-96c5-96f1adf691c6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e6df278b-9062-4ed6-bc05-9a098484feec', N'emilia.barczyk@gmail.com', N'EMILIA.BARCZYK@GMAIL.COM', N'emilia.barczyk@gmail.com', N'EMILIA.BARCZYK@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAECyg0mQNo1tQaUOKEhbwHRMHj2rUJRbIrBbDKZEJehEVhvfd6s196RaZ9KnRoSnQ1A==', N'KF474RWW3LYIBUH3JZCBUSSRIYMI3TZJ', N'4e108499-55de-44d4-b8ca-a399644c3bd9', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e9c4fa1d-4abe-4572-8830-64b5371abda5', N'nowy@gmail.com', N'NOWY@GMAIL.COM', N'nowy@gmail.com', N'NOWY@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAENffWRLG2Hv0dSI6i1LKNWIZbopXvDmKB3lhpJM53fv4WJV2VRItfijPlnixM6fnsg==', N'VRPQOV7MRYECNLGFZI5DNOWN67DBZKNS', N'380c94df-9da8-4ea5-94f1-2d419d6bb09b', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fc661842-578c-42c2-b1dd-61075379e9a1', N'trainer5@gmail.com', N'TRAINER5@GMAIL.COM', N'trainer5@gmail.com', N'TRAINER5@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAELIeN04ba7D8SdkNR/1GllUCU/Eyj2L/eh03El1RVa5YFxRVIlkNFJ0V/ws6mhd7Xg==', N'IYBG2PST7AXC4SWL54N27GHQX6VXG3SE', N'7b5fb27d-5d91-4dd6-ac4f-93d2b008a6a0', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Equipments] ON 

INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (51, 2, N'Bench Press', N'Flat bench with barbell for chest workouts', N'Check bolts monthly', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (52, 3, N'Squat Rack', N'Rack for squats, pull-ups, and bench press', N'Needs rubber flooring', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (53, 1, N'Dumbbells Set', N'Full dumbbell set from 2kg to 40kg', N'Organized by weight', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (54, 4, N'Kettlebells', N'Cast iron kettlebells 4–32kg', N'Wipe after use', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (55, 5, N'Treadmill', N'Motorized running treadmill with incline', N'Service every 6 months', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (56, 4, N'Stationary Bike', N'Adjustable resistance spinning bikes', N'Replace worn pedals', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (57, 2, N'Rowing Machine', N'Air-resistance rower for full-body cardio', N'Lubricate chain monthly', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (58, 2, N'Lat Pulldown', N'Cable machine for back and arm exercises', N'Cable replaced last month', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (59, 1, N'Cable Crossover', N'Dual adjustable pulleys for functional training', N'Use clip-on accessories', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (60, 2, N'Leg Press', N'45-degree plate-loaded leg press machine', N'Keep safety locks engaged', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (61, 1, N'Hack Squat', N'Machine for quad-focused squats', N'Grease rail guides', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (62, 2, N'Preacher Curl Bench', N'Bench for isolated bicep curls', N'Foam cover worn', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (63, 2, N'Smith Machine', N'Guided barbell system for safe lifts', N'Inspect guide rods', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (64, 1, N'Chest Fly Machine', N'Pec deck for chest isolation', N'Light resistance band missing', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (65, 2, N'Leg Extension Machine', N'Targets the quadriceps muscles', N'Adjustments stiff', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (66, 2, N'Leg Curl Machine', N'Hamstring-focused isolation equipment', N'Monitor tension cable', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (67, 1, N'Seated Row Machine', N'Cable machine for back rows', N'Footplate loose', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (68, 1, N'Ab Crunch Machine', N'Machine-assisted abdominal workout', N'Pad needs replacement', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (69, 2, N'Dip Station', N'Vertical dip bars for triceps and shoulders', N'Rubber grips cracking', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (70, 3, N'Pull-Up Bar', N'Wall-mounted pull-up station', N'Bolts checked weekly', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (71, 2, N'Battle Ropes', N'Thick ropes for full-body cardio training', N'Store on hooks', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (72, 1, N'Sled Push', N'Weighted sled for explosive strength', N'Slight rust on skids', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (73, 10, N'Medicine Balls', N'Weighted balls for core and coordination work', N'Varied weights available', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (74, 3, N'TRX Bands', N'Suspension training system', N'Anchor to ceiling hooks', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (75, 15, N'Resistance Bands', N'Various strengths for mobility and rehab', N'Inspect for tears', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (76, 6, N'Plyo Boxes', N'Boxes of different heights for jump training', N'Corners padded', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (77, 8, N'Barbells', N'Olympic barbells for powerlifting', N'Labelled by weight', N'http://localhost:5119/uploads/default-equipment3.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (78, 100, N'Weight Plates', N'Standard and bumper plates from 2.5kg to 25kg', N'Stacked on plate tree', N'http://localhost:5119/uploads/default-equipment1.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (79, 4, N'EZ Curl Bar', N'Barbell with angled grip for arm exercises', N'One needs rebalancing', N'http://localhost:5119/uploads/default-equipment2.jpg')
INSERT [dbo].[Equipments] ([Id], [Quantity], [Name], [Description], [Notes], [PhotoPath]) VALUES (80, 12, N'Foam Rollers', N'Used for muscle recovery and stretching', N'Disinfect regularly', N'http://localhost:5119/uploads/default-equipment3.jpg')
SET IDENTITY_INSERT [dbo].[Equipments] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 

INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (2, N'John', N'Smith', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'123123123', N'3c4d3abf-d1ce-4449-a7cb-9e9576f8ed57')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (5, N'Monika', N'Szymańska', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222334', N'17cd82a0-b8d7-4dcd-a3cb-b6e502bac0b9')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (6, N'Jan', N'Dąbrowski', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'444222333', N'30fb1037-53d4-4e6d-813b-7d413d8eabf3')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (8, N'Maria', N'Lewandowska', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111277333', N'0cf95a78-afb5-4d24-bc1c-4240c4d3565f')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (9, N'Adam', N'Woźniak', N'', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'', N'111277333', N'17654199-4c10-4b5e-87d8-e9f4ad62cdfc')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (10, N'Gustaw', N'Nowak', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222903', N'251a5fcd-00c1-41f9-a7d9-d999ed8e1eea')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (12, N'Joanna', N'Krawczyk', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111225903', N'6cf6b090-5178-485f-9089-61868256733e')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (13, N'Marcin', N'Gajewski', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'100225903', N'79c42384-7459-4490-b0ad-79b59db09d81')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (14, N'Kasia ', N'Wisniewska', N'', CAST(N'2004-01-01T00:00:00.0000000' AS DateTime2), N'', N'100205903', N'02ca0993-1402-4770-bca6-7e112079d46e')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (15, N'Andrzej', N'Grabowski', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'100205903', N'84fc4144-ee29-4d6e-b0da-43b363566f86')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (16, N'Alicja', N'Król', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'100205903', N'4fd174f3-de3e-4536-898f-12b60a5a5be0')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (19, N'Michał', N'Wójcik', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222333', N'080f715c-9bbe-4f11-b5f2-009585291b4b')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (21, N'Aleksandra', N'Michalak', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222333', N'9b2a6145-a148-4a38-87d0-7d02a7c5cd49')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (24, N'Paweł', N'Kowalczyk', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222333', N'312a8137-f9b7-494c-8809-9831628836ee')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (26, N'Ewa', N'Kaczmarek', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222333', N'3bb15303-5f86-4471-bf2b-a4daa2553a6f')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (27, N'Barbara', N'Piotrowska', N'', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'', N'111222333', N'7dd391d0-7493-4690-91fc-975cb06132db')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (29, N'Pawel', N'Nowak', N'', CAST(N'2025-05-09T00:00:00.0000000' AS DateTime2), N'', N'123000000', N'1f1fb704-7287-4d9a-81e4-adba74d43d6e')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (30, N'Asia', N'Krol', N'', CAST(N'2001-10-21T00:00:00.0000000' AS DateTime2), N'', N'000999555', N'45392617-2029-4f28-a3b5-74b45ebb33de')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (33, N'Magdalena', N'Zielińska', N'', CAST(N'2005-01-01T00:00:00.0000000' AS DateTime2), N'', N'883333222', N'0dc490d5-f466-4a64-8c77-99f32a59af93')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (34, N'Krzysztof', N'Pawlak', N'', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'', N'777666222', N'9a36a710-3f38-4312-b5ad-9fa0cf84793d')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (35, N'Grzegorz', N'Kwiatkowski', N'', CAST(N'2002-01-01T00:00:00.0000000' AS DateTime2), N'', N'777666223', N'60930438-70c2-4bf0-9f16-575d91259880')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (37, N'Robert', N'Sikora', N'', CAST(N'2003-01-29T00:00:00.0000000' AS DateTime2), N'', N'666333222', N'a22ac8f2-c4b4-4cd7-99c1-753f777d7848')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (39, N'Natalia', N'Wróbel', N'', CAST(N'2001-02-20T00:00:00.0000000' AS DateTime2), N'', N'777333444', N'e9c4fa1d-4abe-4572-8830-64b5371abda5')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (40, N'Justyna', N'Nowicka', N'', CAST(N'2003-06-09T00:00:00.0000000' AS DateTime2), N'', N'999222111', N'939a3f07-d3c2-479c-8bd5-e7b373672d2a')
INSERT [dbo].[Members] ([Id], [FirstName], [LastName], [Email], [DateOfBirth], [MembershipCardNumber], [PhoneNumber], [UserId]) VALUES (41, N'Emilia', N'Barczyk', N'', CAST(N'2008-07-11T00:00:00.0000000' AS DateTime2), N'', N'777231444', N'e6df278b-9062-4ed6-bc05-9a098484feec')
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[Memberships] ON 

INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (21, 5, 1, CAST(N'2025-06-06T16:19:24.8300000' AS DateTime2), CAST(N'2025-07-06T16:19:24.8300000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (14, 9, 1, CAST(N'2025-05-23T10:00:00.0000000' AS DateTime2), CAST(N'2025-06-22T10:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (16, 10, 2, CAST(N'2025-06-06T15:23:25.4620000' AS DateTime2), CAST(N'2025-06-07T15:23:25.4620000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (15, 14, 1, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (27, 15, 1, CAST(N'2025-06-07T03:01:14.5950000' AS DateTime2), CAST(N'2025-07-07T03:01:14.5950000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (25, 21, 1, CAST(N'2025-06-06T17:19:38.9820000' AS DateTime2), CAST(N'2025-07-06T17:19:38.9820000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (30, 27, 1, CAST(N'2025-06-07T17:03:12.5200000' AS DateTime2), CAST(N'2025-07-07T17:03:12.5200000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (31, 29, 1, CAST(N'2025-06-08T12:07:20.9580000' AS DateTime2), CAST(N'2025-07-08T12:07:20.9580000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (26, 34, 1, CAST(N'2025-06-07T02:57:15.1160000' AS DateTime2), CAST(N'2025-07-07T02:57:15.1160000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (28, 35, 1, CAST(N'2025-06-07T03:01:33.5410000' AS DateTime2), CAST(N'2025-07-07T03:01:33.5410000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (34, 37, 1, CAST(N'2025-06-08T20:29:02.8270000' AS DateTime2), CAST(N'2025-07-08T20:29:02.8270000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (36, 39, 1, CAST(N'2025-06-08T22:20:30.6430000' AS DateTime2), CAST(N'2025-07-08T22:20:30.6430000' AS DateTime2), 1)
INSERT [dbo].[Memberships] ([Id], [MemberId], [MembershipTypeId], [StartDate], [EndDate], [IsActive]) VALUES (37, 40, 1, CAST(N'2025-06-09T23:45:12.5960103' AS DateTime2), CAST(N'2025-07-09T23:45:12.5961889' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Memberships] OFF
GO
SET IDENTITY_INSERT [dbo].[MembershipTypes] ON 

INSERT [dbo].[MembershipTypes] ([Id], [Name], [Description], [Price], [DurationInDays], [IncludesPersonalTrainer], [PersonalTrainingsPerMonth], [AllowTrainerSelection], [IncludesProgressTracking], [IsVisible]) VALUES (1, N'Basic Plus', N'Only 149.99$ per 30 days to be able to use everything!', CAST(149.99 AS Decimal(10, 2)), 30, 1, 1, 1, 0, 1)
INSERT [dbo].[MembershipTypes] ([Id], [Name], [Description], [Price], [DurationInDays], [IncludesPersonalTrainer], [PersonalTrainingsPerMonth], [AllowTrainerSelection], [IncludesProgressTracking], [IsVisible]) VALUES (2, N'Pro Month', N'Only 229.99$ for personal training!', CAST(229.99 AS Decimal(10, 2)), 30, 1, 2, 1, 0, 1)
INSERT [dbo].[MembershipTypes] ([Id], [Name], [Description], [Price], [DurationInDays], [IncludesPersonalTrainer], [PersonalTrainingsPerMonth], [AllowTrainerSelection], [IncludesProgressTracking], [IsVisible]) VALUES (4, N'Basic', N'Access essential features to get started!', CAST(99.99 AS Decimal(10, 2)), 30, 0, NULL, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[MembershipTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [MessageContent], [Date], [MemberId], [TrainerId], [SentByMember]) VALUES (9, N'Hello, I assigned training, I am looking forward to!!', CAST(N'2025-06-10T14:59:37.7730000' AS DateTime2), 27, 12, 1)
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[ProgressPhotos] ON 

INSERT [dbo].[ProgressPhotos] ([Id], [MemberId], [Date], [Comment], [ImagePath], [IsPublic]) VALUES (5, 39, CAST(N'2025-06-10T14:54:41.3627217' AS DateTime2), N'Feeling incredible! This journey with trainer is really paying off. So excited to see what''s next!', N'http://localhost:5119/uploads/e1ae80e8-e82f-4676-a9c1-a5a11eb69a07.jpg', 1)
INSERT [dbo].[ProgressPhotos] ([Id], [MemberId], [Date], [Comment], [ImagePath], [IsPublic]) VALUES (6, 9, CAST(N'2025-06-10T14:56:31.5609791' AS DateTime2), N'Progress, not perfection! Each session brings me closer to my goals, and I''m feeling stronger and more confident than ever. This is what dedication looks like!', N'http://localhost:5119/uploads/e8539d17-dda2-427d-8e7e-23a4379e894a.jpg', 1)
INSERT [dbo].[ProgressPhotos] ([Id], [MemberId], [Date], [Comment], [ImagePath], [IsPublic]) VALUES (7, 27, CAST(N'2025-06-10T14:58:24.9905189' AS DateTime2), N'Crushing it! So proud of the progress I''m making with accompaniament of my trainer. Feeling amazing!', N'http://localhost:5119/uploads/b4a5d142-6139-43d9-b7aa-093fa248e1f1.jpg', 1)
SET IDENTITY_INSERT [dbo].[ProgressPhotos] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceRequests] ON 

INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (5, N'Loose Cable', N'The cable on the lat pulldown machine is loose and jerky.', NULL, CAST(N'2025-06-08T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (6, N'Broken Treadmill', N'The treadmill #3 stops suddenly mid-run.', NULL, CAST(N'2025-06-05T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (7, N'Damaged Bench Pad', N'Flat bench has a torn pad that needs replacement.', NULL, CAST(N'2025-06-09T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (8, N'Non-functioning Bike', N'Stationary bike does not display metrics or resistance.', NULL, CAST(N'2025-05-31T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (9, N'Noisy Rowing Machine', N'Rowing machine makes a loud clanking noise during pull.', NULL, CAST(N'2025-06-03T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (10, N'Broken Lockers', N'Two lockers near the entrance are missing keys.', NULL, CAST(N'2025-06-07T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (11, N'Unstable Squat Rack', N'Left side of squat rack wobbles when racked.', NULL, CAST(N'2025-05-26T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (12, N'Rust on Barbell', N'Barbell bar showing signs of rusting.', NULL, CAST(N'2025-05-21T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (13, N'Loose TRX Mount', N'Ceiling mount for TRX is coming loose.', NULL, CAST(N'2025-06-06T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (14, N'Flickering Lights', N'Lights in the back stretching area are flickering.', NULL, CAST(N'2025-05-30T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (15, N'Unresponsive Screen', N'Touchscreen on treadmill #2 does not respond.', NULL, CAST(N'2025-06-01T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (16, N'Missing Dumbbells', N'12kg dumbbells are missing from the rack.', NULL, CAST(N'2025-05-27T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (17, N'Unclean Mats', N'Mats in yoga room have not been cleaned.', NULL, CAST(N'2025-06-02T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (18, N'Low Water Pressure', N'Shower in men’s locker room has low pressure.', NULL, CAST(N'2025-05-29T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (19, N'Faulty Timer', N'Wall-mounted timer in HIIT room is not counting.', NULL, CAST(N'2025-06-04T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (20, N'Leaking Pipe', N'Small leak under sink in ladies locker room.', NULL, CAST(N'2025-05-23T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (21, N'No AC in Studio B', N'Air conditioning in studio B isn’t working.', NULL, CAST(N'2025-05-28T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (22, N'Slippery Floor', N'Sweat not cleaned after class makes it slippery.', NULL, CAST(N'2025-05-25T03:46:31.9933333' AS DateTime2), 0)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (23, N'Broken Resistance Band', N'One of the heavy bands snapped during use.', NULL, CAST(N'2025-05-24T03:46:31.9933333' AS DateTime2), 1)
INSERT [dbo].[ServiceRequests] ([Id], [ServiceProblemTitle], [ProblemNote], [EquipmentId], [RequestDate], [IsResolved]) VALUES (24, N'Faulty Speaker', N'One of the studio speakers has crackling audio.', NULL, CAST(N'2025-05-22T03:46:31.9933333' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[ServiceRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[TrainerAssignments] ON 

INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (7, 1, 9, CAST(N'2025-05-23T10:00:00.0000000' AS DateTime2), CAST(N'2025-06-22T10:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (22, 9, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (23, 11, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (24, 11, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (25, 10, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (26, 11, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (27, 11, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (28, 9, 14, CAST(N'2025-06-06T14:56:23.8870000' AS DateTime2), CAST(N'2025-07-06T14:56:23.8870000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (30, 11, 35, CAST(N'2025-06-07T03:01:33.5410000' AS DateTime2), CAST(N'2025-07-07T03:01:33.5410000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (32, 10, 15, CAST(N'2025-06-07T03:01:14.5950000' AS DateTime2), CAST(N'2025-07-07T03:01:14.5950000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (34, 2, 34, CAST(N'2025-06-07T02:57:15.1160000' AS DateTime2), CAST(N'2025-07-07T02:57:15.1160000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (35, 2, 21, CAST(N'2025-06-06T17:19:38.9820000' AS DateTime2), CAST(N'2025-07-06T17:19:38.9820000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (36, 12, 27, CAST(N'2025-06-07T17:03:12.5200000' AS DateTime2), CAST(N'2025-07-07T17:03:12.5200000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (37, 11, 29, CAST(N'2025-06-08T12:07:20.9580000' AS DateTime2), CAST(N'2025-07-08T12:07:20.9580000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (39, 1, 37, CAST(N'2025-06-08T20:56:37.7825498' AS DateTime2), NULL, 0)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (40, 1, 37, CAST(N'2025-06-08T20:57:07.1999601' AS DateTime2), NULL, 0)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (41, 1, 37, CAST(N'2025-06-08T20:29:02.8270000' AS DateTime2), CAST(N'2025-07-08T20:29:02.8270000' AS DateTime2), 1)
INSERT [dbo].[TrainerAssignments] ([Id], [TrainerId], [MemberId], [StartDate], [EndDate], [IsActive]) VALUES (42, 1, 39, CAST(N'2025-06-08T22:20:30.6430000' AS DateTime2), CAST(N'2025-07-08T22:20:30.6430000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[TrainerAssignments] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainers] ON 

INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (1, N'Johnny', N'Bravo', N'trainer@gmail.com', N'888888888', N'An experienced fitness and performance coach with a strong background in sports science. David excels at creating personalized training regimens that maximize athletic potential and prevent injuries, catering to both amateur and professional athletes.', N'http://localhost:5119/uploads/5c3f2233-4260-4b29-92bd-268e9a0430af.jpg', N'2e95a081-e9fa-439c-969a-7b161794d4a2')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (2, N'Dawn', N'Smith', N'trainer1@gmail.com', N'888888828', N'A seasoned business coach and strategist, Mark specializes in operational efficiency and market expansion. He provides practical, results-driven advice to entrepreneurs and SMEs looking to scale their operations and improve profitability.', N'http://localhost:5119/uploads/9c58902d-8d76-4368-9fb9-9ccb5feac311.jpg', N'aa5d7d71-c1a9-4540-a478-016fa52e5558')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (4, N'Patryk', N'Persak', N'trainer3@gmail.com', N'333666555', N'A dedicated strength and conditioning coach with a focus on functional movement and long-term health. Ben''s holistic approach integrates nutrition and recovery, ensuring clients achieve sustainable fitness improvements and a robust physical foundation.', N'http://localhost:5119/uploads/7fc52fd1-6d58-4e3d-aa2e-fb36b05d5f6e.jpg', N'5e10d96c-e2d5-4fa6-8f7d-c58cc92cbfb6')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (9, N'Olivia', N'Chen', N'trainer11@gmail.com', N'888222234', N'A dynamic and innovative coach specializing in team building and leadership development. Olivia is known for her ability to inspire confidence and foster a collaborative environment, making her a sought-after mentor for emerging leaders.', N'http://localhost:5119/uploads/496cbf47-fd43-4378-84a3-c36f43fd73c7.jpg', N'd9b496b1-b946-4f30-b494-55089ae29764')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (10, N'Anya', N'Sharma', N'trainer12@gmail.com', N'838222234', N'A passionate career development coach, Anya guides professionals through career transitions, job searching, and skill enhancement. She helps clients identify their strengths, refine their professional narrative, and navigate the complexities of the modern job market.', N'http://localhost:5119/uploads/24df65ca-5d98-47a7-b968-d3d6991ff6b6.jpg', N'd2019b42-d4aa-4134-8252-0e95ceff759f')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (11, N'Alex', N'Ramirez', N'ojojojtrainer123@gmail.com', N'000999555', N'An energetic and insightful technology coach, Alex focuses on digital literacy and software proficiency. He assists individuals and businesses in adapting to new technologies, optimizing workflows, and leveraging digital tools for increased productivity.', N'http://localhost:5119/uploads/faacf31b-1f26-4e74-93a2-5405d94595c3.jpg', N'26a9652f-6e11-475e-9464-cba05feef31c')
INSERT [dbo].[Trainers] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Description], [PhotoPath], [UserId]) VALUES (12, N'Emily', N'White', N'ejeje@gmail.com', N'882666555', N'A highly effective wellness coach specializing in stress management and mindfulness. Emily provides tools and techniques to help clients achieve mental clarity, emotional balance, and improved overall well-being in their daily lives.', N'http://localhost:5119/uploads/00f7a78e-17b6-4cf4-85b4-314be919db05.jpg', N'c7db0523-f23e-46dd-bd4e-f766aa3ca0e6')
SET IDENTITY_INSERT [dbo].[Trainers] OFF
GO
SET IDENTITY_INSERT [dbo].[TrainingSessions] ON 

INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (8, 1, N'some some, your training with your trainer Johnny Bravoo will take placeon 01-06-2025 at 14:00', CAST(N'2025-06-01T14:00:00.0000000' AS DateTime2), 0, 9, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (28, 2, N'Personal', CAST(N'2025-06-11T19:00:00.0000000' AS DateTime2), 0, 21, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (48, 1, N'Test Test, your training with your trainer Johnny Bravoo will take placeon 18-06-2025 at 12:00', CAST(N'2025-06-18T12:00:00.0000000' AS DateTime2), 0, 37, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (349, 1, N'Nowy Nowy, your training with your trainer Johnny Bravoo will take place on 18-06-2025 at 10:00', CAST(N'2025-06-18T10:00:00.0000000' AS DateTime2), 0, 39, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (350, 1, N'Full-body HIIT class for advanced members', CAST(N'2025-06-11T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (351, 2, N'Strength and endurance circuit', CAST(N'2025-06-12T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (352, 4, N'Functional training with kettlebells', CAST(N'2025-06-13T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (353, 9, N'Cardio blast for fat burning', CAST(N'2025-06-14T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (354, 10, N'Powerlifting group intro session', CAST(N'2025-06-15T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (355, 11, N'Yoga and mobility class', CAST(N'2025-06-16T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (356, 12, N'Cross-training with mixed equipment', CAST(N'2025-06-17T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (357, 1, N'Core and conditioning', CAST(N'2025-06-18T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (358, 2, N'Team workout – partner drills and games', CAST(N'2025-06-19T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (359, 4, N'Strength bootcamp: barbell focus', CAST(N'2025-06-20T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (360, 9, N'Agility and speed session', CAST(N'2025-06-21T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (361, 10, N'Weightlifting technique workshop', CAST(N'2025-06-22T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (362, 11, N'Group cardio circuits', CAST(N'2025-06-23T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (363, 12, N'Recovery mobility and breathing', CAST(N'2025-06-24T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (364, 1, N'Group conditioning challenge', CAST(N'2025-06-25T03:51:04.0500000' AS DateTime2), 1, NULL, 120)
INSERT [dbo].[TrainingSessions] ([Id], [TrainerId], [Description], [StartTime], [IsGroupSession], [MemberId], [DurationInMinutes]) VALUES (365, 12, N'Barbara Piotrowska, your training with your trainer Emily White will take place on 20-06-2025 at 10:00', CAST(N'2025-06-20T10:00:00.0000000' AS DateTime2), 0, 27, 120)
SET IDENTITY_INSERT [dbo].[TrainingSessions] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkoutNotes] ON 

INSERT [dbo].[WorkoutNotes] ([Id], [CurrentWeight], [CurrentHeight], [WorkoutInfo], [WorkoutStartTime], [MemberId], [TrainerId], [TrainingSessionId]) VALUES (6, NULL, NULL, N'', CAST(N'2025-06-01T14:00:00.0000000' AS DateTime2), 9, 1, 8)
INSERT [dbo].[WorkoutNotes] ([Id], [CurrentWeight], [CurrentHeight], [WorkoutInfo], [WorkoutStartTime], [MemberId], [TrainerId], [TrainingSessionId]) VALUES (15, NULL, NULL, N'', CAST(N'2025-06-11T19:00:00.0000000' AS DateTime2), 21, 2, 28)
INSERT [dbo].[WorkoutNotes] ([Id], [CurrentWeight], [CurrentHeight], [WorkoutInfo], [WorkoutStartTime], [MemberId], [TrainerId], [TrainingSessionId]) VALUES (35, NULL, NULL, N'', CAST(N'2025-06-18T12:00:00.0000000' AS DateTime2), 37, 1, 48)
INSERT [dbo].[WorkoutNotes] ([Id], [CurrentWeight], [CurrentHeight], [WorkoutInfo], [WorkoutStartTime], [MemberId], [TrainerId], [TrainingSessionId]) VALUES (36, NULL, NULL, N'', CAST(N'2025-06-18T10:00:00.0000000' AS DateTime2), 39, 1, 349)
INSERT [dbo].[WorkoutNotes] ([Id], [CurrentWeight], [CurrentHeight], [WorkoutInfo], [WorkoutStartTime], [MemberId], [TrainerId], [TrainingSessionId]) VALUES (37, NULL, NULL, N'', CAST(N'2025-06-20T10:00:00.0000000' AS DateTime2), 27, 12, 365)
SET IDENTITY_INSERT [dbo].[WorkoutNotes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/06/2025 16:41:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/06/2025 16:41:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Members_UserId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Members_UserId] ON [dbo].[Members]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Memberships_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Memberships_MemberId] ON [dbo].[Memberships]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Memberships_MembershipTypeId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Memberships_MembershipTypeId] ON [dbo].[Memberships]
(
	[MembershipTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Memberships_MemberId_IsActive_Index]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [Memberships_MemberId_IsActive_Index] ON [dbo].[Memberships]
(
	[MemberId] ASC,
	[IsActive] ASC
)
INCLUDE([StartDate],[EndDate],[MembershipTypeId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_MemberId] ON [dbo].[Messages]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_TrainerId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_TrainerId] ON [dbo].[Messages]
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProgressPhotos_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_ProgressPhotos_MemberId] ON [dbo].[ProgressPhotos]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ServiceRequests_EquipmentId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_ServiceRequests_EquipmentId] ON [dbo].[ServiceRequests]
(
	[EquipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrainerAssignments_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_TrainerAssignments_MemberId] ON [dbo].[TrainerAssignments]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrainerAssignments_TrainerId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_TrainerAssignments_TrainerId] ON [dbo].[TrainerAssignments]
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Trainers_UserId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_Trainers_UserId] ON [dbo].[Trainers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrainingSessions_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_TrainingSessions_MemberId] ON [dbo].[TrainingSessions]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrainingSessions_TrainerId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_TrainingSessions_TrainerId] ON [dbo].[TrainingSessions]
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [TrainingSessions_Trainer_StartTime_INDEX]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [TrainingSessions_Trainer_StartTime_INDEX] ON [dbo].[TrainingSessions]
(
	[TrainerId] ASC,
	[StartTime] ASC
)
INCLUDE([MemberId],[DurationInMinutes],[IsGroupSession]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutNotes_MemberId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutNotes_MemberId] ON [dbo].[WorkoutNotes]
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutNotes_TrainerId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutNotes_TrainerId] ON [dbo].[WorkoutNotes]
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutNotes_TrainingSessionId]    Script Date: 11/06/2025 16:41:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_WorkoutNotes_TrainingSessionId] ON [dbo].[WorkoutNotes]
(
	[TrainingSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Members] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [dbo].[Memberships] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Messages] ADD  DEFAULT (CONVERT([bit],(0))) FOR [SentByMember]
GO
ALTER TABLE [dbo].[ServiceRequests] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [RequestDate]
GO
ALTER TABLE [dbo].[ServiceRequests] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsResolved]
GO
ALTER TABLE [dbo].[TrainerAssignments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Trainers] ADD  DEFAULT (N'') FOR [UserId]
GO
ALTER TABLE [dbo].[TrainingSessions] ADD  DEFAULT ((0)) FOR [DurationInMinutes]
GO
ALTER TABLE [dbo].[WorkoutNotes] ADD  DEFAULT ((0)) FOR [TrainingSessionId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [FK_Memberships_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [FK_Memberships_Members_MemberId]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [FK_Memberships_MembershipTypes_MembershipTypeId] FOREIGN KEY([MembershipTypeId])
REFERENCES [dbo].[MembershipTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [FK_Memberships_MembershipTypes_MembershipTypeId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Members_MemberId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Trainers_TrainerId] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Trainers_TrainerId]
GO
ALTER TABLE [dbo].[ProgressPhotos]  WITH CHECK ADD  CONSTRAINT [FK_ProgressPhotos_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProgressPhotos] CHECK CONSTRAINT [FK_ProgressPhotos_Members_MemberId]
GO
ALTER TABLE [dbo].[ServiceRequests]  WITH CHECK ADD  CONSTRAINT [FK_ServiceRequests_Equipments_EquipmentId] FOREIGN KEY([EquipmentId])
REFERENCES [dbo].[Equipments] ([Id])
GO
ALTER TABLE [dbo].[ServiceRequests] CHECK CONSTRAINT [FK_ServiceRequests_Equipments_EquipmentId]
GO
ALTER TABLE [dbo].[TrainerAssignments]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAssignments_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainerAssignments] CHECK CONSTRAINT [FK_TrainerAssignments_Members_MemberId]
GO
ALTER TABLE [dbo].[TrainerAssignments]  WITH CHECK ADD  CONSTRAINT [FK_TrainerAssignments_Trainers_TrainerId] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainerAssignments] CHECK CONSTRAINT [FK_TrainerAssignments_Trainers_TrainerId]
GO
ALTER TABLE [dbo].[Trainers]  WITH CHECK ADD  CONSTRAINT [FK_Trainers_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Trainers] CHECK CONSTRAINT [FK_Trainers_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[TrainingSessions]  WITH CHECK ADD  CONSTRAINT [FK_TrainingSessions_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[TrainingSessions] CHECK CONSTRAINT [FK_TrainingSessions_Members_MemberId]
GO
ALTER TABLE [dbo].[TrainingSessions]  WITH CHECK ADD  CONSTRAINT [FK_TrainingSessions_Trainers_TrainerId] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrainingSessions] CHECK CONSTRAINT [FK_TrainingSessions_Trainers_TrainerId]
GO
ALTER TABLE [dbo].[WorkoutNotes]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutNotes_Members_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutNotes] CHECK CONSTRAINT [FK_WorkoutNotes_Members_MemberId]
GO
ALTER TABLE [dbo].[WorkoutNotes]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutNotes_Trainers_TrainerId] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Trainers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutNotes] CHECK CONSTRAINT [FK_WorkoutNotes_Trainers_TrainerId]
GO
USE [master]
GO
ALTER DATABASE [GymManagerDb] SET  READ_WRITE 
GO
