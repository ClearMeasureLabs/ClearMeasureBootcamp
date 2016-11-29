using System;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public abstract class StateCommandBase : IStateCommand
    {
        protected StateCommandBase()
        {
        }

        public abstract string TransitionVerbPastTense { get; }

        public abstract ExpenseReportStatus GetBeginStatus();
        public abstract string TransitionVerbPresentTense { get; }

        public bool IsValid(ExecuteTransitionCommand transitionCommand)
        {
            bool beginStatusMatches = transitionCommand.Report.Status.Equals(GetBeginStatus());
            bool currentUserIsCorrectRole = userCanExecute(transitionCommand.CurrentUser, transitionCommand.Report);
            return beginStatusMatches && currentUserIsCorrectRole;
        }

        public ExecuteTransitionResult Execute(ExecuteTransitionCommand transitionCommand)
        {
            preExecute(transitionCommand);
            string currentUserFullName = transitionCommand.CurrentUser.GetFullName();
            transitionCommand.Report.ChangeStatus(transitionCommand.CurrentUser, transitionCommand.CurrentDate, GetBeginStatus(), GetEndStatus());

            string loweredTransitionVerb = TransitionVerbPastTense.ToLower();
            string reportNumber = transitionCommand.Report.Number;
            string message = string.Format("You have {0} expense report {1}", loweredTransitionVerb, reportNumber);
            string debugMessage = string.Format("{0} has {1} expense report {2}", currentUserFullName, loweredTransitionVerb,
                reportNumber);

            return new ExecuteTransitionResult {NewStatus = GetEndStatus().FriendlyName
                , NextStep = NextStep.Edit, Action = debugMessage, Message = message };
        }

        public bool Matches(string commandName)
        {
            return TransitionVerbPresentTense == commandName;
        }

        protected abstract ExpenseReportStatus GetEndStatus();
        protected abstract bool userCanExecute(Employee currentUser, ExpenseReport report);

        protected virtual void preExecute(ExecuteTransitionCommand transitionCommand)
        {
        }
    }
}