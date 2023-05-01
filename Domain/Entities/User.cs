using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime RefreshTokenExpirationTime { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public Profile Profile { get; set; }

    public bool IsTransient()
    {
        return Id == default;
    }
}
