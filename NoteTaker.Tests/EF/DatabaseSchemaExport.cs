using System;
using System.Data.Entity.Migrations;
using NoteTaker.Core.EF;
using NoteTaker.Core.Models;
using NUnit.Framework;

namespace NoteTaker.Tests.EF
{
    [TestFixture]
    public class DatabaseSchemaExport
    {
        [TestCase]
        public void Export()
        {
            using (var context = new NoteTakerContext())
            {
                var manager = new DatabaseManager(context);
                manager.DropAndCreate();
                Seed(context);
            }
        }
        private static void Seed(NoteTakerContext context)
        {
            var note1 = new Note
            {
                Author = new Person
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "sportsguy23@gmail.com",
                    FirstName = "Brock",
                    MiddleName = "Brockington",
                    LastName = "Brockwell",
                    Bio = "I like sports"
                },
                Text = "This is a note about how much I like sports."
            };
            var note2 = new Note
            {
                Author = new Person
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "bikerdude76@gmail.com",
                    FirstName = "Guts",
                    MiddleName = "McGee",
                    LastName = "Willy",
                    Bio = "I'll cut you into peices."
                },
                Text = "This is a note about my biker gang."
            };
            context.Note.AddOrUpdate(note1);
            context.Note.AddOrUpdate(note2);
            context.SaveChanges();
        }
    }
}
