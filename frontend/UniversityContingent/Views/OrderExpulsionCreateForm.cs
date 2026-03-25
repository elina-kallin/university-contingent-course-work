using UniversityContingent.Controller.Api;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class OrderExpulsionCreateForm : Form
    {
        private readonly ApiService _apiService;
        private List<Group> _groups = new();
        private List<Direction> _directions = new();
        private List<Student> _students = new();
        private List<Guid> _selectedStudentIds = new();

        public OrderExpulsionCreateForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OrderExpulsionCreateForm_Load(object sender, EventArgs e)
        {
            await LoadDirectionsAsync();
            dtpExpulsionDate.Value = DateTime.Now;

            // Инициализация причин отчисления
            cmbReason.DataSource = new[]
            {
                new { Value = "debts", Text = "Академическая задолженность" },
                new { Value = "health", Text = "По состоянию здоровья" },
                new { Value = "personal_reason", Text = "По личным обстоятельствам" },
                new { Value = "own", Text = "По собственному желанию" }
            };
            cmbReason.DisplayMember = "Text";
            cmbReason.ValueMember = "Value";

            // Блокируем все комбобоксы пока не выбрано направление
            cmbGroup.Enabled = false;
            btnAddStudent.Enabled = false;
        }

        private async Task LoadDirectionsAsync()
        {
            _directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();
            
            // Отписываем событие перед установкой DataSource
            cmbDirection.SelectedIndexChanged -= cmbDirection_SelectedIndexChanged;
            
            cmbDirection.DataSource = _directions.Select(d => new { d.Id, d.Name, d.Code }).ToList();
            cmbDirection.DisplayMember = "Name";
            cmbDirection.ValueMember = "Id";
            cmbDirection.SelectedIndex = -1;
            
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
                // Загружаем группы выбранного направления
                var allGroups = await _apiService.GetGroupsAsync() ?? new List<Group>();
                _groups = allGroups.Where(g => g.DirectionId == directionId).ToList();

                if (_groups.Any())
                {
                    // Отписываем событие перед установкой DataSource
                    cmbGroup.SelectedIndexChanged -= cmbGroup_SelectedIndexChanged;
                    
                    cmbGroup.DataSource = _groups.Select(g => new { g.Id, g.Name }).ToList();
                    cmbGroup.DisplayMember = "Name";
                    cmbGroup.ValueMember = "Id";
                    cmbGroup.Enabled = true;
                    cmbGroup.SelectedIndex = -1;
                    
                    // Подписываем событие после установки DataSource
                    cmbGroup.SelectedIndexChanged += cmbGroup_SelectedIndexChanged;
                }
                else
                {
                    cmbGroup.DataSource = null;
                    cmbGroup.Enabled = false;
                }

                // Сбрасываем список студентов
                lstStudents.Items.Clear();
                _students.Clear();
                btnAddStudent.Enabled = false;
            }
            else
            {
                cmbGroup.DataSource = null;
                cmbGroup.Enabled = false;
                lstStudents.Items.Clear();
                _students.Clear();
                btnAddStudent.Enabled = false;
            }
        }

        private async void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbGroup.SelectedIndex < 0 || cmbGroup.SelectedValue == null)
                return;
                
            if (cmbGroup.SelectedValue is Guid groupId && groupId != Guid.Empty)
            {
                await LoadStudentsAsync(groupId);
            }
            else
            {
                lstStudents.Items.Clear();
                _students.Clear();
                btnAddStudent.Enabled = false;
            }
        }

        private async Task LoadStudentsAsync(Guid groupId)
        {
            var allStudents = await _apiService.GetStudentsAsync() ?? new List<Student>();
            _students = allStudents.Where(s => s.GroupId == groupId && s.Status == StudentStatus.study).ToList();

            lstStudents.Items.Clear();
            foreach (var student in _students)
            {
                lstStudents.Items.Add($"{student.LastName} {student.Name} {student.Patronymic} ({student.StudyBookNumber})");
            }

            btnAddStudent.Enabled = _students.Any();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (lstStudents.SelectedItem == null)
            {
                MessageBox.Show("Выберите студента из списка", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndex = lstStudents.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < _students.Count)
            {
                var student = _students[selectedIndex];
                
                // Проверяем, не добавлен ли уже студент
                if (_selectedStudentIds.Contains(student.Id))
                {
                    MessageBox.Show("Этот студент уже добавлен в приказ", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _selectedStudentIds.Add(student.Id);
                UpdateSelectedStudentsGrid();

                lstStudents.ClearSelected();
            }
        }

        private void UpdateSelectedStudentsGrid()
        {
            dgvSelectedStudents.DataSource = null;
            var selectedStudents = _students.Where(s => _selectedStudentIds.Contains(s.Id)).ToList();
            dgvSelectedStudents.DataSource = selectedStudents.Select(s => new
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
            if (dgvSelectedStudents.SelectedRows.Count > 0)
            {
                var index = dgvSelectedStudents.SelectedRows[0].Index;
                var selectedStudents = _students.Where(s => _selectedStudentIds.Contains(s.Id)).ToList();
                if (index >= 0 && index < selectedStudents.Count)
                {
                    var student = selectedStudents[index];
                    _selectedStudentIds.Remove(student.Id);
                    UpdateSelectedStudentsGrid();
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedStudentIds.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одного студента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbReason.SelectedValue == null)
            {
                MessageBox.Show("Выберите причину отчисления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                var reasonText = cmbReason.Text;

                var orderData = new ExpulsionOrderWithStudentsCreate
                {
                    Order = new OrderCreateData
                    {
                        Number = $"Приказ об отчислении от {dtpExpulsionDate.Value:dd.MM.yyyy}",
                        Date = DateTime.Now,
                        Type = OrderType.expulsion,
                        Reason = reasonText
                    },
                    ExpulsionOrder = new ExpulsionOrderCreateData
                    {
                        OrderId = Guid.NewGuid(),
                        ExpulsionDate = dtpExpulsionDate.Value,
                        ExpulsionReason = cmbReason.SelectedValue?.ToString() ?? "debts"
                    },
                    StudentIds = _selectedStudentIds
                };

                var result = await _apiService.CreateExpulsionOrderAsync(orderData);

                MessageBox.Show("Приказ об отчислении успешно создан!", "Информация",
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
    }
}
