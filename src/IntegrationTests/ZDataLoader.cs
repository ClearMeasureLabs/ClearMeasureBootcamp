using System;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.IntegrationTests.DataAccess;
using NHibernate;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.None)]
namespace ClearMeasure.Bootcamp.IntegrationTests
{
    [TestFixture, Explicit]
    public class ZDataLoader
    {
        [Test, Category("DataLoader")]
        public void PopulateDatabase()
        {
            new DatabaseTester().Clean();
            ISession session = DataContext.GetTransactedSession();


            //Trainer1
            var jpalermo = new Employee("jpalermo", "Jeffrey", "Palermo", "jeffrey@clear-measure.com");
            session.SaveOrUpdate(jpalermo);

            //Person 1
            
            //Person 2
            
            //Person 3
            var damian = new Employee("damian", "Damian", "Brady", "damian@Gmail.com");
            session.SaveOrUpdate(damian);
            
            //Person 4
            
            //Person 5

            //Person 6
            var paul = new Employee("paul", "Paul", "Stovell", "Paul@myemail.com");
            session.SaveOrUpdate(paul);
            
            //Person 7
            
            //Person 8
            
            //Person 9

            //Person 10

            //Person 11

            //Person 12

            //Person 13

            var hsimpson = new Employee("hsimpson", "Homer", "Simpson", "homer@simpson.com");
            session.SaveOrUpdate(hsimpson);

            foreach (ExpenseReportStatus status in ExpenseReportStatus.GetAllItems())
            {
                var report = new ExpenseReport();
                report.Number = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                report.Submitter = jpalermo;
                report.Approver = jpalermo;
                report.Status = status;
                report.Title = "Expense report starting in status " + status;
                report.Description = "Foo, foo, foo, foo " + status;
                new DateTime(2000, 1, 1, 8, 0, 0);
                report.ChangeStatus(ExpenseReportStatus.Draft);
                report.ChangeStatus(ExpenseReportStatus.Submitted);
                report.ChangeStatus(ExpenseReportStatus.Approved);

                session.SaveOrUpdate(report);
            }

            var order2 = new ExpenseReport();
            order2.Number = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            order2.Submitter = jpalermo;
            order2.Approver = jpalermo;
            order2.Status = ExpenseReportStatus.Approved;
            order2.Title = "Expense report starting in status ";
            order2.Description = "Foo, foo, foo, foo ";
            new DateTime(2000, 1, 1, 8, 0, 0);
            session.SaveOrUpdate(order2);

            session.Transaction.Commit();
            session.Dispose();
        }
    }
}