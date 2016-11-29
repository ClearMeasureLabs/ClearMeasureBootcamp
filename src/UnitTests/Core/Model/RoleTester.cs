using System;
using ClearMeasure.Bootcamp.Core.Model;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model
{
    [TestFixture]
    public class RoleTester
    {
        [Test]
        public void Role_defaults_properly()
        {
            var role = new Role();

            Assert.That(role.Name, Is.Null);
            Assert.That(role.Id, Is.EqualTo(Guid.Empty));

            var role2 = new Role("roleName");

            Assert.That(role2.Name, Is.EqualTo("roleName"));
        }
    }
}