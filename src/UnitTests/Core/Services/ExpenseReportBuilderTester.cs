using System;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Services
{
    [TestFixture]
    public class ExpenseReportBuilderTester
    {
        [Test]
        public void ShouldCorrectlyBuild()
        {
            var mocks = new MockRepository();
            var generator = mocks.StrictMock<INumberGenerator>();
            ICalendar calendar = new StubbedCalendar(new DateTime(2000, 1, 1));
            Expect.Call(generator.GenerateNumber()).Return("124");
            mocks.ReplayAll();

            var builder = new ExpenseReportBuilder(generator, calendar);
            var creator = new Employee();
            ExpenseReport expenseReport = builder.Build(creator);

            mocks.VerifyAll();
            Assert.That(expenseReport.Submitter, Is.EqualTo(creator));
            Assert.That(expenseReport.Number, Is.EqualTo("124"));
            Assert.That(expenseReport.Approver, Is.Null);
            Assert.That(expenseReport.Title, Is.Empty);
            Assert.That(expenseReport.Description, Is.Empty);
            Assert.That(expenseReport.Status, Is.EqualTo(ExpenseReportStatus.Draft));
            Assert.That(expenseReport.Created, Is.EqualTo(new DateTime(2000, 1, 1)));
        }
    }
}