using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Model.ExpenseReportWorkflow
{
    public abstract class StateCommandBaseTester
    {
        protected abstract StateCommandBase GetStateCommand(ExpenseReport order, Employee employee);
    }
}