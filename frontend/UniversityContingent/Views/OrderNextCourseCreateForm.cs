using UniversityContingent.Controller.Api;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class OrderNextCourseCreateForm : Form
    {
        private readonly ApiService _apiService;
        private List<Group> _groups = new();
        private List<Direction> _directions = new();
        private List<Student> _students = new();
        private List<Guid> _selectedStudentIds = new();

        public OrderNextCourseCreateForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OrderNextCourseCreateForm_Load(object sender, EventArgs e)
        {
            await LoadDirectionsAsync();
            dtpDate.Value = DateTime.Now;

            // Блокируем все комбобоксы пока не выбрано направление
            cmbFromCourse.Enabled = false;
            cmbToCourse.Enabled = false;
            cmbFromGroup.Enabled = false;
            cmbToGroup.Enabled = false;
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
            cmbDirection.SelectedIndex = -1; // Сбрасываем выбор
            
            // Подписываем событие после установки DataSource
            cmbDirection.SelectedIndexChanged += cmbDirection_SelectedIndexChanged;
        }

        private async void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbDirection.SelectedIndex < 0 || cmbDirection.SelectedValue == null)
                return;
                
            Console.WriteLine($"[DEBUG] cmbDirection_SelectedIndexChanged: SelectedValue={cmbDirection.SelectedValue}");
                
            if (cmbDirection.SelectedValue is Guid directionId && directionId != Guid.Empty)
            {
                // Загружаем группы выбранного направления
                var allGroups = await _apiService.GetGroupsAsync() ?? new List<Group>();
                _groups = allGroups.Where(g => g.DirectionId == directionId).ToList();

                // Заполняем комбобокс курсов (с которых можно перевести)
                var availableCourses = _groups.Select(g => g.Course).Distinct().OrderBy(c => c).ToList();
                
                if (availableCourses.Any())
                {
                    // Отписываем событие перед установкой DataSource
                    cmbFromCourse.SelectedIndexChanged -= cmbFromCourse_SelectedIndexChanged;
                    
                    cmbFromCourse.DataSource = availableCourses.Select(c => new { Value = c, Text = $"{c} курс" }).ToList();
                    cmbFromCourse.DisplayMember = "Text";
                    cmbFromCourse.ValueMember = "Value";
                    cmbFromCourse.Enabled = true;
                    cmbFromCourse.SelectedIndex = -1; // Сбрасываем выбор
                    
                    // Подписываем событие после установки DataSource
                    cmbFromCourse.SelectedIndexChanged += cmbFromCourse_SelectedIndexChanged;
                }
                else
                {
                    cmbFromCourse.DataSource = null;
                    cmbFromCourse.Enabled = false;
                }

                // Сбрасываем остальные комбобоксы
                cmbToCourse.DataSource = null;
                cmbToCourse.Enabled = false;
                cmbFromGroup.DataSource = null;
                cmbFromGroup.Enabled = false;
                cmbToGroup.DataSource = null;
                cmbToGroup.Enabled = false;
                btnAddStudent.Enabled = false;
                lstStudents.Items.Clear();
                _students.Clear();
                _selectedStudentIds.Clear();
                dgvSelectedStudents.DataSource = null;
            }
            else
            {
                cmbFromCourse.DataSource = null;
                cmbFromCourse.Enabled = false;
                cmbToCourse.DataSource = null;
                cmbToCourse.Enabled = false;
                cmbFromGroup.DataSource = null;
                cmbFromGroup.Enabled = false;
                cmbToGroup.DataSource = null;
                cmbToGroup.Enabled = false;
                btnAddStudent.Enabled = false;
                lstStudents.Items.Clear();
                _students.Clear();
                _selectedStudentIds.Clear();
                dgvSelectedStudents.DataSource = null;
            }
        }

        private async void cmbFromCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbFromCourse.SelectedIndex < 0 || cmbFromCourse.SelectedValue == null)
            {
                Console.WriteLine($"[DEBUG] cmbFromCourse: SelectedIndex={cmbFromCourse.SelectedIndex}, SelectedValue={cmbFromCourse.SelectedValue}");
                return;
            }
                
            Console.WriteLine($"[DEBUG] cmbFromCourse_SelectedIndexChanged triggered");
            Console.WriteLine($"[DEBUG] SelectedValue: {cmbFromCourse.SelectedValue}, Type: {cmbFromCourse.SelectedValue?.GetType()}");
            
            if (cmbFromCourse.SelectedValue is int fromCourse && fromCourse > 0)
            {
                Console.WriteLine($"[DEBUG] fromCourse = {fromCourse}");
                
                // Заполняем комбобокс курсов (на которые можно перевести)
                // Доступны курсы больше текущего
                var availableToCourses = _groups.Where(g => g.Course > fromCourse)
                    .Select(g => g.Course).Distinct().OrderBy(c => c).ToList();

                Console.WriteLine($"[DEBUG] availableToCourses count: {availableToCourses.Count}");

                if (availableToCourses.Any())
                {
                    // Отписываем событие перед установкой DataSource
                    cmbToCourse.SelectedIndexChanged -= cmbToCourse_SelectedIndexChanged;
                    
                    cmbToCourse.DataSource = availableToCourses.Select(c => new { Value = c, Text = $"{c} курс" }).ToList();
                    cmbToCourse.DisplayMember = "Text";
                    cmbToCourse.ValueMember = "Value";
                    cmbToCourse.Enabled = true;
                    cmbToCourse.SelectedIndex = -1; // Сбрасываем выбор
                    
                    // Подписываем событие после установки DataSource
                    cmbToCourse.SelectedIndexChanged += cmbToCourse_SelectedIndexChanged;
                    
                    Console.WriteLine($"[DEBUG] cmbToCourse enabled");
                }
                else
                {
                    cmbToCourse.DataSource = null;
                    cmbToCourse.Enabled = false;
                    Console.WriteLine($"[DEBUG] cmbToCourse disabled (no courses)");
                }

                // Заполняем комбобокс групп (с которых переводим)
                var fromGroups = _groups.Where(g => g.Course == fromCourse).ToList();
                Console.WriteLine($"[DEBUG] fromGroups count: {fromGroups.Count}");
                
                // Отписываем событие перед установкой DataSource
                cmbFromGroup.SelectedIndexChanged -= cmbFromGroup_SelectedIndexChanged;
                
                cmbFromGroup.DataSource = fromGroups.Select(g => new { g.Id, g.Name }).ToList();
                cmbFromGroup.DisplayMember = "Name";
                cmbFromGroup.ValueMember = "Id";
                cmbFromGroup.Enabled = true;
                cmbFromGroup.SelectedIndex = -1; // Сбрасываем выбор
                
                // Подписываем событие после установки DataSource
                cmbFromGroup.SelectedIndexChanged += cmbFromGroup_SelectedIndexChanged;
                
                Console.WriteLine($"[DEBUG] cmbFromGroup enabled");

                // Сбрасываем комбобокс групп (на которые переводим)
                cmbToGroup.DataSource = null;
                cmbToGroup.Enabled = false;
                btnAddStudent.Enabled = false;
            }
            else
            {
                Console.WriteLine($"[DEBUG] Invalid fromCourse value");
                cmbToCourse.DataSource = null;
                cmbToCourse.Enabled = false;
                cmbFromGroup.DataSource = null;
                cmbFromGroup.Enabled = false;
                cmbToGroup.DataSource = null;
                cmbToGroup.Enabled = false;
                btnAddStudent.Enabled = false;
            }
        }

        private async void cmbToCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbToCourse.SelectedIndex < 0 || cmbToCourse.SelectedValue == null)
                return;
                
            if (cmbToCourse.SelectedValue is int toCourse && toCourse > 0)
            {
                // Заполняем комбобокс групп (на которые переводим)
                var toGroups = _groups.Where(g => g.Course == toCourse).ToList();
                cmbToGroup.DataSource = toGroups.Select(g => new { g.Id, g.Name }).ToList();
                cmbToGroup.DisplayMember = "Name";
                cmbToGroup.ValueMember = "Id";
                cmbToGroup.Enabled = true;

                // Загружаем студентов для выбранной группы (с которой переводим)
                await LoadStudentsAsync();
            }
            else
            {
                cmbToGroup.DataSource = null;
                cmbToGroup.Enabled = false;
                btnAddStudent.Enabled = false;
            }
        }

        private async void cmbFromGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем событие если оно сработало до выбора пользователем
            if (cmbFromGroup.SelectedIndex < 0 || cmbFromGroup.SelectedValue == null)
                return;
                
            // Загружаем студентов для выбранной группы
            await LoadStudentsAsync();
        }

        private async Task LoadStudentsAsync()
        {
            if (cmbFromGroup.SelectedValue is not Guid fromGroupId || fromGroupId == Guid.Empty)
            {
                lstStudents.Items.Clear();
                _students.Clear();
                btnAddStudent.Enabled = false;
                return;
            }

            var allStudents = await _apiService.GetStudentsAsync() ?? new List<Student>();
            _students = allStudents.Where(s => s.GroupId == fromGroupId && s.Status == StudentStatus.study).ToList();

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

                // Не удаляем студента из списка, но помечаем как добавленного
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

            if (cmbFromCourse.SelectedValue is not int fromCourse || cmbToCourse.SelectedValue is not int toCourse)
            {
                MessageBox.Show("Выберите курсы перевода", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                var orderData = new NextCourseOrderWithStudentsCreate
                {
                    Order = new OrderCreateData
                    {
                        Number = $"Приказ о переводе на следующий курс от {dtpDate.Value:dd.MM.yyyy}",
                        Date = dtpDate.Value,
                        Type = OrderType.next_course,
                        Reason = "В связи с успешным завершением промежуточной аттестации"
                    },
                    NextCourseOrder = new NextCourseOrderCreateData
                    {
                        OrderId = Guid.NewGuid(),
                        FromCourse = fromCourse,
                        ToCourse = toCourse
                    },
                    StudentIds = _selectedStudentIds
                };

                var result = await _apiService.CreateNextCourseOrderAsync(orderData);

                MessageBox.Show("Приказ о переводе на следующий курс успешно создан!", "Информация",
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
