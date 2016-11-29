
ALTER TABLE [dbo].[EmployeeRoles] DROP CONSTRAINT [FKEmployeeRoles_Role];


GO
PRINT N'Dropping [dbo].[FK_EmployeeRoles_Employee]...';


GO
ALTER TABLE [dbo].[EmployeeRoles] DROP CONSTRAINT [FK_EmployeeRoles_Employee];


GO
PRINT N'Dropping [dbo].[EmployeeRoles]...';


GO
DROP TABLE [dbo].[EmployeeRoles];


GO
PRINT N'Dropping [dbo].[Role]...';


GO
DROP TABLE [dbo].[Role];


GO
PRINT N'Altering [dbo].[AuditEntry]...';


GO
ALTER TABLE [dbo].[AuditEntry] DROP COLUMN [ArchivedEmployeeName], COLUMN [BeginStatus];


GO
PRINT N'Altering [dbo].[ExpenseReport]...';


GO
ALTER TABLE [dbo].[ExpenseReport] DROP COLUMN [CreatedDate];


GO
PRINT N'Update complete.';


GO
