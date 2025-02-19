using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eBookStoreWebAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-by-id")]
        public IActionResult GetById(int key)
        {
            var user = _userRepository.GetById(key);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut] // PUT odata/User/{key}
        [Authorize(Roles = "User")]
        public IActionResult Update(int key, [FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != user.user_id) return BadRequest();

            _userRepository.Update(user);
            return NoContent();
        }
    }
}
