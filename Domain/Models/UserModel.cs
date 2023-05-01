namespace Domain.Models;

public record UserModel()
{
    public string Email { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public ProfileModel Profile { get; set; }
};