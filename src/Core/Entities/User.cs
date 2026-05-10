using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Artist? ArtistProfile { get; set; }
        public Client? ClientProfile { get; set; }
    }
}
