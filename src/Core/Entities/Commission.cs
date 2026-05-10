using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Commission
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public int TypeId { get; set; }
        public CommissionType Type { get; set; } = null!;

        public int SizeId { get; set; }
        public CommissionSize Size { get; set; } = null!;

        public bool IsPrivate { get; set; }

        [MaxLength(255)]
        public string? PrivateClientEmail { get; set; }

        [MaxLength(100)]
        public string? PrivateDisplayName { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AgreedPrice { get; set; }

        public CommissionStatus Status { get; set; }

        public bool IsPaid { get; set; }

        public Guid? DownloadToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
