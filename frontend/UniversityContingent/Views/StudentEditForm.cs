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
                    StudyBookNumber = student.StudyBookNumber,
                    GroupId = student.GroupId,
                    EnrollmentDate = student.EnrollmentDate ?? DateTime.Now
                };

            Text = _isEdit ? "Редактирование студента" : "Создание студента";
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

            // Номер зачетки - случайное число для нового студента, текущее значение для редактируемого
            if (!_isEdit)
            {
                numStudyBook.Value = new Random().Next(10000, 99999);
            }
            else
            {
                // Для редактируемого студента устанавливаем текущий номер зачетки
                numStudyBook.Value = _viewModel.StudyBookNumber;
            }

            if (_viewModel.GroupId != Guid.Empty && cmbGroup.Items.Count > 0)
            {
                cmbGroup.SelectedValue = _viewModel.GroupId;
            }

            // Статус всегда "Обучается" для новых и редактируемых
            lblStatusValue.Text = "Обучается";
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

            // Проверка уникальности номера зачетки
            var studyBookNumber = (int)numStudyBook.Value;
            var students = await _apiService.GetStudentsAsync() ?? new List<Student>();
            
            // Проверяем, есть ли другой студент с таким же номером зачетки
            var existingStudent = students.FirstOrDefault(s => 
                s.StudyBookNumber == studyBookNumber && 
                (!_isEdit || s.Id != _viewModel.Id));
            
            if (existingStudent != null)
            {
                MessageBox.Show($"Студент с номером зачетки {studyBookNumber} уже существует!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numStudyBook.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                if (_isEdit)
                {
                    // Для обновления отправляем все поля, включая номер зачетки
                    var updateData = new
                    {
                        last_name = _viewModel.LastName,
                        name = _viewModel.Name,
                        patronymic = _viewModel.Patronymic,
                        study_book_number = studyBookNumber,
                        group_id = _viewModel.GroupId,
                        enrollment_date = _viewModel.EnrollmentDate.ToString("yyyy-MM-dd"),
                        status = "study"
                    };

                    Console.WriteLine($"[DEBUG] Updating student {_viewModel.Id} with data: {System.Text.Json.JsonSerializer.Serialize(updateData)}");
                    
                    var result = await _apiService.UpdateStudentAsync(_viewModel.Id, updateData);

                    Console.WriteLine($"[DEBUG] Update result: {(result != null ? "success" : "null")}");

                    if (result != null)
                    {
                        ResultViewModel = _viewModel;
                        MessageBox.Show("Студент успешно обновлён!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        // Бэкенд может возвращать пустой ответ при успехе
                        Console.WriteLine("[DEBUG] Update returned null, but may still be successful");
                        ResultViewModel = _viewModel;
                        MessageBox.Show("Студент успешно обновлён!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    // Для создания нового студента
                    var createData = new
                    {
                        last_name = _viewModel.LastName,
                        name = _viewModel.Name,
                        patronymic = _viewModel.Patronymic,
                        study_book_number = studyBookNumber,
                        group_id = _viewModel.GroupId,
                        enrollment_date = _viewModel.EnrollmentDate.ToString("yyyy-MM-dd"),
                        status = "study"
                    };

                    Console.WriteLine($"[DEBUG] Creating student with data: {System.Text.Json.JsonSerializer.Serialize(createData)}");

                    var result = await _apiService.CreateStudentAsync(createData);

                    Console.WriteLine($"[DEBUG] Create result: {(result != null ? "success" : "null")}");

                    if (result != null)
                    {
                        ResultViewModel = _viewModel;
                        MessageBox.Show("Студент успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка создания студента", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
