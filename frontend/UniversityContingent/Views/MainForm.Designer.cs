namespace UniversityContingent.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            справочникиToolStripMenuItem = new ToolStripMenuItem();
            факультетыToolStripMenuItem = new ToolStripMenuItem();
            направленияToolStripMenuItem = new ToolStripMenuItem();
            группыToolStripMenuItem = new ToolStripMenuItem();
            студентыToolStripMenuItem = new ToolStripMenuItem();
            приказыToolStripMenuItem = new ToolStripMenuItem();
            создатьПриказОЗачисленииToolStripMenuItem = new ToolStripMenuItem();
            создатьПриказОбОтчисленииToolStripMenuItem = new ToolStripMenuItem();
            создатьПриказОПереводеНаСледующийКурсToolStripMenuItem = new ToolStripMenuItem();
            создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem = new ToolStripMenuItem();
            создатьПриказОбАкадемическомОтпускеToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            просмотретьВсеПриказыToolStripMenuItem = new ToolStripMenuItem();
            отчетыToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            panelTop = new Panel();
            lblUserName = new Label();
            lblRole = new Label();
            btnLogout = new Button();
            panelContent = new Panel();
            tabControl = new TabControl();
            tabStudents = new TabPage();
            dgvStudents = new DataGridView();
            panelFilters = new Panel();
            btnEdit = new Button();
            btnRefresh = new Button();
            cmbGroups = new ComboBox();
            cmbDirections = new ComboBox();
            lblGroups = new Label();
            lblDirections = new Label();
            lblStatus = new Label();
            lblFaculty = new Label();
            menuStrip.SuspendLayout();
            panelTop.SuspendLayout();
            panelContent.SuspendLayout();
            tabControl.SuspendLayout();
            tabStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            panelFilters.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, справочникиToolStripMenuItem, студентыToolStripMenuItem, приказыToolStripMenuItem, отчетыToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(1239, 30);
            menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(59, 24);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(136, 26);
            exitToolStripMenuItem.Text = "Выход";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // справочникиToolStripMenuItem
            // 
            справочникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { факультетыToolStripMenuItem, направленияToolStripMenuItem, группыToolStripMenuItem });
            справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            справочникиToolStripMenuItem.Size = new Size(117, 24);
            справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // факультетыToolStripMenuItem
            // 
            факультетыToolStripMenuItem.Name = "факультетыToolStripMenuItem";
            факультетыToolStripMenuItem.Size = new Size(187, 26);
            факультетыToolStripMenuItem.Text = "Факультеты";
            факультетыToolStripMenuItem.Click += факультетыToolStripMenuItem_Click;
            // 
            // направленияToolStripMenuItem
            // 
            направленияToolStripMenuItem.Name = "направленияToolStripMenuItem";
            направленияToolStripMenuItem.Size = new Size(187, 26);
            направленияToolStripMenuItem.Text = "Направления";
            направленияToolStripMenuItem.Click += направленияToolStripMenuItem_Click;
            // 
            // группыToolStripMenuItem
            // 
            группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            группыToolStripMenuItem.Size = new Size(187, 26);
            группыToolStripMenuItem.Text = "Группы";
            группыToolStripMenuItem.Click += группыToolStripMenuItem_Click;
            // 
            // студентыToolStripMenuItem
            // 
            студентыToolStripMenuItem.Name = "студентыToolStripMenuItem";
            студентыToolStripMenuItem.Size = new Size(87, 24);
            студентыToolStripMenuItem.Text = "Студенты";
            студентыToolStripMenuItem.Click += студентыToolStripMenuItem_Click;
            // 
            // приказыToolStripMenuItem
            // 
            приказыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьПриказОЗачисленииToolStripMenuItem, создатьПриказОбОтчисленииToolStripMenuItem, создатьПриказОПереводеНаСледующийКурсToolStripMenuItem, создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem, создатьПриказОбАкадемическомОтпускеToolStripMenuItem, toolStripSeparator1, просмотретьВсеПриказыToolStripMenuItem });
            приказыToolStripMenuItem.Name = "приказыToolStripMenuItem";
            приказыToolStripMenuItem.Size = new Size(85, 24);
            приказыToolStripMenuItem.Text = "Приказы";
            // 
            // создатьПриказОЗачисленииToolStripMenuItem
            // 
            создатьПриказОЗачисленииToolStripMenuItem.Name = "создатьПриказОЗачисленииToolStripMenuItem";
            создатьПриказОЗачисленииToolStripMenuItem.Size = new Size(453, 26);
            создатьПриказОЗачисленииToolStripMenuItem.Text = "Создать приказ о зачислении";
            создатьПриказОЗачисленииToolStripMenuItem.Click += создатьПриказОЗачисленииToolStripMenuItem_Click;
            // 
            // создатьПриказОбОтчисленииToolStripMenuItem
            // 
            создатьПриказОбОтчисленииToolStripMenuItem.Name = "создатьПриказОбОтчисленииToolStripMenuItem";
            создатьПриказОбОтчисленииToolStripMenuItem.Size = new Size(453, 26);
            создатьПриказОбОтчисленииToolStripMenuItem.Text = "Создать приказ об отчислении";
            создатьПриказОбОтчисленииToolStripMenuItem.Click += создатьПриказОбОтчисленииToolStripMenuItem_Click;
            // 
            // создатьПриказОПереводеНаСледующийКурсToolStripMenuItem
            // 
            создатьПриказОПереводеНаСледующийКурсToolStripMenuItem.Name = "создатьПриказОПереводеНаСледующийКурсToolStripMenuItem";
            создатьПриказОПереводеНаСледующийКурсToolStripMenuItem.Size = new Size(453, 26);
            создатьПриказОПереводеНаСледующийКурсToolStripMenuItem.Text = "Создать приказ о переводе на следующий курс";
            создатьПриказОПереводеНаСледующийКурсToolStripMenuItem.Click += создатьПриказОПереводеНаСледующийКурсToolStripMenuItem_Click;
            // 
            // создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem
            // 
            создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem.Name = "создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem";
            создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem.Size = new Size(453, 26);
            создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem.Text = "Создать приказ о переводе на другое направление";
            создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem.Click += создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem_Click;
            // 
            // создатьПриказОбАкадемическомОтпускеToolStripMenuItem
            // 
            создатьПриказОбАкадемическомОтпускеToolStripMenuItem.Name = "создатьПриказОбАкадемическомОтпускеToolStripMenuItem";
            создатьПриказОбАкадемическомОтпускеToolStripMenuItem.Size = new Size(453, 26);
            создатьПриказОбАкадемическомОтпускеToolStripMenuItem.Text = "Создать приказ об академическом отпуске";
            создатьПриказОбАкадемическомОтпускеToolStripMenuItem.Click += создатьПриказОбАкадемическомОтпускеToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(450, 6);
            // 
            // просмотретьВсеПриказыToolStripMenuItem
            // 
            просмотретьВсеПриказыToolStripMenuItem.Name = "просмотретьВсеПриказыToolStripMenuItem";
            просмотретьВсеПриказыToolStripMenuItem.Size = new Size(453, 26);
            просмотретьВсеПриказыToolStripMenuItem.Text = "Просмотреть все приказы";
            просмотретьВсеПриказыToolStripMenuItem.Click += просмотретьВсеПриказыToolStripMenuItem_Click;
            // 
            // отчетыToolStripMenuItem
            // 
            отчетыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            отчетыToolStripMenuItem.Size = new Size(73, 24);
            отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(172, 26);
            toolStripMenuItem1.Text = "Контингент";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(0, 120, 215);
            panelTop.Controls.Add(lblUserName);
            panelTop.Controls.Add(lblRole);
            panelTop.Controls.Add(btnLogout);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 30);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1239, 67);
            panelTop.TabIndex = 1;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(11, 16);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(0, 23);
            lblUserName.TabIndex = 0;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F);
            lblRole.ForeColor = Color.WhiteSmoke;
            lblRole.Location = new Point(11, 43);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(0, 20);
            lblRole.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.Transparent;
            btnLogout.FlatAppearance.BorderColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1120, 13);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(106, 40);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(tabControl);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 97);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1239, 694);
            panelContent.TabIndex = 2;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabStudents);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(3, 4, 3, 4);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1239, 694);
            tabControl.TabIndex = 0;
            // 
            // tabStudents
            // 
            tabStudents.Controls.Add(dgvStudents);
            tabStudents.Controls.Add(panelFilters);
            tabStudents.Controls.Add(lblStatus);
            tabStudents.Location = new Point(4, 29);
            tabStudents.Margin = new Padding(3, 4, 3, 4);
            tabStudents.Name = "tabStudents";
            tabStudents.Padding = new Padding(3, 4, 3, 4);
            tabStudents.Size = new Size(1231, 661);
            tabStudents.TabIndex = 0;
            tabStudents.Text = "Студенты";
            tabStudents.UseVisualStyleBackColor = true;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Dock = DockStyle.Fill;
            dgvStudents.Location = new Point(3, 78);
            dgvStudents.Margin = new Padding(3, 4, 3, 4);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(1225, 539);
            dgvStudents.TabIndex = 2;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(btnEdit);
            panelFilters.Controls.Add(btnRefresh);
            panelFilters.Controls.Add(cmbGroups);
            panelFilters.Controls.Add(cmbDirections);
            panelFilters.Controls.Add(lblGroups);
            panelFilters.Controls.Add(lblDirections);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(3, 4);
            panelFilters.Margin = new Padding(3, 4, 3, 4);
            panelFilters.Name = "panelFilters";
            panelFilters.Size = new Size(1225, 74);
            panelFilters.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEdit.BackColor = Color.FromArgb(0, 120, 215);
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(967, 20);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(136, 40);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(0, 120, 215);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1109, 20);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(106, 40);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // cmbGroups
            // 
            cmbGroups.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroups.Location = new Point(613, 26);
            cmbGroups.Margin = new Padding(3, 4, 3, 4);
            cmbGroups.Name = "cmbGroups";
            cmbGroups.Size = new Size(244, 28);
            cmbGroups.TabIndex = 5;
            cmbGroups.SelectedIndexChanged += cmbGroups_SelectedIndexChanged;
            // 
            // cmbDirections
            // 
            cmbDirections.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirections.Location = new Point(308, 26);
            cmbDirections.Margin = new Padding(3, 4, 3, 4);
            cmbDirections.Name = "cmbDirections";
            cmbDirections.Size = new Size(244, 28);
            cmbDirections.TabIndex = 4;
            cmbDirections.SelectedIndexChanged += cmbDirections_SelectedIndexChanged;
            // 
            // lblGroups
            // 
            lblGroups.AutoSize = true;
            lblGroups.Location = new Point(613, 6);
            lblGroups.Name = "lblGroups";
            lblGroups.Size = new Size(61, 20);
            lblGroups.TabIndex = 2;
            lblGroups.Text = "Группы";
            // 
            // lblDirections
            // 
            lblDirections.AutoSize = true;
            lblDirections.Location = new Point(308, 6);
            lblDirections.Name = "lblDirections";
            lblDirections.Size = new Size(104, 20);
            lblDirections.TabIndex = 1;
            lblDirections.Text = "Направления";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Location = new Point(3, 617);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(10);
            lblStatus.Size = new Size(191, 40);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Загружено студентов: 0";
            // 
            // lblFaculty
            // 
            lblFaculty.AutoSize = true;
            lblFaculty.Dock = DockStyle.Bottom;
            lblFaculty.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblFaculty.ForeColor = Color.Gray;
            lblFaculty.Location = new Point(3, 590);
            lblFaculty.Name = "lblFaculty";
            lblFaculty.Padding = new Padding(10);
            lblFaculty.Size = new Size(150, 30);
            lblFaculty.TabIndex = 4;
            lblFaculty.Text = "Факультет: —";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1239, 791);
            Controls.Add(panelContent);
            Controls.Add(panelTop);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "University Contingent - Учёт контингента";
            WindowState = FormWindowState.Maximized;
            Shown += MainForm_Shown;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelContent.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabStudents.ResumeLayout(false);
            tabStudents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            panelFilters.ResumeLayout(false);
            panelFilters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem справочникиToolStripMenuItem;
        private ToolStripMenuItem факультетыToolStripMenuItem;
        private ToolStripMenuItem направленияToolStripMenuItem;
        private ToolStripMenuItem группыToolStripMenuItem;
        private ToolStripMenuItem студентыToolStripMenuItem;
        private ToolStripMenuItem приказыToolStripMenuItem;
        private ToolStripMenuItem отчетыToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private Panel panelTop;
        private Label lblUserName;
        private Label lblRole;
        private Button btnLogout;
        private Panel panelContent;
        private TabControl tabControl;
        private TabPage tabStudents;
        private DataGridView dgvStudents;
        private Panel panelFilters;
        private ComboBox cmbGroups;
        private ComboBox cmbDirections;
        private Label lblGroups;
        private Label lblDirections;
        private Button btnRefresh;
        private Button btnEdit;
        private Label lblFaculty;
        private Label lblStatus;
        private ToolStripMenuItem создатьПриказОЗачисленииToolStripMenuItem;
        private ToolStripMenuItem создатьПриказОбОтчисленииToolStripMenuItem;
        private ToolStripMenuItem создатьПриказОПереводеНаСледующийКурсToolStripMenuItem;
        private ToolStripMenuItem создатьПриказОПереводеНаДругоеНаправлениеToolStripMenuItem;
        private ToolStripMenuItem создатьПриказОбАкадемическомОтпускеToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem просмотретьВсеПриказыToolStripMenuItem;
    }
}
