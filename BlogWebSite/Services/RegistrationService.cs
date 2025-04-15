using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BlogWebSite.Models;

namespace BlogWebSite.Services;

public interface IRegistrationService
{
    Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(RegisterModel model);
}

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public RegistrationService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(RegisterModel model)
    {
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Add custom user claims for first and last name
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, model.FirstName),
                new Claim(ClaimTypes.Surname, model.LastName)
            };

            await _userManager.AddClaimsAsync(user, claims);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return (true, Array.Empty<string>());
        }

        return (false, result.Errors.Select(e => e.Description));
    }
} 