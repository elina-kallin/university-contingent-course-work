using UniversityContingent.Controller.Api;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class MainForm : Form
    {
        private readonly ApiService _apiService;
        private readonly LoginResponse _userResponse;

        public MainForm(ApiService apiService, LoginResponse userResponse)
        {
            InitializeComponent();
            _apiService = apiService;
            _userResponse = userResponse;

            lblUserName.Text = $"{userResponse.FullName} ({userResponse.UserLogin})";
            lblRole.Text = userResponse.Role ?? "Пользователь";
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Загружаем данные для ComboBox
                var faculties = await _apiService.GetFacultiesAsync();
                var directions = await _apiService.GetDirectionsAsync();
                var groups = await _apiService.GetGroupsAsync();
                var students = await _apiService.GetStudentsAsync();

                // Заполняем ComboBox
                cmbFaculties.DataSource = faculties?.Select(f => new { f.Id, f.Name }).ToList();
                cmbFaculties.DisplayMember = "Name";
                cmbFaculties.ValueMember = "Id";

                cmbDirections.DataSource = directions?.Select(d => new { d.Id, d.Name }).ToList();
                cmbDirections.DisplayMember = "Name";
                cmbDirections.ValueMember = "Id";

                cmbGroups.DataSource = groups?.Select(g => new { g.Id, g.Name }).ToList();
                cmbGroups.DisplayMember = "Name";
                cmbGroups.ValueMember = "Id";

                // Заполняем DataGridView студентами
                dgvStudents.DataSource = students?.Select(s => new
                {
                    s.Id,
                    ФИО = s.FullName,
                    s.StudyBookNumber,
                    Группа = groups?.FirstOrDefault(g => g.Id == s.GroupId)?.Name ?? "Не указана",
                    s.EnrollmentDate,
                    Статус = s.Status switch
                    {
                        StudentStatus.study => "Обучается",
                        StudentStatus.expelled => "Отчислен",
                        StudentStatus.academic_leave => "Академический отпуск",
                        _ => "Не указано"
                    }
                }).ToList();

                lblStatus.Text = $"Загружено студентов: {students?.Count ?? 0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            await _apiService.LogoutAsync();
            Application.Restart();
        }

        private void cmbFaculties_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Фильтрация по факультету
            if (cmbFaculties.SelectedValue != null)
            {
                // TODO: Фильтрация направлений и групп
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        // Меню - Справочники
        private void факультетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Форма факультетов в разработке", "Информация", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void направленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new DirectionsForm(_apiService);
            form.ShowDialog();
            _ = LoadDataAsync(); // Обновляем данные после закрытия
        }

        private void группыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new GroupsForm(_apiService);
            form.ShowDialog();
            _ = LoadDataAsync(); // Обновляем данные после закрытия
        }

        // Меню - Файл
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Меню - Студенты
        private void студентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0; // Переключаемся на вкладку студентов
        }

        // Меню - Приказы
        private void приказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new OrdersForm(_apiService);
            form.ShowDialog();
            _ = LoadDataAsync(); // Обновляем данные после закрытия
        }

        // Меню - Отчеты
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Форма отчетов в разработке", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
