/****** Tento create + insert script byl vygenerovany pomoci MS SQL Serveru a nasledne upraven do teto podoby ******/
/****** Uplne dole je potreba nastavit jmeno databaze odpovidajici databazi v https://mssql.aspify.com ******/
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 30.09.2022 9:18:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incident]    Script Date: 30.09.2022 9:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incident](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Amount] [decimal](10, 2) NULL,
	[InsuranceId] [int] NOT NULL,
 CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insurance]    Script Date: 30.09.2022 9:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insurance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[ValidFrom] [datetime2](7) NOT NULL,
	[ValidTo] [datetime2](7) NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_Insurance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 30.09.2022 9:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[HouseNumber] [nvarchar](max) NOT NULL,
	[City] [nvarchar](max) NOT NULL,
	[PostCode] [nvarchar](max) NOT NULL,
	[Street] [nvarchar](max) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220829123544_Create_Table_Person', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220829130545_Create_Table_Person_2', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220829131417_Update_Table_Person', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220829131938_Update_Table_Person_2', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220830125356_Update_Table_Person_3', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220830131458_Update_Table_Person_4', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220831071335_Create_Table_Insurance', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220901095000_Update_Person_model_class', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220901175507_Create_Insurance_Update', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220902065159_Update_2', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220902074510_Update_3', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220905144507_Database_Update', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220908114032_Create_Table_Incident', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220913095315_Update_4', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220915140027_Update_5', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220915141516_Update_6', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916185836_Database_update_7', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916190441_Database_update_8', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916192144_Database_update_9', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916194923_Database_update_10', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916195041_Database_update_11', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916195842_Database_update_12', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220916201559_Database_update_13', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220918093358_Update_14', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220919073606_Update_Database_15', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220919074805_Update_Database_16', N'6.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220925141550_Migrate_Database', N'6.0.8')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'fd70457d-ed51-41f0-b9df-fe309fd8013b', N'Admin', N'ADMIN', N'3071354e-868c-401d-b8a7-41092fa08353')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7decb1a2-5e68-4490-b290-ddfce44c617a', N'fd70457d-ed51-41f0-b9df-fe309fd8013b')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2ef7d9c2-105c-4c75-9c2c-3bb91f319b85', N'frantisek.fiala@email.cz', N'FRANTISEK.FIALA@EMAIL.CZ', N'frantisek.fiala@email.cz', N'FRANTISEK.FIALA@EMAIL.CZ', 0, N'AQAAAAEAACcQAAAAELKi+JmXeh541KdLV/k8NmeC8qgDThczeoxq8aEBJULXfPSbSs1l5XeeFWhzcU4v0g==', N'5J6BN7HDNOMLUKX5HRFT2GKREKZZ4R22', N'28c3536f-5f6e-4d93-93a7-67ff5a49eb8d', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'722fe67d-5ee1-43fb-a49c-274c856ea26d', N'kvetoslav.zapadak@email.cz', N'KVETOSLAV.ZAPADAK@EMAIL.CZ', N'kvetoslav.zapadak@email.cz', N'KVETOSLAV.ZAPADAK@EMAIL.CZ', 0, N'AQAAAAEAACcQAAAAENuYKXfziRLsk2/Mp5KGa0nHVRvmkW3GsPQdz+VQCaspxDU3osK6pm7MIObEy6v2Iw==', N'3W7TZX5NP4P22KOTXTCYFZDXXT3PCABH', N'69101ebf-aca3-4504-aa21-19d324910a58', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7decb1a2-5e68-4490-b290-ddfce44c617a', N'admin@email.cz', N'ADMIN@EMAIL.CZ', N'admin@email.cz', N'ADMIN@EMAIL.CZ', 0, N'AQAAAAEAACcQAAAAEPaC+lICmHXMWjjEQ65NfWHUaTvJT1Tmz4KgJF33X5cFwcdOMFNM94fRNq+vWNINkA==', N'7B2CNFUGYKXYGQX6BRDEDZFDHM7WC66B', N'e8ce7dcd-f9c8-4a05-87f1-7c91255ef5f8', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ca3ab441-6839-4492-8b19-6c9f756ebc8c', N'petr.benda@email.cz', N'PETR.BENDA@EMAIL.CZ', N'petr.benda@email.cz', N'PETR.BENDA@EMAIL.CZ', 0, N'AQAAAAEAACcQAAAAEGRaePidBajar2jZV3ep2oROdd56wLTA8wPk4b9K0+oukXi16p10sNq3qLaeFtlxHg==', N'T65JBQLSEOSY5OYONG4B7GLYSIPKA7JP', N'cefcc925-21fc-421c-9d3d-b10252a9f0a6', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd9f186df-d24f-4bff-b033-5b294ad6b713', N'jan.novak@email.cz', N'JAN.NOVAK@EMAIL.CZ', N'jan.novak@email.cz', N'JAN.NOVAK@EMAIL.CZ', 0, N'AQAAAAEAACcQAAAAEBR45SngDJGtUhfK3Fagt9NniK5q9WNJ7A4BUCnqVZH+p4nzpU3N0YebXJTUW/Y0UA==', N'ILF2CP2A2EAESIMCPV63XJFW6IGTRKZ7', N'75b06970-cfcc-4700-88f9-0ef291a25592', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Incident] ON 

INSERT [dbo].[Incident] ([Id], [Description], [Date], [Status], [Amount], [InsuranceId]) VALUES (1, N'Vykradení', CAST(N'2010-07-07T00:00:00.0000000' AS DateTime2), N'Vyplaceno', CAST(10000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Incident] ([Id], [Description], [Date], [Status], [Amount], [InsuranceId]) VALUES (2, N'Autonehoda', CAST(N'2011-08-08T00:00:00.0000000' AS DateTime2), N'Zamítnuto', NULL, 2)
INSERT [dbo].[Incident] ([Id], [Description], [Date], [Status], [Amount], [InsuranceId]) VALUES (3, N'Vyhoření', CAST(N'2022-09-01T00:00:00.0000000' AS DateTime2), N'Vyřizuje se', CAST(2000000.00 AS Decimal(10, 2)), 3)
SET IDENTITY_INSERT [dbo].[Incident] OFF
GO
SET IDENTITY_INSERT [dbo].[Insurance] ON 

INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (1, N'Pojištění majetku', CAST(5000000.00 AS Decimal(10, 2)), N'Dům', CAST(N'2001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2031-01-09T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (2, N'Pojištění majetku', CAST(400000.00 AS Decimal(10, 2)), N'Auto', CAST(N'2001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2031-01-01T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (3, N'Pojištění majetku', CAST(2000000.00 AS Decimal(10, 2)), N'Chata', CAST(N'2001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2031-01-01T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (4, N'Pojištění majetku', CAST(5000000.00 AS Decimal(10, 2)), N'Dům', CAST(N'2002-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2032-02-02T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (5, N'Pojištění majetku', CAST(400000.00 AS Decimal(10, 2)), N'Auto', CAST(N'2002-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2032-02-02T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (6, N'Pojištění majetku', CAST(2000000.00 AS Decimal(10, 2)), N'Chata', CAST(N'2002-02-02T00:00:00.0000000' AS DateTime2), CAST(N'2032-02-02T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (7, N'Pojištění majetku', CAST(5000000.00 AS Decimal(10, 2)), N'Dům', CAST(N'2003-03-03T00:00:00.0000000' AS DateTime2), CAST(N'2033-03-03T00:00:00.0000000' AS DateTime2), 3)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (8, N'Pojištění majetku', CAST(400000.00 AS Decimal(10, 2)), N'Auto', CAST(N'2003-03-03T00:00:00.0000000' AS DateTime2), CAST(N'2033-03-03T00:00:00.0000000' AS DateTime2), 3)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (9, N'Pojištění majetku', CAST(2000000.00 AS Decimal(10, 2)), N'Chata', CAST(N'2003-03-03T00:00:00.0000000' AS DateTime2), CAST(N'2033-03-03T00:00:00.0000000' AS DateTime2), 3)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (10, N'Pojištění majetku', CAST(5000000.00 AS Decimal(10, 2)), N'Dům', CAST(N'2004-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2034-04-04T00:00:00.0000000' AS DateTime2), 4)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (11, N'Pojištění majetku', CAST(400000.00 AS Decimal(10, 2)), N'Auto', CAST(N'2004-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2034-04-04T00:00:00.0000000' AS DateTime2), 4)
INSERT [dbo].[Insurance] ([Id], [Type], [Amount], [Subject], [ValidFrom], [ValidTo], [PersonId]) VALUES (12, N'Pojištění majetku', CAST(2000000.00 AS Decimal(10, 2)), N'Chata', CAST(N'2004-04-04T00:00:00.0000000' AS DateTime2), CAST(N'2034-04-04T00:00:00.0000000' AS DateTime2), 4)
SET IDENTITY_INSERT [dbo].[Insurance] OFF
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Email], [Phone], [HouseNumber], [City], [PostCode], [Street]) VALUES (1, N'Jan', N'Novák', N'jan.novak@email.cz', N'777 111 111', N'1', N'Praha 1', N'110 00', N'Novákova')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Email], [Phone], [HouseNumber], [City], [PostCode], [Street]) VALUES (2, N'Petr', N'Benda', N'petr.benda@email.cz', N'777 222 222', N'2', N'Praha 2', N'120 00', N'Bendova')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Email], [Phone], [HouseNumber], [City], [PostCode], [Street]) VALUES (3, N'František', N'Fiala', N'frantisek.fiala@email.cz', N'777 333 333', N'3', N'Praha 3', N'130 00', N'Fialova')
INSERT [dbo].[Person] ([Id], [FirstName], [LastName], [Email], [Phone], [HouseNumber], [City], [PostCode], [Street]) VALUES (4, N'Květoslav', N'Zapadák', N'kvetoslav.zapadak@email.cz', N'777 444 444', N'44', N'Zapadákov', N'990 00', NULL)
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 30.09.2022 9:18:54 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 30.09.2022 9:18:54 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Incident_InsuranceId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_Incident_InsuranceId] ON [dbo].[Incident]
(
	[InsuranceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Insurance_PersonId]    Script Date: 30.09.2022 9:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_Insurance_PersonId] ON [dbo].[Insurance]
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Insurance] ADD  DEFAULT (N'') FOR [Type]
GO
ALTER TABLE [dbo].[Insurance] ADD  DEFAULT (N'') FOR [Subject]
GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT (N'') FOR [PostCode]
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
ALTER TABLE [dbo].[Incident]  WITH CHECK ADD  CONSTRAINT [FK_Incident_Insurance_InsuranceId] FOREIGN KEY([InsuranceId])
REFERENCES [dbo].[Insurance] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Incident] CHECK CONSTRAINT [FK_Incident_Insurance_InsuranceId]
GO
ALTER TABLE [dbo].[Insurance]  WITH CHECK ADD  CONSTRAINT [FK_Insurance_Person_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Insurance] CHECK CONSTRAINT [FK_Insurance_Person_PersonId]
GO
USE [master]
GO
ALTER DATABASE [db2020] SET  READ_WRITE 
GO
