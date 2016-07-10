using System.ComponentModel.DataAnnotations.Schema;
using NoteTaker.Core.EF;

namespace NoteTaker.Core.Models
{
    [Table("Person")]
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
    }
}