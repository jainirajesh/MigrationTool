/****** Object:  Table [dbo].[LAppServer]    Script Date: 11/2/2020 10:00:09 AM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[LAppServer](
              [Application] [varchar](255) NOT NULL,
              [Server] [varchar](255) NOT NULL,
              [bundle] [nvarchar](50) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Applications](
              [Name] [varchar](255) ,
              [Environment] [varchar](255) NULL,
              [Owner_Primary] [varchar](255) NULL,
              [Owner_Secondary] [varchar](255) NULL,
              [In_Scope] [varchar](255) NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Analysis_Status] [varchar](255) NULL,
              [Description] [varchar](255) NULL,
              [Technical_Contact_Primary] [varchar](255) NULL,
              [Technical_Contact_Secondary] [varchar](255) NULL,
              [Business_Unit] [varchar](255) NULL,
              [Vendor] [varchar](255) NULL,
              [Version] [varchar](255) NULL,
              [Business_Criticality] [varchar](255) NULL,
              [Comments] [text] NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[Hosts](
              [Name] [varchar](255) ,
              [OS] [varchar](255) NULL,
              [OS_Version] [varchar](255) NULL,
              [Physical_or_Virtual] [varchar](255) NULL,
              [In_Scope] [varchar](255) NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Vendor] [varchar](255) NULL,
              [Model] [varchar](255) NULL,
              [Source_DC] [varchar](255) NULL,
              [Function_or_Role] [varchar](255) NULL,
              [Host_Type] [varchar](255) NULL,
              [Technical_Contact] [varchar](255) NULL,
              [Discovery_Source] [varchar](255) NULL,
              [Environment] [varchar](255) NULL,
              [Comments] [text] NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[Databases](
              [Name] [varchar](255),
              [Server_Name] [varchar](255) NULL,
              [DB_Type] [varchar](255) NULL,
              [DB_Version] [varchar](255) NULL,
              [In_Scope] [varchar](255) NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Technical_Contact] [varchar](255) NULL,
              [DB_Instance] [varchar](255) NULL,
              [DB_Server_Name] [varchar](255) NULL,
              [DB_Size_GB] [varchar](255) NULL,
              [Discovery_Source] [varchar](255) NULL,
              [Environment] [varchar](255) NULL,
              [Comments] [text] NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[Storage](
              [Name] [varchar](255),
              [Type] [varchar](255) NULL,
              [Vendor] [varchar](255) NULL,
              [Model] [varchar](255) NULL,
              [In_Scope] [varchar](255) NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Storage_Capacity_Allocated_GB] [varchar](255) NULL,
              [Storage_Capacity_Used_GB] [varchar](255) NULL,
              [Owner_Primary] [varchar](255) NULL,
              [Owner_Secondary] [varchar](255) NULL,
              [Host_Type] [varchar](255) NULL,
              [Technical_Contact_Primary] [varchar](255) NULL,
              [Technical_Contact_Secondary] [varchar](255) NULL,
              [Discovery_Source] [varchar](255) NULL,
              [Environment] [varchar](255) NULL,
              [Comments] [text] NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Relationships](
              [Entity1_Name] [varchar](255) NULL,
              [Entity1_Type] [varchar](255) NULL,
              [Entity2_Name] [varchar](255) NULL,
              [Entity2_Type] [varchar](255) NULL,
              [Score] [varchar](50) NULL,
              [Migration_Type] [varchar](50) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[UserRole] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY]
