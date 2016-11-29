using System.Collections.Generic;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using NHibernate;
using NHibernate.Criterion;

namespace ClearMeasure.Bootcamp.DataAccess.Repositories
{
    public class ExpenseReportRepository : IExpenseReportRepository
    {
        public void Save(ExpenseReport expenseReport)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                ITransaction transaction = session.BeginTransaction();
                session.SaveOrUpdate(expenseReport);
                transaction.Commit();
            }
        }

        public ExpenseReport GetSingle(string number)
        {
            using (ISession session = DataContext.GetTransactedSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof (ExpenseReport));
                criteria.Add(Restrictions.Eq("Number", number));
                criteria.SetFetchMode("AuditEntries", FetchMode.Eager);
                var result = criteria.UniqueResult<ExpenseReport>();
                return result;
            }
        }

        public ExpenseReport[] GetMany(SearchSpecification specification)
        {
            using(ISession session = DataContext.GetTransactedSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof (ExpenseReport));

                if (specification.Approver != null)
                {
                    criteria.Add(Restrictions.Eq("Approver", specification.Approver));
                }

                if (specification.Submitter != null)
                {
                    criteria.Add(Restrictions.Eq("Submitter", specification.Submitter));
                }

                if (specification.Status != null)
                {
                    criteria.Add(Restrictions.Eq("Status", specification.Status));
                }

                IList<ExpenseReport> list = criteria.List<ExpenseReport>();
                return new List<ExpenseReport>(list).ToArray();
            }
        }
    }
}