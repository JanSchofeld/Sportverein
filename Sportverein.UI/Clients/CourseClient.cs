using System;
using Sportverein.Shared.Models;
using Sportverein.UI.Interfaces;
using Newtonsoft.Json;

namespace Sportverein.UI.Clients;

public class CourseClient : ICourseClient
{
    private readonly IHttpClientFactory httpClient;
    

    public CourseClient(IHttpClientFactory httpClientFactory)
    {
        this.httpClient = httpClientFactory;
    }

    public async Task AddAsync(Course newCourse)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PostAsJsonAsync("courses", newCourse);
    }

    public async Task DeleteAsync(int ID)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.DeleteAsync($"courses/{ID}");
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync("courses");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Course>>(content)!;
        }
        return null!;
    }

    public async Task<Course> GetByIdAsync(int ID)
    {
        var client = this.httpClient.CreateClient("Api");
        var response = await client.GetAsync($"courses/{ID}");
        if (response.IsSuccessStatusCode){
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Course>(content)!;
        }
        return null!;
    }

    public async Task UpdateAsync(Course updatedCourse)
    {
        var client = this.httpClient.CreateClient("Api");
        await client.PutAsJsonAsync($"courses/{updatedCourse.ID}", updatedCourse);
    }
}
