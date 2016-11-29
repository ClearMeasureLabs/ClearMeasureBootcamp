using System.Linq;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.UI.Helpers.ActionFilters;
using ClearMeasure.Bootcamp.UI.Models;
using UI.Models;

namespace ClearMeasure.Bootcamp.UI.Controllers
{
    [AddUserMetaDataToViewData]
    [Authorize]
    public class ExpenseReportController : Controller
    {
        private readonly IExpenseReportBuilder _expenseReportBuilder;
        private readonly IUserSession _session;
        private readonly IWorkflowFacilitator _workflowFacilitator;
        private readonly Bus _bus;
        private readonly ICalendar _calendar;

        public ExpenseReportController(IExpenseReportBuilder expenseReportBuilder, IUserSession session,
            IWorkflowFacilitator workflowFacilitator, Bus bus, ICalendar calendar)
        {
            _expenseReportBuilder = expenseReportBuilder;
            _session = session;
            _workflowFacilitator = workflowFacilitator;
            _bus = bus;
            _calendar = calendar;
        }

        public ActionResult Manage(string id, EditMode mode)
        {
            Employee currentUser = _session.GetCurrentUser();

            ExpenseReport expenseReport;

            if (mode == EditMode.New)
            {
                expenseReport = _expenseReportBuilder.Build(currentUser);
                if (!string.IsNullOrEmpty(id))
                    expenseReport.Number = id;
            }
            else
            {
                expenseReport = _bus.Send(new ExpenseReportByNumberQuery {ExpenseReportNumber = id}).Result;
            }

            ExpenseReportManageModel model = CreateViewModel(mode, expenseReport);
            model.IsReadOnly = !_workflowFacilitator.GetValidStateCommands(new ExecuteTransitionCommand(expenseReport, null, currentUser, _calendar.GetCurrentTime())).Any();
            ViewBag.ExpenseReport = expenseReport;
            ViewBag.CurrentUser = currentUser;

            return View("Manage", model);
        }

        [HttpPost]
        public ActionResult Manage(ExpenseReportManageModel model, string command)
        {
            Employee currentUser = _session.GetCurrentUser();

            ExpenseReport expenseReport;

            if (model.Mode == EditMode.New)
                expenseReport = _expenseReportBuilder.Build(currentUser);
            else
                expenseReport = _bus.Send(new ExpenseReportByNumberQuery { ExpenseReportNumber = model.ExpenseReportNumber }).Result;

            if (!ModelState.IsValid)
            {
                ViewBag.ExpenseReport = expenseReport;
                ViewBag.CurrentUser = currentUser;
                return View("Manage", model);
            }

            Employee approver = _bus.Send(new EmployeeByUserNameQuery(model.ApproverUserName)).Result;
            Employee submitter = _bus.Send(new EmployeeByUserNameQuery(model.SubmitterUserName)).Result;

            expenseReport.Number = model.ExpenseReportNumber;
            expenseReport.Submitter = submitter;
            expenseReport.Approver = approver;
            expenseReport.Title = model.Title;
            expenseReport.Description = model.Description;
            expenseReport.Total = model.Total;

            var transitionCommand = new ExecuteTransitionCommand(expenseReport, command, currentUser, 
                _calendar.GetCurrentTime());

            ExecuteTransitionResult transitionResult = _bus.Send(transitionCommand);

            if (transitionResult.NextStep == NextStep.Dashboard)
                return Redirect("~/Home");
            
            return RedirectToAction("Manage", new {id = expenseReport.Number, mode = "edit"});
        }

        private ExpenseReportManageModel CreateViewModel(EditMode mode, ExpenseReport expenseReport)
        {
            var model = new ExpenseReportManageModel
            {
                ExpenseReport = expenseReport,
                Mode = mode,
                ExpenseReportNumber = expenseReport.Number,
                Status = expenseReport.FriendlyStatus,
                SubmitterUserName = expenseReport.Submitter.UserName,
                SubmitterFullName = expenseReport.Submitter.GetFullName(),
                ApproverUserName = expenseReport.Approver != null ? expenseReport.Approver.UserName : "",
                Title = expenseReport.Title,
                Description = expenseReport.Description,
                CanReassign = UserCanChangeAssignee(expenseReport),
                Total = expenseReport.Total,
            };
            return model;
        }

        public bool UserCanChangeAssignee(ExpenseReport expenseReport)
        {
            if (expenseReport.Status != ExpenseReportStatus.Draft)
            {
                return false;
            }

            return true;
        }
    }


}