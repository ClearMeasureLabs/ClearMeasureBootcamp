using System;
using ClearMeasure.Bootcamp.Core.Model;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model
{
    [TestFixture]
    public class ManagerTester
    {
        [Test]
        public void AdminAssistantShouldBeAbleToActOnBehalf()
        {
            var employee = new Employee();
            var adminAssistant = new Employee();
            var manager = new Manager();
            manager.AdminAssistant = adminAssistant;
            Assert.IsTrue(manager.CanActOnBehalf(adminAssistant));
            Assert.IsTrue(manager.CanActOnBehalf(manager));
            Assert.IsFalse(manager.CanActOnBehalf(employee));
        }
    }
}