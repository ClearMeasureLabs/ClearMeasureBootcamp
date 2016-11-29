using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IUserSession
	{
		Employee GetCurrentUser();
		void LogIn(Employee employee);
		void LogOut();
	}
}