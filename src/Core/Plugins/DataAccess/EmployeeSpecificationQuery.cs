using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Plugins.DataAccess
{
	public class EmployeeSpecificationQuery : IRequest<MultipleResult<Employee>>
	{
		public static readonly EmployeeSpecificationQuery All = new EmployeeSpecificationQuery();
	}
}