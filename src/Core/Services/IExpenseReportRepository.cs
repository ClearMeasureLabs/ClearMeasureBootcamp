using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IExpenseReportRepository
	{
		void Save(ExpenseReport expenseReport);
		ExpenseReport GetSingle(string number);
		ExpenseReport[] GetMany(SearchSpecification specification);
	}
}