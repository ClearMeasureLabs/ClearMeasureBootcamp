using System;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using ClearMeasure.Bootcamp.UI.Services;

namespace ClearMeasure.Bootcamp.UI.Helpers
{
    public class ExecuteCommandResult : ActionResult
    {
        private readonly ExpenseReport _expenseReport;
        private readonly string _command;
        private IWorkflowFacilitator _facilitator;
        private IStateCommandVisitor _visitor;
        private readonly IUserSession _session;
        private INotifier _notifier;

        public ExecuteCommandResult(ExpenseReport expenseReport, string command, IWorkflowFacilitator facilitator, IStateCommandVisitor visitor, IUserSession session, INotifier notifier)
        {
            _expenseReport = expenseReport;
            _session = session;
            _notifier = notifier;
            _command = command;
            _facilitator = facilitator;
            _visitor = visitor;
        }

        public ExecuteCommandResult(ExpenseReport expenseReport, string command) : this(expenseReport, command, 
            new WorkflowFacilitator(new Calendar()), 
            new StateCommandVisitor(), new UserSession(), DependencyResolver.Current.GetService<INotifier>())
        {
            _expenseReport = expenseReport;
            _command = command;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            IStateCommand[] commands = _facilitator.GetValidStateCommands(_expenseReport, _session.GetCurrentUser());
            IStateCommand matchingCommand =
                Array.Find(commands, delegate(IStateCommand obj) { return obj.Matches(_command); });

            matchingCommand.Execute(_visitor);

            context.Controller.TempData["Flash"] = _session.PopUserMessage();
        }
    }
}