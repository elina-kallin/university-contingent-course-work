namespace UniversityContingent.Views
{
    partial class StudentEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblLastName = new Label();
            lblName = new Label();
            lblPatronymic = new Label();
            lblGroup = new Label();
            lblEnrollmentDate = new Label();
            lblStudyBook = new Label();
            lblStatus = new Label();
            txtLastName = new TextBox();
            txtName = new TextBox();
            txtPatronymic = new TextBox();
            numStudyBook = new NumericUpDown();
            cmbGroup = new ComboBox();
            dtpEnrollmentDate = new DateTimePicker();
            lblStatusValue = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            panelButtons = new FlowLayoutPanel();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // lblLastName
            //
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(20, 20);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(56, 15);
            lblLastName.TabIndex = 0;
            lblLastName.Text = "Фамилия";
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 70);
            lblName.Name = "lblName";
            lblName.Size = new Size(31, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Имя";
            //
            // lblPatronymic
            //
            lblPatronymic.AutoSize = true;
            lblPatronymic.Location = new Point(20, 120);
            lblPatronymic.Name = "lblPatronymic";
            lblPatronymic.Size = new Size(60, 15);
            lblPatronymic.TabIndex = 2;
            lblPatronymic.Text = "Отчество";
            //
            // lblStudyBook
            //
            lblStudyBook.AutoSize = true;
            lblStudyBook.Location = new Point(20, 170);
            lblStudyBook.Name = "lblStudyBook";
            lblStudyBook.Size = new Size(107, 15);
            lblStudyBook.TabIndex = 3;
            lblStudyBook.Text = "Номер зачетки";
            //
            // lblGroup
            //
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(20, 220);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(50, 15);
            lblGroup.TabIndex = 4;
            lblGroup.Text = "Группа";
            //
            // lblEnrollmentDate
            //
            lblEnrollmentDate.AutoSize = true;
            lblEnrollmentDate.Location = new Point(20, 270);
            lblEnrollmentDate.Name = "lblEnrollmentDate";
            lblEnrollmentDate.Size = new Size(96, 15);
            lblEnrollmentDate.TabIndex = 5;
            lblEnrollmentDate.Text = "Дата зачисления";
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 320);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Статус";
            //
            // txtLastName
            //
            txtLastName.Location = new Point(20, 38);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(300, 23);
            txtLastName.TabIndex = 7;
            //
            // txtName
            //
            txtName.Location = new Point(20, 88);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 8;
            //
            // txtPatronymic
            //
            txtPatronymic.Location = new Point(20, 138);
            txtPatronymic.Name = "txtPatronymic";
            txtPatronymic.Size = new Size(300, 23);
            txtPatronymic.TabIndex = 9;
            //
            // numStudyBook
            //
            numStudyBook.Location = new Point(20, 188);
            numStudyBook.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numStudyBook.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStudyBook.Name = "numStudyBook";
            numStudyBook.Size = new Size(120, 23);
            numStudyBook.TabIndex = 10;
            numStudyBook.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // cmbGroup
            //
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.Location = new Point(20, 238);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(200, 23);
            cmbGroup.TabIndex = 11;
            //
            // dtpEnrollmentDate
            //
            dtpEnrollmentDate.Location = new Point(20, 288);
            dtpEnrollmentDate.Name = "dtpEnrollmentDate";
            dtpEnrollmentDate.Size = new Size(200, 23);
            dtpEnrollmentDate.TabIndex = 12;
            //
            // lblStatusValue
            //
            lblStatusValue.AutoSize = true;
            lblStatusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusValue.Location = new Point(20, 338);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(66, 15);
            lblStatusValue.TabIndex = 13;
            lblStatusValue.Text = "Обучается";
            //
            // panelButtons
            //
            panelButtons.AutoSize = true;
            panelButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.FlowDirection = FlowDirection.RightToLeft;
            panelButtons.Location = new Point(0, 380);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20, 10, 20, 10);
            panelButtons.Size = new Size(360, 60);
            panelButtons.TabIndex = 14;
            //
            // btnSave
            //
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(220, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.FromArgb(200, 200, 200);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.Location = new Point(70, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // StudentEditForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 440);
            Controls.Add(panelButtons);
            Controls.Add(lblStatusValue);
            Controls.Add(dtpEnrollmentDate);
            Controls.Add(cmbGroup);
            Controls.Add(numStudyBook);
            Controls.Add(txtPatronymic);
            Controls.Add(txtName);
            Controls.Add(txtLastName);
            Controls.Add(lblStatus);
            Controls.Add(lblEnrollmentDate);
            Controls.Add(lblGroup);
            Controls.Add(lblStudyBook);
            Controls.Add(lblPatronymic);
            Controls.Add(lblName);
            Controls.Add(lblLastName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StudentEditForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование студента";
            Load += StudentEditForm_Load;
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblLastName;
        private Label lblName;
        private Label lblPatronymic;
        private Label lblGroup;
        private Label lblEnrollmentDate;
        private Label lblStudyBook;
        private Label lblStatus;
        private TextBox txtLastName;
        private TextBox txtName;
        private TextBox txtPatronymic;
        private NumericUpDown numStudyBook;
        private ComboBox cmbGroup;
        private DateTimePicker dtpEnrollmentDate;
        private Label lblStatusValue;
        private Button btnSave;
        private Button btnCancel;
        private FlowLayoutPanel panelButtons;
    }
}
