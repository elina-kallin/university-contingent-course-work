namespace UniversityContingent.Views
{
    partial class OrderExpulsionCreateForm
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
            panelTop = new Panel();
            lblTitle = new Label();
            grpOrder = new GroupBox();
            lblExpulsionDate = new Label();
            dtpExpulsionDate = new DateTimePicker();
            lblReason = new Label();
            cmbReason = new ComboBox();
            lblDirection = new Label();
            cmbDirection = new ComboBox();
            lblGroup = new Label();
            cmbGroup = new ComboBox();
            grpStudents = new GroupBox();
            lstStudents = new ListBox();
            btnAddStudent = new Button();
            grpSelectedStudents = new GroupBox();
            dgvSelectedStudents = new DataGridView();
            btnRemoveStudent = new Button();
            panelBottom = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            grpOrder.SuspendLayout();
            grpStudents.SuspendLayout();
            grpSelectedStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSelectedStudents).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            //
            // panelTop
            //
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(950, 10);
            panelTop.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(280, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Приказ об отчислении студентов";
            //
            // grpOrder
            //
            grpOrder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpOrder.Controls.Add(lblExpulsionDate);
            grpOrder.Controls.Add(dtpExpulsionDate);
            grpOrder.Controls.Add(lblReason);
            grpOrder.Controls.Add(cmbReason);
            grpOrder.Controls.Add(lblDirection);
            grpOrder.Controls.Add(cmbDirection);
            grpOrder.Controls.Add(lblGroup);
            grpOrder.Controls.Add(cmbGroup);
            grpOrder.Location = new Point(20, 60);
            grpOrder.Name = "grpOrder";
            grpOrder.Size = new Size(910, 150);
            grpOrder.TabIndex = 2;
            grpOrder.TabStop = false;
            grpOrder.Text = "Данные приказа";
            //
            // lblExpulsionDate
            //
            lblExpulsionDate.AutoSize = true;
            lblExpulsionDate.Location = new Point(20, 30);
            lblExpulsionDate.Name = "lblExpulsionDate";
            lblExpulsionDate.Size = new Size(155, 15);
            lblExpulsionDate.TabIndex = 0;
            lblExpulsionDate.Text = "Дата отчисления (с которой)";
            //
            // dtpExpulsionDate
            //
            dtpExpulsionDate.Location = new Point(20, 48);
            dtpExpulsionDate.Name = "dtpExpulsionDate";
            dtpExpulsionDate.Size = new Size(200, 23);
            dtpExpulsionDate.TabIndex = 1;
            //
            // lblReason
            //
            lblReason.AutoSize = true;
            lblReason.Location = new Point(250, 30);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(129, 15);
            lblReason.TabIndex = 2;
            lblReason.Text = "Причина отчисления";
            //
            // cmbReason
            //
            cmbReason.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReason.Location = new Point(250, 48);
            cmbReason.Name = "cmbReason";
            cmbReason.Size = new Size(250, 23);
            cmbReason.TabIndex = 3;
            //
            // lblDirection
            //
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(20, 75);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(75, 15);
            lblDirection.TabIndex = 4;
            lblDirection.Text = "Направление";
            //
            // cmbDirection
            //
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.Location = new Point(20, 93);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(400, 23);
            cmbDirection.TabIndex = 5;
            //
            // lblGroup
            //
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(450, 75);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(50, 15);
            lblGroup.TabIndex = 6;
            lblGroup.Text = "Группа";
            //
            // cmbGroup
            //
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.Location = new Point(450, 93);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(200, 23);
            cmbGroup.TabIndex = 7;
            //
            // grpStudents
            //
            grpStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpStudents.Controls.Add(lstStudents);
            grpStudents.Controls.Add(btnAddStudent);
            grpStudents.Location = new Point(20, 220);
            grpStudents.Name = "grpStudents";
            grpStudents.Size = new Size(400, 320);
            grpStudents.TabIndex = 3;
            grpStudents.TabStop = false;
            grpStudents.Text = "Студенты группы";
            //
            // lstStudents
            //
            lstStudents.Dock = DockStyle.Fill;
            lstStudents.FormattingEnabled = true;
            lstStudents.ItemHeight = 15;
            lstStudents.Location = new Point(3, 19);
            lstStudents.Name = "lstStudents";
            lstStudents.Size = new Size(394, 258);
            lstStudents.TabIndex = 0;
            //
            // btnAddStudent
            //
            btnAddStudent.BackColor = Color.FromArgb(0, 120, 215);
            btnAddStudent.Dock = DockStyle.Bottom;
            btnAddStudent.FlatAppearance.BorderSize = 0;
            btnAddStudent.FlatStyle = FlatStyle.Flat;
            btnAddStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddStudent.ForeColor = Color.White;
            btnAddStudent.Location = new Point(3, 277);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(394, 40);
            btnAddStudent.TabIndex = 1;
            btnAddStudent.Text = "Добавить студента в приказ →";
            btnAddStudent.UseVisualStyleBackColor = false;
            btnAddStudent.Click += btnAddStudent_Click;
            //
            // grpSelectedStudents
            //
            grpSelectedStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpSelectedStudents.Controls.Add(dgvSelectedStudents);
            grpSelectedStudents.Controls.Add(btnRemoveStudent);
            grpSelectedStudents.Location = new Point(430, 220);
            grpSelectedStudents.Name = "grpSelectedStudents";
            grpSelectedStudents.Size = new Size(500, 320);
            grpSelectedStudents.TabIndex = 4;
            grpSelectedStudents.TabStop = false;
            grpSelectedStudents.Text = "Студенты в приказе";
            //
            // dgvSelectedStudents
            //
            dgvSelectedStudents.AllowUserToAddRows = false;
            dgvSelectedStudents.AllowUserToDeleteRows = false;
            dgvSelectedStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSelectedStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSelectedStudents.Dock = DockStyle.Fill;
            dgvSelectedStudents.Location = new Point(3, 19);
            dgvSelectedStudents.MultiSelect = false;
            dgvSelectedStudents.Name = "dgvSelectedStudents";
            dgvSelectedStudents.ReadOnly = true;
            dgvSelectedStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSelectedStudents.Size = new Size(494, 258);
            dgvSelectedStudents.TabIndex = 0;
            //
            // btnRemoveStudent
            //
            btnRemoveStudent.BackColor = Color.FromArgb(200, 50, 50);
            btnRemoveStudent.Dock = DockStyle.Bottom;
            btnRemoveStudent.FlatAppearance.BorderSize = 0;
            btnRemoveStudent.FlatStyle = FlatStyle.Flat;
            btnRemoveStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveStudent.ForeColor = Color.White;
            btnRemoveStudent.Location = new Point(3, 277);
            btnRemoveStudent.Name = "btnRemoveStudent";
            btnRemoveStudent.Size = new Size(494, 40);
            btnRemoveStudent.TabIndex = 1;
            btnRemoveStudent.Text = "← Удалить выбранного студента";
            btnRemoveStudent.UseVisualStyleBackColor = false;
            btnRemoveStudent.Click += btnRemoveStudent_Click;
            //
            // panelBottom
            //
            panelBottom.AutoSize = true;
            panelBottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelBottom.Controls.Add(btnSave);
            panelBottom.Controls.Add(btnCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.FlowDirection = FlowDirection.RightToLeft;
            panelBottom.Location = new Point(0, 510);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(20, 10, 20, 10);
            panelBottom.Size = new Size(950, 60);
            panelBottom.TabIndex = 5;
            //
            // btnSave
            //
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(500, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 35);
            btnSave.TabIndex = 2;
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
            btnCancel.Location = new Point(350, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // OrderExpulsionCreateForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 570);
            Controls.Add(grpSelectedStudents);
            Controls.Add(grpStudents);
            Controls.Add(grpOrder);
            Controls.Add(lblTitle);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OrderExpulsionCreateForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание приказа об отчислении";
            Load += OrderExpulsionCreateForm_Load;
            grpOrder.ResumeLayout(false);
            grpOrder.PerformLayout();
            grpStudents.ResumeLayout(false);
            grpSelectedStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSelectedStudents).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panelTop;
        private Label lblTitle;
        private GroupBox grpOrder;
        private Label lblExpulsionDate;
        private DateTimePicker dtpExpulsionDate;
        private Label lblReason;
        private ComboBox cmbReason;
        private Label lblDirection;
        private ComboBox cmbDirection;
        private Label lblGroup;
        private ComboBox cmbGroup;
        private GroupBox grpStudents;
        private ListBox lstStudents;
        private Button btnAddStudent;
        private GroupBox grpSelectedStudents;
        private DataGridView dgvSelectedStudents;
        private Button btnRemoveStudent;
        private FlowLayoutPanel panelBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}
