using ClearMeasure.Bootcamp.Core.Model;
using FluentNHibernate.Mapping;

namespace ClearMeasure.Bootcamp.DataAccess.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Not.LazyLoad();
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Length(50);
        }
    }
}