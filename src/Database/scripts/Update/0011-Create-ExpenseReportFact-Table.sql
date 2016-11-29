create table ExpenseReportFact(
Id uniqueIdentifier primary key, Number varChar(5), TimeStamp DateTime, Total money, Status varChar(200), Submitter varChar(200), Approver varChar(200))
 