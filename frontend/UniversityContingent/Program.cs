using UniversityContingent.Controller.Api;
using UniversityContingent.Views;

namespace UniversityContingent
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            // Создаём API сервис
            var apiService = new ApiService("http://localhost:8000");
            
            // Запускаем форму авторизации
            Application.Run(new LoginForm(apiService));
        }
    }
}
