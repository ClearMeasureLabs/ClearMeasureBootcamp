using System.Linq;
using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.UI.Models
{
    public class ExpenseReportSearchModel
    {
        public ExpenseReportSearchModel()
        {
            Results = Enumerable.Empty<ExpenseReport>().ToArray();
            Filters = new SearchFilters();
        }

        public ExpenseReport[] Results { get; set; }
        public SearchFilters Filters { get; set; }

        public class SearchFilters
        {
            public string Submitter { get; set; }
            public string Approver { get; set; }
            public string Status { get; set; }
        }
    }
}