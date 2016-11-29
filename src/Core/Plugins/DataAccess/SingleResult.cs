namespace ClearMeasure.Bootcamp.Core.Plugins.DataAccess
{
    public class SingleResult<TAggregateRoot>
    {
        public TAggregateRoot Result { get; private set; }

        public SingleResult(TAggregateRoot result)
        {
            Result = result;
        }
    }
}