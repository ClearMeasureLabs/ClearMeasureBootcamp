using System;
using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Features.Workflow
{
    public class ExecuteTransitionCommand : IRequest<ExecuteTransitionResult>
    {
        public ExpenseReport Report { get; set; }
        public string Command { get; set; }
        public Employee CurrentUser { get; set; }
        public DateTime CurrentDate { get; set; }

        public ExecuteTransitionCommand(ExpenseReport report, string command, Employee currentUser, DateTime currentDate)
        {
            CurrentDate = currentDate;
            Report = report;
            Command = command;
            CurrentUser = currentUser;
        }

        public ExecuteTransitionCommand()
        {
        }
    }
}