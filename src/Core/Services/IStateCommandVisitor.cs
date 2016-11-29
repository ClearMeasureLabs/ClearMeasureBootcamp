using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IStateCommandVisitor
	{
		void Save(ExpenseReport expenseReport);
		void GoToEdit(ExpenseReport expenseReport);
		void GoToSearch(Employee accountManager, Employee practiceOwner, ExpenseReportStatus status);
		void SendMessage(string message);
		void SendError(string message); 
	    void GoToDashboard();
	}
}