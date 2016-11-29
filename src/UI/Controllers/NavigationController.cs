using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Services;
using UI.Models;

namespace ClearMeasure.Bootcamp.UI.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IUserSession _session;

        public NavigationController(IUserSession session)
        {
            _session = session;
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var currentUser = _session.GetCurrentUser();

            var model = new NavigationMenuModel();

            return PartialView(model);
        }

    }
}
