using System;
using System.ComponentModel.DataAnnotations;

namespace NoteTaker.Core.EF
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? CreatedDate { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}