using NoteTaker.Core.EF;

namespace NoteTaker.Core.Command
{
    public abstract class Command<T>
    {
        public NoteTakerContext Context { get; set; }

        public T Execute()
        {
            using (var context = new NoteTakerContext())
            {
                Context = context;
                return CommandAction();
            }
        }
        public abstract T CommandAction();
    }
}