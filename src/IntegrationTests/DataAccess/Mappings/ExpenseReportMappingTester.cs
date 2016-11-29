using System;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using FluentNHibernate.Utils;
using NHibernate;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess.Mappings
{
    [TestFixture]
    public class ExpenseReportMappingTester
    {
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
            report.AddAuditEntry(new AuditEntry(creator, DateTime.Now, ExpenseReportStatus.Submitted,
                                                  ExpenseReportStatus.Approved));

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(assignee);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            ExpenseReport rehydratedExpenseReport;
            using (ISession session2 = DataContext.GetTransactedSession())
            {
                rehydratedExpenseReport = session2.Load<ExpenseReport>(report.Id);
            }

            var x = report.GetAuditEntries()[0];
            var y = rehydratedExpenseReport.GetAuditEntries()[0];
            Assert.That(x.EndStatus, Is.EqualTo(y.EndStatus));
        }

        [Test]
        public void ShouldSaveExpenses()
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
            report.AddExpense("howdy", 123.45m);

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(assignee);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            ExpenseReport rehydratedExpenseReport;
            using (ISession session2 = DataContext.GetTransactedSession())
            {
                rehydratedExpenseReport = session2.Load<ExpenseReport>(report.Id);
            }

            Expense x = report.GetExpenses()[0];
            Expense y = rehydratedExpenseReport.GetExpenses()[0];
            Assert.That(x.Description, Is.EqualTo(y.Description));
            Assert.That(x.Amount, Is.EqualTo(y.Amount));
        }

        [Test]
        public void ShouldSaveExpenseReportWithNewProperties()
        {
            // Clean the database
            new DatabaseTester().Clean();
            // Make employees
            var creator = new Employee("1", "1", "1", "1");
            var assignee = new Employee("2", "2", "2", "2");
            DateTime testTime = new DateTime(2015, 1, 1);
            // popluate ExpenseReport
            var report = new ExpenseReport
            {
                Submitter = creator,
                Approver = assignee,
                Title = "TestExpenseReport",
                Description = "This is an expense report test",
                Number = "123",
                MilesDriven = 100,
                Created = testTime,
                FirstSubmitted = testTime,
                LastSubmitted = testTime,
                LastWithdrawn = testTime,
                LastCancelled = testTime,
                LastApproved = testTime,
                LastDeclined = testTime,
                Total = 100.25m
            };

            report.ChangeStatus(ExpenseReportStatus.Approved);
            report.AddAuditEntry(new AuditEntry(creator, DateTime.Now, ExpenseReportStatus.Submitted,
                                                  ExpenseReportStatus.Approved));
            
            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(assignee);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            ExpenseReport pulledExpenseReport;
            using (ISession session = DataContext.GetTransactedSession())
            {
                pulledExpenseReport = session.Load<ExpenseReport>(report.Id);
            }

            Assert.That(pulledExpenseReport.MilesDriven, Is.EqualTo(report.MilesDriven));
            Assert.That(pulledExpenseReport.Created, Is.EqualTo(report.Created));
            Assert.That(pulledExpenseReport.FirstSubmitted, Is.EqualTo(report.FirstSubmitted));
            Assert.That(pulledExpenseReport.LastSubmitted, Is.EqualTo(report.LastSubmitted));
            Assert.That(pulledExpenseReport.LastWithdrawn, Is.EqualTo(report.LastWithdrawn));
            Assert.That(pulledExpenseReport.LastCancelled, Is.EqualTo(report.LastCancelled));
            Assert.That(pulledExpenseReport.LastApproved, Is.EqualTo(report.LastApproved));
            Assert.That(pulledExpenseReport.LastDeclined, Is.EqualTo(report.LastDeclined));
            Assert.That(pulledExpenseReport.Total, Is.EqualTo(report.Total));
        }
    }
}