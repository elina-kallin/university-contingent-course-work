namespace UniversityContingent.Views
{
    partial class DirectionEditForm
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
            lblName = new Label();
            lblCode = new Label();
            lblFaculty = new Label();
            lblEducationForm = new Label();
            txtName = new TextBox();
            txtCode = new TextBox();
            cmbFaculty = new ComboBox();
            cmbEducationForm = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            panelButtons = new FlowLayoutPanel();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(90, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Наименование";
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(20, 70);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(35, 15);
            lblCode.TabIndex = 1;
            lblCode.Text = "Код";
            // 
            // lblFaculty
            // 
            lblFaculty.AutoSize = true;
            lblFaculty.Location = new Point(20, 120);
            lblFaculty.Name = "lblFaculty";
            lblFaculty.Size = new Size(72, 15);
            lblFaculty.TabIndex = 2;
            lblFaculty.Text = "Факультет";
            // 
            // lblEducationForm
            // 
            lblEducationForm.AutoSize = true;
            lblEducationForm.Location = new Point(20, 170);
            lblEducationForm.Name = "lblEducationForm";
            lblEducationForm.Size = new Size(97, 15);
            lblEducationForm.TabIndex = 3;
            lblEducationForm.Text = "Форма обучения";
            // 
            // txtName
            // 
            txtName.Location = new Point(20, 38);
            txtName.Name = "txtName";
            txtName.Size = new Size(400, 23);
            txtName.TabIndex = 4;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(20, 88);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(200, 23);
            txtCode.TabIndex = 5;
            // 
            // cmbFaculty
            // 
            cmbFaculty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFaculty.Location = new Point(20, 138);
            cmbFaculty.Name = "cmbFaculty";
            cmbFaculty.Size = new Size(400, 23);
            cmbFaculty.TabIndex = 6;
            // 
            // cmbEducationForm
            // 
            cmbEducationForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEducationForm.Location = new Point(20, 188);
            cmbEducationForm.Name = "cmbEducationForm";
            cmbEducationForm.Size = new Size(200, 23);
            cmbEducationForm.TabIndex = 7;
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
            panelButtons.Location = new Point(0, 250);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(20, 10, 20, 10);
            panelButtons.Size = new Size(464, 60);
            panelButtons.TabIndex = 10;
            // 
            // DirectionEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 310);
            Controls.Add(panelButtons);
            Controls.Add(cmbEducationForm);
            Controls.Add(cmbFaculty);
            Controls.Add(txtCode);
            Controls.Add(txtName);
            Controls.Add(lblEducationForm);
            Controls.Add(lblFaculty);
            Controls.Add(lblCode);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DirectionEditForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "DirectionEditForm";
            Load += DirectionEditForm_Load;
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblName;
        private Label lblCode;
        private Label lblFaculty;
        private Label lblEducationForm;
        private TextBox txtName;
        private TextBox txtCode;
        private ComboBox cmbFaculty;
        private ComboBox cmbEducationForm;
        private Button btnSave;
        private Button btnCancel;
        private FlowLayoutPanel panelButtons;
    }
}
