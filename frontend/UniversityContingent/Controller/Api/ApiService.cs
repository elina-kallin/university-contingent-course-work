using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        Task<Direction?> UpdateDirectionAsync(Guid id, Direction direction);
        Task<bool> DeleteDirectionAsync(Guid id);

        // CRUD Группы
        Task<Group?> CreateGroupAsync(Group group);
        Task<Group?> UpdateGroupAsync(Guid id, Group group);
        Task<bool> DeleteGroupAsync(Guid id);

        // Студенты
        Task<Student?> GetStudentAsync(Guid id);
        Task<Student?> CreateStudentAsync(Student student);
        Task<Student?> UpdateStudentAsync(Guid id, Student student);
        Task<bool> DeleteStudentAsync(Guid id);

        // Приказы
        Task<Order?> CreateEnrollmentOrderAsync(EnrollmentOrderWithStudentsCreate data);
        Task<Order?> GetOrderAsync(Guid id);
        Task<string?> GetOrderPrintHtmlAsync(Guid id);
        Task<List<Order>?> GetOrdersAsync();
        Task<Order?> CreateOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Guid id, Order order);
        Task<bool> DeleteOrderAsync(Guid id);
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

            // Отключаем автоматические редиректы для контроля заголовков
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
            // Очищаем старые заголовки
            _httpClient.DefaultRequestHeaders.Authorization = null;
            
            if (!string.IsNullOrEmpty(AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
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

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var response = await _httpClient.SendAsync(request);

                // Обрабатываем редирект вручную с сохранением заголовка
                if (response.StatusCode == System.Net.HttpStatusCode.Redirect ||
                    response.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                {
                    var location = response.Headers.Location?.ToString();
                    if (!string.IsNullOrEmpty(location))
                    {
                        var redirectUrl = location.StartsWith("http") ? location : _baseUrl + location;
                        Console.WriteLine($"[DEBUG] GET redirect to: {redirectUrl}");
                        
                        using var redirectRequest = new HttpRequestMessage(HttpMethod.Get, redirectUrl);
                        SetAuthHeader(); // Сохраняем заголовок авторизации
                        response = await _httpClient.SendAsync(redirectRequest);
                    }
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG] GET {endpoint} - Status: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = System.Text.Json.JsonSerializer.Deserialize<T>(content, JsonOptions);
                        Console.WriteLine($"[DEBUG] Deserialized successfully, type: {typeof(T).Name}, count: {(result is System.Collections.IEnumerable e ? e.Cast<object>().Count().ToString() : "N/A")}");
                        return result;
                    }
                    catch (System.Text.Json.JsonException ex)
                    {
                        Console.WriteLine($"[DEBUG] JSON Deserialize error: {ex.Message}");
                        Console.WriteLine($"[DEBUG] JSON: {content}");
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
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var response = await _httpClient.SendAsync(request);

                // Обрабатываем редирект вручную с сохранением заголовка
                if (response.StatusCode == System.Net.HttpStatusCode.Redirect ||
                    response.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                {
                    var location = response.Headers.Location?.ToString();
                    if (!string.IsNullOrEmpty(location))
                    {
                        var redirectUrl = location.StartsWith("http") ? location : _baseUrl + location;
                        
                        using var redirectRequest = new HttpRequestMessage(HttpMethod.Get, redirectUrl);
                        SetAuthHeader();
                        response = await _httpClient.SendAsync(redirectRequest);
                    }
                }

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

        public async Task<Student?> GetStudentAsync(Guid id)
        {
            return await GetAsync<Student>($"/students/{id}");
        }

        public async Task<Student?> CreateStudentAsync(Student student)
        {
            return await PostAsync<Student>("/students", student);
        }

        public async Task<Student?> UpdateStudentAsync(Guid id, Student student)
        {
            return await PutAsync<Student>($"/students/{id}", student);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await DeleteAsync($"/students/{id}");
        }

        public async Task<Direction?> CreateDirectionAsync(Direction direction)
        {
            return await PostAsync<Direction>("/directions", direction);
        }

        public async Task<Direction?> UpdateDirectionAsync(Guid id, Direction direction)
        {
            return await PutAsync<Direction>($"/directions/{id}", direction);
        }

        public async Task<bool> DeleteDirectionAsync(Guid id)
        {
            return await DeleteAsync($"/directions/{id}");
        }

        public async Task<Group?> CreateGroupAsync(Group group)
        {
            return await PostAsync<Group>("/groups", group);
        }

        public async Task<Group?> UpdateGroupAsync(Guid id, Group group)
        {
            return await PutAsync<Group>($"/groups/{id}", group);
        }

        public async Task<bool> DeleteGroupAsync(Guid id)
        {
            return await DeleteAsync($"/groups/{id}");
        }

        public async Task<Order?> CreateEnrollmentOrderAsync(EnrollmentOrderWithStudentsCreate data)
        {
            // Бэкенд делает редирект с / на без /, отправляем сразу без trailing slash
            return await PostAsyncWithEmptyResponse<Order>("/special-orders/enrollment-with-students", data);
        }

        private async Task<T?> PostAsyncWithEmptyResponse<T>(string endpoint, object data) where T : class, new()
        {
            try
            {
                SetAuthHeader();
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = content };
                SetAuthHeader(); // Устанавливаем заголовок перед отправкой
                
                var response = await _httpClient.SendAsync(request);
                
                // Обрабатываем редирект вручную с сохранением заголовка и метода
                if (response.StatusCode == System.Net.HttpStatusCode.Redirect ||
                    response.StatusCode == System.Net.HttpStatusCode.TemporaryRedirect)
                {
                    var location = response.Headers.Location?.ToString();
                    if (!string.IsNullOrEmpty(location))
                    {
                        // Преобразуем относительный URL в абсолютный
                        var redirectUrl = location.StartsWith("http") ? location : _baseUrl + location;
                        
                        Console.WriteLine($"[DEBUG] Redirect to: {redirectUrl}");
                        
                        // Создаём новый POST запрос с тем же телом и заголовком
                        using var redirectRequest = new HttpRequestMessage(HttpMethod.Post, redirectUrl);
                        redirectRequest.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        SetAuthHeader(); // Сохраняем заголовок авторизации
                        
                        response = await _httpClient.SendAsync(redirectRequest);
                    }
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[DEBUG] POST {endpoint} success");
                    // Если ответ пустой, возвращаем null (успешно)
                    if (string.IsNullOrWhiteSpace(responseContent))
                    {
                        return null;
                    }

                    return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
                }

                Console.WriteLine($"[DEBUG] POST {endpoint} failed: {response.StatusCode} - {responseContent}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] POST {endpoint} error: {ex.Message}");
                return default;
            }
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await GetAsync<Order>($"/orders/{id}");
        }

        public async Task<string?> GetOrderPrintHtmlAsync(Guid id)
        {
            return await GetRawAsync($"/orders/{id}/print");
        }

        public async Task<List<Order>?> GetOrdersAsync()
        {
            return await GetAsync<List<Order>>("/orders");
        }

        public async Task<Order?> CreateOrderAsync(Order order)
        {
            return await PostAsync<Order>("/orders", order);
        }

        public async Task<Order?> UpdateOrderAsync(Guid id, Order order)
        {
            return await PutAsync<Order>($"/orders/{id}", order);
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await DeleteAsync($"/orders/{id}");
        }
    }
}
