using System;
using System.Collections.Generic;
using System.Data;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using ClearMeasure.Bootcamp.IntegrationTests.DataAccess;
using ClearMeasure.Bootcamp.UI.DependencyResolution;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using StructureMap;

namespace ClearMeasure.Bootcamp.IntegrationTests.Core.Features.Workflow
{
    [TestFixture]
    public class MostRecentExpenseReportFactViewIntegratedTester
    {
        [Test]
        public void ShouldReturnOnlyMostRecentExpenseReportFacts()
        {
            new DatabaseTester().Clean();

            Setup();

            using (ISession session = DataContext.GetTransactedSession())
            {
                var query = session.CreateSQLQuery("select count(1), status from MostRecentExpenseReportFactView group by status");
                var results = query.List();
                Dictionary<string, int> statuses = new Dictionary<string, int>();
                foreach (object[] fields in results)
                {
                    int count = (int) fields[0];
                    string status = (string) fields[1];
                    statuses[status] = count;
                }
                Assert.That(statuses["Approved"], Is.EqualTo(50));
                Assert.That(statuses["Drafting"], Is.EqualTo(25));
                Assert.That(statuses["Submitted"], Is.EqualTo(25));
            }

        }

        private static void Setup()
        {
            var employee = new Employee("jpalermo", "Jeffrey", "Palermo", "jeffrey @ clear dash measure.com");
            using (ISession session = DataContext.GetTransactedSession())
            {
                session.Save(employee);
                session.Transaction.Commit();
            }

            var startingDate = new DateTime(1974, 8, 4);
            for (int i = 0; i < 25; i++)
            {
                RunToDraft(new NumberGenerator().GenerateNumber(), employee, 13*i, startingDate.AddMinutes(i), "Save");
            }
            for (int i = 0; i < 25; i++)
            {
                RunToDraft(new NumberGenerator().GenerateNumber(), employee, 13*i, startingDate.AddMinutes(i), "Save", "Submit");
            }
            for (int i = 0; i < 25; i++)
            {
                RunToDraft(new NumberGenerator().GenerateNumber(), employee, 13*i, startingDate.AddMinutes(i), "Save", "Submit",
                    "Approve");
            }
            for (int i = 0; i < 25; i++)
            {
                RunToDraft(new NumberGenerator().GenerateNumber(), employee, 13*i, startingDate.AddMinutes(i), "Save", "Submit",
                    "Approve");
            }
        }

        private static void RunToDraft(string number, Employee employee, int total, DateTime startingDate, params string[] commandsToRun)
        {
            var report = new ExpenseReport();
            report.Number = number;
            report.Status = ExpenseReportStatus.Draft;
            report.Submitter = employee;
            report.Approver = employee;
            report.Total = total;

            using (ISession session = DataContext.GetTransactedSession())
            {
                session.SaveOrUpdate(report);
                session.Transaction.Commit();
            }

            IContainer container = DependencyRegistrarModule.EnsureDependenciesRegistered();
            var bus = container.GetInstance<Bus>();

            for (int j = 0; j < commandsToRun.Length; j++)
            {
                DateTime timestamp = startingDate.AddSeconds(j);
                var command = new ExecuteTransitionCommand(report, commandsToRun[j], employee, timestamp);
                bus.Send(command);
            }
        }
    }
}