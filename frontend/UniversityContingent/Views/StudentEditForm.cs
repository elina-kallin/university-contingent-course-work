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
                    LastName = student.LastName ?? string.Empty,
                    Name = student.Name ?? string.Empty,
                    Patronymic = student.Patronymic ?? string.Empty,
                    GroupId = student.GroupId,
                    EnrollmentDate = student.EnrollmentDate ?? DateTime.Now
                };

            Text = _isEdit ? "Редактирование студента" : "Просмотр студента";
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
            txtLastName.Text = _viewModel.LastName;
            txtName.Text = _viewModel.Name;
            txtPatronymic.Text = _viewModel.Patronymic;
            dtpEnrollmentDate.Value = _viewModel.EnrollmentDate;
            
            if (_viewModel.GroupId != Guid.Empty && _groups.Any())
            {
                cmbGroup.SelectedValue = _viewModel.GroupId;
            }
        }

        private void SaveViewModel()
        {
            _viewModel.LastName = txtLastName.Text.Trim();
            _viewModel.Name = txtName.Text.Trim();
            _viewModel.Patronymic = txtPatronymic.Text.Trim();
            _viewModel.GroupId = cmbGroup.SelectedValue is Guid groupId ? groupId : Guid.Empty;
            _viewModel.EnrollmentDate = dtpEnrollmentDate.Value;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveViewModel();

            if (string.IsNullOrEmpty(_viewModel.LastName))
            {
                MessageBox.Show("Введите фамилию студента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Введите имя студента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (_viewModel.GroupId == Guid.Empty)
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
                var studentData = new Student
                {
                    Id = _viewModel.Id,
                    LastName = _viewModel.LastName,
                    Name = _viewModel.Name,
                    Patronymic = _viewModel.Patronymic,
                    GroupId = _viewModel.GroupId,
                    EnrollmentDate = _viewModel.EnrollmentDate,
                    Status = StudentStatus.study
                };

                Student? result;
                if (_isEdit)
                {
                    result = await _apiService.UpdateStudentAsync(_viewModel.Id, studentData);
                }
                else
                {
                    result = await _apiService.CreateStudentAsync(studentData);
                }

                if (result != null)
                {
                    ResultViewModel = _viewModel;
                    MessageBox.Show($"Студент успешно {_viewModel.Id == Guid.Empty ? "создан" : "обновлен"}!",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения студента", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
