using ClearMeasure.Bootcamp.Core.Model;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model
{
    [TestFixture]
    public class ExpenseReportStatusTester
    {
        [Test]
        public void ShouldListAllStatuses()
        {
            ExpenseReportStatus[] statuses = ExpenseReportStatus.GetAllItems();

            Assert.That(statuses.Length, Is.EqualTo(4));
            Assert.That(statuses[0], Is.EqualTo(ExpenseReportStatus.Draft));
            Assert.That(statuses[1], Is.EqualTo(ExpenseReportStatus.Submitted));
            Assert.That(statuses[2], Is.EqualTo(ExpenseReportStatus.Approved));
            Assert.That(statuses[3], Is.EqualTo(ExpenseReportStatus.Cancelled));
        }

        [Test]
        public void CanParseOnKey()
        {
            ExpenseReportStatus draft = ExpenseReportStatus.Parse("draft");
            Assert.That(draft, Is.EqualTo(ExpenseReportStatus.Draft));

            ExpenseReportStatus submitted = ExpenseReportStatus.Parse("submitted");
            Assert.That(submitted, Is.EqualTo(ExpenseReportStatus.Submitted));

        }
    }
}