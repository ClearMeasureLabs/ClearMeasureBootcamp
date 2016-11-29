using System.Collections.Generic;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;

namespace ClearMeasure.Bootcamp.Core.Services.Impl
{
    public class WorkflowFacilitator : IWorkflowFacilitator
    {
        public IStateCommand[] GetValidStateCommands(ExecuteTransitionCommand transitionCommand)
        {
            var commands = new List<IStateCommand>(
                GetAllStateCommands());
            commands.RemoveAll(delegate(IStateCommand obj) { return !obj.IsValid(transitionCommand); });

            return commands.ToArray();
        }

        public virtual IStateCommand[] GetAllStateCommands()
        {
            var commands = new List<IStateCommand>();
            commands.Add(new DraftingCommand());
            commands.Add(new DraftToSubmittedCommand());
            commands.Add(new DraftToCancelledCommand());
            commands.Add(new SubmittedToApprovedCommand());
            return commands.ToArray();
        }
    }
}