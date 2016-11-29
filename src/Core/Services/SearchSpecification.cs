using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public class SearchSpecification
	{
		private ExpenseReportStatus _status;
		private Employee _approver;
		private Employee _submitter;

		public void MatchStatus(ExpenseReportStatus status)
		{
			_status = status;
		}

		public void MatchApprover(Employee assignee)
		{
			_approver = assignee;
		}

		public void MatchSubmitter(Employee creator)
		{
			_submitter = creator;
		}

		public ExpenseReportStatus Status
		{
			get { return _status; }
		}

		public Employee Approver
		{
			get { return _approver; }
		}

		public Employee Submitter
		{
			get { return _submitter; }
		}
	}
}