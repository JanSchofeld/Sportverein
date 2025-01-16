using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Sportverein.Shared.Models;

public class User : Entity
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(30, ErrorMessage = "First Name has to be between 2 and 30 characters long.", MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(30, ErrorMessage = "First Name has to be between 2 and 30 characters long.", MinimumLength = 2)]
    public string LastName { get; set;}

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Password has to be 8 characters or longer.", MinimumLength = 8)]
    public string PasswordHash { get; set; }

    public bool IsAdmin { get; set; } = false;

    public List<Claim> ToClaims(){
        var claims = new List<Claim>() {
            new Claim(ClaimTypes.Name, $"{FirstName} {LastName}"),
            new Claim(ClaimTypes.Email, Email),
            new Claim("ID", ID.ToString()),
        };

        if (IsAdmin){
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        return claims;
    }
}
