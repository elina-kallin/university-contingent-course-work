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
            lblFullName = new Label();
            lblBirthDate = new Label();
            lblGroup = new Label();
            lblEnrollmentDate = new Label();
            txtFullName = new TextBox();
            cmbGroup = new ComboBox();
            dtpBirthDate = new DateTimePicker();
            dtpEnrollmentDate = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            panelButtons = new FlowLayoutPanel();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 20);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(40, 15);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "ФИО";
            // 
            // lblBirthDate
            // 
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(20, 70);
            lblBirthDate.Name = "lblBirthDate";
            lblBirthDate.Size = new Size(91, 15);
            lblBirthDate.TabIndex = 1;
            lblBirthDate.Text = "Дата рождения";
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(20, 120);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(50, 15);
            lblGroup.TabIndex = 2;
            lblGroup.Text = "Группа";
            // 
            // lblEnrollmentDate
            // 
            lblEnrollmentDate.AutoSize = true;
            lblEnrollmentDate.Location = new Point(20, 170);
            lblEnrollmentDate.Name = "lblEnrollmentDate";
            lblEnrollmentDate.Size = new Size(96, 15);
            lblEnrollmentDate.TabIndex = 3;
            lblEnrollmentDate.Text = "Дата зачисления";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(20, 38);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(400, 23);
            txtFullName.TabIndex = 4;
            // 
            // cmbGroup
            // 
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.Location = new Point(20, 138);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(200, 23);
            cmbGroup.TabIndex = 5;
            // 
            // dtpBirthDate
            // 
            dtpBirthDate.Location = new Point(20, 88);
            dtpBirthDate.Name = "dtpBirthDate";
            dtpBirthDate.Size = new Size(200, 23);
            dtpBirthDate.TabIndex = 6;
            // 
            // dtpEnrollmentDate
            // 
            dtpEnrollmentDate.Location = new Point(20, 188);
            dtpEnrollmentDate.Name = "dtpEnrollmentDate";
            dtpEnrollmentDate.Size = new Size(200, 23);
            dtpEnrollmentDate.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 8;
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
            btnCancel.Location = new Point(150, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // panelButtons
            // 
            panelButtons.AutoSize = true;
            panelButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.FlowDirection = FlowDirection.RightToLeft;
            panelButtons.Location = new Point(0, 240);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20, 10, 20, 10);
            panelButtons.Size = new Size(464, 60);
            panelButtons.TabIndex = 10;
            // 
            // StudentEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 300);
            Controls.Add(panelButtons);
            Controls.Add(dtpEnrollmentDate);
            Controls.Add(dtpBirthDate);
            Controls.Add(cmbGroup);
            Controls.Add(txtFullName);
            Controls.Add(lblEnrollmentDate);
            Controls.Add(lblGroup);
            Controls.Add(lblBirthDate);
            Controls.Add(lblFullName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StudentEditForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "StudentEditForm";
            Load += StudentEditForm_Load;
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblFullName;
        private Label lblBirthDate;
        private Label lblGroup;
        private Label lblEnrollmentDate;
        private TextBox txtFullName;
        private ComboBox cmbGroup;
        private DateTimePicker dtpBirthDate;
        private DateTimePicker dtpEnrollmentDate;
        private Button btnSave;
        private Button btnCancel;
        private FlowLayoutPanel panelButtons;
    }
}
