using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {

		[HttpPost]
		public IActionResult Login() {
				return Ok("You are logged in");
		}
		
}
