namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class DraftingCommand : StateCommandBase
    {
        public DraftingCommand()
            : base()
        {
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Save"; }
        }

        public override string TransitionVerbPastTense
        {
            get { return "Saved"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            return currentUser == report.Submitter;
        }
    }
}

