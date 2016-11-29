namespace ClearMeasure.Bootcamp.Core.Plugins.DataAccess
{
    public class MultipleResult<TAggregateRoot>
    {
        public TAggregateRoot[] Results { get; set; }
    }
}