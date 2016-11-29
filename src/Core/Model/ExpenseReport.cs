using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearMeasure.Bootcamp.Core.Model
{
    public class ExpenseReport
    {
        public IList<AuditEntry> _auditEntries = new List<AuditEntry>();
        public IList<Expense> _expenses = new List<Expense>();
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ExpenseReportStatus Status { get; set; }
        public Employee Submitter { get; set; }
        public Employee Approver { get; set; }
        public string Number { get; set; }
        // New Properties
        public int MilesDriven { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? FirstSubmitted { get; set; }
        public DateTime? LastSubmitted { get; set; }
        public DateTime? LastWithdrawn { get; set; }
        public DateTime? LastCancelled { get; set; }
        public DateTime? LastApproved { get; set; }
        public DateTime? LastDeclined { get; set; }
        public decimal Total { get; set; }

        public ExpenseReport()
        {
            Status = ExpenseReportStatus.Draft;
            Description = "";
            Title = "";
        }

        public string FriendlyStatus
        {
            get { return GetTextForStatus(); }
        }

        protected string GetTextForStatus()
        {
            return Status.ToString();
        }

        public override string ToString()
        {
            return "ExpenseReport " + Number;
        }

        public void ChangeStatus(ExpenseReportStatus status)
        {
            Status = status;
        }

        public void ChangeStatus(Employee employee, DateTime date, ExpenseReportStatus beginStatus, ExpenseReportStatus endStatus)
        {
            var auditItem = new AuditEntry(employee, date, beginStatus, endStatus);
            _auditEntries.Add(auditItem);
            Status = endStatus;
        }

        public AuditEntry[] GetAuditEntries()
        {
            return _auditEntries.ToArray();
        }

        public void AddAuditEntry(AuditEntry auditEntry)
        {
            _auditEntries.Add(auditEntry);
        }

        public void AddExpense(string description, decimal total)
        {
            var expense = new Expense(total, description);
            _expenses.Add(expense);
        }

        public Expense[] GetExpenses()
        {
            return _expenses.ToArray();
        }
    }
}