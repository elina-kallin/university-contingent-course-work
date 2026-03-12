using UniversityContingent.Controller.Api;
using UniversityContingent.Models;
using UniversityContingent.Views;

namespace UniversityContingent.Views
{
    public partial class LoginForm : Form
    {
        private readonly ApiService _apiService;
        private bool _loginSuccessful = false;

        public LoginForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            
            // Значения по умолчанию для тестирования
            textBoxLogin.Text = "dean";
            textBoxPassword.Text = "dean123";
        }

        public string? ApiUrl { get; set; }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Вход...";
            lblError.Text = string.Empty;

            try
            {
                var response = await _apiService.LoginAsync(login, password);

                if (response != null)
                {
                    _loginSuccessful = true;
                    
                    // Открываем главную форму
                    var mainForm = new MainForm(_apiService, response);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    lblError.Text = "Неверный логин или пароль";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"Ошибка подключения: {ex.Message}";
                lblError.Visible = true;
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Войти";
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_loginSuccessful)
            {
                Application.Exit();
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
