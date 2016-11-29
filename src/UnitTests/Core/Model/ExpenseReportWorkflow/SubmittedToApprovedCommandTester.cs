using System;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.ExpenseReportWorkflow
{
    [TestFixture]
    public class SubmittedToApprovedCommandTester : StateCommandBaseTester
    {
        [Test]
        public void ShouldNotBeValidInWrongStatus()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Draft;
            var employee = new Employee();
            report.Approver = employee;

            var command = new SubmittedToApprovedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldNotBeValidWithWrongEmployee()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            var approver = new Employee();
            report.Approver = approver;

            var command = new SubmittedToApprovedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldNotBeValidWithWrongApprover()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Approver = employee;
            var differentEmployee = new Employee();
           
            var command = new SubmittedToApprovedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, differentEmployee, new DateTime())), Is.False);
        }

        [Test]
        public void ShouldBeValid()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Approver = employee;

            var command = new SubmittedToApprovedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, employee, new DateTime())), Is.True);
        }

        [Test]
        public void ShouldBeValidWithOnBehalfApprover()
        {
            var report = new ExpenseReport();
            report.Status = ExpenseReportStatus.Submitted;
            var manager = new Manager();
            var assistant = new Employee();
            manager.AdminAssistant = assistant;
            report.Approver = manager;

            var command = new SubmittedToApprovedCommand();
            Assert.That(command.IsValid(new ExecuteTransitionCommand(report, null, assistant, new DateTime())), Is.True);
        }

        [Test]
        public void ShouldTransitionStateProperly()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Approver = employee;

            var command = new SubmittedToApprovedCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, new DateTime()));

            Assert.That(report.Status, Is.EqualTo(ExpenseReportStatus.Approved));
        }

        [Test]
        public void ShouldSetLastApprovedEachTime()
        {
            var report = new ExpenseReport();
            report.Number = "123";
            report.Status = ExpenseReportStatus.Submitted;
            var employee = new Employee();
            report.Approver = employee;

            var approvedDate = new DateTime(2015,01,01);

            var command = new SubmittedToApprovedCommand();
            command.Execute(new ExecuteTransitionCommand(report, null, employee, approvedDate));

            Assert.That(report.LastApproved, Is.EqualTo(approvedDate));

            var approvedDate2 = new DateTime(2015, 02, 02);

            var command2 = new SubmittedToApprovedCommand();
            command2.Execute(new ExecuteTransitionCommand(report, null, employee, approvedDate2));

            Assert.That(report.LastApproved, Is.Not.EqualTo(approvedDate));
            Assert.That(report.LastApproved, Is.EqualTo(approvedDate2));
        }
        
        protected override StateCommandBase GetStateCommand(ExpenseReport order, Employee employee)
        {
            return new SubmittedToApprovedCommand();
        }
    }
}