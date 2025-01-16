using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sportverein.Shared.Models;

namespace Sportverein.Api.Misc;

public class JwtTokenHelper
{
    private JwtTokenSettings settings;

    public JwtTokenHelper(IOptions<JwtTokenSettings> settings)
    {
        this.settings = settings.Value;
    }


    public string CreateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.EncryptionKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = user.ToClaims();

        var securityToken = new JwtSecurityToken(
            "Sportverein.Api", "Sportverein.Api", claims, 
            expires: DateTime.UtcNow.AddMinutes(120), 
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}
