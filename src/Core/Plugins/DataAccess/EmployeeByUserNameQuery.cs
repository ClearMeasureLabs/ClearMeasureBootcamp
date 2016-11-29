using ClearMeasure.Bootcamp.Core.Model;

namespace ClearMeasure.Bootcamp.Core.Plugins.DataAccess
{
    public class EmployeeByUserNameQuery : IRequest<SingleResult<Employee>>
    {
        public string UserName { get; private set; }

        public EmployeeByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}