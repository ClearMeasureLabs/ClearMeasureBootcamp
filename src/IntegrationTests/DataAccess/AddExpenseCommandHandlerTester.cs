using System;
using System.Linq;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.MutlipleExpenses;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using Should;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess
{
    [TestFixture]
    public class AddExpenseCommandHandlerTester
    {
        [Test]
        public void ShouldCreateExpense()
        {
            new DatabaseTester().Clean();
            var creator = new Employee("1", "1", "1", "1");
            var assignee = new Employee("2", "2", "2", "2");
            var report = new ExpenseReport
            {
                Submitter = creator,
                Approver = assignee,
                Title = "foo",
                Description = "bar",
                Number = "123"
            };
            var request = new AddExpenseCommand
            {
                Report = report,
                CurrentUser = creator,
                Amount = 100.00m,
                Description = "foo",
                CurrentDate = new DateTime(2000, 1,1 )
            };

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(assignee);
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            bus.Send(request);

            ExpenseReport loadedReport;
            using (ISession session = DataContext.GetTransactedSession())
            {
                loadedReport = session.Load<ExpenseReport>(report.Id);
            }
            loadedReport.GetExpenses().Count().ShouldEqual(1);
            loadedReport.GetExpenses()[0].Amount.ShouldEqual(100);
            loadedReport.GetExpenses()[0].Description.ShouldEqual("foo");

        }

    }
}
