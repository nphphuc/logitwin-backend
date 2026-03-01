namespace csharp_service.Models.Responses
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string IdToken { get; set; }
    }

    public class RegisterResponse
    {
        public string Message { get; set; } = string.Empty;
        public string UserSub { get; set; } = string.Empty;
    }

    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}