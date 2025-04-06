USE [master]
GO
/****** Object:  Database [FlowerInventoryAssessmentLogs]    Script Date: 06/04/2025 4:04:52 pm ******/
CREATE DATABASE [FlowerInventoryAssessmentLogs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlowerInventoryAssessmentLogs', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FlowerInventoryAssessmentLogs.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlowerInventoryAssessmentLogs_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FlowerInventoryAssessmentLogs_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlowerInventoryAssessmentLogs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET  MULTI_USER 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET QUERY_STORE = ON
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FlowerInventoryAssessmentLogs]
GO
/****** Object:  Table [dbo].[MainLogs]    Script Date: 06/04/2025 4:04:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[Timestamp] [datetime2](7) NULL,
	[Exception] [nvarchar](max) NULL,
	[LogEvent] [nvarchar](max) NULL,
 CONSTRAINT [PK_MainLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [FlowerInventoryAssessmentLogs] SET  READ_WRITE 
GO
