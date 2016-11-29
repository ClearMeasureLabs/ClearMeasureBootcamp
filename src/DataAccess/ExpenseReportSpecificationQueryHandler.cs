using System.Collections.Generic;
using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Features.SearchExpenseReports;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Plugins.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using NHibernate;
using NHibernate.Criterion;

namespace ClearMeasure.Bootcamp.DataAccess
{
    public class ExpenseReportSpecificationQueryHandler : IRequestHandler<ExpenseReportSpecificationQuery, MultipleResult<ExpenseReport>>
    {
        public MultipleResult<ExpenseReport> Handle(ExpenseReportSpecificationQuery command)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(ExpenseReport));

                if (command.Approver != null)
                {
                    criteria.Add(Restrictions.Eq("Approver", command.Approver));
                }

                if (command.Submitter != null)
                {
                    criteria.Add(Restrictions.Eq("Submitter", command.Submitter));
                }

                if (command.Status != null)
                {
                    criteria.Add(Restrictions.Eq("Status", command.Status));
                }

                IList<ExpenseReport> list = criteria.List<ExpenseReport>();
                return new MultipleResult<ExpenseReport> {Results = new List<ExpenseReport>(list).ToArray()};
            }


        }
    }
}