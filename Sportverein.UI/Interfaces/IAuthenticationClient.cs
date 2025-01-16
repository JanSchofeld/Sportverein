using System;
using Microsoft.IdentityModel.JsonWebTokens;
using Sportverein.Shared.Models;
using Sportverein.Shared.ViewModels;

namespace Sportverein.UI.Interfaces;

public interface IAuthenticationClient
{
    Task<string> LoginAsync(LoginViewModel loginViewModel);
}
