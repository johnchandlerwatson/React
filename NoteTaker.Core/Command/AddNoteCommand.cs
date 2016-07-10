using System;
using System.Data.Entity.Migrations;
using NoteTaker.Core.Models;

namespace NoteTaker.Core.Command
{
    public class AddNoteCommand : Command<bool>
    {
        private readonly Note _note;

        public AddNoteCommand(Note note)
        {
            _note = note;
        }

        public override bool CommandAction()
        {
            if (_note == null)
            {
                return false;
            }
            Context.Note.AddOrUpdate(_note);           
            return Context.SaveChanges() > 0 ? true : false;
        }
    }
}
