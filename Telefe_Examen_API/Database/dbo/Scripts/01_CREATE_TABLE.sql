CREATE TABLE [dbo].[Search_Record]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Search] [varchar](50) NOT NULL,
	[Result] [bit] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	CONSTRAINT [PK_Search_Record] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO