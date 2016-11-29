using ClearMeasure.Bootcamp.Core.Features.Workflow;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
	public interface IStateCommand
	{
		bool IsValid(ExecuteTransitionCommand transitionCommand);
		ExecuteTransitionResult Execute(ExecuteTransitionCommand transitionCommand);

		string TransitionVerbPresentTense { get; }
		bool Matches(string commandName);
		ExpenseReportStatus GetBeginStatus();
	}
}