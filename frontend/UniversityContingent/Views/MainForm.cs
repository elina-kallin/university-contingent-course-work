using UniversityContingent.Controller.Api;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class MainForm : Form
    {
        private readonly ApiService _apiService;
        private readonly LoginResponse _userResponse;
        private bool _isLoading = false; // Флаг для предотвращения рекурсивных вызовов
        private Faculty? _deanFaculty; // Факультет декана

        public MainForm(ApiService apiService, LoginResponse userResponse)
        {
            InitializeComponent();
            _apiService = apiService;
            _userResponse = userResponse;

            // Отображаем приветствие с ФИО декана
            var displayName = !string.IsNullOrEmpty(userResponse.FullName)
                ? userResponse.FullName
                : userResponse.UserLogin;
            lblUserName.Text = $"Здравствуйте, {displayName}";
            lblRole.Text = userResponse.Role ?? "Пользователь";
            menuStrip.Text = displayName;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            // Загружаем факультет декана и направления
            await LoadDeanFacultyAsync();
            await LoadDirectionsAsync();
        }

        private async Task LoadDeanFacultyAsync()
        {
            try
            {
                // Получаем все факультеты и находим факультет декана
                // (в реальном проекте нужно получать через /auth/me или из токена)
                var faculties = await _apiService.GetFacultiesAsync() ?? new List<Faculty>();

                // Для демонстрации берём первый факультет
                // В реальности нужно знать faculty_id декана
                _deanFaculty = faculties.FirstOrDefault();

                if (_deanFaculty != null)
                {
                    lblFaculty.Text = $"Факультет: {_deanFaculty.Name}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error loading faculty: {ex.Message}");
            }
        }

        private async Task LoadDirectionsAsync()
        {
            var directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();

            // Добавляем пустой элемент "Выбрать направление"
            var directionList = new List<dynamic> { new { Id = Guid.Empty, Name = "— Выберите направление —" } };
            directionList.AddRange(directions.Select(d => new { d.Id, d.Name }));

            // Отписываем событие перед установкой DataSource
            cmbDirections.SelectedIndexChanged -= cmbDirections_SelectedIndexChanged;

            cmbDirections.DataSource = directionList;
            cmbDirections.DisplayMember = "Name";
            cmbDirections.ValueMember = "Id";
            cmbDirections.SelectedIndex = 0; // Выбираем первый элемент (пустой)
            cmbDirections.Enabled = true;

            // Подписываем событие после установки DataSource
            cmbDirections.SelectedIndexChanged += cmbDirections_SelectedIndexChanged;

            // Группы и студенты заблокированы до выбора направления
            cmbGroups.Enabled = false;
        }

        private async Task LoadDataAsync()
        {
            if (_isLoading) return; // Защита от рекурсивных вызовов

            try
            {
                _isLoading = true;

                // Загружаем все данные
                var groups = await _apiService.GetGroupsAsync() ?? new List<Group>();
                var students = await _apiService.GetStudentsAsync() ?? new List<Student>();

                // Фильтруем группы по выбранному направлению
                var selectedDirectionId = cmbDirections.SelectedValue is Guid dirId && dirId != Guid.Empty ? dirId : Guid.Empty;
                var filteredGroups = selectedDirectionId != Guid.Empty
                    ? groups.Where(g => g.DirectionId == selectedDirectionId).ToList()
                    : groups;

                // Сохраняем текущий выбранный ID группы
                var currentSelectedGroupId = cmbGroups.SelectedValue is Guid grpId ? grpId : Guid.Empty;

                // Отключаем событие чтобы не вызывать рекурсивную загрузку
                cmbGroups.SelectedIndexChanged -= cmbGroups_SelectedIndexChanged;

                // Обновляем DataSource групп
                cmbGroups.DataSource = filteredGroups.Select(g => new { g.Id, g.Name }).ToList();
                cmbGroups.DisplayMember = "Name";
                cmbGroups.ValueMember = "Id";

                // Восстанавливаем выбор если группа есть в новом списке
                if (currentSelectedGroupId != Guid.Empty && filteredGroups.Any(g => g.Id == currentSelectedGroupId))
                {
                    cmbGroups.SelectedValue = currentSelectedGroupId;
                }

                cmbGroups.Enabled = selectedDirectionId != Guid.Empty && filteredGroups.Count > 0;

                // Включаем событие обратно
                cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;

                // Фильтруем студентов по выбранной группе или направлению
                var selectedGroupId = cmbGroups.SelectedValue is Guid selectedGrpId ? selectedGrpId : Guid.Empty;
                List<Student> filteredStudents;

                if (selectedGroupId != Guid.Empty)
                {
                    filteredStudents = students.Where(s => s.GroupId == selectedGroupId).ToList();
                }
                else if (selectedDirectionId != Guid.Empty)
                {
                    var directionGroupIds = filteredGroups.Select(g => g.Id).ToHashSet();
                    filteredStudents = students.Where(s => directionGroupIds.Contains(s.GroupId)).ToList();
                }
                else
                {
                    filteredStudents = students;
                }

                dgvStudents.DataSource = filteredStudents.Select(s => new
                {
                    Id = s.Id,
                    ФИО = $"{s.LastName} {s.Name} {s.Patronymic}".Trim(),
                    Зачетка = s.StudyBookNumber,
                    Группа = groups.FirstOrDefault(g => g.Id == s.GroupId)?.Name ?? "Не указана",
                    ДатаЗачисления = s.EnrollmentDate?.ToString("dd.MM.yyyy") ?? "Не указана",
                    Статус = s.Status switch
                    {
                        StudentStatus.study => "Обучается",
                        StudentStatus.expelled => "Отчислен",
                        StudentStatus.academic_leave => "Академический отпуск",
                        _ => "Не указано"
                    }
                }).ToList();

                // Русификация заголовков колонок
                if (dgvStudents.Columns.Count > 0)
                {
                    dgvStudents.Columns["Id"].Visible = false;
                    dgvStudents.Columns["ФИО"].HeaderText = "ФИО";
                    dgvStudents.Columns["Зачетка"].HeaderText = "№ зачетки";
                    dgvStudents.Columns["Группа"].HeaderText = "Группа";
                    dgvStudents.Columns["ДатаЗачисления"].HeaderText = "Дата зачисления";
                    dgvStudents.Columns["Статус"].HeaderText = "Статус";
                }

                lblStatus.Text = $"Загружено студентов: {filteredStudents.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async void btnLogout_Click(object sender, EventArgs e)
        {
            await _apiService.LogoutAsync();
            Application.Restart();
        }

        private async void cmbDirections_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Игнорируем если SelectedValue null или Guid.Empty (элемент "— Выберите направление —")
            if (cmbDirections.SelectedValue == null || cmbDirections.SelectedValue is Guid dirId && dirId == Guid.Empty)
                return;

            // При выборе направления загружаем группы этого направления
            if (!_isLoading && cmbDirections.SelectedValue is Guid selectedDirId && selectedDirId != Guid.Empty)
            {
                await LoadDataAsync();
            }
        }

        private async void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            // При выборе группы загружаем студентов этой группы
            if (!_isLoading && cmbGroups.SelectedValue != null && cmbGroups.SelectedValue is Guid)
            {
                await LoadDataAsync();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvStudents.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (Guid.TryParse(idStr, out var id))
            {
                await EditStudentAsync(id);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите студента для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvStudents.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (Guid.TryParse(idStr, out var id))
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого студента?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = await _apiService.DeleteStudentAsync(id);
                    if (success)
                    {
                        MessageBox.Show("Студент успешно удалён", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления студента", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async Task EditStudentAsync(Guid id)
        {
            try
            {
                var student = await _apiService.GetStudentAsync(id);
                if (student != null)
                {
                    using var form = new StudentEditForm(_apiService, student);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        await LoadDataAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Студент не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void создатьПриказОЗачисленииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new OrderCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void создатьПриказОбОтчисленииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new OrderExpulsionCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void создатьПриказОПереводеНаСледующийКурсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new OrderNextCourseCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создание приказа о переводе на другое направление в разработке", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void создатьПриказОбАкадемическомОтпускеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создание приказа об академическом отпуске в разработке", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void просмотретьВсеПриказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new OrdersForm(_apiService);
            form.ShowDialog();
            _ = LoadDataAsync();
        }

        // Меню - Отчеты
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Форма отчетов в разработке", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
