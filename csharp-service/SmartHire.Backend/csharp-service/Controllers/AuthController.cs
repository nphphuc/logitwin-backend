using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;

namespace csharp_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Nhớ thay bằng thông tin thực tế từ AWS Console của bạn
        private readonly string _clientId = "4ndsse07i177h21htah3ul7qhb";
        private readonly string _userPoolId = "ap-southeast-1_XXgbLSkb8";

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var provider = new AmazonCognitoIdentityProviderClient(RegionEndpoint.APSoutheast1);
            var authRequest = new AdminInitiateAuthRequest
            {
                UserPoolId = _userPoolId,
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", request.Email },
                    { "PASSWORD", request.Password }
                }
            };

            try
            {
                var response = await provider.AdminInitiateAuthAsync(authRequest);
                return Ok(new { AccessToken = response.AuthenticationResult.AccessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}