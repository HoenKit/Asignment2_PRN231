using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http.Headers;

namespace eBookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        [BindProperty]
        public BookDto Book { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(Book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Books", content);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/AccessDenied");
            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
