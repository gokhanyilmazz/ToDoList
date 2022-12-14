USE [ToDoList]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 4.10.2022 13:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDos]    Script Date: 4.10.2022 13:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[EndDate] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoUserRel]    Script Date: 4.10.2022 13:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoUserRel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToDoId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_ToDoUserRel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4.10.2022 13:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PassWord] [nvarchar](50) NOT NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[isDeleted] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 4.10.2022 13:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (1, N'Aktif')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (2, N'Pasif')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[ToDos] ON 

INSERT [dbo].[ToDos] ([Id], [Description], [EndDate], [isDeleted], [isActive], [DateCreated], [StatusId]) VALUES (3, N'asd', CAST(N'2022-10-14T00:00:00.000' AS DateTime), 0, 1, CAST(N'2022-10-04T13:13:32.580' AS DateTime), 2)
INSERT [dbo].[ToDos] ([Id], [Description], [EndDate], [isDeleted], [isActive], [DateCreated], [StatusId]) VALUES (4, N'asd', CAST(N'2022-10-17T00:00:00.000' AS DateTime), 0, 1, CAST(N'2022-10-04T13:13:42.197' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[ToDos] OFF
GO
SET IDENTITY_INSERT [dbo].[ToDoUserRel] ON 

INSERT [dbo].[ToDoUserRel] ([Id], [ToDoId], [UserId]) VALUES (1, 3, 1)
INSERT [dbo].[ToDoUserRel] ([Id], [ToDoId], [UserId]) VALUES (2, 4, 1)
SET IDENTITY_INSERT [dbo].[ToDoUserRel] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserTypeId], [FullName], [UserName], [PassWord], [ProfilePicture], [isDeleted], [isActive], [DateCreated]) VALUES (1, 0, N'Gökhan', N'gokhan', N'123', NULL, 0, 1, CAST(N'2022-07-27T19:30:44.863' AS DateTime))
INSERT [dbo].[Users] ([Id], [UserTypeId], [FullName], [UserName], [PassWord], [ProfilePicture], [isDeleted], [isActive], [DateCreated]) VALUES (2, 0, N'Ahmet', N'ahmet', N'123', NULL, 0, 1, CAST(N'2022-10-04T13:14:07.930' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserTypes] ([Id], [Name]) VALUES (0, N'Admin')
GO
ALTER TABLE [dbo].[ToDos] ADD  CONSTRAINT [DF_Tasks_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[ToDos] ADD  CONSTRAINT [DF_Tasks_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[ToDos] ADD  CONSTRAINT [DF_Tasks_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ToDos]  WITH CHECK ADD  CONSTRAINT [FK_ToDos_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[ToDos] CHECK CONSTRAINT [FK_ToDos_Statuses]
GO
ALTER TABLE [dbo].[ToDoUserRel]  WITH CHECK ADD  CONSTRAINT [FK_ToDoUserRel_ToDos] FOREIGN KEY([ToDoId])
REFERENCES [dbo].[ToDos] ([Id])
GO
ALTER TABLE [dbo].[ToDoUserRel] CHECK CONSTRAINT [FK_ToDoUserRel_ToDos]
GO
ALTER TABLE [dbo].[ToDoUserRel]  WITH CHECK ADD  CONSTRAINT [FK_ToDoUserRel_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ToDoUserRel] CHECK CONSTRAINT [FK_ToDoUserRel_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserTypes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes]
GO
