using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;

namespace ClearMeasure.Bootcamp.Core.Features.SearchExpenseReports
{
    public class ExpenseReportSpecificationQuery : IRequest<MultipleResult<ExpenseReport>>
    {
        public ExpenseReportStatus Status { get; set; }
        public Employee Approver { get; set; }
        public Employee Submitter { get; set; } 
    }
}