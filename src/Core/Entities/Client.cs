using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; } = null!;

        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = null!;
    }
}
