using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class GroupsForm : Form
    {
        private readonly ApiService _apiService;
        private List<Group> _groups = new();
        private List<Direction> _directions = new();

        public GroupsForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void GroupsForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "Загрузка...";

            try
            {
                _groups = await _apiService.GetGroupsAsync() ?? new List<Group>();
                _directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();

                dgvGroups.DataSource = _groups.Select(g => new
                {
                    Id = g.Id,
                    Название = g.Name ?? "Не указана",
                    Направление = _directions.FirstOrDefault(d => d.Id == g.DirectionId)?.Name ?? "Не указано",
                    Курс = $"Курс {g.Course}"
                }).ToList();

                // Русификация заголовков и скрытие ID
                if (dgvGroups.Columns.Count > 0)
                {
                    dgvGroups.Columns["Id"].Visible = false;
                    dgvGroups.Columns["Название"].HeaderText = "Название группы";
                    dgvGroups.Columns["Направление"].HeaderText = "Направление";
                    dgvGroups.Columns["Курс"].HeaderText = "Курс";
                }

                lblStatus.Text = $"Загружено групп: {_groups.Count}";
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
            using var form = new GroupEditForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDataAsync();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvGroups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите группу для редактирования", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvGroups.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (!Guid.TryParse(idStr, out var id))
            {
                MessageBox.Show("Неверный ID группы", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var group = _groups.FirstOrDefault(g => g.Id == id);

            if (group != null)
            {
                using var form = new GroupEditForm(_apiService, group);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _ = LoadDataAsync();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvGroups.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите группу для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить группу?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var idStr = dgvGroups.SelectedRows[0].Cells["Id"].Value?.ToString();
                if (Guid.TryParse(idStr, out var id))
                {
                    var success = await _apiService.DeleteGroupAsync(id);
                    if (success)
                    {
                        MessageBox.Show("Группа успешно удалена", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _ = LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления группы", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadDataAsync();
        }

        private void dgvGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit.PerformClick();
            }
        }
    }
}
