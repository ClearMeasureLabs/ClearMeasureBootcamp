using System.Web.Mvc;
using ClearMeasure.Bootcamp.UI.Helpers.ActionFilters;

namespace ClearMeasure.Bootcamp.UI.Controllers
{
    [AddUserMetaDataToViewData]
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}