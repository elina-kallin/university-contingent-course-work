using UniversityContingent.Models;

namespace UniversityContingent.Controller.Api
{
    /// <summary>
    /// Интерфейс сервиса для работы с API
    /// </summary>
    public interface IApiService
    {
        string? AccessToken { get; set; }
        
        // Авторизация
        Task<LoginResponse?> LoginAsync(string login, string password);
        Task LogoutAsync();
        
        // Справочники
        Task<List<Faculty>?> GetFacultiesAsync();
        Task<List<Direction>?> GetDirectionsAsync();
        Task<List<Group>?> GetGroupsAsync();
        Task<List<Student>?> GetStudentsAsync();
        
        // CRUD Направления
        Task<Direction?> CreateDirectionAsync(Direction direction);
        Task<Direction?> UpdateDirectionAsync(string id, Direction direction);
        Task<bool> DeleteDirectionAsync(string id);
        
        // CRUD Группы
        Task<Group?> CreateGroupAsync(Group group);
        Task<Group?> UpdateGroupAsync(string id, Group group);
        Task<bool> DeleteGroupAsync(string id);
        
        // Студенты
        Task<Student?> GetStudentAsync(string id);
        
        // Приказы
        Task<Order?> CreateEnrollmentOrderAsync(EnrollmentOrderData data);
        Task<Order?> GetOrderAsync(int id);
        Task<string?> GetOrderPrintHtmlAsync(int id);
    }

    /// <summary>
    /// Сервис для работы с API
    /// </summary>
    public class ApiService : IApiService
    {
        private readonly HttpClientHandler _httpClientHandler;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public string? AccessToken { get; set; }

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            
            // Отключаем автоматическое следование за редиректами
            _httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            
            _httpClient = new HttpClient(_httpClientHandler)
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        /// <summary>
        /// Публичный метод для установки заголовка авторизации
        /// </summary>
        public void SetAuthHeader()
        {
            if (!string.IsNullOrEmpty(AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        /// <summary>
        /// Публичный метод для отправки HTTP запросов
        /// </summary>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            SetAuthHeader();
            return await _httpClient.SendAsync(request);
        }

        /// <summary>
        /// Публичный метод для POST запросов
        /// </summary>
        public async Task<HttpResponseMessage> PostAsyncRaw(string endpoint, HttpContent content)
        {
            SetAuthHeader();
            return await _httpClient.PostAsync(endpoint, content);
        }

        private async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var response = await _httpClient.GetAsync(endpoint);
                
                // Обрабатываем редирект вручную
                if (response.StatusCode == System.Net.HttpStatusCode.Redirect || 
                    response.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                {
                    var location = response.Headers.Location?.ToString();
                    if (!string.IsNullOrEmpty(location))
                    {
                        var redirectUrl = location.StartsWith("http") ? location : _baseUrl + location;
                        using var redirectRequest = new HttpRequestMessage(HttpMethod.Get, redirectUrl);
                        SetAuthHeader();
                        var redirectResponse = await _httpClient.SendAsync(redirectRequest);
                        if (redirectResponse.IsSuccessStatusCode)
                        {
                            var content = await redirectResponse.Content.ReadAsStringAsync();
                            Console.WriteLine($"[DEBUG] GET {endpoint} (redirect): {content}");
                            try
                            {
                                return System.Text.Json.JsonSerializer.Deserialize<T>(content);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[DEBUG] Deserialize error: {ex.Message}");
                                return default;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[DEBUG] Redirect failed: {redirectResponse.StatusCode}");
                        }
                    }
                }
                else if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[DEBUG] GET {endpoint}: {content}");
                    try
                    {
                        return System.Text.Json.JsonSerializer.Deserialize<T>(content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[DEBUG] Deserialize error: {ex.Message}");
                        return default;
                    }
                }
                else
                {
                    Console.WriteLine($"[DEBUG] GET {endpoint} failed: {response.StatusCode}");
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Ошибка GET {endpoint}: {ex.Message}");
                return default;
            }
        }

        private async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthHeader();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
                }
                
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка POST {endpoint}: {response.StatusCode} - {errorContent}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка POST {endpoint}: {ex.Message}");
                return default;
            }
        }

        private async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthHeader();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, endpoint) { Content = content };
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка PUT {endpoint}: {ex.Message}");
                return default;
            }
        }

        private async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var response = await _httpClient.DeleteAsync(endpoint);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка DELETE {endpoint}: {ex.Message}");
                return false;
            }
        }

        private async Task<string?> GetRawAsync(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка GET {endpoint}: {ex.Message}");
                return default;
            }
        }

        public async Task<LoginResponse?> LoginAsync(string login, string password)
        {
            try
            {
                var requestData = new LoginRequest { Login = login, Password = password };
                var json = System.Text.Json.JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("/auth/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseContent);
                    if (result != null)
                    {
                        AccessToken = result.AccessToken;
                        SetAuthHeader();
                    }
                    return result;
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка авторизации: {ex.Message}");
                return default;
            }
        }

        public async Task LogoutAsync()
        {
            AccessToken = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            await Task.CompletedTask;
        }

        public async Task<List<Faculty>?> GetFacultiesAsync()
        {
            return await GetAsync<List<Faculty>>("/faculties");
        }

        public async Task<List<Direction>?> GetDirectionsAsync()
        {
            return await GetAsync<List<Direction>>("/directions");
        }

        public async Task<List<Group>?> GetGroupsAsync()
        {
            return await GetAsync<List<Group>>("/groups");
        }

        public async Task<List<Student>?> GetStudentsAsync()
        {
            return await GetAsync<List<Student>>("/students");
        }

        public async Task<Student?> GetStudentAsync(string id)
        {
            return await GetAsync<Student>($"/students/{id}");
        }

        public async Task<Direction?> CreateDirectionAsync(Direction direction)
        {
            return await PostAsync<Direction>("/directions", direction);
        }

        public async Task<Direction?> UpdateDirectionAsync(string id, Direction direction)
        {
            return await PutAsync<Direction>($"/directions/{id}", direction);
        }

        public async Task<bool> DeleteDirectionAsync(string id)
        {
            return await DeleteAsync($"/directions/{id}");
        }

        public async Task<Group?> CreateGroupAsync(Group group)
        {
            return await PostAsync<Group>("/groups", group);
        }

        public async Task<Group?> UpdateGroupAsync(string id, Group group)
        {
            return await PutAsync<Group>($"/groups/{id}", group);
        }

        public async Task<bool> DeleteGroupAsync(string id)
        {
            return await DeleteAsync($"/groups/{id}");
        }

        public async Task<Order?> CreateEnrollmentOrderAsync(EnrollmentOrderData data)
        {
            return await PostAsync<Order>("/special-orders/enrollment-with-students", data);
        }

        public async Task<Order?> GetOrderAsync(int id)
        {
            return await GetAsync<Order>($"/orders/{id}");
        }

        public async Task<string?> GetOrderPrintHtmlAsync(int id)
        {
            return await GetRawAsync($"/orders/{id}/print");
        }
    }
}
