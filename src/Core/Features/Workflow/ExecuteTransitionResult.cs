using System;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;

namespace ClearMeasure.Bootcamp.Core.Features.Workflow
{
    public class ExecuteTransitionResult
    {
        public string NewStatus { get; set; }
        public NextStep NextStep { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
    }
}