using UniversityContingent.Controller.Api;
using UniversityContingent.Controller.ViewModels;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class DirectionEditForm : Form
    {
        private readonly ApiService _apiService;
        private readonly DirectionEditViewModel _viewModel;
        private readonly bool _isEdit;
        private List<Faculty> _faculties = new();

        public DirectionEditForm(ApiService apiService, Direction? direction = null)
        {
            InitializeComponent();
            _apiService = apiService;
            _isEdit = direction != null;

            _viewModel = direction == null 
                ? new DirectionEditViewModel() 
                : new DirectionEditViewModel
                {
                    Id = direction.Id,
                    Name = direction.Name ?? string.Empty,
                    Code = direction.Code ?? string.Empty,
                    FacultyId = direction.FacultyId,
                    EducationForm = direction.EducationForm
                };

            Text = _isEdit ? "Редактирование направления" : "Добавление направления";
        }

        public DirectionEditViewModel? ResultViewModel { get; private set; }

        private async void DirectionEditForm_Load(object sender, EventArgs e)
        {
            await LoadFacultiesAsync();
            BindData();
        }

        private async Task LoadFacultiesAsync()
        {
            _faculties = await _apiService.GetFacultiesAsync() ?? new List<Faculty>();
            cmbFaculty.DataSource = _faculties.Select(f => new { f.Id, f.Name }).ToList();
            cmbFaculty.DisplayMember = "Name";
            cmbFaculty.ValueMember = "Id";
        }

        private void BindData()
        {
            txtName.Text = _viewModel.Name;
            txtCode.Text = _viewModel.Code;
            
            if (!string.IsNullOrEmpty(_viewModel.FacultyId) && _faculties.Any())
            {
                cmbFaculty.SelectedValue = _viewModel.FacultyId;
            }

            cmbEducationForm.DataSource = Enum.GetValues(typeof(EducationForm));
            cmbEducationForm.SelectedItem = _viewModel.EducationForm;
        }

        private void SaveViewModel()
        {
            _viewModel.Name = txtName.Text.Trim();
            _viewModel.Code = txtCode.Text.Trim();
            _viewModel.FacultyId = cmbFaculty.SelectedValue?.ToString() ?? string.Empty;
            _viewModel.EducationForm = cmbEducationForm.SelectedItem is EducationForm form 
                ? form 
                : EducationForm.full_time;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            SaveViewModel();

            if (string.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Введите наименование направления", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_viewModel.FacultyId))
            {
                MessageBox.Show("Выберите факультет", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFaculty.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Сохранение...";

            try
            {
                var direction = new Direction
                {
                    Name = _viewModel.Name,
                    Code = _viewModel.Code,
                    FacultyId = _viewModel.FacultyId,
                    EducationFormRaw = _viewModel.EducationForm switch
                    {
                        EducationForm.full_time => "full_time",
                        EducationForm.part_time => "part_time",
                        EducationForm.extramural => "extramural",
                        _ => "full_time"
                    }
                };

                Direction? result;
                if (_isEdit)
                {
                    result = await _apiService.UpdateDirectionAsync(_viewModel.Id, direction);
                }
                else
                {
                    result = await _apiService.CreateDirectionAsync(direction);
                }

                if (result != null)
                {
                    ResultViewModel = new DirectionEditViewModel
                    {
                        Id = result.Id,
                        Name = result.Name ?? string.Empty,
                        Code = result.Code ?? string.Empty,
                        FacultyId = result.FacultyId,
                        EducationForm = result.EducationForm
                    };
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения направления", "Ошибка",
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
