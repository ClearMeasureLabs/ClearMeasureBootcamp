using System;
using System.Collections.Generic;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using NHibernate;

namespace ClearMeasure.Bootcamp.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Employee GetByUserName(string userName)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                IQuery query =
                    session.CreateQuery("from Employee emp left join fetch emp.Roles where emp.UserName = :username");
                query.SetParameter("username", userName);
                var match = query.UniqueResult<Employee>();
                return match;
            }
        }

        public Employee[] GetEmployees(EmployeeSpecification spec)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof (Employee));
                criteria.SetCacheable(true);
                if (spec.CanFulfill)
                {
                    criteria.CreateAlias("Roles", "role");
                    criteria.SetFetchMode("Roles", FetchMode.Select);
                }
                IList<Employee> list = criteria.List<Employee>();
                Employee[] employees = new List<Employee>(list).ToArray();
                Array.Sort(employees);
                return employees;
            }
        }
    }
}