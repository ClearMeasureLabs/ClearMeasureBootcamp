using ClearMeasure.Bootcamp.Core.Model;
using System.Threading.Tasks;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
	public class ExpenseReportBuilder : IExpenseReportBuilder
	{
		private readonly INumberGenerator _numberGenerator;
		private readonly ICalendar _calendar;

		public ExpenseReportBuilder(INumberGenerator numberGenerator, ICalendar calendar)
		{
			_numberGenerator = numberGenerator;
			_calendar = calendar;
		}

		public async Task<ExpenseReport> Build(Employee creator)
		{         
			ExpenseReport expenseReport = new ExpenseReport();
			expenseReport.Number = _numberGenerator.GenerateNumber();
			expenseReport.Submitter = creator;
			expenseReport.Status = ExpenseReportStatus.Draft;
		    expenseReport.Created = _calendar.GetCurrentTime();
			return expenseReport;
		}
	}
}