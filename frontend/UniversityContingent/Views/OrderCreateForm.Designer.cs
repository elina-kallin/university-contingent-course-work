namespace UniversityContingent.Views
{
    partial class OrderCreateForm
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
            lblEducationForm = new Label();
            cmbEducationForm = new ComboBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblDirection = new Label();
            cmbDirection = new ComboBox();
            grpStudent = new GroupBox();
            lblLastName = new Label();
            lblName = new Label();
            lblPatronymic = new Label();
            lblStudyBook = new Label();
            lblGroup = new Label();
            txtLastName = new TextBox();
            txtName = new TextBox();
            txtPatronymic = new TextBox();
            numStudyBook = new NumericUpDown();
            cmbGroup = new ComboBox();
            btnAddStudent = new Button();
            grpStudents = new GroupBox();
            dgvStudents = new DataGridView();
            btnRemoveStudent = new Button();
            panelBottom = new FlowLayoutPanel();
            btnSave = new Button();
            btnCancel = new Button();
            grpOrder.SuspendLayout();
            grpStudent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStudyBook).BeginInit();
            grpStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(900, 10);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(290, 25);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Приказ о зачислении студентов";
            //
            // grpOrder
            //
            grpOrder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpOrder.Controls.Add(lblDate);
            grpOrder.Controls.Add(dtpDate);
            grpOrder.Controls.Add(lblEducationForm);
            grpOrder.Controls.Add(cmbEducationForm);
            grpOrder.Controls.Add(lblPrice);
            grpOrder.Controls.Add(txtPrice);
            grpOrder.Controls.Add(lblDirection);
            grpOrder.Controls.Add(cmbDirection);
            grpOrder.Location = new Point(20, 60);
            grpOrder.Name = "grpOrder";
            grpOrder.Size = new Size(860, 120);
            grpOrder.TabIndex = 2;
            grpOrder.TabStop = false;
            grpOrder.Text = "Данные приказа";
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
            // lblEducationForm
            // 
            lblEducationForm.AutoSize = true;
            lblEducationForm.Location = new Point(200, 30);
            lblEducationForm.Name = "lblEducationForm";
            lblEducationForm.Size = new Size(101, 15);
            lblEducationForm.TabIndex = 2;
            lblEducationForm.Text = "Форма обучения";
            // 
            // cmbEducationForm
            // 
            cmbEducationForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEducationForm.Location = new Point(200, 48);
            cmbEducationForm.Name = "cmbEducationForm";
            cmbEducationForm.Size = new Size(180, 23);
            cmbEducationForm.TabIndex = 3;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(400, 30);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(166, 15);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Стоимость обучения (в год)";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(400, 48);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "0 - бесплатно";
            txtPrice.Size = new Size(150, 23);
            txtPrice.TabIndex = 5;
            txtPrice.KeyPress += txtPrice_KeyPress;
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(20, 70);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(75, 15);
            lblDirection.TabIndex = 6;
            lblDirection.Text = "Направление";
            // 
            // cmbDirection
            // 
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.Location = new Point(20, 88);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(400, 23);
            cmbDirection.TabIndex = 7;
            //
            // grpStudent
            //
            grpStudent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpStudent.Controls.Add(lblLastName);
            grpStudent.Controls.Add(lblName);
            grpStudent.Controls.Add(lblPatronymic);
            grpStudent.Controls.Add(lblStudyBook);
            grpStudent.Controls.Add(lblGroup);
            grpStudent.Controls.Add(txtLastName);
            grpStudent.Controls.Add(txtName);
            grpStudent.Controls.Add(txtPatronymic);
            grpStudent.Controls.Add(numStudyBook);
            grpStudent.Controls.Add(cmbGroup);
            grpStudent.Controls.Add(btnAddStudent);
            grpStudent.Location = new Point(20, 190);
            grpStudent.Name = "grpStudent";
            grpStudent.Size = new Size(860, 130);
            grpStudent.TabIndex = 3;
            grpStudent.TabStop = false;
            grpStudent.Text = "Добавить студента";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(20, 25);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(56, 15);
            lblLastName.TabIndex = 0;
            lblLastName.Text = "Фамилия";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 65);
            lblName.Name = "lblName";
            lblName.Size = new Size(31, 15);
            lblName.TabIndex = 1;
            lblName.Text = "Имя";
            // 
            // lblPatronymic
            // 
            lblPatronymic.AutoSize = true;
            lblPatronymic.Location = new Point(200, 25);
            lblPatronymic.Name = "lblPatronymic";
            lblPatronymic.Size = new Size(60, 15);
            lblPatronymic.TabIndex = 2;
            lblPatronymic.Text = "Отчество";
            // 
            // lblStudyBook
            // 
            lblStudyBook.AutoSize = true;
            lblStudyBook.Location = new Point(200, 65);
            lblStudyBook.Name = "lblStudyBook";
            lblStudyBook.Size = new Size(107, 15);
            lblStudyBook.TabIndex = 3;
            lblStudyBook.Text = "Номер зачетки";
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Location = new Point(400, 25);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(50, 15);
            lblGroup.TabIndex = 4;
            lblGroup.Text = "Группа";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(20, 43);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(150, 23);
            txtLastName.TabIndex = 5;
            txtLastName.KeyPress += txtLastName_KeyPress;
            // 
            // txtName
            // 
            txtName.Location = new Point(20, 83);
            txtName.Name = "txtName";
            txtName.Size = new Size(150, 23);
            txtName.TabIndex = 6;
            txtName.KeyPress += txtLastName_KeyPress;
            // 
            // txtPatronymic
            // 
            txtPatronymic.Location = new Point(200, 43);
            txtPatronymic.Name = "txtPatronymic";
            txtPatronymic.Size = new Size(150, 23);
            txtPatronymic.TabIndex = 7;
            txtPatronymic.KeyPress += txtLastName_KeyPress;
            // 
            // numStudyBook
            // 
            numStudyBook.Location = new Point(200, 83);
            numStudyBook.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numStudyBook.Name = "numStudyBook";
            numStudyBook.Size = new Size(100, 23);
            numStudyBook.TabIndex = 8;
            // 
            // cmbGroup
            // 
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.Location = new Point(400, 43);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(200, 23);
            cmbGroup.TabIndex = 9;
            // 
            // btnAddStudent
            // 
            btnAddStudent.BackColor = Color.FromArgb(0, 120, 215);
            btnAddStudent.FlatAppearance.BorderSize = 0;
            btnAddStudent.FlatStyle = FlatStyle.Flat;
            btnAddStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddStudent.ForeColor = Color.White;
            btnAddStudent.Location = new Point(620, 40);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(220, 35);
            btnAddStudent.TabIndex = 10;
            btnAddStudent.Text = "Добавить студента в приказ";
            btnAddStudent.UseVisualStyleBackColor = false;
            btnAddStudent.Click += btnAddStudent_Click;
            // 
            // grpStudents
            // 
            grpStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpStudents.Controls.Add(dgvStudents);
            grpStudents.Controls.Add(btnRemoveStudent);
            grpStudents.Location = new Point(20, 330);
            grpStudents.Name = "grpStudents";
            grpStudents.Size = new Size(860, 280);
            grpStudents.TabIndex = 4;
            grpStudents.TabStop = false;
            grpStudents.Text = "Студенты в приказе";
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.Location = new Point(3, 19);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(854, 228);
            dgvStudents.TabIndex = 0;
            // 
            // btnRemoveStudent
            // 
            btnRemoveStudent.BackColor = Color.FromArgb(200, 50, 50);
            btnRemoveStudent.Dock = DockStyle.Bottom;
            btnRemoveStudent.FlatAppearance.BorderSize = 0;
            btnRemoveStudent.FlatStyle = FlatStyle.Flat;
            btnRemoveStudent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemoveStudent.ForeColor = Color.White;
            btnRemoveStudent.Location = new Point(3, 247);
            btnRemoveStudent.Name = "btnRemoveStudent";
            btnRemoveStudent.Size = new Size(854, 30);
            btnRemoveStudent.TabIndex = 1;
            btnRemoveStudent.Text = "Удалить выбранного студента";
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
            panelBottom.Location = new Point(0, 580);
            panelBottom.Name = "panelBottom";
            panelBottom.Padding = new Padding(20, 10, 20, 10);
            panelBottom.Size = new Size(900, 60);
            panelBottom.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(450, 10);
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
            btnCancel.Location = new Point(300, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // OrderCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 680);
            Controls.Add(grpStudents);
            Controls.Add(grpStudent);
            Controls.Add(grpOrder);
            Controls.Add(lblTitle);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OrderCreateForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание приказа о зачислении";
            Load += OrderCreateForm_Load;
            grpOrder.ResumeLayout(false);
            grpOrder.PerformLayout();
            grpStudent.ResumeLayout(false);
            grpStudent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numStudyBook).EndInit();
            grpStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Panel panelTop;
        private Label lblTitle;
        private GroupBox grpOrder;
        private Label lblDate;
        private DateTimePicker dtpDate;
        private Label lblEducationForm;
        private ComboBox cmbEducationForm;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblDirection;
        private ComboBox cmbDirection;
        private GroupBox grpStudent;
        private Label lblLastName;
        private Label lblName;
        private Label lblPatronymic;
        private Label lblStudyBook;
        private Label lblGroup;
        private TextBox txtLastName;
        private TextBox txtName;
        private TextBox txtPatronymic;
        private NumericUpDown numStudyBook;
        private ComboBox cmbGroup;
        private Button btnAddStudent;
        private GroupBox grpStudents;
        private DataGridView dgvStudents;
        private Button btnRemoveStudent;
        private FlowLayoutPanel panelBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}
