using System.Text.Json.Serialization;

namespace UniversityContingent.Models
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class LoginRequest
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Ответ на авторизацию
    /// </summary>
    public class LoginResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("login")]
        public string UserLogin { get; set; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
