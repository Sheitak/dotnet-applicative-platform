using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;

        public UsersController(UserManager<IdentityUser> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        // POST: api/Users
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            var result = await _userManager.CreateAsync(
                new IdentityUser()
                {
                    UserName = user.UserName,
                    Email = user.Email
                },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetUser),
                new UserDTO { UserName = user.UserName, Email = user.Email }
            );
        }

        // GET: api/Users/aa0d77e3-b79b-44a5-a817-176dff334a33
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            /* Récupère le "sub" du token via la revendication.
            var id = HttpContext.User.Claims.FirstOrDefault(
                c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            )?.Value;
            */

            if (id == null)
            {
                return BadRequest("Unable to retrieve username from token.");
            }

            IdentityUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDTO { UserName = user.UserName, Email = user.Email };
        }

        // POST: api/Users/Auth
        [HttpPost("Auth")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            var response = new AuthenticationResponse
            {
                Token = token.Token,
                Expiration = token.Expiration,
                User = new UserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email
                }
            };

            return Ok(response);
        }
    }
}