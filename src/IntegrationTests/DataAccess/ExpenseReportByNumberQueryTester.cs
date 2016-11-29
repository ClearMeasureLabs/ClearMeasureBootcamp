using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ExpenseReportByNumberQueryTester
    {
        [Test]
        public void ShouldGetByNumber()
        {
            new DatabaseTester().Clean();

            var creator = new Employee("1", "1", "1", "1");
            var order1 = new ExpenseReport();
            order1.Submitter = creator;
            order1.Number = "123";
            var report = new ExpenseReport();
            report.Submitter = creator;
            report.Number = "456";

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator);
                session.SaveOrUpdate(order1);
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();

            ExpenseReport order123 = bus.Send(new ExpenseReportByNumberQuery {ExpenseReportNumber = "123"}).Result;
            ExpenseReport order456 = bus.Send(new ExpenseReportByNumberQuery {ExpenseReportNumber = "456"}).Result;

            Assert.That(order123.Id, Is.EqualTo(order1.Id));
            Assert.That(order456.Id, Is.EqualTo(report.Id));
        }

    }
}