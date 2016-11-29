using System.Web;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;

namespace ClearMeasure.Bootcamp.UI.Services
{
    public class StateCommandVisitor : IStateCommandVisitor
    {
        private readonly ICalendar _calendar;
        private readonly IExpenseReportRepository _repository;
        private readonly IUserSession _session;


        public StateCommandVisitor() : this(DependencyResolver.Current.GetService<IExpenseReportRepository>(), new UserSession(), new Calendar())
        {
        }

        public StateCommandVisitor(IExpenseReportRepository repository, IUserSession session, ICalendar calendar)
        {
            _repository = repository;
            _session = session;
            _calendar = calendar;
        }

        public void Save(ExpenseReport expenseReport)
        {
            _repository.Save(expenseReport);
        }

        public void GoToEdit(ExpenseReport expenseReport)
        {
            redirect(string.Format("/ExpenseReport/Manage/{0}?mode=edit", expenseReport.Number));
        }

        public void GoToSearch(Employee creator, Employee assignee, ExpenseReportStatus status)
        {
            string url = string.Format("~/ExpenseReportSearch?accountManager={0}&approver={1}&status={2}",
                                       getNullSafeUserName(creator), getNullSafeUserName(assignee),
                                       getNullSafeStatusKey(status));
            HttpResponse response = HttpContext.Current.Response;
            response.Redirect(url);
        }

        public void SendMessage(string message)
        {
            _session.PushUserMessage(new FlashMessage(FlashMessage.MessageType.Message, message));
        }

        public void SendError(string message)
        {
            _session.PushUserMessage(new FlashMessage(FlashMessage.MessageType.Error, message));
        }

        public void GoToDashboard()
        {
            HttpContext.Current.Response.Redirect("~/Home");
        }

        protected virtual void redirect(string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Redirect(url);
        }

        private string getNullSafeStatusKey(ExpenseReportStatus status)
        {
            if (status == null)
            {
                return null;
            }

            return status.Key;
        }

        private string getNullSafeUserName(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            return employee.UserName;
        }
    }
}