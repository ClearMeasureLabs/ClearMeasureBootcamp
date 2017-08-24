using System;
using System.Diagnostics;
using System.Linq;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.IntegrationTests.DataAccess;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using Should;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.Core.Features.Workflow
{
    [TestFixture]
    public class ExecuteTransitionCommandHandlerIntegratedTester
    {
        [Test]
        public async void ShouldExecuteDraftTransition()
        {
            new DatabaseTester().Clean();

            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee("jpalermo", "Jeffrey", "Palermo", "jeffrey @ clear dash measure.com");
            report.Submitter = employee;
            report.Approver = employee;

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(employee);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            var command = new ExecuteTransitionCommand(report, "Save", employee, new DateTime(2001, 1, 1));

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();

            ExecuteTransitionResult result = bus.Send(command);
            result.NewStatus.ShouldEqual("Drafting");
        }


        [Test]
        public void ShouldPersistExportReportFact()
        {
            new DatabaseTester().Clean();
            var employee = new Employee("somethingelse", "Jeffrey", "Palermo", "jeffrey @ clear dash measure.com");
            employee.Id = Guid.NewGuid();
            var report = new ExpenseReport
            {
                Number = "123",
                Status = ExpenseReportStatus.Draft,
                Submitter = employee
            };

            DateTime setDate = new DateTime(2015, 1, 1);
            ExpenseReportFact expenseReportFact = new ExpenseReportFact(report, setDate);

            var command = new AddExpenseReportFactCommand(expenseReportFact);

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();

            bus.Send(command);

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.Save(expenseReportFact);
                session.Transaction.Commit();
            }

            ExpenseReportFact reHydratedExpenseReportFact;

            using (ISession session = DataContext.GetTransactedSession())
            {
                reHydratedExpenseReportFact = session.Load<ExpenseReportFact>(expenseReportFact.Id);
            }

            reHydratedExpenseReportFact.Approver.ShouldEqual(expenseReportFact.Approver);
            reHydratedExpenseReportFact.Number.ShouldEqual(expenseReportFact.Number);
            reHydratedExpenseReportFact.Status.ShouldEqual(expenseReportFact.Status);
            reHydratedExpenseReportFact.Submitter.ShouldEqual(expenseReportFact.Submitter);
            reHydratedExpenseReportFact.TimeStamp.ShouldEqual(expenseReportFact.TimeStamp);
            reHydratedExpenseReportFact.Total.ShouldEqual(expenseReportFact.Total);
        }

        [Test]
        public void sample()
        {
            var matchingProcess = Process.GetProcessesByName("iisexpress").FirstOrDefault();
            if (matchingProcess != null && matchingProcess.StartInfo.Arguments.Contains("43507"))
            {
                matchingProcess.Kill();
            }
        }
}
}