namespace UniversityContingent.Views
{
    partial class GroupEditForm
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
            lblDirection = new Label();
            lblCourse = new Label();
            lblYear = new Label();
            txtName = new TextBox();
            cmbDirection = new ComboBox();
            numCourse = new NumericUpDown();
            numYear = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            panelButtons = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)numCourse).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(112, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Название группы";
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(20, 70);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(88, 15);
            lblDirection.TabIndex = 1;
            lblDirection.Text = "Направление";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(20, 120);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(36, 15);
            lblCourse.TabIndex = 2;
            lblCourse.Text = "Курс";
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Location = new Point(20, 170);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(81, 15);
            lblYear.TabIndex = 3;
            lblYear.Text = "Год набора";
            // 
            // txtName
            // 
            txtName.Location = new Point(20, 38);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 4;
            // 
            // cmbDirection
            // 
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.Location = new Point(20, 88);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(400, 23);
            cmbDirection.TabIndex = 5;
            // 
            // numCourse
            // 
            numCourse.Location = new Point(20, 138);
            numCourse.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            numCourse.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCourse.Name = "numCourse";
            numCourse.Size = new Size(100, 23);
            numCourse.TabIndex = 6;
            numCourse.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numYear
            // 
            numYear.Location = new Point(20, 188);
            numYear.Maximum = new decimal(new int[] { 2030, 0, 0, 0 });
            numYear.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            numYear.Name = "numYear";
            numYear.Size = new Size(100, 23);
            numYear.TabIndex = 7;
            numYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
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
            // GroupEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 300);
            Controls.Add(panelButtons);
            Controls.Add(numYear);
            Controls.Add(numCourse);
            Controls.Add(cmbDirection);
            Controls.Add(txtName);
            Controls.Add(lblYear);
            Controls.Add(lblCourse);
            Controls.Add(lblDirection);
            Controls.Add(lblName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GroupEditForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "GroupEditForm";
            Load += GroupEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)numCourse).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblName;
        private Label lblDirection;
        private Label lblCourse;
        private Label lblYear;
        private TextBox txtName;
        private ComboBox cmbDirection;
        private NumericUpDown numCourse;
        private NumericUpDown numYear;
        private Button btnSave;
        private Button btnCancel;
        private FlowLayoutPanel panelButtons;
    }
}
