using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.UI.Models
{
    public class ToDoModel
    {
        public ExpenseReport[] Submitted { get; set; }
        public ExpenseReport[] Approved { get; set; }
    }
}