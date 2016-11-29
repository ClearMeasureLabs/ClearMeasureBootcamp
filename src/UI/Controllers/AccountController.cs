using System.Web.Mvc;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.Core.Services;
using UI.Models;

namespace ClearMeasure.Bootcamp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly Bus _bus;
        private readonly IUserSession _session;

        public AccountController(Bus bus, IUserSession session)
        {
            _bus = bus;
            _session = session;
        }

        public ActionResult Login()
        {
            _session.LogOut();
            return View();
        }

        [HttpPost]
        public void Login(LoginModel model)
        {
            Employee employee = _bus.Send(new EmployeeByUserNameQuery(model.UserName)).Result;
            _session.LogIn(employee);
        }

        public ActionResult Logout()
        {
            _session.LogOut();
            return Redirect("~/");
        }
    }
}
