using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class OrderCreateForm : Form
    {
        private readonly ApiService _apiService;
        private readonly EnrollmentOrderCreateViewModel _viewModel;
        private List<Group> _groups = new();
        private List<Direction> _directions = new();

        public OrderCreateForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            _viewModel = new EnrollmentOrderCreateViewModel();

            // Инициализация формы обучения
            cmbEducationForm.DataSource = new[]
            {
                new { Value = "full-time", Text = "Очная" },
                new { Value = "part-time", Text = "Заочная" },
                new { Value = "extramural", Text = "Дистанционная" }
            };
            cmbEducationForm.DisplayMember = "Text";
            cmbEducationForm.ValueMember = "Value";
        }

        private async void OrderCreateForm_Load(object sender, EventArgs e)
        {
            await LoadDirectionsAsync();
            dtpDate.Value = DateTime.Now;
            txtPrice.Text = "0";
            
            // Блокируем группу пока не выбрано направление
            cmbGroup.Enabled = false;
        }

        private async Task LoadDirectionsAsync()
        {
            _directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();
            
            // Отписываем событие перед установкой DataSource
            cmbDirection.SelectedIndexChanged -= cmbDirection_SelectedIndexChanged;
            
            cmbDirection.DataSource = _directions.Select(d => new { d.Id, d.Name, d.Code }).ToList();
            cmbDirection.DisplayMember = "Name";
            cmbDirection.ValueMember = "Id";
            cmbDirection.SelectedIndex = -1; // Сбрасываем выбор
            
            // Подписываем событие после установки DataSource
            cmbDirection.SelectedIndexChanged += cmbDirection_SelectedIndexChanged;
        }

        private async void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbDirection.SelectedIndex < 0 || cmbDirection.SelectedValue == null)
                return;
                
            if (cmbDirection.SelectedValue is Guid directionId && directionId != Guid.Empty)
            {
                await LoadGroupsByDirectionAsync(directionId);
                cmbGroup.Enabled = true;
            }
            else
            {
                cmbGroup.DataSource = null;
                cmbGroup.Enabled = false;
            }
        }

        private async Task LoadGroupsByDirectionAsync(Guid directionId)
        {
            _groups = (await _apiService.GetGroupsAsync() ?? new List<Group>())
                .Where(g => g.DirectionId == directionId).ToList();

            cmbGroup.DataSource = _groups.Select(g => new { g.Id, g.Name }).ToList();
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "Id";
        }

        private async Task LoadGroupsAsync()
        {
            _groups = await _apiService.GetGroupsAsync() ?? new List<Group>();
            cmbGroup.DataSource = _groups.Select(g => new { g.Id, g.Name }).ToList();
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "Id";
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            var lastName = txtLastName.Text.Trim();
            var name = txtName.Text.Trim();
            var patronymic = txtPatronymic.Text.Trim();
            
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите фамилию и имя студента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbGroup.SelectedValue is not Guid groupId || groupId == Guid.Empty)
            {
                MessageBox.Show("Выберите группу", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var student = new EnrollmentStudentViewModel
            {
                LastName = lastName,
                Name = name,
                Patronymic = patronymic,
                StudyBookNumber = (int)numStudyBook.Value,
                GroupId = groupId
            };

            _viewModel.Students.Add(student);
            UpdateStudentsGrid();

            // Очистка полей
            txtLastName.Clear();
            txtName.Clear();
            txtPatronymic.Clear();
            numStudyBook.Value = numStudyBook.Minimum;
            txtLastName.Focus();
        }

        private void UpdateStudentsGrid()
        {
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = _viewModel.Students.Select(s => new
            {
                s.LastName,
                s.Name,
                s.Patronymic,
                ФИО = s.FullName,
                s.StudyBookNumber,
                Группа = _groups.FirstOrDefault(g => g.Id == s.GroupId)?.Name ?? "Не указана"
            }).ToList();
        }

        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var index = dgvStudents.SelectedRows[0].Index;
                if (index >= 0 && index < _viewModel.Students.Count)
                {
                    _viewModel.Students.RemoveAt(index);
                    UpdateStudentsGrid();
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_viewModel.Students.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одного студента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                // Получаем форму обучения
                var educationForm = cmbEducationForm.SelectedValue?.ToString() ?? "full-time";
                
                // Получаем цену (если 0 или пусто, то null)
                string? price = null;
                if (decimal.TryParse(txtPrice.Text, out decimal priceValue) && priceValue > 0)
                {
                    price = priceValue.ToString();
                }

                var orderData = new EnrollmentOrderWithStudentsCreate
                {
                    Order = new OrderCreateData
                    {
                        Number = $"Приказ о зачислении от {dtpDate.Value:dd.MM.yyyy}",
                        Date = dtpDate.Value,
                        Type = OrderType.enrollment,
                        Reason = "Зачисление студентов"
                    },
                    EnrollmentOrder = new EnrollmentOrderCreateData
                    {
                        OrderId = Guid.NewGuid(),
                        EducationForm = educationForm,
                        Price = price
                    },
                    Students = _viewModel.Students.Select(s => new StudentForEnrollment
                    {
                        LastName = s.LastName,
                        Name = s.Name,
                        Patronymic = s.Patronymic,
                        StudyBookNumber = s.StudyBookNumber,
                        GroupId = s.GroupId
                    }).ToList()
                };

                var result = await _apiService.CreateEnrollmentOrderAsync(orderData);

                // Бэкенд возвращает пустой ответ при успешном создании
                // result == null означает успех (пустой ответ от сервера)
                MessageBox.Show("Приказ о зачислении успешно создан!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
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

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAddStudent.PerformClick();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры и backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
