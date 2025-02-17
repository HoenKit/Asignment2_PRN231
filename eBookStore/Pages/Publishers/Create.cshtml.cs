using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http.Headers;

namespace eBookStore.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        [BindProperty]
        public PublisherDto Publisher { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(Publisher);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Publishers", content);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!response.IsSuccessStatusCode) return Page();

            return RedirectToPage("Index");
        }
    }
}
