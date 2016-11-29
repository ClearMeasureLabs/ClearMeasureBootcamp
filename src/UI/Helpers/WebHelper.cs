using System;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using ClearMeasure.Bootcamp.UI.Services;

namespace ClearMeasure.Bootcamp.UI.Helpers
{
    public static class WebHelper
    {
         public static IStateCommand[] GetValidCommands(this HtmlHelper helper, ExpenseReport expenseReport, Employee employee)
         {
             var facilitator = DependencyResolver.Current.GetService<IWorkflowFacilitator>();
             IStateCommand[] validStateCommands = facilitator.GetValidStateCommands(new ExecuteTransitionCommand(expenseReport, null, employee, new DateTime()));
             return validStateCommands;
         }

        public static Employee GetCurrentUser(this HtmlHelper helper)
        {
            return new UserSession().GetCurrentUser();
        }

        public static string GetProductVersionNumber(this HtmlHelper helper)
        {
            return new ApplicationInformation().ProductVersion;
        }
    }
}