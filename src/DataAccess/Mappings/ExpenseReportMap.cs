using System.Collections.Generic;
using ClearMeasure.Bootcamp.Core.Model;
using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace ClearMeasure.Bootcamp.DataAccess.Mappings
{
    public class ExpenseReportMap : ClassMap<ExpenseReport>
    {
        public ExpenseReportMap()
        {
            Not.LazyLoad();
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Number).Not.Nullable().Length(5);
            Map(x => x.Title).Not.Nullable().Length(200);
            Map(x => x.Description).Not.Nullable().Length(4000);
            Map(x => x.Status).Not.Nullable().CustomType<ExpenseReportStatusType>();
            // New property mappings
            Map(x => x.MilesDriven).Column("MilesDriven");
            Map(x => x.Created).Column("Created");
            Map(x => x.FirstSubmitted).Column("FirstSubmitted");
            Map(x => x.LastSubmitted).Column("LastSubmitted");
            Map(x => x.LastWithdrawn).Column("LastWithdrawn");
            Map(x => x.LastCancelled).Column("LastCancelled");
            Map(x => x.LastApproved).Column("LastApproved");
            Map(x => x.LastDeclined).Column("LastDeclined");
            Map(x => x.Total).Column("Total");

            References(x => x.Submitter).Column("SubmitterId");
            References(x => x.Approver).Column("ApproverId");

            HasMany(Reveal.Member<ExpenseReport, IEnumerable<AuditEntry>>("_auditEntries"))
                .AsList(part =>
                {
                    part.Column("Sequence");
                    part.Type<int>();
                })
                .Table("AuditEntry")
                .Cascade.AllDeleteOrphan()
                .KeyColumn("ExpenseReportId")
                .Component(part =>
                {
                    part.References(x => x.Employee).Column("EmployeeId");
                    part.Map(x => x.EmployeeName).Length(200);
                    part.Map(x => x.Date);
                    part.Map(x => x.EndStatus).CustomType<ExpenseReportStatusType>();
                    part.Map(x => x.BeginStatus).CustomType<ExpenseReportStatusType>();
                })
                .Access.CamelCaseField()
                .Not.LazyLoad();

            HasMany(Reveal.Member<ExpenseReport, IEnumerable<Expense>>("_expenses"))
                .AsList(part =>
                {
                    part.Column("Sequence");
                    part.Type<int>();
                })
                .Table("Expense")
                .Cascade.AllDeleteOrphan()
                .KeyColumn("ExpenseReportId")
                .Component(part =>
                {
                    part.Map(x => x.Description);
                    part.Map(x => x.Amount);
                })
                .Access.CamelCaseField()
                .Not.LazyLoad();
        }
    }
}