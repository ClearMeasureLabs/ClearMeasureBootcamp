using System;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.ExpenseReportWorkflow
{
    [TestFixture]
    public class DraftToSubmittedCommandTester : StateCommandBaseTester
    {
        protected override StateCommandBase GetStateCommand(ExpenseReport order, Employee employee)
        {
            return new DraftToSubmittedCommand();
        }

        [Test]
        public void ShouldBeValid()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftToSubmittedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.True);
        }

        [Test]
        public void ShouldNotBeValidInWrongStatus()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var command = new DraftToSubmittedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldNotBeValidWithWrongEmployee()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Approver = employee;

            var command = new DraftToSubmittedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldTransitionStateProperly()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var command = new DraftToSubmittedCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, new DateTime()));

            Assert.That(report.Status, Is.EqualTo(ExpenseReportStatus.Submitted));
        }

        [Test]
        public void ShouldSetFirstSubmittedOnFirstSubmission()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var firstSubmitDate = new DateTime();

            var command = new DraftToSubmittedCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, firstSubmitDate));

            Assert.That(report.FirstSubmitted, Is.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.EqualTo(firstSubmitDate));
        }

        [Test]
        public void ShouldSetLastSubmittedOnEachSubmission()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var firstSubmitDate = new DateTime(2015,01,01);

            var command = new DraftToSubmittedCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, firstSubmitDate));

            Assert.That(report.FirstSubmitted, Is.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.EqualTo(firstSubmitDate));

            var secondSubmitDate = new DateTime(2015,02,02);

            var command2 = new DraftToSubmittedCommand();
            command2.Execute(new ExecuteTransitionCommand(report, null, employee, secondSubmitDate));

            Assert.That(report.FirstSubmitted, Is.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.Not.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.EqualTo(secondSubmitDate));

            var thirdSubmitDate = new DateTime(2015,03,03);

            var command3 = new DraftToSubmittedCommand();
            command3.Execute(new ExecuteTransitionCommand(report, null, employee, thirdSubmitDate));

            Assert.That(report.FirstSubmitted, Is.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.Not.EqualTo(firstSubmitDate));
            Assert.That(report.LastSubmitted, Is.Not.EqualTo(secondSubmitDate));
            Assert.That(report.LastSubmitted, Is.EqualTo(thirdSubmitDate));
        }
    }
}