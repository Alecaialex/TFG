using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }

        public Guid CommissionId { get; set; }
        public Commission Commission { get; set; } = null!;

        [Required, MaxLength(2048)]
        public string FileUrl { get; set; } = null!;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
