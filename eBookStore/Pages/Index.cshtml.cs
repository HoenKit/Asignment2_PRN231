using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace eBookStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [BindProperty]
        public LoginDto LoginDto { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var json = JsonSerializer.Serialize(LoginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Store Token and Role in cookies
            Response.Cookies.Append("Token", result.Token, new CookieOptions
            {
                HttpOnly = true, // Prevents JavaScript access to the cookie
                Secure = true,   // Ensures the cookie is sent only over HTTPS
                SameSite = SameSiteMode.Strict, // Restricts cross-site cookie sending
                Expires = DateTime.UtcNow.AddDays(1) // Set an expiry for the cookie (optional)
            });

            Response.Cookies.Append("Role", result.Role, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return RedirectToPage("/Authors/Index");
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}

