namespace SMM_D4TA_EDITOR
{
    partial class FORM_Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_SelectFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_BYML_To_XML = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_XML_To_BYML = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileDialog_BYML_To_XML = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog_BYML_To_XML = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog_XML_To_BYML = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog_XML_To_BYML = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog_cdtFile = new System.Windows.Forms.OpenFileDialog();
            this.TB_CourseName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BUTTON_SaveFile = new System.Windows.Forms.Button();
            this.BUTTON_Cancel = new System.Windows.Forms.Button();
            this.CHECK_UploadReady = new System.Windows.Forms.CheckBox();
            this.NUMERIC_CourseTimer = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.GroupBox_Scroll_Settings = new System.Windows.Forms.GroupBox();
            this.RADIO_Scroll_Lock = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Cheetah = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Rabbit = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Turtle = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_None = new System.Windows.Forms.RadioButton();
            this.LABEL_LastItemPlaced = new System.Windows.Forms.Label();
            this.GroupBox_Physics_Settings = new System.Windows.Forms.GroupBox();
            this.RADIO_Physics07 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics06 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics05 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics04 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics03 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics02 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics01 = new System.Windows.Forms.RadioButton();
            this.RADIO_Physics00 = new System.Windows.Forms.RadioButton();
            this.LABEL_ClearCheckStatus = new System.Windows.Forms.Label();
            this.GroupBox_CourseStatus = new System.Windows.Forms.GroupBox();
            this.RADIO_CourseStatusRemoved = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusUploaded = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusDownloaded = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusNone = new System.Windows.Forms.RadioButton();
            this.BUTTON_CourseStatusNone = new System.Windows.Forms.Button();
            this.BUTTON_TimerMaximum = new System.Windows.Forms.Button();
            this.BUTTON_TimerMinimum = new System.Windows.Forms.Button();
            this.GroupBox_Style = new System.Windows.Forms.GroupBox();
            this.RADIO_M1 = new System.Windows.Forms.RadioButton();
            this.RADIO_M3 = new System.Windows.Forms.RadioButton();
            this.RADIO_MW = new System.Windows.Forms.RadioButton();
            this.RADIO_WU = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).BeginInit();
            this.GroupBox_Scroll_Settings.SuspendLayout();
            this.GroupBox_Physics_Settings.SuspendLayout();
            this.GroupBox_CourseStatus.SuspendLayout();
            this.GroupBox_Style.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SelectFile,
            this.ToolStripMenuItem_BYML_To_XML,
            this.ToolStripMenuItem_XML_To_BYML});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_SelectFile
            // 
            this.ToolStripMenuItem_SelectFile.Name = "ToolStripMenuItem_SelectFile";
            this.ToolStripMenuItem_SelectFile.Size = new System.Drawing.Size(37, 20);
            this.ToolStripMenuItem_SelectFile.Text = "File";
            this.ToolStripMenuItem_SelectFile.Click += new System.EventHandler(this.ToolStripMenuItem_SelectFile_Click);
            // 
            // ToolStripMenuItem_BYML_To_XML
            // 
            this.ToolStripMenuItem_BYML_To_XML.Name = "ToolStripMenuItem_BYML_To_XML";
            this.ToolStripMenuItem_BYML_To_XML.Size = new System.Drawing.Size(81, 20);
            this.ToolStripMenuItem_BYML_To_XML.Text = "byml → xml";
            this.ToolStripMenuItem_BYML_To_XML.Click += new System.EventHandler(this.ToolStripMenuItem_BYML_To_XML_Click);
            // 
            // ToolStripMenuItem_XML_To_BYML
            // 
            this.ToolStripMenuItem_XML_To_BYML.Name = "ToolStripMenuItem_XML_To_BYML";
            this.ToolStripMenuItem_XML_To_BYML.Size = new System.Drawing.Size(81, 20);
            this.ToolStripMenuItem_XML_To_BYML.Text = "xml → byml";
            this.ToolStripMenuItem_XML_To_BYML.Click += new System.EventHandler(this.ToolStripMenuItem_XML_To_BYML_Click);
            // 
            // SaveFileDialog_BYML_To_XML
            // 
            this.SaveFileDialog_BYML_To_XML.Filter = "File|*.xml";
            // 
            // OpenFileDialog_BYML_To_XML
            // 
            this.OpenFileDialog_BYML_To_XML.Filter = "File|*.BYML;*.Byml;*.byml;*.byaml|All files|*.*";
            // 
            // SaveFileDialog_XML_To_BYML
            // 
            this.SaveFileDialog_XML_To_BYML.Filter = "File|*.byml";
            // 
            // OpenFileDialog_XML_To_BYML
            // 
            this.OpenFileDialog_XML_To_BYML.Filter = "File|*.XML;*.Xml;*.xml|All files|*.*";
            // 
            // OpenFileDialog_cdtFile
            // 
            this.OpenFileDialog_cdtFile.Filter = "File|*.cdt";
            // 
            // TB_CourseName
            // 
            this.TB_CourseName.Enabled = false;
            this.TB_CourseName.Location = new System.Drawing.Point(90, 27);
            this.TB_CourseName.MaxLength = 32;
            this.TB_CourseName.Name = "TB_CourseName";
            this.TB_CourseName.Size = new System.Drawing.Size(210, 20);
            this.TB_CourseName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Course name:";
            // 
            // BUTTON_SaveFile
            // 
            this.BUTTON_SaveFile.Enabled = false;
            this.BUTTON_SaveFile.Location = new System.Drawing.Point(297, 306);
            this.BUTTON_SaveFile.Name = "BUTTON_SaveFile";
            this.BUTTON_SaveFile.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_SaveFile.TabIndex = 11;
            this.BUTTON_SaveFile.Text = "Save course";
            this.BUTTON_SaveFile.UseVisualStyleBackColor = true;
            this.BUTTON_SaveFile.Click += new System.EventHandler(this.BUTTON_SaveFile_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Enabled = false;
            this.BUTTON_Cancel.Location = new System.Drawing.Point(216, 306);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 10;
            this.BUTTON_Cancel.Text = "Cancel";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // CHECK_UploadReady
            // 
            this.CHECK_UploadReady.AutoSize = true;
            this.CHECK_UploadReady.Enabled = false;
            this.CHECK_UploadReady.Location = new System.Drawing.Point(212, 55);
            this.CHECK_UploadReady.Name = "CHECK_UploadReady";
            this.CHECK_UploadReady.Size = new System.Drawing.Size(89, 17);
            this.CHECK_UploadReady.TabIndex = 3;
            this.CHECK_UploadReady.Text = "Upload ready";
            this.CHECK_UploadReady.UseVisualStyleBackColor = true;
            this.CHECK_UploadReady.Visible = false;
            // 
            // NUMERIC_CourseTimer
            // 
            this.NUMERIC_CourseTimer.Enabled = false;
            this.NUMERIC_CourseTimer.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUMERIC_CourseTimer.Location = new System.Drawing.Point(54, 53);
            this.NUMERIC_CourseTimer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NUMERIC_CourseTimer.Name = "NUMERIC_CourseTimer";
            this.NUMERIC_CourseTimer.Size = new System.Drawing.Size(60, 20);
            this.NUMERIC_CourseTimer.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Timer:";
            // 
            // GroupBox_Scroll_Settings
            // 
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Lock);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Cheetah);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Rabbit);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Turtle);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_None);
            this.GroupBox_Scroll_Settings.Enabled = false;
            this.GroupBox_Scroll_Settings.Location = new System.Drawing.Point(123, 232);
            this.GroupBox_Scroll_Settings.Name = "GroupBox_Scroll_Settings";
            this.GroupBox_Scroll_Settings.Size = new System.Drawing.Size(194, 68);
            this.GroupBox_Scroll_Settings.TabIndex = 5;
            this.GroupBox_Scroll_Settings.TabStop = false;
            this.GroupBox_Scroll_Settings.Text = "Autoscroll settings";
            // 
            // RADIO_Scroll_Lock
            // 
            this.RADIO_Scroll_Lock.AutoSize = true;
            this.RADIO_Scroll_Lock.Location = new System.Drawing.Point(64, 19);
            this.RADIO_Scroll_Lock.Name = "RADIO_Scroll_Lock";
            this.RADIO_Scroll_Lock.Size = new System.Drawing.Size(74, 17);
            this.RADIO_Scroll_Lock.TabIndex = 6;
            this.RADIO_Scroll_Lock.TabStop = true;
            this.RADIO_Scroll_Lock.Text = "Scroll lock";
            this.RADIO_Scroll_Lock.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Cheetah
            // 
            this.RADIO_Scroll_Cheetah.AutoSize = true;
            this.RADIO_Scroll_Cheetah.Location = new System.Drawing.Point(126, 45);
            this.RADIO_Scroll_Cheetah.Name = "RADIO_Scroll_Cheetah";
            this.RADIO_Scroll_Cheetah.Size = new System.Drawing.Size(65, 17);
            this.RADIO_Scroll_Cheetah.TabIndex = 9;
            this.RADIO_Scroll_Cheetah.TabStop = true;
            this.RADIO_Scroll_Cheetah.Text = "Cheetah";
            this.RADIO_Scroll_Cheetah.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Rabbit
            // 
            this.RADIO_Scroll_Rabbit.AutoSize = true;
            this.RADIO_Scroll_Rabbit.Location = new System.Drawing.Point(64, 45);
            this.RADIO_Scroll_Rabbit.Name = "RADIO_Scroll_Rabbit";
            this.RADIO_Scroll_Rabbit.Size = new System.Drawing.Size(56, 17);
            this.RADIO_Scroll_Rabbit.TabIndex = 8;
            this.RADIO_Scroll_Rabbit.TabStop = true;
            this.RADIO_Scroll_Rabbit.Text = "Rabbit";
            this.RADIO_Scroll_Rabbit.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Turtle
            // 
            this.RADIO_Scroll_Turtle.AutoSize = true;
            this.RADIO_Scroll_Turtle.Location = new System.Drawing.Point(6, 45);
            this.RADIO_Scroll_Turtle.Name = "RADIO_Scroll_Turtle";
            this.RADIO_Scroll_Turtle.Size = new System.Drawing.Size(52, 17);
            this.RADIO_Scroll_Turtle.TabIndex = 7;
            this.RADIO_Scroll_Turtle.TabStop = true;
            this.RADIO_Scroll_Turtle.Text = "Turtle";
            this.RADIO_Scroll_Turtle.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_None
            // 
            this.RADIO_Scroll_None.AutoSize = true;
            this.RADIO_Scroll_None.Location = new System.Drawing.Point(6, 19);
            this.RADIO_Scroll_None.Name = "RADIO_Scroll_None";
            this.RADIO_Scroll_None.Size = new System.Drawing.Size(51, 17);
            this.RADIO_Scroll_None.TabIndex = 5;
            this.RADIO_Scroll_None.TabStop = true;
            this.RADIO_Scroll_None.Text = "None";
            this.RADIO_Scroll_None.UseVisualStyleBackColor = true;
            // 
            // LABEL_LastItemPlaced
            // 
            this.LABEL_LastItemPlaced.AutoSize = true;
            this.LABEL_LastItemPlaced.Location = new System.Drawing.Point(12, 76);
            this.LABEL_LastItemPlaced.Name = "LABEL_LastItemPlaced";
            this.LABEL_LastItemPlaced.Size = new System.Drawing.Size(132, 13);
            this.LABEL_LastItemPlaced.TabIndex = 12;
            this.LABEL_LastItemPlaced.Text = "Last item placed (memory):";
            // 
            // GroupBox_Physics_Settings
            // 
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics07);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics06);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics05);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics04);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics03);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics02);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics01);
            this.GroupBox_Physics_Settings.Controls.Add(this.RADIO_Physics00);
            this.GroupBox_Physics_Settings.Enabled = false;
            this.GroupBox_Physics_Settings.Location = new System.Drawing.Point(14, 92);
            this.GroupBox_Physics_Settings.Name = "GroupBox_Physics_Settings";
            this.GroupBox_Physics_Settings.Size = new System.Drawing.Size(100, 208);
            this.GroupBox_Physics_Settings.TabIndex = 13;
            this.GroupBox_Physics_Settings.TabStop = false;
            this.GroupBox_Physics_Settings.Text = "Physics settings";
            // 
            // RADIO_Physics07
            // 
            this.RADIO_Physics07.AutoSize = true;
            this.RADIO_Physics07.Location = new System.Drawing.Point(6, 180);
            this.RADIO_Physics07.Name = "RADIO_Physics07";
            this.RADIO_Physics07.Size = new System.Drawing.Size(90, 17);
            this.RADIO_Physics07.TabIndex = 21;
            this.RADIO_Physics07.TabStop = true;
            this.RADIO_Physics07.Text = "Version 1.46?";
            this.RADIO_Physics07.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics06
            // 
            this.RADIO_Physics06.AutoSize = true;
            this.RADIO_Physics06.Location = new System.Drawing.Point(6, 157);
            this.RADIO_Physics06.Name = "RADIO_Physics06";
            this.RADIO_Physics06.Size = new System.Drawing.Size(84, 17);
            this.RADIO_Physics06.TabIndex = 20;
            this.RADIO_Physics06.TabStop = true;
            this.RADIO_Physics06.Text = "Version 1.45";
            this.RADIO_Physics06.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics05
            // 
            this.RADIO_Physics05.AutoSize = true;
            this.RADIO_Physics05.Location = new System.Drawing.Point(6, 134);
            this.RADIO_Physics05.Name = "RADIO_Physics05";
            this.RADIO_Physics05.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Physics05.TabIndex = 19;
            this.RADIO_Physics05.TabStop = true;
            this.RADIO_Physics05.Text = "Version 1.XX";
            this.RADIO_Physics05.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics04
            // 
            this.RADIO_Physics04.AutoSize = true;
            this.RADIO_Physics04.Location = new System.Drawing.Point(6, 111);
            this.RADIO_Physics04.Name = "RADIO_Physics04";
            this.RADIO_Physics04.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Physics04.TabIndex = 18;
            this.RADIO_Physics04.TabStop = true;
            this.RADIO_Physics04.Text = "Version 1.XX";
            this.RADIO_Physics04.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics03
            // 
            this.RADIO_Physics03.AutoSize = true;
            this.RADIO_Physics03.Location = new System.Drawing.Point(6, 88);
            this.RADIO_Physics03.Name = "RADIO_Physics03";
            this.RADIO_Physics03.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Physics03.TabIndex = 17;
            this.RADIO_Physics03.TabStop = true;
            this.RADIO_Physics03.Text = "Version 1.XX";
            this.RADIO_Physics03.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics02
            // 
            this.RADIO_Physics02.AutoSize = true;
            this.RADIO_Physics02.Location = new System.Drawing.Point(6, 65);
            this.RADIO_Physics02.Name = "RADIO_Physics02";
            this.RADIO_Physics02.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Physics02.TabIndex = 16;
            this.RADIO_Physics02.TabStop = true;
            this.RADIO_Physics02.Text = "Version 1.XX";
            this.RADIO_Physics02.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics01
            // 
            this.RADIO_Physics01.AutoSize = true;
            this.RADIO_Physics01.Location = new System.Drawing.Point(6, 42);
            this.RADIO_Physics01.Name = "RADIO_Physics01";
            this.RADIO_Physics01.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Physics01.TabIndex = 15;
            this.RADIO_Physics01.TabStop = true;
            this.RADIO_Physics01.Text = "Version 1.XX";
            this.RADIO_Physics01.UseVisualStyleBackColor = true;
            // 
            // RADIO_Physics00
            // 
            this.RADIO_Physics00.AutoSize = true;
            this.RADIO_Physics00.Location = new System.Drawing.Point(6, 19);
            this.RADIO_Physics00.Name = "RADIO_Physics00";
            this.RADIO_Physics00.Size = new System.Drawing.Size(84, 17);
            this.RADIO_Physics00.TabIndex = 14;
            this.RADIO_Physics00.TabStop = true;
            this.RADIO_Physics00.Text = "Version 1.00";
            this.RADIO_Physics00.UseVisualStyleBackColor = true;
            // 
            // LABEL_ClearCheckStatus
            // 
            this.LABEL_ClearCheckStatus.AutoSize = true;
            this.LABEL_ClearCheckStatus.Location = new System.Drawing.Point(308, 30);
            this.LABEL_ClearCheckStatus.Name = "LABEL_ClearCheckStatus";
            this.LABEL_ClearCheckStatus.Size = new System.Drawing.Size(64, 13);
            this.LABEL_ClearCheckStatus.TabIndex = 14;
            this.LABEL_ClearCheckStatus.Text = "Clear check";
            // 
            // GroupBox_CourseStatus
            // 
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusRemoved);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusUploaded);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusDownloaded);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusNone);
            this.GroupBox_CourseStatus.Enabled = false;
            this.GroupBox_CourseStatus.Location = new System.Drawing.Point(123, 92);
            this.GroupBox_CourseStatus.Name = "GroupBox_CourseStatus";
            this.GroupBox_CourseStatus.Size = new System.Drawing.Size(100, 110);
            this.GroupBox_CourseStatus.TabIndex = 15;
            this.GroupBox_CourseStatus.TabStop = false;
            this.GroupBox_CourseStatus.Text = "Course status";
            // 
            // RADIO_CourseStatusRemoved
            // 
            this.RADIO_CourseStatusRemoved.AutoSize = true;
            this.RADIO_CourseStatusRemoved.Location = new System.Drawing.Point(6, 88);
            this.RADIO_CourseStatusRemoved.Name = "RADIO_CourseStatusRemoved";
            this.RADIO_CourseStatusRemoved.Size = new System.Drawing.Size(71, 17);
            this.RADIO_CourseStatusRemoved.TabIndex = 19;
            this.RADIO_CourseStatusRemoved.TabStop = true;
            this.RADIO_CourseStatusRemoved.Text = "Removed";
            this.RADIO_CourseStatusRemoved.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusUploaded
            // 
            this.RADIO_CourseStatusUploaded.AutoSize = true;
            this.RADIO_CourseStatusUploaded.Location = new System.Drawing.Point(6, 65);
            this.RADIO_CourseStatusUploaded.Name = "RADIO_CourseStatusUploaded";
            this.RADIO_CourseStatusUploaded.Size = new System.Drawing.Size(71, 17);
            this.RADIO_CourseStatusUploaded.TabIndex = 18;
            this.RADIO_CourseStatusUploaded.TabStop = true;
            this.RADIO_CourseStatusUploaded.Text = "Uploaded";
            this.RADIO_CourseStatusUploaded.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusDownloaded
            // 
            this.RADIO_CourseStatusDownloaded.AutoSize = true;
            this.RADIO_CourseStatusDownloaded.Location = new System.Drawing.Point(6, 42);
            this.RADIO_CourseStatusDownloaded.Name = "RADIO_CourseStatusDownloaded";
            this.RADIO_CourseStatusDownloaded.Size = new System.Drawing.Size(85, 17);
            this.RADIO_CourseStatusDownloaded.TabIndex = 17;
            this.RADIO_CourseStatusDownloaded.TabStop = true;
            this.RADIO_CourseStatusDownloaded.Text = "Downloaded";
            this.RADIO_CourseStatusDownloaded.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusNone
            // 
            this.RADIO_CourseStatusNone.AutoSize = true;
            this.RADIO_CourseStatusNone.Location = new System.Drawing.Point(6, 19);
            this.RADIO_CourseStatusNone.Name = "RADIO_CourseStatusNone";
            this.RADIO_CourseStatusNone.Size = new System.Drawing.Size(51, 17);
            this.RADIO_CourseStatusNone.TabIndex = 16;
            this.RADIO_CourseStatusNone.TabStop = true;
            this.RADIO_CourseStatusNone.Text = "None";
            this.RADIO_CourseStatusNone.UseVisualStyleBackColor = true;
            // 
            // BUTTON_CourseStatusNone
            // 
            this.BUTTON_CourseStatusNone.Enabled = false;
            this.BUTTON_CourseStatusNone.Location = new System.Drawing.Point(123, 203);
            this.BUTTON_CourseStatusNone.Name = "BUTTON_CourseStatusNone";
            this.BUTTON_CourseStatusNone.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_CourseStatusNone.TabIndex = 16;
            this.BUTTON_CourseStatusNone.Text = "Set none";
            this.BUTTON_CourseStatusNone.UseVisualStyleBackColor = true;
            this.BUTTON_CourseStatusNone.Click += new System.EventHandler(this.BUTTON_CourseStatusNone_Click);
            // 
            // BUTTON_TimerMaximum
            // 
            this.BUTTON_TimerMaximum.Enabled = false;
            this.BUTTON_TimerMaximum.Location = new System.Drawing.Point(166, 51);
            this.BUTTON_TimerMaximum.Name = "BUTTON_TimerMaximum";
            this.BUTTON_TimerMaximum.Size = new System.Drawing.Size(40, 23);
            this.BUTTON_TimerMaximum.TabIndex = 17;
            this.BUTTON_TimerMaximum.Text = "Max";
            this.BUTTON_TimerMaximum.UseVisualStyleBackColor = true;
            this.BUTTON_TimerMaximum.Click += new System.EventHandler(this.BUTTON_TimerMaximum_Click);
            // 
            // BUTTON_TimerMinimum
            // 
            this.BUTTON_TimerMinimum.Enabled = false;
            this.BUTTON_TimerMinimum.Location = new System.Drawing.Point(120, 51);
            this.BUTTON_TimerMinimum.Name = "BUTTON_TimerMinimum";
            this.BUTTON_TimerMinimum.Size = new System.Drawing.Size(40, 23);
            this.BUTTON_TimerMinimum.TabIndex = 18;
            this.BUTTON_TimerMinimum.Text = "Min";
            this.BUTTON_TimerMinimum.UseVisualStyleBackColor = true;
            this.BUTTON_TimerMinimum.Click += new System.EventHandler(this.BUTTON_TimerMinimum_Click);
            // 
            // GroupBox_Style
            // 
            this.GroupBox_Style.Controls.Add(this.RADIO_WU);
            this.GroupBox_Style.Controls.Add(this.RADIO_MW);
            this.GroupBox_Style.Controls.Add(this.RADIO_M3);
            this.GroupBox_Style.Controls.Add(this.RADIO_M1);
            this.GroupBox_Style.Enabled = false;
            this.GroupBox_Style.Location = new System.Drawing.Point(229, 92);
            this.GroupBox_Style.Name = "GroupBox_Style";
            this.GroupBox_Style.Size = new System.Drawing.Size(75, 110);
            this.GroupBox_Style.TabIndex = 19;
            this.GroupBox_Style.TabStop = false;
            this.GroupBox_Style.Text = "Style";
            // 
            // RADIO_M1
            // 
            this.RADIO_M1.AutoSize = true;
            this.RADIO_M1.Location = new System.Drawing.Point(6, 19);
            this.RADIO_M1.Name = "RADIO_M1";
            this.RADIO_M1.Size = new System.Drawing.Size(54, 17);
            this.RADIO_M1.TabIndex = 0;
            this.RADIO_M1.TabStop = true;
            this.RADIO_M1.Text = "SMB1";
            this.RADIO_M1.UseVisualStyleBackColor = true;
            // 
            // RADIO_M3
            // 
            this.RADIO_M3.AutoSize = true;
            this.RADIO_M3.Location = new System.Drawing.Point(6, 42);
            this.RADIO_M3.Name = "RADIO_M3";
            this.RADIO_M3.Size = new System.Drawing.Size(54, 17);
            this.RADIO_M3.TabIndex = 1;
            this.RADIO_M3.TabStop = true;
            this.RADIO_M3.Text = "SMB3";
            this.RADIO_M3.UseVisualStyleBackColor = true;
            // 
            // RADIO_MW
            // 
            this.RADIO_MW.AutoSize = true;
            this.RADIO_MW.Location = new System.Drawing.Point(6, 65);
            this.RADIO_MW.Name = "RADIO_MW";
            this.RADIO_MW.Size = new System.Drawing.Size(52, 17);
            this.RADIO_MW.TabIndex = 2;
            this.RADIO_MW.TabStop = true;
            this.RADIO_MW.Text = "SMW";
            this.RADIO_MW.UseVisualStyleBackColor = true;
            // 
            // RADIO_WU
            // 
            this.RADIO_WU.AutoSize = true;
            this.RADIO_WU.Location = new System.Drawing.Point(6, 88);
            this.RADIO_WU.Name = "RADIO_WU";
            this.RADIO_WU.Size = new System.Drawing.Size(64, 17);
            this.RADIO_WU.TabIndex = 3;
            this.RADIO_WU.TabStop = true;
            this.RADIO_WU.Text = "NSMBU";
            this.RADIO_WU.UseVisualStyleBackColor = true;
            // 
            // FORM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 341);
            this.Controls.Add(this.GroupBox_Style);
            this.Controls.Add(this.BUTTON_TimerMinimum);
            this.Controls.Add(this.BUTTON_TimerMaximum);
            this.Controls.Add(this.BUTTON_CourseStatusNone);
            this.Controls.Add(this.GroupBox_CourseStatus);
            this.Controls.Add(this.LABEL_ClearCheckStatus);
            this.Controls.Add(this.GroupBox_Physics_Settings);
            this.Controls.Add(this.LABEL_LastItemPlaced);
            this.Controls.Add(this.GroupBox_Scroll_Settings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NUMERIC_CourseTimer);
            this.Controls.Add(this.CHECK_UploadReady);
            this.Controls.Add(this.BUTTON_Cancel);
            this.Controls.Add(this.BUTTON_SaveFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_CourseName);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FORM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "v0.0.X D4TA EDITOR";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).EndInit();
            this.GroupBox_Scroll_Settings.ResumeLayout(false);
            this.GroupBox_Scroll_Settings.PerformLayout();
            this.GroupBox_Physics_Settings.ResumeLayout(false);
            this.GroupBox_Physics_Settings.PerformLayout();
            this.GroupBox_CourseStatus.ResumeLayout(false);
            this.GroupBox_CourseStatus.PerformLayout();
            this.GroupBox_Style.ResumeLayout(false);
            this.GroupBox_Style.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BYML_To_XML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_XML_To_BYML;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_BYML_To_XML;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_BYML_To_XML;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_XML_To_BYML;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_XML_To_BYML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SelectFile;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_cdtFile;
        private System.Windows.Forms.TextBox TB_CourseName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BUTTON_SaveFile;
        private System.Windows.Forms.Button BUTTON_Cancel;
        private System.Windows.Forms.CheckBox CHECK_UploadReady;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GroupBox_Scroll_Settings;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Cheetah;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Rabbit;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Turtle;
        private System.Windows.Forms.RadioButton RADIO_Scroll_None;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Lock;
        private System.Windows.Forms.Label LABEL_LastItemPlaced;
        private System.Windows.Forms.GroupBox GroupBox_Physics_Settings;
        private System.Windows.Forms.RadioButton RADIO_Physics07;
        private System.Windows.Forms.RadioButton RADIO_Physics06;
        private System.Windows.Forms.RadioButton RADIO_Physics05;
        private System.Windows.Forms.RadioButton RADIO_Physics04;
        private System.Windows.Forms.RadioButton RADIO_Physics03;
        private System.Windows.Forms.RadioButton RADIO_Physics02;
        private System.Windows.Forms.RadioButton RADIO_Physics01;
        private System.Windows.Forms.RadioButton RADIO_Physics00;
        private System.Windows.Forms.Label LABEL_ClearCheckStatus;
        private System.Windows.Forms.GroupBox GroupBox_CourseStatus;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusDownloaded;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusNone;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusRemoved;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusUploaded;
        private System.Windows.Forms.Button BUTTON_CourseStatusNone;
        private System.Windows.Forms.Button BUTTON_TimerMaximum;
        private System.Windows.Forms.Button BUTTON_TimerMinimum;
        private System.Windows.Forms.GroupBox GroupBox_Style;
        private System.Windows.Forms.RadioButton RADIO_WU;
        private System.Windows.Forms.RadioButton RADIO_MW;
        private System.Windows.Forms.RadioButton RADIO_M3;
        private System.Windows.Forms.RadioButton RADIO_M1;
    }
}

