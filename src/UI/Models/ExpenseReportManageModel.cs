using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Model;
using UI.Models;

namespace ClearMeasure.Bootcamp.UI.Models
{
    public class ExpenseReportManageModel
    {
        public ExpenseReport ExpenseReport { get; set; }
        public EditMode Mode { get; set; }

        [HiddenInput]
        public string ExpenseReportNumber { get; set; }

        [HiddenInput]
        public string Status { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SubmitterUserName { get; set; }

        [HiddenInput]
        [DisplayName("Submitter")]
        public string SubmitterFullName { get; set; }

        [DisplayName("Submitted To")]
        public string ApproverUserName { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public bool IsReadOnly { get; set; }
        public bool CanReassign { get; set; }
    }
}