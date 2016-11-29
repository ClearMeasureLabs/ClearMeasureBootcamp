/****** Object:  Table [dbo].[WorkOrder]    Script Date: 1/11/2015 3:15:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExpenseReport](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](5) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
	[Status] [nchar](3) NOT NULL,
	[SubmitterId] [uniqueidentifier] NOT NULL,
	[ApproverId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL
 CONSTRAINT [PK__ExpenseReport_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ExpenseReport]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseReport_EmployeeForSubmitter] FOREIGN KEY([SubmitterId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[ExpenseReport] CHECK CONSTRAINT [FK_ExpenseReport_EmployeeForSubmitter]
GO

ALTER TABLE [dbo].[ExpenseReport]  WITH CHECK ADD  CONSTRAINT [FKExpenseReport_EmployeeForApprover] FOREIGN KEY([ApproverId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[ExpenseReport] CHECK CONSTRAINT [FKExpenseReport_EmployeeForApprover]
GO


