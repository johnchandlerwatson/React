using System.Data.Entity.Migrations;

namespace NoteTaker.Core.EF
{
    internal sealed class Configuration : DbMigrationsConfiguration<NoteTakerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "React.Core.NoteTakerContext";
            ContextType = typeof(NoteTakerContext);
        }
    }
}
