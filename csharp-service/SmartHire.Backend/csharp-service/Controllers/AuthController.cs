using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using csharp_service.Models.Requests;
using csharp_service.Models.Responses;

namespace csharp_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Thông tin cấu hình từ AWS Cognito
        private readonly string _clientId = "4ndsse07i177h21htah3ul7qhb";
        private readonly string _userPoolId = "ap-southeast-1_XXgbLSkb8";
        private readonly AmazonCognitoIdentityProviderClient _provider;

        public AuthController()
        {
            // Khởi tạo client kết nối với Region Singapore
            _provider = new AmazonCognitoIdentityProviderClient(RegionEndpoint.APSoutheast1);
        }

        /// Đăng ký tài khoản mới trên AWS Cognito
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Username = request.Email,
                Password = request.Password,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = request.Email },
                    new AttributeType { Name = "name", Value = request.FullName }
                }
            };

            try
            {
                var response = await _provider.SignUpAsync(signUpRequest);
                return Ok(new RegisterResponse
                {
                    Message = "Đăng ký thành công! Vui lòng kiểm tra email để lấy mã xác nhận.",
                    UserSub = response.UserSub
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// Xác nhận mã OTP gửi về Email để kích hoạt tài khoản
        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmRequest request)
        {
            var confirmRequest = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = request.Email,
                ConfirmationCode = request.Code
            };

            try
            {
                await _provider.ConfirmSignUpAsync(confirmRequest);
                return Ok(new BaseResponse { Message = "Xác nhận tài khoản thành công! Bạn có thể đăng nhập ngay bây giờ." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// Đăng nhập để lấy AccessToken
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
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
                var response = await _provider.AdminInitiateAuthAsync(authRequest);
                return Ok(new LoginResponse
                {
                    AccessToken = response.AuthenticationResult.AccessToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}