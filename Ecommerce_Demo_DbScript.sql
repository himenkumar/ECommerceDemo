USE [master]
GO
/****** Object:  Database [ECommerceDemo]    Script Date: 07/03/2019 10:20:54 AM ******/
CREATE DATABASE [ECommerceDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ECommerceDemo', FILENAME = N'D:\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ECommerceDemo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ECommerceDemo_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ECommerceDemo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ECommerceDemo] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ECommerceDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ECommerceDemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ECommerceDemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ECommerceDemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ECommerceDemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ECommerceDemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [ECommerceDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ECommerceDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ECommerceDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ECommerceDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ECommerceDemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ECommerceDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ECommerceDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ECommerceDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ECommerceDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ECommerceDemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ECommerceDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ECommerceDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ECommerceDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ECommerceDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ECommerceDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ECommerceDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ECommerceDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ECommerceDemo] SET RECOVERY FULL 
GO
ALTER DATABASE [ECommerceDemo] SET  MULTI_USER 
GO
ALTER DATABASE [ECommerceDemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ECommerceDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ECommerceDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ECommerceDemo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ECommerceDemo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ECommerceDemo', N'ON'
GO
ALTER DATABASE [ECommerceDemo] SET QUERY_STORE = OFF
GO
USE [ECommerceDemo]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [ECommerceDemo]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 07/03/2019 10:20:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProdCatId] [int] NOT NULL,
	[ProdName] [varchar](250) NOT NULL,
	[ProdDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 07/03/2019 10:20:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttribute](
	[ProductAttributeId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ProductId] [bigint] NOT NULL,
	[AttributeId] [int] NOT NULL,
	[AttributeValue] [varchar](250) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttributeLookup]    Script Date: 07/03/2019 10:20:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttributeLookup](
	[AttributeId] [int] IDENTITY(1,1) NOT NULL,
	[ProdCatId] [int] NOT NULL,
	[AttributeName] [varchar](250) NOT NULL,
 CONSTRAINT [PK_ProductAttributeLookup] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 07/03/2019 10:20:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProdCatId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](250) NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProdCatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ProductAttributeLookup] ON 
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (1, 1, N'Color')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (2, 1, N'Make')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (3, 1, N'Model')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (4, 2, N'RAM')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (5, 2, N'Front Camera')
GO
INSERT [dbo].[ProductAttributeLookup] ([AttributeId], [ProdCatId], [AttributeName]) VALUES (6, 2, N'Rear Camera')
GO
SET IDENTITY_INSERT [dbo].[ProductAttributeLookup] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 
GO
INSERT [dbo].[ProductCategory] ([ProdCatId], [CategoryName]) VALUES (1, N'Car')
GO
INSERT [dbo].[ProductCategory] ([ProdCatId], [CategoryName]) VALUES (2, N'Mobile')
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([ProdCatId])
REFERENCES [dbo].[ProductCategory] ([ProdCatId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_Product]
GO
ALTER TABLE [dbo].[ProductAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttribute_ProductAttributeLookup] FOREIGN KEY([AttributeId])
REFERENCES [dbo].[ProductAttributeLookup] ([AttributeId])
GO
ALTER TABLE [dbo].[ProductAttribute] CHECK CONSTRAINT [FK_ProductAttribute_ProductAttributeLookup]
GO
ALTER TABLE [dbo].[ProductAttributeLookup]  WITH CHECK ADD  CONSTRAINT [FK_ProductAttributeLookup_ProductCategory] FOREIGN KEY([ProdCatId])
REFERENCES [dbo].[ProductCategory] ([ProdCatId])
GO
ALTER TABLE [dbo].[ProductAttributeLookup] CHECK CONSTRAINT [FK_ProductAttributeLookup_ProductCategory]
GO
USE [master]
GO
ALTER DATABASE [ECommerceDemo] SET  READ_WRITE 
GO
