using ClearMeasure.Bootcamp.Core.Model;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;

namespace ClearMeasure.Bootcamp.DataAccess.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Not.LazyLoad();
            Access.CamelCaseField(CamelCasePrefix.Underscore);
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.UserName).Not.Nullable().Length(50);
            Map(x => x.FirstName).Not.Nullable().Length(25);
            Map(x => x.LastName).Not.Nullable().Length(25);
            Map(x => x.EmailAddress).Not.Nullable().Length(100);
            DiscriminateSubClassesOnColumn("Type");
        }
    }
}