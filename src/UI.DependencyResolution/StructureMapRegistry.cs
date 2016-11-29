using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using StructureMap.Configuration.DSL;

namespace ClearMeasure.Bootcamp.UI.DependencyResolution
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<Employee>();
                scanner.AssemblyContainingType<DataContext>();
                scanner.Assembly("ClearMeasure.Bootcamp.UI");
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof (IRequestHandler<,>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        }
    }
}