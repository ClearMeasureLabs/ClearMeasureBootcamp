using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Plugins.DataAccess
{
    public class ExpenseReportSaveCommand : IRequest<SingleResult<ExpenseReport>>
    {
        public ExpenseReport ExpenseReport { get; set; }
    }
}