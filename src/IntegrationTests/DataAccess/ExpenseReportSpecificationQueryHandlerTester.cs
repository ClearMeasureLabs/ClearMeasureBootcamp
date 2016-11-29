using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.SearchExpenseReports;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NUnit.Framework;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess
{
    [TestFixture]
    public class ExpenseReportSpecificationQueryHandlerTester
    {
        [Test]
        public void ShouldSearchBySpecificationWithAssignee()
        {
            new DatabaseTester().Clean();

            var employee1 = new Employee("1", "1", "1", "1");
            var employee2 = new Employee("2", "2", "2", "2");
            var order1 = new ExpenseReport();
            order1.Submitter = employee2;
            order1.Approver = employee1;
            order1.Number = "123";
            var order2 = new ExpenseReport();
            order2.Submitter = employee1;
            order2.Approver = employee2;
            order2.Number = "456";

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(employee1);
                session.SaveOrUpdate(employee2);
                session.SaveOrUpdate(order1);
                session.SaveOrUpdate(order2);
                session.Transaction.Commit();
            }

            var specification = new ExpenseReportSpecificationQuery {Approver = employee1};

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            MultipleResult<ExpenseReport> result = bus.Send(specification);
            ExpenseReport[] reports = result.Results;

            Assert.That(reports.Length, Is.EqualTo(1));
            Assert.That(reports[0].Id, Is.EqualTo(order1.Id));
        }

        [Test]
        public void ShouldSearchBySpecificationWithCreator()
        {
            new DatabaseTester().Clean();

            var creator1 = new Employee("1", "1", "1", "1");
            var creator2 = new Employee("2", "2", "2", "2");
            var order1 = new ExpenseReport();
            order1.Submitter = creator1;
            order1.Number = "123";
            var order2 = new ExpenseReport();
            order2.Submitter = creator2;
            order2.Number = "456";

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(creator1);
                session.SaveOrUpdate(creator2);
                session.SaveOrUpdate(order1);
                session.SaveOrUpdate(order2);
                session.Transaction.Commit();
            }

            var specification = new ExpenseReportSpecificationQuery{Submitter = creator1};

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            MultipleResult<ExpenseReport> result = bus.Send(specification);
            ExpenseReport[] reports = result.Results;

            Assert.That(reports.Length, Is.EqualTo(1));
            Assert.That(reports[0].Id, Is.EqualTo(order1.Id));
        }

        [Test]
        public void ShouldSearchBySpecificationWithFullSpecification()
        {
            new DatabaseTester().Clean();

            var employee1 = new Employee("1", "1", "1", "1");
            var employee2 = new Employee("2", "2", "2", "2");
            var order1 = new ExpenseReport();
            order1.Submitter = employee2;
            order1.Approver = employee1;
            order1.Number = "123";
            order1.Status = ExpenseReportStatus.Submitted;
            var order2 = new ExpenseReport();
            order2.Submitter = employee1;
            order2.Approver = employee2;
            order2.Number = "456";
            order2.Status = ExpenseReportStatus.Draft;

            using(ISession session = DataContext.GetTransactedSession())
            {

                session.SaveOrUpdate(employee1);
                session.SaveOrUpdate(employee2);
                session.SaveOrUpdate(order1);
                session.SaveOrUpdate(order2);
                session.Transaction.Commit();
            }

            var specification = new ExpenseReportSpecificationQuery()
            {
                Submitter = employee2,
                Approver = employee1,
                Status = ExpenseReportStatus.Submitted
            };

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            MultipleResult<ExpenseReport> result = bus.Send(specification);
            ExpenseReport[] reports = result.Results;

            Assert.That(reports.Length, Is.EqualTo(1));
            Assert.That(reports[0].Id, Is.EqualTo(order1.Id));
        }

        [Test]
        public void ShouldSearchBySpecificationWithStatus()
        {
            new DatabaseTester().Clean();

            var employee1 = new Employee("1", "1", "1", "1");
            var employee2 = new Employee("2", "2", "2", "2");
            var order1 = new ExpenseReport();
            order1.Submitter = employee2;
            order1.Approver = employee1;
            order1.Number = "123";
            order1.Status = ExpenseReportStatus.Submitted;
            var order2 = new ExpenseReport();
            order2.Submitter = employee1;
            order2.Approver = employee2;
            order2.Number = "456";
            order2.Status = ExpenseReportStatus.Draft;

            using (ISession session = DataContext.GetTransactedSession())
            {

                session.SaveOrUpdate(employee1);
                session.SaveOrUpdate(employee2);
                session.SaveOrUpdate(order1);
                session.SaveOrUpdate(order2);
                session.Transaction.Commit();
            }

            var specification = new ExpenseReportSpecificationQuery()
            {
                Status = ExpenseReportStatus.Submitted
            };

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();
            MultipleResult<ExpenseReport> result = bus.Send(specification);
            ExpenseReport[] reports = result.Results;

            Assert.That(reports.Length, Is.EqualTo(1));
            Assert.That(reports[0].Id, Is.EqualTo(order1.Id));
        }
    }
}