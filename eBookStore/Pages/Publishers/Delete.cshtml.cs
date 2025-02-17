using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eBookStore.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        [BindProperty]
        public PublisherDto Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"Publishers/get-by-id?key={id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            Publisher = JsonSerializer.Deserialize<PublisherDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"Publishers?key={Publisher.pub_id}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!response.IsSuccessStatusCode) return Page();

            return RedirectToPage("Index");
        }
    }
}
