
create view
MostRecentExpenseReportFactView
as
SELECT eft.*
  FROM ExpenseReportFact eft
	join (select number,MAX(timestamp) as timestamp from ExpenseReportFact group by number) lastFact
		on eft.Number = lastFact.Number
			and eft.TimeStamp = lastFact.timestamp
  