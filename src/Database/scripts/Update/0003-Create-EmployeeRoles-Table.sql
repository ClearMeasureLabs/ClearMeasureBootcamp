/****** Object:  Table [dbo].[EmployeeRoles]    Script Date: 1/11/2015 3:15:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmployeeRoles](
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRoles_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO

ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [FK_EmployeeRoles_Employee]
GO

ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [FKEmployeeRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [FKEmployeeRoles_Role]
GO


