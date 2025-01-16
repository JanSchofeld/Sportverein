using System;
using Newtonsoft.Json;
using Sportverein.Shared.ViewModels;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Clients;

public class AuthenticationClient : IAuthenticationClient
{
    private IHttpClientFactory httpClient;

    public AuthenticationClient(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory;
    }

    public async Task<string> LoginAsync(LoginViewModel loginViewModel)
    {
        string loginJson = JsonConvert.SerializeObject(loginViewModel);
        StringContent httpContent = new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json");
        
        var client = httpClient.CreateClient("Api");
        var response = await client.PostAsync("authentication/login", httpContent);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        return null!;
    }
}
