using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NoteTaker.Core.Models;

namespace NoteTaker.Core.Queries
{
    public class GetNotesQuery : Query<List<Note>>
    {
        public override List<Note> QueryDefinition()
        {
            return Context.Note.Include(x => x.Author).ToList();
        }
    }
}