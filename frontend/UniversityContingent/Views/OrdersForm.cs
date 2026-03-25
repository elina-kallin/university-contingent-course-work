using UniversityContingent.Controller.Api;
using UniversityContingent.Models;

namespace UniversityContingent.Views
{
    public partial class OrdersForm : Form
    {
        private readonly ApiService _apiService;
        private List<Order> _orders = new();

        public OrdersForm(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OrdersForm_Shown(object sender, EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "Загрузка...";

            try
            {
                _orders = await _apiService.GetOrdersAsync() ?? new List<Order>();

                dgvOrders.DataSource = _orders.Select(o => new
                {
                    Id = o.Id,
                    НомерДела = o.Number ?? "б/н",
                    Дата = o.Date.ToString("dd.MM.yyyy"),
                    ТипПриказа = o.Type switch
                    {
                        OrderType.enrollment => "О зачислении",
                        OrderType.expulsion => "Об отчислении",
                        OrderType.next_course => "О переводе на следующий курс",
                        OrderType.academic_leave => "О предоставлении академического отпуска",
                        OrderType.transfer_direction => "О переводе на другое направление",
                        _ => "Не указано"
                    },
                    Причина = o.Reason ?? "—"
                }).ToList();

                // Русификация заголовков колонок
                if (dgvOrders.Columns.Count > 0)
                {
                    dgvOrders.Columns["Id"].Visible = false;
                    dgvOrders.Columns["НомерДела"].HeaderText = "Номер дела";
                    dgvOrders.Columns["Дата"].HeaderText = "Дата";
                    dgvOrders.Columns["ТипПриказа"].HeaderText = "Тип приказа";
                    dgvOrders.Columns["Причина"].HeaderText = "Причина";
                }

                lblStatus.Text = $"Загружено приказов: {_orders.Count}";
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

        private async void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите приказ для печати", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvOrders.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (Guid.TryParse(idStr, out var id))
            {
                try
                {
                    var html = await _apiService.GetOrderPrintHtmlAsync(id);
                    if (!string.IsNullOrEmpty(html))
                    {
                        // Открываем HTML в браузере по умолчанию
                        var tempFile = Path.Combine(Path.GetTempPath(), $"order_{id}.html");
                        File.WriteAllText(tempFile, html, System.Text.Encoding.UTF8);
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = tempFile,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить HTML для печати", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка печати: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form = new OrderCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadOrdersAsync();
            }
        }

        private void btnAddNextCourse_Click(object sender, EventArgs e)
        {
            using var form = new OrderNextCourseCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadOrdersAsync();
            }
        }

        private void btnAddExpulsion_Click(object sender, EventArgs e)
        {
            using var form = new OrderExpulsionCreateForm(_apiService);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadOrdersAsync();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Редактирование приказов недоступно. Приказы можно только создавать новые.",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите приказ для удаления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idStr = dgvOrders.SelectedRows[0].Cells["Id"].Value?.ToString();
            if (Guid.TryParse(idStr, out var id))
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот приказ? Студенты не будут удалены.",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var success = await _apiService.DeleteOrderAsync(id);
                    if (success)
                    {
                        MessageBox.Show("Приказ успешно удалён", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadOrdersAsync();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления приказа", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ = LoadOrdersAsync();
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnPrint.PerformClick();
            }
        }
    }
}
