using System;
using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Features.MutlipleExpenses
{
    public class AddExpenseCommand : IRequest<AddExpenseResult>
    {
        public ExpenseReport Report { get; set; }
        public Employee CurrentUser { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}