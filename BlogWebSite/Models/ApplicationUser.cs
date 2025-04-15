using Microsoft.AspNetCore.Identity;

namespace BlogWebSite.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
} 