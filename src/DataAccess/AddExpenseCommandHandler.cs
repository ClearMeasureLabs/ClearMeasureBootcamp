using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.MutlipleExpenses;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;

namespace ClearMeasure.Bootcamp.DataAccess
{
    public class AddExpenseCommandHandler : IRequestHandler<AddExpenseCommand, AddExpenseResult>
    {
        private readonly Bus _bus;

        public AddExpenseCommandHandler(Bus bus)
        {
            _bus = bus;
        }

        public AddExpenseResult Handle(AddExpenseCommand request)
        {
            var entry = new AuditEntry()
            {
                Employee = request.CurrentUser,
                Date = request.CurrentDate,
                EmployeeName = request.CurrentUser.FirstName,
                BeginStatus = request.Report.Status
            };

            request.Report.AddAuditEntry(entry);
            request.Report.AddExpense(request.Description, request.Amount);
            _bus.Send(new ExpenseReportSaveCommand() {ExpenseReport = request.Report});

            return new AddExpenseResult();
        }
    }
}