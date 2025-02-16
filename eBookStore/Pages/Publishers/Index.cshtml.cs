using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace eBookStore.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        public List<PublisherDto> Publishers { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("Publishers");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Publishers = JsonSerializer.Deserialize<List<PublisherDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }
    public class PublisherDto
    {
        public int pub_id { get; set; }
        public string publisher_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
    }
}
