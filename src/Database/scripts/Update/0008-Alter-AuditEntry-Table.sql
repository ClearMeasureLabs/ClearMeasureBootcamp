/****** Object:  Table [dbo].[AuditEntry]    Script Date: 1/11/2015 3:15:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[AuditEntry]
ADD EmployeeName VARCHAR(200), BeginStatus CHAR(3)

GO

SET ANSI_PADDING OFF
GO


