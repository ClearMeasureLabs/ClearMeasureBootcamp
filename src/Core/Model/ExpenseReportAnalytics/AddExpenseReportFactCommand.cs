namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics
{
    public class AddExpenseReportFactCommand : IRequest<AddExpenseReportFactResult>
    {
        public AddExpenseReportFactCommand(ExpenseReportFact expenseReportFact)
        {
            ExpenseReportFact = expenseReportFact;
        }

        public ExpenseReportFact ExpenseReportFact { get; private set; }
    }
}