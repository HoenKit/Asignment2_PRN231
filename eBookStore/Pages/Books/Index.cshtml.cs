using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace eBookStore.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        public List<BookDto> Books { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("Books");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Books = JsonSerializer.Deserialize<List<BookDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }

    public class BookDto
    {
        public int book_id { get; set; }
        public string Title { get; set; }
        public string type { get; set; }
        public int pub_id { get; set; }
        public float price { get; set; }
        public string advance { get; set; }
        public string royalty { get; set; }
        public float ytd_sales { get; set; }
        public string notes { get; set; }
        public DateTime published_date { get; set; }
    }
}

