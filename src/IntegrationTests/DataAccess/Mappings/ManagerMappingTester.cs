using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;
using NUnit.Framework;
using Should;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess.Mappings
{
    [TestFixture]
    public class ManagerMappingTester
    {
        [Test]
        public void ShouldPersist()
        {
            new DatabaseTester().Clean();

            var one = new Manager("manager1", "Manager", "One", "Email");
            Employee adminAssistant = new Employee("Assistant1", "Assistant", "One", "Email2");
            Employee adminAssistant2 = new Employee("Assistant2", "Assistant", "Two", "Email3");
            Employee adminAssistant3 = new Employee("Assistant3", "Assistant", "Three", "Email4");
            one.AdminAssistant = adminAssistant;
            using (ISession session = DataContext.GetTransactedSession())
            {
                session.Save(one);
                session.Save(adminAssistant);
                session.Save(adminAssistant2);
                session.Save(adminAssistant3);
                session.Transaction.Commit();
            }

            Manager rehydratedEmployee;
            using (ISession session = DataContext.GetTransactedSession())
            {
                rehydratedEmployee = session.Load<Manager>(one.Id);
            }

            rehydratedEmployee.UserName.ShouldEqual(one.UserName);
            rehydratedEmployee.FirstName.ShouldEqual(one.FirstName);
            rehydratedEmployee.LastName.ShouldEqual(one.LastName);
            rehydratedEmployee.EmailAddress.ShouldEqual(one.EmailAddress);
            rehydratedEmployee.AdminAssistant.ShouldEqual(adminAssistant);

        }
    }
}