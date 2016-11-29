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
    public class DraftingCommandTester : StateCommandBaseTester
    {
        protected override StateCommandBase GetStateCommand(ExpenseReport order, Employee employee)
        {
            return new DraftingCommand();
        }

        [Test]
        public void ShouldBeValid()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftingCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.True);
        }

        [Test]
        public void ShouldNotBeValidInWrongStatus()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftingCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldNotBeValidWithWrongEmployee()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftingCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, new Employee(), new DateTime())), Is.False);
        }

        [Test]
        public void ShouldTransitionStateProperly()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftingCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, new DateTime()));

            Assert.That(report.Status, Is.EqualTo(ExpenseReportStatus.Draft));
        }

    }
}