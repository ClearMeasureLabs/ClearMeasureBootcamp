using System.Collections.Generic;
using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.UI.Models.SelectListProviders
{
    public class ExpenseReportStatusSelectListProvider
    {
        public static IEnumerable<SelectListItem> GetOptions()
        {
            return GetOptions(null);
        }

        public static IEnumerable<SelectListItem> GetOptions(string selected)
        {
            var result = new List<SelectListItem>();

            foreach (var status in ExpenseReportStatus.GetAllItems())
            {
                result.Add(new SelectListItem
                               {
                                   Text = status.FriendlyName,
                                   Value = status.Key,
                                   Selected = (status.Key == selected)
                               });
            }
            
            return result;
            
        }

        public static IEnumerable<SelectListItem> GetOptionsWithBlank(string selected)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "<Any>", Value = "" });

            result.AddRange(GetOptions(selected));

            return result;
        }
    }
}