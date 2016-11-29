using System;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class DraftToCancelledCommand : StateCommandBase
    {

        public override string TransitionVerbPastTense
        {
            get { return "Cancelled"; }
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Cancel"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Cancelled;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            return currentUser == report.Submitter;
        }

        protected override void preExecute(ExecuteTransitionCommand transitionCommand)
        {
            transitionCommand.Report.LastCancelled = transitionCommand.CurrentDate;
        }
    }
}