using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NoteTaker.Core.EF;

namespace NoteTaker.Core.Models
{
    [Table("Note")]
    public class Note : Entity
    {
        public Person Author { get; set; }
        public string Text { get; set; }
        public List<Note> Notes { get; set; }
    }
}