using System;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;
using NUnit.Framework;
using Should;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess.Mappings
{
    [TestFixture]
    public class ExpenseReportFactMappingTester
    {
        [Test]
        public void ShouldAddFact()
        {
            new DatabaseTester().Clean();

            var creator = new Employee("1", "1", "1", "1");
            var assignee = new Employee("2", "2", "2", "2");
            var expenseReport = new ExpenseReport
            {
                Submitter = creator,
                Approver = assignee,
                Title = "foo",
                Description = "bar",
                Number = "123"
            };
            expenseReport.ChangeStatus(ExpenseReportStatus.Approved);
            ExpenseReportFact expenseReportFact = new ExpenseReportFact(expenseReport,new DateTime(2012,1,1));


            using (ISession session = DataContext.GetTransactedSession())
            {
                //session.SaveOrUpdate(expenseReport);
                session.SaveOrUpdate(expenseReportFact);
                session.Transaction.Commit();
            }

            using (ISession session = DataContext.GetTransactedSession())
            {
                var reportFact = session.Load<ExpenseReportFact>(expenseReportFact.Id);
                reportFact.Total.ShouldEqual(expenseReportFact.Total);
                reportFact.TimeStamp.ShouldEqual(expenseReportFact.TimeStamp);
                reportFact.Number.ShouldEqual(expenseReportFact.Number);
                reportFact.Status.ShouldEqual(expenseReportFact.Status);
                reportFact.Submitter.ShouldEqual(expenseReportFact.Submitter);
                reportFact.Approver.ShouldEqual(expenseReportFact.Approver);
            }
        }
    }
}