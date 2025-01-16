using System;
using Newtonsoft.Json;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Clients;

public class CourseMembershipClient : ICourseMembershipClient
{
    private readonly IHttpClientFactory httpClient;

    public CourseMembershipClient(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory;
    }


    public async Task AddAsync(CourseMembership newCourseMembership)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PostAsJsonAsync("coursememberships", newCourseMembership);
    }

    public async Task DeleteAsync(int membershipId)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.DeleteAsync($"coursememberships/{membershipId}");
    }

    public async Task DeleteAsync(int userId, int courseId)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.DeleteAsync($"coursememberships/{userId}/{courseId}");
    }

    public async Task<IEnumerable<CourseMembership>> GetAllAsync()
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"coursememberships");
        if(response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CourseMembership>>(content)!;
        }
        return null!;
    }

    public async Task<CourseMembership> GetByIdAsync(int membershipId)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"coursememberships/{membershipId}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CourseMembership>(content)!;
        }
        return null!;
    }

    public async Task<IEnumerable<Course>> GetCoursesByMemberAsync(int userId)
    {
        var client = this.httpClient.CreateClient("Api");

        var respones = await client.GetAsync($"coursememberships/user/{userId}");
        if (respones.IsSuccessStatusCode){
            var content = await respones.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Course>>(content)!;
        }

        return null!;
    }

    public async Task<IEnumerable<User>> GetMembersByCourseIdAsync(int courseId)
    {
        var client = this.httpClient.CreateClient("Api");
        var respones = await client.GetAsync($"coursememberships/course/{courseId}");
        if (respones.IsSuccessStatusCode){
            var content = await respones.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<User>>(content)!;
        }

        return null!;
    }

    public async Task UpdateAsync(CourseMembership updatedCourseMembership)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PutAsJsonAsync($"coursememberships/{updatedCourseMembership.ID}", updatedCourseMembership);
    }
}
