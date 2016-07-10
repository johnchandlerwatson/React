using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;

namespace NoteTaker.Core.EF
{
    public class DatabaseManager
    {
        public DatabaseManager(DbContext context)
        {
            _context = context;
        }
        private readonly DbContext _context;

        public void Migrate()
        {
            DbContextValidator.Validate();
            var migratorConfiguration = new Configuration
            {
                TargetDatabase = new DbConnectionInfo(
                    _context.Database.Connection.ConnectionString, "System.Data.SqlClient")
            };
            var migrator = new DbMigrator(migratorConfiguration);
            migrator.Update();
        }

        public void DropAndCreate()
        {
            DbContextValidator.Validate();
            var initalizer = new DbContextInitializer();
            initalizer.InitializeDatabase(_context);          
        }

        internal class DbContextInitializer : DropCreateDatabaseAlways<DbContext>
        {
            public override void InitializeDatabase(DbContext context)
            {
                var manager = new DatabaseManager(context);
                manager.Migrate();
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                    , $"ALTER DATABASE [{context.Database.Connection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                base.InitializeDatabase(context);
            }
        }
    }
}