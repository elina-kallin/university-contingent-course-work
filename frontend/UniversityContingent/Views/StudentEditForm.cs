using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class StudentEditForm : Form
    {
        private readonly ApiService _apiService;
        private readonly StudentEditViewModel _viewModel;
        private readonly bool _isEdit;
        private List<Group> _groups = new();

        public StudentEditForm(ApiService apiService, Student? student = null)
        {
            InitializeComponent();
            _apiService = apiService;
            _isEdit = student != null;

            _viewModel = student == null 
                ? new StudentEditViewModel() 
                : new StudentEditViewModel
                {
                    Id = student.Id,
                    FullName = student.FullName ?? string.Empty,
                    BirthDate = student.BirthDate,
                    GroupId = student.GroupId,
                    EnrollmentDate = student.EnrollmentDate
                };

            Text = _isEdit ? "Редактирование студента" : "Просмотр студента";
            
            // Если не режим редактирования, отключаем кнопку сохранения
            btnSave.Visible = _isEdit;
        }

        public StudentEditViewModel? ResultViewModel { get; private set; }

        private async void StudentEditForm_Load(object sender, EventArgs e)
        {
            await LoadGroupsAsync();
            BindData();
        }

        private async Task LoadGroupsAsync()
        {
            _groups = await _apiService.GetGroupsAsync() ?? new List<Group>();
            cmbGroup.DataSource = _groups.Select(g => new { g.Id, g.Name }).ToList();
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "Id";
        }

        private void BindData()
        {
            txtFullName.Text = _viewModel.FullName;
            dtpBirthDate.Value = _viewModel.BirthDate;
            dtpEnrollmentDate.Value = _viewModel.EnrollmentDate;
            
            if (!string.IsNullOrEmpty(_viewModel.GroupId) && _groups.Any())
            {
                cmbGroup.SelectedValue = _viewModel.GroupId;
            }
        }

        private void SaveViewModel()
        {
            _viewModel.FullName = txtFullName.Text.Trim();
            _viewModel.BirthDate = dtpBirthDate.Value;
            _viewModel.GroupId = cmbGroup.SelectedValue?.ToString() ?? string.Empty;
            _viewModel.EnrollmentDate = dtpEnrollmentDate.Value;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveViewModel();

            if (string.IsNullOrEmpty(_viewModel.FullName))
            {
                MessageBox.Show("Введите ФИО студента", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.GroupId))
            {
                MessageBox.Show("Выберите группу", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGroup.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                // Примечание: API бекенда не поддерживает прямое редактирование студентов
                // Студенты создаются/изменяются только через приказы
                MessageBox.Show("Редактирование студентов доступно только через приказы", 
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Сохранить";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
