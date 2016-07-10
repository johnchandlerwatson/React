using System;
using System.Data.Entity;
using System.Linq;
using NoteTaker.Core.Models;
using NoteTaker.Core.Utility;

namespace NoteTaker.Core.EF
{
    public class NoteTakerContext :  DbContext
    {
        public NoteTakerContext() : base("NoteTaker") {}
        public DbSet<Note> Note { get; set; }
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<NoteTakerContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return SetEntityProperties();
        }

        private int SetEntityProperties()
        {
            var datetime = DateTime.Now;
            foreach (var newEntry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity.GetType().IsSubclassOf(typeof(Entity))))
            {
                if (newEntry.Property("CreatedDate").CurrentValue == null)
                {
                    newEntry.Property("CreatedDate").CurrentValue = datetime;
                }
            }

            foreach (var modifiedEntry in ChangeTracker.Entries().Where(x => x.State.InList(EntityState.Added, EntityState.Modified) && x.Entity.GetType().IsSubclassOf(typeof(Entity))))
            {
                modifiedEntry.Property("ChangedDate").CurrentValue = datetime;
            }
            return base.SaveChanges();
        }
    }
}
