using System;
using Newtonsoft.Json;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;

namespace Sportverein.UI.Clients;

public class CourseEventClient : ICourseEventClient
{
    private readonly IHttpClientFactory httpClient;
    
    public CourseEventClient(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory;
    }


    public async Task AddAsync(CourseEvent newEvent)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PostAsJsonAsync($"events", newEvent);
    }

    public async Task DeleteAsync(int eventId)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.DeleteAsync($"events/{eventId}");
    }

    public async Task<List<CourseEvent>> GetAllAsync()
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync("events");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CourseEvent>>(content)!;
        }
        return null!;
    }

    public async Task<CourseEvent> GetByIdAsync(int eventId)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"events/{eventId}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<CourseEvent>(content)!;
            return test;
        }
        return null!;
    }

    public async Task<List<CourseEvent>> GetByCourseIdAsync(int courseId)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"events/courses/{courseId}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CourseEvent>>(content)!;
        }
        return null!;
    }

    public async Task UpdateAsync(CourseEvent updatedEvent)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PutAsJsonAsync($"events/{updatedEvent.ID}", updatedEvent);
    }

    public async Task<List<CourseEvent>> GetByUserIdAsync(int userId)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"events/users/{userId}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CourseEvent>>(content)!;
        }
        return null!;
    }
}
