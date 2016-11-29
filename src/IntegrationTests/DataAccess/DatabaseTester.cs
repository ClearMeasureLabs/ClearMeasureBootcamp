using ClearMeasure.Bootcamp.DataAccess.Mappings;

namespace ClearMeasure.Bootcamp.IntegrationTests.DataAccess
{
    public class DatabaseTester
    {
        public void Clean()
        {
            new DatabaseEmptier(DataContext.GetTransactedSession().SessionFactory).DeleteAllData();
        }
    }
}