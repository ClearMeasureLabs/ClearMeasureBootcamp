using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;

namespace ClearMeasure.Bootcamp.DataAccess
{
    public class AddExpenseReportFactHandler : IRequestHandler<AddExpenseReportFactCommand, AddExpenseReportFactResult>
    {
        public AddExpenseReportFactResult Handle(AddExpenseReportFactCommand command)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                session.Save(command.ExpenseReportFact);
                session.Transaction.Commit();
            }

            return new AddExpenseReportFactResult
            {
            };
        }
    }
}