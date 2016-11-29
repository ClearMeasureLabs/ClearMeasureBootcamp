using System;
using ClearMeasure.Bootcamp.Core.Model;
using NUnit.Framework;
using Should;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model
{
    [TestFixture]
    public class EmployeeTester
    {
        [Test]
        public void PropertiesShouldInitializeProperly()
        {
            var employee = new Employee();
            Assert.That(employee.Id, Is.EqualTo(Guid.Empty));
            Assert.That(employee.UserName, Is.EqualTo(null));
            Assert.That(employee.FirstName, Is.EqualTo(null));
            Assert.That(employee.LastName, Is.EqualTo(null));
            Assert.That(employee.EmailAddress, Is.EqualTo(null));
        }

        [Test]
        public void ToStringShouldReturnFullName()
        {
            var employee = new Employee("", "Joe", "Camel", "");
            Assert.That(employee.ToString(), Is.EqualTo("Joe Camel"));
        }

        [Test]
        public void PropertiesShouldGetAndSetProperly()
        {
            var employee = new Employee();
            Guid guid = Guid.NewGuid();

            employee.EmailAddress = "Test";
            employee.FirstName = "Bob";
            employee.Id = guid;
            employee.LastName = "Joe";
            employee.UserName = "bobjoe";

            Assert.That(employee.EmailAddress, Is.EqualTo("Test"));
            Assert.That(employee.FirstName, Is.EqualTo("Bob"));
            Assert.That(employee.Id, Is.EqualTo(guid));
            Assert.That(employee.LastName, Is.EqualTo("Joe"));
            Assert.That(employee.UserName, Is.EqualTo("bobjoe"));
        }

        [Test]
        public void ConstructorSetsFieldsProperly()
        {
            var employee = new Employee("bobjoe", "Bob", "Joe", "Test");

            Assert.That(employee.EmailAddress, Is.EqualTo("Test"));
            Assert.That(employee.FirstName, Is.EqualTo("Bob"));
            Assert.That(employee.LastName, Is.EqualTo("Joe"));
            Assert.That(employee.UserName, Is.EqualTo("bobjoe"));
        }

        [Test]
        public void FullNameShouldCombineFirstAndLastName()
        {
            var employee = new Employee();

            employee.FirstName = "Bob";
            employee.LastName = "Joe";

            Assert.That(employee.GetFullName(), Is.EqualTo("Bob Joe"));
        }

        [Test]
        public void ShouldCompareEmployeesByLastName()
        {
            var employee1 = new Employee("", "1", "1", "");
            var employee2 = new Employee("", "1", "2", "");

            Assert.That(employee1.CompareTo(employee2), Is.EqualTo(-1));
            Assert.That(employee1.CompareTo(employee1), Is.EqualTo(0));
            Assert.That(employee2.CompareTo(employee1), Is.EqualTo(1));
        }

        [Test]
        public void ShouldCompareEmployeesByLastNameThenFirstName()
        {
            var employee1 = new Employee("", "1", "1", "");
            var employee2 = new Employee("", "2", "1", "");

            Assert.That(employee1.CompareTo(employee2), Is.EqualTo(-1));
            Assert.That(employee1.CompareTo(employee1), Is.EqualTo(0));
            Assert.That(employee2.CompareTo(employee1), Is.EqualTo(1));
        }

        [Test]
        public void ShouldImplementEquality()
        {
            var employee1 = new Employee();
            var employee2 = new Employee();

            employee1.ShouldNotEqual(employee2);
            employee2.ShouldNotEqual(employee1);
            employee1.Id = Guid.NewGuid();
            employee2.Id = employee1.Id;
            employee1.ShouldEqual(employee2);
            employee2.ShouldEqual(employee1);
            (employee1 == employee2).ShouldBeTrue();
        }

        [Test]
        public void ShouldActOnBehalf()
        {
            var thisEmployee = new Employee();
            Assert.That(thisEmployee.CanActOnBehalf(thisEmployee), Is.EqualTo(true));
        }
        [Test]
        public void ShouldNotActOnBehalf()
        {
            var thisEmployee = new Employee();
            var thatEmployee = new Employee();
            Assert.That(thisEmployee.CanActOnBehalf(thatEmployee), Is.EqualTo(false));
        }
        public class EmployeeProxy : Employee
        {
        }
    }
}