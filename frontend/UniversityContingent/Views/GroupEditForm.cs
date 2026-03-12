using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class GroupEditForm : Form
    {
        private readonly ApiService _apiService;
        private readonly GroupEditViewModel _viewModel;
        private readonly bool _isEdit;
        private List<Direction> _directions = new();

        public GroupEditForm(ApiService apiService, Group? group = null)
        {
            InitializeComponent();
            _apiService = apiService;
            _isEdit = group != null;

            _viewModel = group == null 
                ? new GroupEditViewModel() 
                : new GroupEditViewModel
                {
                    Id = group.Id,
                    Name = group.Name ?? string.Empty,
                    DirectionId = group.DirectionId,
                    Course = group.Course,
                    Year = group.Year
                };

            Text = _isEdit ? "Редактирование группы" : "Добавление группы";
        }

        public GroupEditViewModel? ResultViewModel { get; private set; }

        private async void GroupEditForm_Load(object sender, EventArgs e)
        {
            await LoadDirectionsAsync();
            BindData();
        }

        private async Task LoadDirectionsAsync()
        {
            _directions = await _apiService.GetDirectionsAsync() ?? new List<Direction>();
            cmbDirection.DataSource = _directions.Select(d => new { d.Id, d.Name }).ToList();
            cmbDirection.DisplayMember = "Name";
            cmbDirection.ValueMember = "Id";
        }

        private void BindData()
        {
            txtName.Text = _viewModel.Name;
            numCourse.Value = _viewModel.Course;
            numYear.Value = _viewModel.Year;
            
            if (!string.IsNullOrEmpty(_viewModel.DirectionId) && _directions.Any())
            {
                cmbDirection.SelectedValue = _viewModel.DirectionId;
            }
        }

        private void SaveViewModel()
        {
            _viewModel.Name = txtName.Text.Trim();
            _viewModel.DirectionId = cmbDirection.SelectedValue?.ToString() ?? string.Empty;
            _viewModel.Course = (int)numCourse.Value;
            _viewModel.Year = (int)numYear.Value;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveViewModel();

            if (string.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Введите название группы", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.DirectionId))
            {
                MessageBox.Show("Выберите направление", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDirection.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                var group = new Group
                {
                    Name = _viewModel.Name,
                    DirectionId = _viewModel.DirectionId,
                    Course = _viewModel.Course,
                    Year = _viewModel.Year
                };

                Group? result;
                if (_isEdit)
                {
                    result = await _apiService.UpdateGroupAsync(_viewModel.Id, group);
                }
                else
                {
                    result = await _apiService.CreateGroupAsync(group);
                }

                if (result != null)
                {
                    ResultViewModel = new GroupEditViewModel
                    {
                        Id = result.Id,
                        Name = result.Name ?? string.Empty,
                        DirectionId = result.DirectionId,
                        Course = result.Course,
                        Year = result.Year
                    };
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения группы", "Ошибка",
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
