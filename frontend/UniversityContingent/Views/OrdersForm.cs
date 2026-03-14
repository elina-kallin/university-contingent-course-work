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
                    o.Id,
                    o.Number,
                    Дата = o.Date.ToString("dd.MM.yyyy"),
                    Тип = o.Type switch
                    {
                        OrderType.enrollment => "О зачислении",
                        OrderType.expulsion => "Об отчислении",
                        OrderType.next_course => "О переводе на следующий курс",
                        OrderType.academic_leave => "О предоставлении академического отпуска",
                        OrderType.transfer_direction => "О переводе на другое направление",
                        _ => "Не указано"
                    },
                    o.Reason
                }).ToList();

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
