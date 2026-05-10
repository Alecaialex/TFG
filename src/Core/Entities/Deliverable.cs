using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Deliverable
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CommissionId { get; set; }
        public Commission Commission { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Phase { get; set; } = null!;

        [Required, MaxLength(2048)]
        public string FileUrl { get; set; } = null!;

        public bool ClientApproved { get; set; }
        public string? ClientNote { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
