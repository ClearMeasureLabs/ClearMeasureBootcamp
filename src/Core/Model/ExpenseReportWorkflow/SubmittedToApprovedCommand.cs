using System.Configuration;
using ClearMeasure.Bootcamp.Core.Features.Workflow;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class SubmittedToApprovedCommand : StateCommandBase
    {
        public SubmittedToApprovedCommand()
            : base()
        {
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Approve"; }
        }

        public override string TransitionVerbPastTense
        {
            get { return "Approved"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Submitted;
        }

        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Approved;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            if (report.Approver == null) return false;
            return report.Approver.CanActOnBehalf(currentUser);
        }

        protected override void preExecute(ExecuteTransitionCommand transitionCommand)
        {
            transitionCommand.Report.LastApproved = transitionCommand.CurrentDate;
        }
    }
}