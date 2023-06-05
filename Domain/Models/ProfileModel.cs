namespace Domain.Models;

public record ProfileModel
{
    public string Firstname { get; set; }
    public string Middlename { get; set; }
    public string Lastname { get; set; }
    public string Country { get; set; }
}
