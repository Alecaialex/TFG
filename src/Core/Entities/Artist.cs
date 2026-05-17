using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; } = null!;

        [MaxLength(2048)]
        public string? ProfilePicture { get; set; }

        public string? Bio { get; set; }
        public int OpenSlots { get; set; }
        public bool IsAccepting { get; set; }

        [MaxLength(500)]
        public string? PaymentInfo { get; set; }

        public List<Commission> Commissions { get; set; } = new();
        public List<CommissionType> CommissionTypes { get; set; } = new();
        public List<CommissionSize> CommissionSizes { get; set; } = new();
    }
}