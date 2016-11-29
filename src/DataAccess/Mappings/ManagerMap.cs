using ClearMeasure.Bootcamp.Core.Model;
using FluentNHibernate.Mapping;

namespace ClearMeasure.Bootcamp.DataAccess.Mappings
{
    public class ManagerMap : SubclassMap<Manager>
    {
        public ManagerMap()
        {
            Not.LazyLoad();
            References(x => x.AdminAssistant).Column("AdminAssistantId");
            DiscriminatorValue("MGR");
        }
    }
}