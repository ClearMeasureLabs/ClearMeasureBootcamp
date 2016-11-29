using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess;
using NHibernate;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess.Mappings
{
    [TestFixture]
    public class RoleMappingTester
    {
        [Test]
        public void ShouldPersist()
        {
            new DatabaseTester().Clean();

            var role = new Role("foo");
            using (ISession session = DataContext.GetTransactedSession())
            {
                session.Save(role);
                session.Transaction.Commit();
            }

            Role rehydratedRole;
            using (ISession session2 = DataContext.GetTransactedSession())
            {
                rehydratedRole = session2.Load<Role>(role.Id);
            }

            rehydratedRole.Name.ShouldEqual("foo");
        }
    }
}