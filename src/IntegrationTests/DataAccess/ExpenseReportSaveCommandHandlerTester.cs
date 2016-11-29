using System;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using Should;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ExpenseReportSaveCommandHandlerTester
    {
        [Test]
        public void ShouldSave()
        {
            new DatabaseTester().Clean();

            var creator = new Employee("1", "1", "1", "1");
            var assignee = new Employee("2", "2", "2", "2");
            var report = new ExpenseReport();
            report.Submitter = creator;
            report.Approver = assignee;
            report.Title = "foo";
            report.Description = "bar";
            report.ChangeStatus(ExpenseReportStatus.Approved);
            report.Number = "123";

            using(ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(assignee);
                session.Transaction.Commit();
            }

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            bus.Send(new ExpenseReportSaveCommand {ExpenseReport = report});

            ExpenseReport rehydratedReport;
            using(ISession session2 = DataContext.GetTransactedSession())
            {
                rehydratedReport = session2.Load<ExpenseReport>(report.Id);
            }

            rehydratedReport.Id.ShouldEqual(report.Id);
            rehydratedReport.Submitter.Id.ShouldEqual(report.Submitter.Id);
            rehydratedReport.Approver.Id.ShouldEqual(report.Approver.Id);
            rehydratedReport.Title.ShouldEqual(report.Title);
            rehydratedReport.Description.ShouldEqual(report.Description);
            rehydratedReport.Status.ShouldEqual(report.Status);
            
            rehydratedReport.Number.ShouldEqual(report.Number);
        }
        
        [Test]
        public void ShouldSaveAuditEntries()
        {
            new DatabaseTester().Clean();

            var creator = new Employee("1", "1", "1", "1");
            var assignee = new Employee("2", "2", "2", "2");
            var report = new ExpenseReport();
            report.Submitter = creator;
            report.Approver = assignee;
            report.Title = "foo";
            report.Description = "bar";
            report.ChangeStatus(ExpenseReportStatus.Approved);
            report.Number = "123";
            report.AddAuditEntry(new AuditEntry(creator, DateTime.Now,ExpenseReportStatus.Submitted,
                                                  ExpenseReportStatus.Approved));

            using(ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(assignee);
                session.Transaction.Commit();   
            }

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            bus.Send(new ExpenseReportSaveCommand { ExpenseReport = report });

            ExpenseReport rehydratedReport;
            using(ISession session2 = DataContext.GetTransactedSession())
            {
                rehydratedReport = session2.Load<ExpenseReport>(report.Id);
            }

            var x = report.GetAuditEntries()[0];
            var y = rehydratedReport.GetAuditEntries()[0];
            y.EndStatus.ShouldEqual(x.EndStatus);
            y.BeginStatus.ShouldEqual(x.BeginStatus);
            y.EmployeeName.ShouldEqual(x.EmployeeName);
        }

    }
}