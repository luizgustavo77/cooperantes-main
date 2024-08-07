USE [base_monsanto]
GO

/****** Object:  Table [dbo].[REGISTER]    Script Date: 31/08/2015 23:58:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[REGISTER](
	[NAME] [varchar](200) NOT NULL,
	[EMAIL] [varchar](300) NOT NULL,
	[LOGIN] [varchar](150) NOT NULL,
	[PASS] [varchar](200) NOT NULL,
	[TYPE] [int] NOT NULL,
	[ACTIVE] [int] NOT NULL,
	[DEL] [varchar](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

