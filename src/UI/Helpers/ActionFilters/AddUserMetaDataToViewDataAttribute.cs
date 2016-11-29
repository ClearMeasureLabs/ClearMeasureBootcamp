using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.UI.Helpers.ActionFilters
{
    public class AddUserMetaDataToViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userSession = DependencyResolver.Current.GetService<IUserSession>();
            Employee currentUser = userSession.GetCurrentUser();
            if (currentUser != null)
            {
                filterContext.Controller.ViewData["CurrentUserName"] = currentUser.UserName;
                filterContext.Controller.ViewData["CurrentUserFullName"] = currentUser.GetFullName();
            }
            base.OnActionExecuted(filterContext);
        }
    }
}