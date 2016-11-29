using System.Collections.Specialized;

namespace ClearMeasure.Bootcamp.SmokeTests
{
    public static class SmokeTestPageUrls
    {
        public static NameValueCollection PageUrls { get; } = new NameValueCollection
        {
            {"Home", "/"},
            {"Login", "/Account/Login?ReturnUrl=%2F"},
            {"New", "/ExpenseReport/Manage?mode=New"},
            {"Search", "/ExpenseReportSearch"},
            {"My Expenses", "/ExpenseReportSearch?Submitter=jpalermo"},
            {"ExpenseReport", "/ExpenseReport/Manage/"},
        };
    }
}
