namespace UniversityContingent.Views
{
    partial class OrderNextCourseCreateForm
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
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblDirection = new Label();
            cmbDirection = new ComboBox();
            lblFromCourse = new Label();
            cmbFromCourse = new ComboBox();
            lblToCourse = new Label();
            cmbToCourse = new ComboBox();
            lblFromGroup = new Label();
            cmbFromGroup = new ComboBox();
            lblToGroup = new Label();
            cmbToGroup = new ComboBox();
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
            lblTitle.Size = new Size(420, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Приказ о переводе на следующий курс";
            //
            // grpOrder
            //
            grpOrder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpOrder.Controls.Add(lblDate);
            grpOrder.Controls.Add(dtpDate);
            grpOrder.Controls.Add(lblDirection);
            grpOrder.Controls.Add(cmbDirection);
            grpOrder.Controls.Add(lblFromCourse);
            grpOrder.Controls.Add(cmbFromCourse);
            grpOrder.Controls.Add(lblToCourse);
            grpOrder.Controls.Add(cmbToCourse);
            grpOrder.Controls.Add(lblFromGroup);
            grpOrder.Controls.Add(cmbFromGroup);
            grpOrder.Controls.Add(lblToGroup);
            grpOrder.Controls.Add(cmbToGroup);
            grpOrder.Location = new Point(20, 60);
            grpOrder.Name = "grpOrder";
            grpOrder.Size = new Size(910, 180);
            grpOrder.TabIndex = 2;
            grpOrder.TabStop = false;
            grpOrder.Text = "Параметры перевода";
            //
            // lblDate
            //
            lblDate.AutoSize = true;
            lblDate.Location = new Point(20, 30);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(33, 15);
            lblDate.TabIndex = 0;
            lblDate.Text = "Дата";
            //
            // dtpDate
            //
            dtpDate.Location = new Point(20, 48);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(150, 23);
            dtpDate.TabIndex = 1;
            //
            // lblDirection
            //
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(200, 30);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(75, 15);
            lblDirection.TabIndex = 2;
            lblDirection.Text = "Направление";
            //
            // cmbDirection
            //
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.Location = new Point(200, 48);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(300, 23);
            cmbDirection.TabIndex = 3;
            //
            // lblFromCourse
            //
            lblFromCourse.AutoSize = true;
            lblFromCourse.Location = new Point(20, 75);
            lblFromCourse.Name = "lblFromCourse";
            lblFromCourse.Size = new Size(139, 15);
            lblFromCourse.TabIndex = 4;
            lblFromCourse.Text = "Курс, с которого перевести";
            //
            // cmbFromCourse
            //
            cmbFromCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFromCourse.Location = new Point(20, 93);
            cmbFromCourse.Name = "cmbFromCourse";
            cmbFromCourse.Size = new Size(150, 23);
            cmbFromCourse.TabIndex = 5;
            //
            // lblToCourse
            //
            lblToCourse.AutoSize = true;
            lblToCourse.Location = new Point(200, 75);
            lblToCourse.Name = "lblToCourse";
            lblToCourse.Size = new Size(135, 15);
            lblToCourse.TabIndex = 6;
            lblToCourse.Text = "Курс, на который перевести";
            //
            // cmbToCourse
            //
            cmbToCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbToCourse.Location = new Point(200, 93);
            cmbToCourse.Name = "cmbToCourse";
            cmbToCourse.Size = new Size(150, 23);
            cmbToCourse.TabIndex = 7;
            //
            // lblFromGroup
            //
            lblFromGroup.AutoSize = true;
            lblFromGroup.Location = new Point(400, 75);
            lblFromGroup.Name = "lblFromGroup";
            lblFromGroup.Size = new Size(136, 15);
            lblFromGroup.TabIndex = 8;
            lblFromGroup.Text = "Группа, из которой перевести";
            //
            // cmbFromGroup
            //
            cmbFromGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFromGroup.Location = new Point(400, 93);
            cmbFromGroup.Name = "cmbFromGroup";
            cmbFromGroup.Size = new Size(200, 23);
            cmbFromGroup.TabIndex = 9;
            //
            // lblToGroup
            //
            lblToGroup.AutoSize = true;
            lblToGroup.Location = new Point(650, 75);
            lblToGroup.Name = "lblToGroup";
            lblToGroup.Size = new Size(132, 15);
            lblToGroup.TabIndex = 10;
            lblToGroup.Text = "Группа, в которую перевести";
            //
            // cmbToGroup
            //
            cmbToGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbToGroup.Location = new Point(650, 93);
            cmbToGroup.Name = "cmbToGroup";
            cmbToGroup.Size = new Size(200, 23);
            cmbToGroup.TabIndex = 11;
            //
            // grpStudents
            //
            grpStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpStudents.Controls.Add(lstStudents);
            grpStudents.Controls.Add(btnAddStudent);
            grpStudents.Location = new Point(20, 250);
            grpStudents.Name = "grpStudents";
            grpStudents.Size = new Size(400, 320);
            grpStudents.TabIndex = 3;
            grpStudents.TabStop = false;
            grpStudents.Text = "Студенты для перевода";
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
            grpSelectedStudents.Location = new Point(430, 250);
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
            panelBottom.Location = new Point(0, 540);
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
            // OrderNextCourseCreateForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 600);
            Controls.Add(grpSelectedStudents);
            Controls.Add(grpStudents);
            Controls.Add(grpOrder);
            Controls.Add(lblTitle);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OrderNextCourseCreateForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание приказа о переводе на следующий курс";
            Load += OrderNextCourseCreateForm_Load;
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
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblDirection;
        private ComboBox cmbDirection;
        private Label lblFromCourse;
        private ComboBox cmbFromCourse;
        private Label lblToCourse;
        private ComboBox cmbToCourse;
        private Label lblFromGroup;
        private ComboBox cmbFromGroup;
        private Label lblToGroup;
        private ComboBox cmbToGroup;
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
