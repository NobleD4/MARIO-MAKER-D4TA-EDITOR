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
            this.ToolStripMenuItem_BYMLConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_XML_To_BYML = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_BYML_To_XML = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_TNLConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_IMAGE_To_TNL = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_TNL_To_IMAGE = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripComboBox_Language_Settings = new System.Windows.Forms.ToolStripComboBox();
            this.SaveFileDialog_BYML_To_XML = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog_BYML_To_XML = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog_XML_To_BYML = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog_XML_To_BYML = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog_cdtFile = new System.Windows.Forms.OpenFileDialog();
            this.TB_CourseName = new System.Windows.Forms.TextBox();
            this.LABEL_CourseName = new System.Windows.Forms.Label();
            this.BUTTON_SaveFile = new System.Windows.Forms.Button();
            this.BUTTON_Cancel = new System.Windows.Forms.Button();
            this.CHECK_UploadReady = new System.Windows.Forms.CheckBox();
            this.NUMERIC_CourseTimer = new System.Windows.Forms.NumericUpDown();
            this.LABEL_Timer = new System.Windows.Forms.Label();
            this.GroupBox_Scroll_Settings = new System.Windows.Forms.GroupBox();
            this.RADIO_Scroll_Lock = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Cheetah = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Rabbit = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_Turtle = new System.Windows.Forms.RadioButton();
            this.RADIO_Scroll_None = new System.Windows.Forms.RadioButton();
            this.LABEL_LastItemPlaced = new System.Windows.Forms.Label();
            this.LABEL_ClearCheckStatus = new System.Windows.Forms.Label();
            this.GroupBox_CourseStatus = new System.Windows.Forms.GroupBox();
            this.RADIO_CourseStatusRemoved = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusUploaded = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusDownloaded = new System.Windows.Forms.RadioButton();
            this.RADIO_CourseStatusNone = new System.Windows.Forms.RadioButton();
            this.BUTTON_CourseStatusNone = new System.Windows.Forms.Button();
            this.BUTTON_TimerMaximum = new System.Windows.Forms.Button();
            this.BUTTON_TimerMinimum = new System.Windows.Forms.Button();
            this.OpenFileDialog_IMAGE_To_TNL = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog_TNL_To_IMAGE = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog_IMAGE_To_TNL = new System.Windows.Forms.SaveFileDialog();
            this.SaveFileDialog_TNL_To_IMAGE = new System.Windows.Forms.SaveFileDialog();
            this.ComboBox_Style_Settings = new System.Windows.Forms.ComboBox();
            this.ComboBox_Physics_Settings = new System.Windows.Forms.ComboBox();
            this.LABEL_Physics = new System.Windows.Forms.Label();
            this.LABEL_Style = new System.Windows.Forms.Label();
            this.ComboBox_Theme_Settings = new System.Windows.Forms.ComboBox();
            this.LABEL_Theme = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).BeginInit();
            this.GroupBox_Scroll_Settings.SuspendLayout();
            this.GroupBox_CourseStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SelectFile,
            this.ToolStripMenuItem_BYMLConverter,
            this.ToolStripMenuItem_TNLConverter,
            this.ToolStripComboBox_Language_Settings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(494, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_SelectFile
            // 
            this.ToolStripMenuItem_SelectFile.Name = "ToolStripMenuItem_SelectFile";
            this.ToolStripMenuItem_SelectFile.Size = new System.Drawing.Size(53, 23);
            this.ToolStripMenuItem_SelectFile.Text = "<File>";
            this.ToolStripMenuItem_SelectFile.Click += new System.EventHandler(this.ToolStripMenuItem_SelectFile_Click);
            // 
            // ToolStripMenuItem_BYMLConverter
            // 
            this.ToolStripMenuItem_BYMLConverter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_XML_To_BYML,
            this.ToolStripMenuItem_BYML_To_XML});
            this.ToolStripMenuItem_BYMLConverter.Name = "ToolStripMenuItem_BYMLConverter";
            this.ToolStripMenuItem_BYMLConverter.Size = new System.Drawing.Size(119, 23);
            this.ToolStripMenuItem_BYMLConverter.Text = "<BYML converter>";
            // 
            // ToolStripMenuItem_XML_To_BYML
            // 
            this.ToolStripMenuItem_XML_To_BYML.Name = "ToolStripMenuItem_XML_To_BYML";
            this.ToolStripMenuItem_XML_To_BYML.Size = new System.Drawing.Size(161, 22);
            this.ToolStripMenuItem_XML_To_BYML.Text = "<XML → BYML>";
            this.ToolStripMenuItem_XML_To_BYML.Click += new System.EventHandler(this.ToolStripMenuItem_XML_To_BYML_Click);
            // 
            // ToolStripMenuItem_BYML_To_XML
            // 
            this.ToolStripMenuItem_BYML_To_XML.Name = "ToolStripMenuItem_BYML_To_XML";
            this.ToolStripMenuItem_BYML_To_XML.Size = new System.Drawing.Size(161, 22);
            this.ToolStripMenuItem_BYML_To_XML.Text = "<BYML → XML>";
            this.ToolStripMenuItem_BYML_To_XML.Click += new System.EventHandler(this.ToolStripMenuItem_BYML_To_XML_Click);
            // 
            // ToolStripMenuItem_TNLConverter
            // 
            this.ToolStripMenuItem_TNLConverter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_IMAGE_To_TNL,
            this.ToolStripMenuItem_TNL_To_IMAGE});
            this.ToolStripMenuItem_TNLConverter.Name = "ToolStripMenuItem_TNLConverter";
            this.ToolStripMenuItem_TNLConverter.Size = new System.Drawing.Size(110, 23);
            this.ToolStripMenuItem_TNLConverter.Text = "<TNL converter>";
            // 
            // ToolStripMenuItem_IMAGE_To_TNL
            // 
            this.ToolStripMenuItem_IMAGE_To_TNL.Name = "ToolStripMenuItem_IMAGE_To_TNL";
            this.ToolStripMenuItem_IMAGE_To_TNL.Size = new System.Drawing.Size(164, 22);
            this.ToolStripMenuItem_IMAGE_To_TNL.Text = "<IMAGE → TNL>";
            this.ToolStripMenuItem_IMAGE_To_TNL.Click += new System.EventHandler(this.ToolStripMenuItem_IMAGE_To_TNL_Click);
            // 
            // ToolStripMenuItem_TNL_To_IMAGE
            // 
            this.ToolStripMenuItem_TNL_To_IMAGE.Name = "ToolStripMenuItem_TNL_To_IMAGE";
            this.ToolStripMenuItem_TNL_To_IMAGE.Size = new System.Drawing.Size(164, 22);
            this.ToolStripMenuItem_TNL_To_IMAGE.Text = "<TNL → IMAGE>";
            this.ToolStripMenuItem_TNL_To_IMAGE.Click += new System.EventHandler(this.ToolStripMenuItem_TNL_To_IMAGE_Click);
            // 
            // ToolStripComboBox_Language_Settings
            // 
            this.ToolStripComboBox_Language_Settings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ToolStripComboBox_Language_Settings.Name = "ToolStripComboBox_Language_Settings";
            this.ToolStripComboBox_Language_Settings.Size = new System.Drawing.Size(121, 23);
            this.ToolStripComboBox_Language_Settings.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBox_Language_Settings_SelectedIndexChanged);
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
            this.TB_CourseName.Location = new System.Drawing.Point(121, 46);
            this.TB_CourseName.MaxLength = 32;
            this.TB_CourseName.Name = "TB_CourseName";
            this.TB_CourseName.Size = new System.Drawing.Size(210, 20);
            this.TB_CourseName.TabIndex = 0;
            // 
            // LABEL_CourseName
            // 
            this.LABEL_CourseName.AutoSize = true;
            this.LABEL_CourseName.Location = new System.Drawing.Point(118, 30);
            this.LABEL_CourseName.Name = "LABEL_CourseName";
            this.LABEL_CourseName.Size = new System.Drawing.Size(81, 13);
            this.LABEL_CourseName.TabIndex = 0;
            this.LABEL_CourseName.Text = "<Course name>";
            // 
            // BUTTON_SaveFile
            // 
            this.BUTTON_SaveFile.Enabled = false;
            this.BUTTON_SaveFile.Location = new System.Drawing.Point(382, 286);
            this.BUTTON_SaveFile.Name = "BUTTON_SaveFile";
            this.BUTTON_SaveFile.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_SaveFile.TabIndex = 12;
            this.BUTTON_SaveFile.Text = "<Save course>";
            this.BUTTON_SaveFile.UseVisualStyleBackColor = true;
            this.BUTTON_SaveFile.Click += new System.EventHandler(this.BUTTON_SaveFile_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Enabled = false;
            this.BUTTON_Cancel.Location = new System.Drawing.Point(301, 286);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 11;
            this.BUTTON_Cancel.Text = "<Cancel>";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // CHECK_UploadReady
            // 
            this.CHECK_UploadReady.AutoSize = true;
            this.CHECK_UploadReady.Enabled = false;
            this.CHECK_UploadReady.Location = new System.Drawing.Point(299, 88);
            this.CHECK_UploadReady.Name = "CHECK_UploadReady";
            this.CHECK_UploadReady.Size = new System.Drawing.Size(101, 17);
            this.CHECK_UploadReady.TabIndex = 5;
            this.CHECK_UploadReady.Text = "<Upload ready>";
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
            this.NUMERIC_CourseTimer.Location = new System.Drawing.Point(121, 85);
            this.NUMERIC_CourseTimer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NUMERIC_CourseTimer.Name = "NUMERIC_CourseTimer";
            this.NUMERIC_CourseTimer.Size = new System.Drawing.Size(60, 20);
            this.NUMERIC_CourseTimer.TabIndex = 2;
            // 
            // LABEL_Timer
            // 
            this.LABEL_Timer.AutoSize = true;
            this.LABEL_Timer.Location = new System.Drawing.Point(118, 69);
            this.LABEL_Timer.Name = "LABEL_Timer";
            this.LABEL_Timer.Size = new System.Drawing.Size(45, 13);
            this.LABEL_Timer.TabIndex = 6;
            this.LABEL_Timer.Text = "<Timer>";
            // 
            // GroupBox_Scroll_Settings
            // 
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Lock);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Cheetah);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Rabbit);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_Turtle);
            this.GroupBox_Scroll_Settings.Controls.Add(this.RADIO_Scroll_None);
            this.GroupBox_Scroll_Settings.Enabled = false;
            this.GroupBox_Scroll_Settings.Location = new System.Drawing.Point(12, 153);
            this.GroupBox_Scroll_Settings.Name = "GroupBox_Scroll_Settings";
            this.GroupBox_Scroll_Settings.Size = new System.Drawing.Size(160, 135);
            this.GroupBox_Scroll_Settings.TabIndex = 7;
            this.GroupBox_Scroll_Settings.TabStop = false;
            this.GroupBox_Scroll_Settings.Text = "<Autoscroll>";
            // 
            // RADIO_Scroll_Lock
            // 
            this.RADIO_Scroll_Lock.AutoSize = true;
            this.RADIO_Scroll_Lock.Location = new System.Drawing.Point(6, 42);
            this.RADIO_Scroll_Lock.Name = "RADIO_Scroll_Lock";
            this.RADIO_Scroll_Lock.Size = new System.Drawing.Size(86, 17);
            this.RADIO_Scroll_Lock.TabIndex = 6;
            this.RADIO_Scroll_Lock.TabStop = true;
            this.RADIO_Scroll_Lock.Text = "<Scroll lock>";
            this.RADIO_Scroll_Lock.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Cheetah
            // 
            this.RADIO_Scroll_Cheetah.AutoSize = true;
            this.RADIO_Scroll_Cheetah.Location = new System.Drawing.Point(6, 111);
            this.RADIO_Scroll_Cheetah.Name = "RADIO_Scroll_Cheetah";
            this.RADIO_Scroll_Cheetah.Size = new System.Drawing.Size(77, 17);
            this.RADIO_Scroll_Cheetah.TabIndex = 9;
            this.RADIO_Scroll_Cheetah.TabStop = true;
            this.RADIO_Scroll_Cheetah.Text = "<Cheetah>";
            this.RADIO_Scroll_Cheetah.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Rabbit
            // 
            this.RADIO_Scroll_Rabbit.AutoSize = true;
            this.RADIO_Scroll_Rabbit.Location = new System.Drawing.Point(6, 88);
            this.RADIO_Scroll_Rabbit.Name = "RADIO_Scroll_Rabbit";
            this.RADIO_Scroll_Rabbit.Size = new System.Drawing.Size(68, 17);
            this.RADIO_Scroll_Rabbit.TabIndex = 8;
            this.RADIO_Scroll_Rabbit.TabStop = true;
            this.RADIO_Scroll_Rabbit.Text = "<Rabbit>";
            this.RADIO_Scroll_Rabbit.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_Turtle
            // 
            this.RADIO_Scroll_Turtle.AutoSize = true;
            this.RADIO_Scroll_Turtle.Location = new System.Drawing.Point(6, 65);
            this.RADIO_Scroll_Turtle.Name = "RADIO_Scroll_Turtle";
            this.RADIO_Scroll_Turtle.Size = new System.Drawing.Size(64, 17);
            this.RADIO_Scroll_Turtle.TabIndex = 7;
            this.RADIO_Scroll_Turtle.TabStop = true;
            this.RADIO_Scroll_Turtle.Text = "<Turtle>";
            this.RADIO_Scroll_Turtle.UseVisualStyleBackColor = true;
            // 
            // RADIO_Scroll_None
            // 
            this.RADIO_Scroll_None.AutoSize = true;
            this.RADIO_Scroll_None.Location = new System.Drawing.Point(6, 19);
            this.RADIO_Scroll_None.Name = "RADIO_Scroll_None";
            this.RADIO_Scroll_None.Size = new System.Drawing.Size(63, 17);
            this.RADIO_Scroll_None.TabIndex = 5;
            this.RADIO_Scroll_None.TabStop = true;
            this.RADIO_Scroll_None.Text = "<None>";
            this.RADIO_Scroll_None.UseVisualStyleBackColor = true;
            // 
            // LABEL_LastItemPlaced
            // 
            this.LABEL_LastItemPlaced.AutoSize = true;
            this.LABEL_LastItemPlaced.Location = new System.Drawing.Point(118, 129);
            this.LABEL_LastItemPlaced.Name = "LABEL_LastItemPlaced";
            this.LABEL_LastItemPlaced.Size = new System.Drawing.Size(144, 13);
            this.LABEL_LastItemPlaced.TabIndex = 12;
            this.LABEL_LastItemPlaced.Text = "<Last item placed (memory):>";
            // 
            // LABEL_ClearCheckStatus
            // 
            this.LABEL_ClearCheckStatus.AutoSize = true;
            this.LABEL_ClearCheckStatus.Location = new System.Drawing.Point(337, 49);
            this.LABEL_ClearCheckStatus.Name = "LABEL_ClearCheckStatus";
            this.LABEL_ClearCheckStatus.Size = new System.Drawing.Size(76, 13);
            this.LABEL_ClearCheckStatus.TabIndex = 14;
            this.LABEL_ClearCheckStatus.Text = "<Clear check>";
            // 
            // GroupBox_CourseStatus
            // 
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusRemoved);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusUploaded);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusDownloaded);
            this.GroupBox_CourseStatus.Controls.Add(this.RADIO_CourseStatusNone);
            this.GroupBox_CourseStatus.Enabled = false;
            this.GroupBox_CourseStatus.Location = new System.Drawing.Point(178, 153);
            this.GroupBox_CourseStatus.Name = "GroupBox_CourseStatus";
            this.GroupBox_CourseStatus.Size = new System.Drawing.Size(125, 110);
            this.GroupBox_CourseStatus.TabIndex = 9;
            this.GroupBox_CourseStatus.TabStop = false;
            this.GroupBox_CourseStatus.Text = "<Course status>";
            // 
            // RADIO_CourseStatusRemoved
            // 
            this.RADIO_CourseStatusRemoved.AutoSize = true;
            this.RADIO_CourseStatusRemoved.Location = new System.Drawing.Point(6, 88);
            this.RADIO_CourseStatusRemoved.Name = "RADIO_CourseStatusRemoved";
            this.RADIO_CourseStatusRemoved.Size = new System.Drawing.Size(83, 17);
            this.RADIO_CourseStatusRemoved.TabIndex = 19;
            this.RADIO_CourseStatusRemoved.TabStop = true;
            this.RADIO_CourseStatusRemoved.Text = "<Removed>";
            this.RADIO_CourseStatusRemoved.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusUploaded
            // 
            this.RADIO_CourseStatusUploaded.AutoSize = true;
            this.RADIO_CourseStatusUploaded.Location = new System.Drawing.Point(6, 65);
            this.RADIO_CourseStatusUploaded.Name = "RADIO_CourseStatusUploaded";
            this.RADIO_CourseStatusUploaded.Size = new System.Drawing.Size(83, 17);
            this.RADIO_CourseStatusUploaded.TabIndex = 18;
            this.RADIO_CourseStatusUploaded.TabStop = true;
            this.RADIO_CourseStatusUploaded.Text = "<Uploaded>";
            this.RADIO_CourseStatusUploaded.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusDownloaded
            // 
            this.RADIO_CourseStatusDownloaded.AutoSize = true;
            this.RADIO_CourseStatusDownloaded.Location = new System.Drawing.Point(6, 42);
            this.RADIO_CourseStatusDownloaded.Name = "RADIO_CourseStatusDownloaded";
            this.RADIO_CourseStatusDownloaded.Size = new System.Drawing.Size(97, 17);
            this.RADIO_CourseStatusDownloaded.TabIndex = 17;
            this.RADIO_CourseStatusDownloaded.TabStop = true;
            this.RADIO_CourseStatusDownloaded.Text = "<Downloaded>";
            this.RADIO_CourseStatusDownloaded.UseVisualStyleBackColor = true;
            // 
            // RADIO_CourseStatusNone
            // 
            this.RADIO_CourseStatusNone.AutoSize = true;
            this.RADIO_CourseStatusNone.Location = new System.Drawing.Point(6, 19);
            this.RADIO_CourseStatusNone.Name = "RADIO_CourseStatusNone";
            this.RADIO_CourseStatusNone.Size = new System.Drawing.Size(63, 17);
            this.RADIO_CourseStatusNone.TabIndex = 16;
            this.RADIO_CourseStatusNone.TabStop = true;
            this.RADIO_CourseStatusNone.Text = "<None>";
            this.RADIO_CourseStatusNone.UseVisualStyleBackColor = true;
            // 
            // BUTTON_CourseStatusNone
            // 
            this.BUTTON_CourseStatusNone.Enabled = false;
            this.BUTTON_CourseStatusNone.Location = new System.Drawing.Point(178, 265);
            this.BUTTON_CourseStatusNone.Name = "BUTTON_CourseStatusNone";
            this.BUTTON_CourseStatusNone.Size = new System.Drawing.Size(125, 23);
            this.BUTTON_CourseStatusNone.TabIndex = 10;
            this.BUTTON_CourseStatusNone.Text = "<Set none>";
            this.BUTTON_CourseStatusNone.UseVisualStyleBackColor = true;
            this.BUTTON_CourseStatusNone.Click += new System.EventHandler(this.BUTTON_CourseStatusNone_Click);
            // 
            // BUTTON_TimerMaximum
            // 
            this.BUTTON_TimerMaximum.Enabled = false;
            this.BUTTON_TimerMaximum.Location = new System.Drawing.Point(243, 84);
            this.BUTTON_TimerMaximum.Name = "BUTTON_TimerMaximum";
            this.BUTTON_TimerMaximum.Size = new System.Drawing.Size(50, 23);
            this.BUTTON_TimerMaximum.TabIndex = 4;
            this.BUTTON_TimerMaximum.Text = "<Max>";
            this.BUTTON_TimerMaximum.UseVisualStyleBackColor = true;
            this.BUTTON_TimerMaximum.Click += new System.EventHandler(this.BUTTON_TimerMaximum_Click);
            // 
            // BUTTON_TimerMinimum
            // 
            this.BUTTON_TimerMinimum.Enabled = false;
            this.BUTTON_TimerMinimum.Location = new System.Drawing.Point(187, 84);
            this.BUTTON_TimerMinimum.Name = "BUTTON_TimerMinimum";
            this.BUTTON_TimerMinimum.Size = new System.Drawing.Size(50, 23);
            this.BUTTON_TimerMinimum.TabIndex = 3;
            this.BUTTON_TimerMinimum.Text = "<Min>";
            this.BUTTON_TimerMinimum.UseVisualStyleBackColor = true;
            this.BUTTON_TimerMinimum.Click += new System.EventHandler(this.BUTTON_TimerMinimum_Click);
            // 
            // OpenFileDialog_IMAGE_To_TNL
            // 
            this.OpenFileDialog_IMAGE_To_TNL.Filter = "File|*.JPEG;*.JPG;*.PNG;*.BMP;*.GIF;*.jpeg;*.jpg;*.png;*.bmp;*.gif";
            // 
            // OpenFileDialog_TNL_To_IMAGE
            // 
            this.OpenFileDialog_TNL_To_IMAGE.Filter = "File|*.tnl";
            // 
            // SaveFileDialog_IMAGE_To_TNL
            // 
            this.SaveFileDialog_IMAGE_To_TNL.Filter = "File|*.tnl";
            // 
            // SaveFileDialog_TNL_To_IMAGE
            // 
            this.SaveFileDialog_TNL_To_IMAGE.Filter = "File|*.jpeg";
            // 
            // ComboBox_Style_Settings
            // 
            this.ComboBox_Style_Settings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Style_Settings.Enabled = false;
            this.ComboBox_Style_Settings.FormattingEnabled = true;
            this.ComboBox_Style_Settings.Items.AddRange(new object[] {
            "SMB1",
            "SMB3",
            "SMW",
            "NSMBU"});
            this.ComboBox_Style_Settings.Location = new System.Drawing.Point(12, 86);
            this.ComboBox_Style_Settings.Name = "ComboBox_Style_Settings";
            this.ComboBox_Style_Settings.Size = new System.Drawing.Size(100, 21);
            this.ComboBox_Style_Settings.TabIndex = 6;
            // 
            // ComboBox_Physics_Settings
            // 
            this.ComboBox_Physics_Settings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Physics_Settings.Enabled = false;
            this.ComboBox_Physics_Settings.FormattingEnabled = true;
            this.ComboBox_Physics_Settings.Location = new System.Drawing.Point(12, 46);
            this.ComboBox_Physics_Settings.Name = "ComboBox_Physics_Settings";
            this.ComboBox_Physics_Settings.Size = new System.Drawing.Size(100, 21);
            this.ComboBox_Physics_Settings.TabIndex = 1;
            // 
            // LABEL_Physics
            // 
            this.LABEL_Physics.AutoSize = true;
            this.LABEL_Physics.Location = new System.Drawing.Point(12, 30);
            this.LABEL_Physics.Name = "LABEL_Physics";
            this.LABEL_Physics.Size = new System.Drawing.Size(55, 13);
            this.LABEL_Physics.TabIndex = 17;
            this.LABEL_Physics.Text = "<Physics>";
            // 
            // LABEL_Style
            // 
            this.LABEL_Style.AutoSize = true;
            this.LABEL_Style.Location = new System.Drawing.Point(12, 70);
            this.LABEL_Style.Name = "LABEL_Style";
            this.LABEL_Style.Size = new System.Drawing.Size(42, 13);
            this.LABEL_Style.TabIndex = 18;
            this.LABEL_Style.Text = "<Style>";
            // 
            // ComboBox_Theme_Settings
            // 
            this.ComboBox_Theme_Settings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Theme_Settings.Enabled = false;
            this.ComboBox_Theme_Settings.FormattingEnabled = true;
            this.ComboBox_Theme_Settings.Location = new System.Drawing.Point(12, 126);
            this.ComboBox_Theme_Settings.Name = "ComboBox_Theme_Settings";
            this.ComboBox_Theme_Settings.Size = new System.Drawing.Size(100, 21);
            this.ComboBox_Theme_Settings.TabIndex = 19;
            // 
            // LABEL_Theme
            // 
            this.LABEL_Theme.AutoSize = true;
            this.LABEL_Theme.Location = new System.Drawing.Point(12, 110);
            this.LABEL_Theme.Name = "LABEL_Theme";
            this.LABEL_Theme.Size = new System.Drawing.Size(52, 13);
            this.LABEL_Theme.TabIndex = 20;
            this.LABEL_Theme.Text = "<Theme>";
            // 
            // FORM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 321);
            this.Controls.Add(this.LABEL_Theme);
            this.Controls.Add(this.ComboBox_Theme_Settings);
            this.Controls.Add(this.LABEL_Style);
            this.Controls.Add(this.LABEL_Physics);
            this.Controls.Add(this.ComboBox_Physics_Settings);
            this.Controls.Add(this.ComboBox_Style_Settings);
            this.Controls.Add(this.GroupBox_CourseStatus);
            this.Controls.Add(this.BUTTON_TimerMinimum);
            this.Controls.Add(this.BUTTON_TimerMaximum);
            this.Controls.Add(this.BUTTON_CourseStatusNone);
            this.Controls.Add(this.LABEL_ClearCheckStatus);
            this.Controls.Add(this.LABEL_LastItemPlaced);
            this.Controls.Add(this.GroupBox_Scroll_Settings);
            this.Controls.Add(this.LABEL_Timer);
            this.Controls.Add(this.NUMERIC_CourseTimer);
            this.Controls.Add(this.CHECK_UploadReady);
            this.Controls.Add(this.BUTTON_Cancel);
            this.Controls.Add(this.BUTTON_SaveFile);
            this.Controls.Add(this.LABEL_CourseName);
            this.Controls.Add(this.TB_CourseName);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FORM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<_TITLE>";
            this.Load += new System.EventHandler(this.FORM_Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).EndInit();
            this.GroupBox_Scroll_Settings.ResumeLayout(false);
            this.GroupBox_Scroll_Settings.PerformLayout();
            this.GroupBox_CourseStatus.ResumeLayout(false);
            this.GroupBox_CourseStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BYMLConverter;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_BYML_To_XML;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_BYML_To_XML;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_XML_To_BYML;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_XML_To_BYML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SelectFile;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_cdtFile;
        private System.Windows.Forms.TextBox TB_CourseName;
        private System.Windows.Forms.Label LABEL_CourseName;
        private System.Windows.Forms.Button BUTTON_SaveFile;
        private System.Windows.Forms.Button BUTTON_Cancel;
        private System.Windows.Forms.CheckBox CHECK_UploadReady;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseTimer;
        private System.Windows.Forms.Label LABEL_Timer;
        private System.Windows.Forms.GroupBox GroupBox_Scroll_Settings;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Cheetah;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Rabbit;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Turtle;
        private System.Windows.Forms.RadioButton RADIO_Scroll_None;
        private System.Windows.Forms.RadioButton RADIO_Scroll_Lock;
        private System.Windows.Forms.Label LABEL_LastItemPlaced;
        private System.Windows.Forms.Label LABEL_ClearCheckStatus;
        private System.Windows.Forms.GroupBox GroupBox_CourseStatus;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusDownloaded;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusNone;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusRemoved;
        private System.Windows.Forms.RadioButton RADIO_CourseStatusUploaded;
        private System.Windows.Forms.Button BUTTON_CourseStatusNone;
        private System.Windows.Forms.Button BUTTON_TimerMaximum;
        private System.Windows.Forms.Button BUTTON_TimerMinimum;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_TNLConverter;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_IMAGE_To_TNL;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_TNL_To_IMAGE;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_IMAGE_To_TNL;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_TNL_To_IMAGE;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_XML_To_BYML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BYML_To_XML;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_IMAGE_To_TNL;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_TNL_To_IMAGE;
        private System.Windows.Forms.ComboBox ComboBox_Style_Settings;
        private System.Windows.Forms.ComboBox ComboBox_Physics_Settings;
        private System.Windows.Forms.Label LABEL_Physics;
        private System.Windows.Forms.Label LABEL_Style;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBox_Language_Settings;
        private System.Windows.Forms.ComboBox ComboBox_Theme_Settings;
        private System.Windows.Forms.Label LABEL_Theme;
    }
}

