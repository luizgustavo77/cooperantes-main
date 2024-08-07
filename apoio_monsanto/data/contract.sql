USE [base_monsanto]
GO

/****** Object:  Table [dbo].[CONTRACT]    Script Date: 03/09/2015 03:22:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CONTRACT](
	[type_contract] [int] NOT NULL,
	[id_client] [int] NOT NULL,
	[dt_receb] [date] NULL,
	[safra] [varchar](9) NULL,
	[obs] [varchar](200) NULL,
	[status] [int] NULL,
	[dt_status] [date] NULL,
	[user_conf] [varchar](100) NULL,
	[criteria] [varchar](200) NULL,
	[id_user_rtv] [int] NULL,
	[id_user_gr] [int] NULL,
	[dt_digital] [date] NULL,
	[dt_archive] [date] NULL,
	[dt_approv] [date] NULL,
	[keeper] [varchar](250) NULL,
	[id] [int] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

