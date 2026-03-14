using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class DirectionsForm : Form
    {
        private readonly ApiService _apiService;
        private List<Direction> _directions = new();
        private List<Faculty> _faculties = new();

        public DirectionsForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void DirectionsForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "Загрузка...";

            try
            {
                _directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();
                _faculties = await _apiService.GetFacultiesAsync() ?? new List<Faculty>();

                dgvDirections.DataSource = _directions.Select(d => new
                {
                    Id = d.Id,
                    Название = d.Name ?? "Не указано",
                    Код = d.Code ?? "Не указан",
                    Факультет = _faculties.FirstOrDefault(f => f.Id == d.FacultyId)?.Name ?? "Не указан",
                    СрокОбучения = $"{d.StudyDurationYears} лет"
                }).ToList();

                // Русификация заголовков и скрытие ID
                if (dgvDirections.Columns.Count > 0)
                {
                    dgvDirections.Columns["Id"].Visible = false;
                    dgvDirections.Columns["Название"].HeaderText = "Наименование";
                    dgvDirections.Columns["Код"].HeaderText = "Код";
                    dgvDirections.Columns["Факультет"].HeaderText = "Факультет";
                    dgvDirections.Columns["СрокОбучения"].HeaderText = "Срок обучения";
                }

                lblStatus.Text = $"Загружено направлений: {_directions.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Обновить";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form = new DirectionEditForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDirections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите направление для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvDirections.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (!Guid.TryParse(idStr, out var id))
            {
                MessageBox.Show("Неверный ID направления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var direction = _directions.FirstOrDefault(d => d.Id == id);

            if (direction != null)
            {
                using var form = new DirectionEditForm(_apiService, direction);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _ = LoadDataAsync();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDirections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите направление для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить направление?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var idStr = dgvDirections.SelectedRows[0].Cells["Id"].Value?.ToString();
                if (Guid.TryParse(idStr, out var id))
                {
                    var success = await _apiService.DeleteDirectionAsync(id);
                    if (success)
                    {
                        MessageBox.Show("Направление успешно удалено", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _ = LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления направления", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void dgvDirections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit.PerformClick();
            }
        }
    }
}
