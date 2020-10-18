
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

 

CREATE TABLE [dbo].[Applications](
              [Name] [varchar](100) NULL,
              [Environment] [varchar](25) NULL,
              [Owner_Primary] [varchar](50) NULL,
              [Owner_Secondary] [varchar](50) NULL,
              [In_Scope] [bit] NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Analysis_Status] [varchar](50) NULL,
              [Description] [varchar](255) NULL,
              [Technical_Contact_Primary] [varchar](50) NULL,
              [Technical_Contact_Secondary] [varchar](50) NULL,
              [Business_Unit] [varchar](50) NULL,
              [Vendor] [varchar](25) NULL,
              [Version] [varchar](25) NULL,
              [Business_Criticality] [varchar](10) NULL,
              [Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/*fill*/
/*-----------------------------------------------------------Databases Table*/

SET ANSI_NULLS ON

 
SET QUOTED_IDENTIFIER ON

 

CREATE TABLE [dbo].[Databases](
              [Name] [varchar](100) NULL,
              [DB_Type] [varchar](50) NULL,
              [DB_Version] [varchar](20) NULL,
              [In_Scope] [bit] NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Technical_Contact] [varchar](50) NULL,
              [DB_Instance] [varchar](255) NULL,
              [DB_Server_Name] [varchar](100) NULL,
              [DB_Size_GB] [varchar](20) NULL,
              [Discovery_Source] [varchar](20) NULL,
              [Environment] [varchar](25) NULL,
              [Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/*-------------------------------------------------------------Hosts Table*/


 
CREATE TABLE [dbo].[Hosts](
              [Name] [varchar](100) NULL,
              [OS] [varchar](50) NULL,
              [OS_Version] [varchar](50) NULL,
              [Physical_or_Virtual] [varchar](10) NULL,
              [In_Scope] [bit] NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Vendor] [varchar](50) NULL,
              [Model] [varchar](50) NULL,
              [Source_DC] [varchar](25) NULL,
              [Function_or_Role] [varchar](100) NULL,
              [Host_Type] [varchar](100) NULL,
              [Technical_Contact] [varchar](20) NULL,
              [Discovery_Source] [varchar](20) NULL,
              [Environment] [varchar](25) NULL,
              [Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

 
/*---------------------------------------------------------------Relationships Table*/
 
CREATE TABLE [dbo].[Relationships](
              [Entity1 Name] [varchar](100) NULL,
              [Entity1 Type] [varchar](20) NULL,
              [Entity2 Name] [varchar](100) NULL,
              [Entity2 Type] [varchar](20) NULL,
              [Score] [varchar](50) NULL,
              [Migration Type] [varchar](50) NULL
) ON [PRIMARY]

 
/*-----------------------------------------------------------------Storage Table*/

CREATE TABLE [dbo].[Storage](
              [Name] [varchar](100) NULL,
              [Type] [varchar](50) NULL,
              [Vendor] [varchar](50) NULL,
              [Model] [varchar](50) NULL,
              [In_Scope] [bit] NULL,
              [Out_of_Scope_Justification] [varchar](255) NULL,
              [Storage Capacity Allocated - GB] [varchar](10) NULL,
              [Storage Capacity Used - GB] [varchar](10) NULL,
              [Owner_Primary] [varchar](25) NULL,
              [Owner_Secondary] [varchar](25) NULL,
              [Host_Type] [varchar](20) NULL,
              [Technical_Contact_Primary] [varchar](25) NULL,
              [Technical_Contact_Secondary] [varchar](25) NULL,
              [Discovery_Source] [varchar](20) NULL,
              [Environment] [varchar](25) NULL,
              [Comments] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
 
 
