using System;
using Sportverein.Shared.Models;
using Sportverein.Shared.ViewModels;

namespace Sportverein.UI.Interfaces;

public interface IAuthenticationService
{
    Task<User> LoginAsync(LoginViewModel loginViewModel);
    Task Logout();
}
