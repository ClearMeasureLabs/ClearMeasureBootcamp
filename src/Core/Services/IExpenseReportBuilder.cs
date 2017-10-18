using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IExpenseReportBuilder
	{
		ExpenseReport Build(Employee creator);
	}
}