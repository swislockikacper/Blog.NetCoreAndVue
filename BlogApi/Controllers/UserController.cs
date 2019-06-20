using BlogApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Interfaces.IAuthorizationService authorizationService;

        public UserController(Interfaces.IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("User/Login")]
        public IActionResult Login([FromBody]User loginUser)
        {
            var user = authorizationService.AuthenticateUser(loginUser);

            if (user != null)
                return Ok(new { token = authorizationService.GenerateJSONWebToken(user) });

            return Unauthorized();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("User/Register")]
        public IActionResult Register([FromBody]User user)
        {
            var newUser = authorizationService.RegisterUser(user);

            if (newUser != null)
                return Ok(newUser);

            return BadRequest("User exists!");
        }
    }
}
