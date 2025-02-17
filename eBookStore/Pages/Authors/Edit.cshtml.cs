using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http.Headers;

namespace eBookStore.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }

        [BindProperty]
        public AuthorDto Author { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"Authors/get-by-id?key={id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            Author = JsonSerializer.Deserialize<AuthorDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Or redirect to login page
            }
            // Add the Authorization header with Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(Author);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"Authors?key={Author.author_id}", content);
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
