USE [master]
GO
/****** Object:  Database [TastyCoffee]    Script Date: 14.05.2022 13:37:57 ******/
CREATE DATABASE [TastyCoffee]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TastyCoffee', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TastyCoffee.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TastyCoffee_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TastyCoffee_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TastyCoffee] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TastyCoffee].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TastyCoffee] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TastyCoffee] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TastyCoffee] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TastyCoffee] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TastyCoffee] SET ARITHABORT OFF 
GO
ALTER DATABASE [TastyCoffee] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TastyCoffee] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TastyCoffee] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TastyCoffee] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TastyCoffee] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TastyCoffee] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TastyCoffee] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TastyCoffee] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TastyCoffee] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TastyCoffee] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TastyCoffee] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TastyCoffee] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TastyCoffee] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TastyCoffee] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TastyCoffee] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TastyCoffee] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TastyCoffee] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TastyCoffee] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TastyCoffee] SET  MULTI_USER 
GO
ALTER DATABASE [TastyCoffee] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TastyCoffee] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TastyCoffee] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TastyCoffee] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TastyCoffee] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TastyCoffee] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TastyCoffee] SET QUERY_STORE = OFF
GO
USE [TastyCoffee]
GO
/****** Object:  Table [dbo].[Tea]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tea](
	[IdTea] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Price] [nvarchar](40) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Tea] PRIMARY KEY CLUSTERED 
(
	[IdTea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Tea_ComboBox]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Tea_ComboBox]
AS
SELECT IdTea, 'Название: ' + Name + '' + CHAR(13) + 'Цена: ' + Price AS Чай
FROM     dbo.Tea
GO
/****** Object:  Table [dbo].[Coffee]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coffee](
	[IdCoffee] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Price] [nvarchar](40) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Coffee] PRIMARY KEY CLUSTERED 
(
	[IdCoffee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Coffee_ComboBox]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Coffee_ComboBox]
AS
SELECT IdCoffee, 'Название: ' + Name + '' + CHAR(13) + 'Цена: ' + Price AS Кофе
FROM     dbo.Coffee
GO
/****** Object:  Table [dbo].[ApplicationClient]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationClient](
	[IdApplicationClient] [int] IDENTITY(1,1) NOT NULL,
	[IdCoffee] [int] NOT NULL,
	[IdTea] [int] NOT NULL,
	[IdClient] [int] NOT NULL,
	[QuantityCoffee] [nvarchar](40) NOT NULL,
	[QuantityTea] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_ApplicationUser] PRIMARY KEY CLUSTERED 
(
	[IdApplicationClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_ApplicationClient]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_ApplicationClient]
AS
SELECT dbo.ApplicationClient.IdApplicationClient, dbo.ApplicationClient.IdTea, dbo.ApplicationClient.IdCoffee, dbo.ApplicationClient.IdClient, 'Название: ' + dbo.Coffee.Name + '' + CHAR(13) + 'Цена: ' + dbo.Coffee.Price AS Кофе, 
                  dbo.ApplicationClient.QuantityCoffee AS [Кол-во кофе], 'Название: ' + dbo.Tea.Name + '' + CHAR(13) + 'Цена: ' + dbo.Tea.Price AS Чай, dbo.ApplicationClient.QuantityTea AS [Кол-во чая]
FROM     dbo.ApplicationClient INNER JOIN
                  dbo.Coffee ON dbo.ApplicationClient.IdCoffee = dbo.Coffee.IdCoffee INNER JOIN
                  dbo.Tea ON dbo.ApplicationClient.IdTea = dbo.Tea.IdTea
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sotrudnik]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sotrudnik](
	[IdSotrudnik] [int] IDENTITY(1,1) NOT NULL,
	[LoginSotrudnik] [nvarchar](40) NOT NULL,
	[PasswordSotrudnik] [nvarchar](40) NOT NULL,
	[LastNameSotrudnik] [nvarchar](40) NOT NULL,
	[FirstNameSotrudnik] [nvarchar](40) NOT NULL,
	[MidleNameSotrudnik] [nvarchar](40) NULL,
	[BirthdaySotrudnik] [nvarchar](40) NOT NULL,
	[Phone] [nvarchar](40) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_Sotrudnik] PRIMARY KEY CLUSTERED 
(
	[IdSotrudnik] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Sotrudnik]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Sotrudnik]
AS
SELECT dbo.Sotrudnik.IdSotrudnik, dbo.Sotrudnik.IdRole, dbo.Sotrudnik.LoginSotrudnik AS Логин, dbo.Sotrudnik.PasswordSotrudnik AS Пароль, dbo.Sotrudnik.LastNameSotrudnik AS Фамилия, dbo.Sotrudnik.FirstNameSotrudnik AS Имя, 
                  dbo.Sotrudnik.MidleNameSotrudnik AS Отчество, dbo.Sotrudnik.BirthdaySotrudnik AS [Дата рождения], dbo.Sotrudnik.Phone AS Телефон, dbo.Role.Role AS Роль
FROM     dbo.Role INNER JOIN
                  dbo.Sotrudnik ON dbo.Role.IdRole = dbo.Sotrudnik.IdRole
GO
/****** Object:  View [dbo].[View_Role_ComboBox]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Role_ComboBox]
AS
SELECT IdRole, Role AS Роль
FROM     dbo.Role
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[LoginClient] [nvarchar](40) NOT NULL,
	[PasswordClient] [nvarchar](40) NOT NULL,
	[LastNameClient] [nvarchar](40) NOT NULL,
	[FirstNameClient] [nvarchar](40) NOT NULL,
	[MidleNameClient] [nvarchar](40) NULL,
	[BirthdayClient] [nvarchar](40) NOT NULL,
	[PhoneClient] [nvarchar](40) NOT NULL,
	[EmailClient] [nvarchar](40) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Clients]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Clients]
AS
SELECT IdClient, LoginClient AS Логин, PasswordClient AS Пароль, LastNameClient AS Фамилия, FirstNameClient AS Имя, MidleNameClient AS Отчество, BirthdayClient AS [Дата рождения], PhoneClient AS Телефон, 
                  EmailClient AS Почта
FROM     dbo.Clients
GO
/****** Object:  View [dbo].[View_Coffee]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Coffee]
AS
SELECT IdCoffee, Name AS Название, Price AS Цена, Quantity AS Количество
FROM     dbo.Coffee
GO
/****** Object:  View [dbo].[View_Tea]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Tea]
AS
SELECT IdTea, Name AS Название, Price AS Цена, Quantity AS Количество
FROM     dbo.Tea
GO
/****** Object:  Table [dbo].[MyOrders]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyOrders](
	[IdMyOrders] [int] IDENTITY(1,1) NOT NULL,
	[IdClient] [int] NOT NULL,
	[IdSotrudnik] [int] NULL,
	[IdStatus] [int] NOT NULL,
	[Date] [nvarchar](40) NOT NULL,
	[Adress] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_MyOrders] PRIMARY KEY CLUSTERED 
(
	[IdMyOrders] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[IdStatus] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[IdStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_MyOrbers_Status]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_MyOrbers_Status]
AS
SELECT dbo.MyOrders.IdMyOrders, dbo.MyOrders.IdClient, dbo.MyOrders.IdSotrudnik, dbo.MyOrders.IdStatus, dbo.MyOrders.Date AS Дата, dbo.MyOrders.Adress AS Адрес, dbo.Status.Status AS Статус
FROM     dbo.MyOrders INNER JOIN
                  dbo.Status ON dbo.MyOrders.IdStatus = dbo.Status.IdStatus
GO
/****** Object:  Table [dbo].[MyOrdersAndOrderInformation]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyOrdersAndOrderInformation](
	[IdMyOrdersAndOrderInformation] [int] IDENTITY(1,1) NOT NULL,
	[IdMyOrders] [int] NOT NULL,
	[IdOrderInformation] [int] NOT NULL,
 CONSTRAINT [PK_MyOrdersAndOrderInformation] PRIMARY KEY CLUSTERED 
(
	[IdMyOrdersAndOrderInformation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInformation]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInformation](
	[IdOrderInformation] [int] IDENTITY(1,1) NOT NULL,
	[IdApplicationClient] [int] NOT NULL,
	[IdCoffee] [int] NOT NULL,
	[IdTea] [int] NOT NULL,
	[IdClient] [int] NOT NULL,
	[QuantityCoffee] [nvarchar](40) NOT NULL,
	[QuantityTea] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_OrderInformation_1] PRIMARY KEY CLUSTERED 
(
	[IdOrderInformation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_OrberInformation]    Script Date: 14.05.2022 13:37:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_OrberInformation]
AS
SELECT dbo.MyOrdersAndOrderInformation.IdMyOrdersAndOrderInformation, dbo.MyOrdersAndOrderInformation.IdMyOrders, dbo.MyOrdersAndOrderInformation.IdOrderInformation, dbo.OrderInformation.IdClient, 
                  dbo.Coffee.Name AS [Название кофе], dbo.OrderInformation.QuantityCoffee AS [Кол-во кофе], dbo.Tea.Name AS [Название чая], dbo.OrderInformation.QuantityTea AS [Кол-во чая]
FROM     dbo.Coffee INNER JOIN
                  dbo.OrderInformation ON dbo.Coffee.IdCoffee = dbo.OrderInformation.IdCoffee INNER JOIN
                  dbo.Tea ON dbo.OrderInformation.IdTea = dbo.Tea.IdTea INNER JOIN
                  dbo.MyOrdersAndOrderInformation ON dbo.OrderInformation.IdOrderInformation = dbo.MyOrdersAndOrderInformation.IdOrderInformation
GO
SET IDENTITY_INSERT [dbo].[ApplicationClient] ON 

INSERT [dbo].[ApplicationClient] ([IdApplicationClient], [IdCoffee], [IdTea], [IdClient], [QuantityCoffee], [QuantityTea]) VALUES (97, 2, 2, 3, N'1', N'2')
SET IDENTITY_INSERT [dbo].[ApplicationClient] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([IdClient], [LoginClient], [PasswordClient], [LastNameClient], [FirstNameClient], [MidleNameClient], [BirthdayClient], [PhoneClient], [EmailClient]) VALUES (1, N'user', N'user', N'пользователь', N'пользователь', N'', N'03.09.2000', N'89009090', N'star@mail')
INSERT [dbo].[Clients] ([IdClient], [LoginClient], [PasswordClient], [LastNameClient], [FirstNameClient], [MidleNameClient], [BirthdayClient], [PhoneClient], [EmailClient]) VALUES (2, N'users', N'users', N'Гапеев', N'Егор', N'', N'03.05.2003', N'89007653', N'eg@mail')
INSERT [dbo].[Clients] ([IdClient], [LoginClient], [PasswordClient], [LastNameClient], [FirstNameClient], [MidleNameClient], [BirthdayClient], [PhoneClient], [EmailClient]) VALUES (3, N'sova', N'123', N'Сова', N'Никита', N'', N'28.03.2022', N'123', N'123')
INSERT [dbo].[Clients] ([IdClient], [LoginClient], [PasswordClient], [LastNameClient], [FirstNameClient], [MidleNameClient], [BirthdayClient], [PhoneClient], [EmailClient]) VALUES (6, N'lera', N'lera', N'Перелыгина', N'Валерия', N'Дмитриевна', N'07.10.2003', N'89007564', N'lera@gmail.com')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Coffee] ON 

INSERT [dbo].[Coffee] ([IdCoffee], [Name], [Price], [Quantity]) VALUES (1, N'Нет', N'0', 0)
INSERT [dbo].[Coffee] ([IdCoffee], [Name], [Price], [Quantity]) VALUES (2, N'Латте', N'500', 0)
INSERT [dbo].[Coffee] ([IdCoffee], [Name], [Price], [Quantity]) VALUES (3, N'Раф', N'500', 398)
INSERT [dbo].[Coffee] ([IdCoffee], [Name], [Price], [Quantity]) VALUES (6, N'Американо', N'400', 10)
INSERT [dbo].[Coffee] ([IdCoffee], [Name], [Price], [Quantity]) VALUES (8, N'Капучино', N'400', 19)
SET IDENTITY_INSERT [dbo].[Coffee] OFF
GO
SET IDENTITY_INSERT [dbo].[MyOrders] ON 

INSERT [dbo].[MyOrders] ([IdMyOrders], [IdClient], [IdSotrudnik], [IdStatus], [Date], [Adress]) VALUES (22, 6, 2, 2, N'17.04.2022 18:06:26', N'Видное')
INSERT [dbo].[MyOrders] ([IdMyOrders], [IdClient], [IdSotrudnik], [IdStatus], [Date], [Adress]) VALUES (23, 3, 1005, 1, N'19.04.2022 19:56:01', N'Видное')
SET IDENTITY_INSERT [dbo].[MyOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[MyOrdersAndOrderInformation] ON 

INSERT [dbo].[MyOrdersAndOrderInformation] ([IdMyOrdersAndOrderInformation], [IdMyOrders], [IdOrderInformation]) VALUES (1038, 22, 71)
INSERT [dbo].[MyOrdersAndOrderInformation] ([IdMyOrdersAndOrderInformation], [IdMyOrders], [IdOrderInformation]) VALUES (1039, 22, 72)
INSERT [dbo].[MyOrdersAndOrderInformation] ([IdMyOrdersAndOrderInformation], [IdMyOrders], [IdOrderInformation]) VALUES (1040, 23, 73)
SET IDENTITY_INSERT [dbo].[MyOrdersAndOrderInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderInformation] ON 

INSERT [dbo].[OrderInformation] ([IdOrderInformation], [IdApplicationClient], [IdCoffee], [IdTea], [IdClient], [QuantityCoffee], [QuantityTea]) VALUES (71, 93, 2, 8, 6, N'5', N'20')
INSERT [dbo].[OrderInformation] ([IdOrderInformation], [IdApplicationClient], [IdCoffee], [IdTea], [IdClient], [QuantityCoffee], [QuantityTea]) VALUES (72, 94, 8, 2, 6, N'1', N'15')
INSERT [dbo].[OrderInformation] ([IdOrderInformation], [IdApplicationClient], [IdCoffee], [IdTea], [IdClient], [QuantityCoffee], [QuantityTea]) VALUES (73, 96, 3, 1, 3, N'2', N'0')
SET IDENTITY_INSERT [dbo].[OrderInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([IdRole], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([IdRole], [Role]) VALUES (3, N'Delivery')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Sotrudnik] ON 

INSERT [dbo].[Sotrudnik] ([IdSotrudnik], [LoginSotrudnik], [PasswordSotrudnik], [LastNameSotrudnik], [FirstNameSotrudnik], [MidleNameSotrudnik], [BirthdaySotrudnik], [Phone], [IdRole]) VALUES (1, N'admin', N'admin', N'Белоконь', N'Даниил', N'Павлович', N'27.09.2003', N'8900553535', 1)
INSERT [dbo].[Sotrudnik] ([IdSotrudnik], [LoginSotrudnik], [PasswordSotrudnik], [LastNameSotrudnik], [FirstNameSotrudnik], [MidleNameSotrudnik], [BirthdaySotrudnik], [Phone], [IdRole]) VALUES (2, N'delivery', N'delivery', N'delivery', N'delivery', N'delivery', N'13.10.2004', N'8976326', 3)
INSERT [dbo].[Sotrudnik] ([IdSotrudnik], [LoginSotrudnik], [PasswordSotrudnik], [LastNameSotrudnik], [FirstNameSotrudnik], [MidleNameSotrudnik], [BirthdaySotrudnik], [Phone], [IdRole]) VALUES (1003, N'grib', N'grib', N'Грибков', N'Кузьма', N'', N'02.09.2003', N'89006565', 3)
INSERT [dbo].[Sotrudnik] ([IdSotrudnik], [LoginSotrudnik], [PasswordSotrudnik], [LastNameSotrudnik], [FirstNameSotrudnik], [MidleNameSotrudnik], [BirthdaySotrudnik], [Phone], [IdRole]) VALUES (1005, N'null324354678', N'null12349876543', N'null', N'null', N'null', N'05.04.2022', N'null', 3)
SET IDENTITY_INSERT [dbo].[Sotrudnik] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([IdStatus], [Status]) VALUES (1, N'Не выполнено')
INSERT [dbo].[Status] ([IdStatus], [Status]) VALUES (2, N'Выполнено')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Tea] ON 

INSERT [dbo].[Tea] ([IdTea], [Name], [Price], [Quantity]) VALUES (1, N'Нет', N'0', 0)
INSERT [dbo].[Tea] ([IdTea], [Name], [Price], [Quantity]) VALUES (2, N'Зеленый', N'300', 383)
INSERT [dbo].[Tea] ([IdTea], [Name], [Price], [Quantity]) VALUES (3, N'Черный', N'300', 300)
INSERT [dbo].[Tea] ([IdTea], [Name], [Price], [Quantity]) VALUES (8, N'Ягодный', N'500', 80)
SET IDENTITY_INSERT [dbo].[Tea] OFF
GO
ALTER TABLE [dbo].[ApplicationClient]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_Coffee] FOREIGN KEY([IdCoffee])
REFERENCES [dbo].[Coffee] ([IdCoffee])
GO
ALTER TABLE [dbo].[ApplicationClient] CHECK CONSTRAINT [FK_ApplicationUser_Coffee]
GO
ALTER TABLE [dbo].[ApplicationClient]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_Tea] FOREIGN KEY([IdTea])
REFERENCES [dbo].[Tea] ([IdTea])
GO
ALTER TABLE [dbo].[ApplicationClient] CHECK CONSTRAINT [FK_ApplicationUser_Tea]
GO
ALTER TABLE [dbo].[ApplicationClient]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUser_User] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Clients] ([IdClient])
GO
ALTER TABLE [dbo].[ApplicationClient] CHECK CONSTRAINT [FK_ApplicationUser_User]
GO
ALTER TABLE [dbo].[MyOrders]  WITH CHECK ADD  CONSTRAINT [FK_MyOrders_Sotrudnik] FOREIGN KEY([IdSotrudnik])
REFERENCES [dbo].[Sotrudnik] ([IdSotrudnik])
GO
ALTER TABLE [dbo].[MyOrders] CHECK CONSTRAINT [FK_MyOrders_Sotrudnik]
GO
ALTER TABLE [dbo].[MyOrders]  WITH CHECK ADD  CONSTRAINT [FK_MyOrders_Status] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[Status] ([IdStatus])
GO
ALTER TABLE [dbo].[MyOrders] CHECK CONSTRAINT [FK_MyOrders_Status]
GO
ALTER TABLE [dbo].[MyOrders]  WITH CHECK ADD  CONSTRAINT [FK_MyOrders_User] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Clients] ([IdClient])
GO
ALTER TABLE [dbo].[MyOrders] CHECK CONSTRAINT [FK_MyOrders_User]
GO
ALTER TABLE [dbo].[MyOrdersAndOrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_MyOrdersAndOrderInformation_MyOrders] FOREIGN KEY([IdMyOrders])
REFERENCES [dbo].[MyOrders] ([IdMyOrders])
GO
ALTER TABLE [dbo].[MyOrdersAndOrderInformation] CHECK CONSTRAINT [FK_MyOrdersAndOrderInformation_MyOrders]
GO
ALTER TABLE [dbo].[MyOrdersAndOrderInformation]  WITH CHECK ADD  CONSTRAINT [FK_MyOrdersAndOrderInformation_OrderInformation1] FOREIGN KEY([IdOrderInformation])
REFERENCES [dbo].[OrderInformation] ([IdOrderInformation])
GO
ALTER TABLE [dbo].[MyOrdersAndOrderInformation] CHECK CONSTRAINT [FK_MyOrdersAndOrderInformation_OrderInformation1]
GO
ALTER TABLE [dbo].[Sotrudnik]  WITH CHECK ADD  CONSTRAINT [FK_Sotrudnik_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[Sotrudnik] CHECK CONSTRAINT [FK_Sotrudnik_Role]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Coffee"
            Begin Extent = 
               Top = 7
               Left = 310
               Bottom = 187
               Right = 511
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tea"
            Begin Extent = 
               Top = 22
               Left = 575
               Bottom = 196
               Right = 776
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ApplicationClient"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 2316
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_ApplicationClient'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_ApplicationClient'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Clients"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Clients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Clients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Coffee"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 148
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Coffee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Coffee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Coffee"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 148
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Coffee_ComboBox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Coffee_ComboBox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MyOrders"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Status"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 126
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_MyOrbers_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_MyOrbers_Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[44] 4[19] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -240
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Coffee"
            Begin Extent = 
               Top = 52
               Left = 544
               Bottom = 215
               Right = 745
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OrderInformation"
            Begin Extent = 
               Top = 305
               Left = 770
               Bottom = 567
               Right = 994
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tea"
            Begin Extent = 
               Top = 58
               Left = 959
               Bottom = 221
               Right = 1160
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MyOrdersAndOrderInformation"
            Begin Extent = 
               Top = 298
               Left = 357
               Bottom = 486
               Right = 672
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1440
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 13' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OrberInformation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'50
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OrberInformation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OrberInformation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Role"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Role_ComboBox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Role_ComboBox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Role"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Sotrudnik"
            Begin Extent = 
               Top = 7
               Left = 297
               Bottom = 170
               Right = 533
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 3828
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Sotrudnik'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Sotrudnik'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Tea"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Tea'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Tea'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Tea"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 148
               Right = 249
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1128
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Tea_ComboBox'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Tea_ComboBox'
GO
USE [master]
GO
ALTER DATABASE [TastyCoffee] SET  READ_WRITE 
GO
