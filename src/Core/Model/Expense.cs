using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace ClearMeasure.Bootcamp.Core.Model
{
    public class Expense
    {
        public Expense()
        {
        }

        public Expense(decimal amount, string description)
        {
            Amount = amount;
            Description = description;
        }

        public virtual decimal Amount { get; set; }
        public virtual string Description { get; set; }
    }
}