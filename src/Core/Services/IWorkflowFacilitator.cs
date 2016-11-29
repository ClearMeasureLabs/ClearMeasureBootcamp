using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IWorkflowFacilitator
	{
		IStateCommand[] GetValidStateCommands(ExecuteTransitionCommand transitionCommand);
	}
}