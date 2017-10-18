using ClearMeasure.Bootcamp.Core.Model;
using System.Threading.Tasks;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IExpenseReportBuilder
	{
		 Task<ExpenseReport> Build(Employee creator);
	}
}