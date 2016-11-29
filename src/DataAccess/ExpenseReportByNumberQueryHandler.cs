using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;
using NHibernate.Criterion;

namespace ClearMeasure.Bootcamp.DataAccess
{
    public class ExpenseReportByNumberQueryHandler : IRequestHandler<ExpenseReportByNumberQuery, SingleResult<ExpenseReport>>
    {
        public SingleResult<ExpenseReport> Handle(ExpenseReportByNumberQuery request)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof (ExpenseReport));
                criteria.Add(Restrictions.Eq("Number", request.ExpenseReportNumber));
                criteria.SetFetchMode("AuditEntries", FetchMode.Eager);
                var result = criteria.UniqueResult<ExpenseReport>();
                return new SingleResult<ExpenseReport>(result);
            }
        }
    }
}