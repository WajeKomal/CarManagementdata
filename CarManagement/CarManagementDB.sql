USE [CarDetails]
GO
/****** Object:  Table [dbo].[CarModel]    Script Date: 05-12-2024 19:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](50) NOT NULL,
	[Class] [nvarchar](50) NOT NULL,
	[ModelName] [nvarchar](100) NOT NULL,
	[ModelCode] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Features] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DateOfManufacturing] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[ModelImage] [nvarchar](255) NULL,
	[DateCreated] [datetime] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarModelImages]    Script Date: 05-12-2024 19:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarModelImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarModelId] [int] NULL,
	[ImagePath] [nvarchar](255) NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 05-12-2024 19:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmanName] [nvarchar](100) NOT NULL,
	[Class] [nvarchar](10) NOT NULL,
	[Brand] [nvarchar](50) NOT NULL,
	[CarCount] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesmanInfo]    Script Date: 05-12-2024 19:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesmanInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SalesmanName] [nvarchar](100) NOT NULL,
	[LastYearSales] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CarModel] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CarModel] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[CarModelImages] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CarModelImages]  WITH CHECK ADD FOREIGN KEY([CarModelId])
REFERENCES [dbo].[CarModel] ([Id])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD CHECK  (([CarCount]>=(0)))
GO
