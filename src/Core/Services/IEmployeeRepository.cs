using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Services
{
	public interface IEmployeeRepository
	{
		Employee GetByUserName(string userName);
		Employee[] GetEmployees(EmployeeSpecification spec);
	}
}