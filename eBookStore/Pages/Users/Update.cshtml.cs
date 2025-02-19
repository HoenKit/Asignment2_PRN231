using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace eBookStore.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public UpdateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BookApi");
        }
        [BindProperty]
        public UserDto User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập
            }
            int? userId = GetUserIdFromToken(token);
            // Thêm Authorization header với Bearer token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"User/get-by-id?key={userId}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            User = JsonSerializer.Deserialize<UserDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = Request.Cookies["Token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(); 
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(User);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"User?key={User.user_id}", content);

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!response.IsSuccessStatusCode) return Page();

            return RedirectToPage("/Index");
        }
        public int? GetUserIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            catch (Exception)
            {
                return null; 
            }

            return null;
        }
    }

    public class UserDto
    {
        public int user_id { get; set; }
        public string email_address { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public int role_id { get; set; }
        public int pub_id { get; set; }
        public DateTime hire_date { get; set; }
    }
}

