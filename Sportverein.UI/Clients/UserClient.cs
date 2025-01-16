using System;
using Newtonsoft.Json;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Clients;

public class UserClient : IUserClient
{
    private readonly IHttpClientFactory httpClient;

    public UserClient(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory;
    }

    public async Task AddAsync(User newUser)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PostAsJsonAsync("users", newUser);
    }

    public async Task DeleteAsync(int ID)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.DeleteAsync($"users/{ID}");
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"users");

        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<User>>(content)!;
        }
        return null!;
    }

    public async Task<User> GetByEmailAsync(string userEmail)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"users/email/{userEmail}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content)!;
        }
        return null!;
    }

    public async Task<User> GetByIdAsync(int ID)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"users/{ID}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(content)!;
        }
        return null!;
    }

    public async Task UpdateAsync(User updatedUser)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PutAsJsonAsync($"users/{updatedUser.ID}", updatedUser);
    }
}
