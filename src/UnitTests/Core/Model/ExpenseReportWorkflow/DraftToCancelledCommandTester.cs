using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Services;
using NUnit.Framework;
using Rhino.Mocks;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.ExpenseReportWorkflow
{
    public class DraftToCancelledCommandTester : StateCommandBaseTester
    {
        protected override StateCommandBase GetStateCommand(ExpenseReport order, Employee employee)
        {
            return new DraftToCancelledCommand();
        }

        [Test]
        public void ShouldBeValid()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftToCancelledCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.True);
        }

        [Test]
        public void ShouldNotBeValidInWrongStatus()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var command = new DraftToCancelledCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldSetLastCancelledOnExecute()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Cancelled;
            DateTime cancelledDate = new DateTime(2015,6,30);
            var employee = new Employee();
            report.Submitter = employee;

            var command = new DraftToCancelledCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, cancelledDate));
            Assert.That(report.LastCancelled, Is.EqualTo(cancelledDate));
        }

        [Test]
        public void ShouldNotBeValidWithWrongEmployee()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Cancelled;
            var employee = new Employee();
            report.Approver = employee;

            var command = new DraftToCancelledCommand();
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

            var command = new DraftToCancelledCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, new DateTime()));

            Assert.That(report.Status, Is.EqualTo(ExpenseReportStatus.Cancelled));
        }
    }
}
