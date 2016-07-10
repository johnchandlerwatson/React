using NoteTaker.Core.EF;

namespace NoteTaker.Core.Queries
{
    public abstract class Query<T>
    {
        public NoteTakerContext Context { get; set; }

        public T PerformQuery()
        {
            using (var context = new NoteTakerContext())
            {
                Context = context;
                return QueryDefinition();
            }
        }
        public abstract T QueryDefinition();
    }
}