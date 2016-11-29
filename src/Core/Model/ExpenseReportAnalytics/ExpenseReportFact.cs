using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics
{
    public class ExpenseReportFact
    {
        public ExpenseReportFact()
        {
        }

        public ExpenseReportFact(ExpenseReport expenseReport, DateTime timeStamp)
        {
            ExpenseReportId = expenseReport.Id;
            Number = expenseReport.Number;
            Status = expenseReport.Status.FriendlyName;
            Submitter = expenseReport.Submitter.GetFullName();
            if(Approver != null)
                Approver = expenseReport.Approver.GetFullName();
            TimeStamp = timeStamp;
            Total = expenseReport.Total;
        }

        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }

        public decimal Total { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Approver { get; set; }

        public string Submitter { get; set; }

        public Guid ExpenseReportId { get; set; }

    }
}
