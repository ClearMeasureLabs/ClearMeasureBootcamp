using System;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportAnalytics;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;

namespace ClearMeasure.Bootcamp.Core.Features.Workflow
{
    public class ExecuteTransitionCommandHandler : IRequestHandler<ExecuteTransitionCommand, ExecuteTransitionResult>
    {
        private readonly Bus _bus;

        public ExecuteTransitionCommandHandler(Bus bus)
        {
            _bus = bus;
        }

        public ExecuteTransitionResult Handle(ExecuteTransitionCommand command)
        {
            IStateCommand[] commands = new WorkflowFacilitator().GetValidStateCommands(command);
            IStateCommand matchingCommand =
                Array.Find(commands, delegate(IStateCommand obj) { return obj.Matches(command.Command); });

            matchingCommand.Execute(command);

            _bus.Send(new ExpenseReportSaveCommand {ExpenseReport = command.Report});
            _bus.Send(new AddExpenseReportFactCommand(new ExpenseReportFact(command.Report, command.CurrentDate)));

            return new ExecuteTransitionResult
            {
                NewStatus = command.Report.Status.FriendlyName,
                NextStep = NextStep.Edit
            };
        }
    }
}