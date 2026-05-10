using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class CommissionSize
    {
        [Key]
        public int Id { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Label { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceModifier { get; set; }
    }
}
