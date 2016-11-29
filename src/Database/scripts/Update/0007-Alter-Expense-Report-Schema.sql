ALTER TABLE dbo.ExpenseReport
ADD MilesDriven INT

ALTER TABLE dbo.ExpenseReport
ADD Created DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD FirstSubmitted DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD LastSubmitted DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD LastWithdrawn DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD LastCancelled DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD LastApproved DATETIME NULL

ALTER TABLE dbo.ExpenseReport
ADD LastDeclined DATETIME NULL