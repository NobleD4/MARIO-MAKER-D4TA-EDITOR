using SLN_SMM_D4TA_EDITOR;

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
            this.LABEL_LastItemOffset = new System.Windows.Forms.Label();
            this.LABEL_CourseCreator = new System.Windows.Forms.Label();
            this.TB_CourseCreator = new System.Windows.Forms.TextBox();
            this.NUMERIC_CourseMonth = new System.Windows.Forms.NumericUpDown();
            this.NUMERIC_CourseDay = new System.Windows.Forms.NumericUpDown();
            this.NUMERIC_CourseHour = new System.Windows.Forms.NumericUpDown();
            this.NUMERIC_CourseMinute = new System.Windows.Forms.NumericUpDown();
            this.CHECK_SetDateTimeNow = new System.Windows.Forms.CheckBox();
            this.LABEL_CourseMonth = new System.Windows.Forms.Label();
            this.LABEL_CourseDay = new System.Windows.Forms.Label();
            this.TwoDots = new System.Windows.Forms.Label();
            this.LABEL_CourseHour = new System.Windows.Forms.Label();
            this.LABEL_CourseMinute = new System.Windows.Forms.Label();
            this.NUMERIC_CourseYear = new System.Windows.Forms.NumericUpDown();
            this.LABEL_CourseYear = new System.Windows.Forms.Label();
            this.TB_CourseIDprefix = new System.Windows.Forms.TextBox();
            this.TB_CourseIDsuffix1 = new System.Windows.Forms.TextBox();
            this.TB_CourseIDsuffix2 = new System.Windows.Forms.TextBox();
            this.TB_CourseIDsuffix3 = new System.Windows.Forms.TextBox();
            this.LABEL_CourseID = new System.Windows.Forms.Label();
            this.CHECK_CourseStatusDownloaded = new System.Windows.Forms.CheckBox();
            this.CHECK_CourseStatusUploaded = new System.Windows.Forms.CheckBox();
            this.CHECK_CourseStatusRemoved = new System.Windows.Forms.CheckBox();
            this.LABEL_LastSFXplaced = new System.Windows.Forms.Label();
            this.BUTTON_CopyID = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).BeginInit();
            this.GroupBox_Scroll_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseYear)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SelectFile,
            this.ToolStripMenuItem_BYMLConverter,
            this.ToolStripMenuItem_TNLConverter});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_SelectFile
            // 
            this.ToolStripMenuItem_SelectFile.Name = "ToolStripMenuItem_SelectFile";
            this.ToolStripMenuItem_SelectFile.Size = new System.Drawing.Size(53, 20);
            this.ToolStripMenuItem_SelectFile.Text = "<File>";
            this.ToolStripMenuItem_SelectFile.Click += new System.EventHandler(this.ToolStripMenuItem_SelectFile_Click);
            // 
            // ToolStripMenuItem_BYMLConverter
            // 
            this.ToolStripMenuItem_BYMLConverter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_XML_To_BYML,
            this.ToolStripMenuItem_BYML_To_XML});
            this.ToolStripMenuItem_BYMLConverter.Name = "ToolStripMenuItem_BYMLConverter";
            this.ToolStripMenuItem_BYMLConverter.Size = new System.Drawing.Size(119, 20);
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
            this.ToolStripMenuItem_TNLConverter.Size = new System.Drawing.Size(110, 20);
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
            this.TB_CourseName.Location = new System.Drawing.Point(141, 71);
            this.TB_CourseName.MaxLength = 32;
            this.TB_CourseName.Name = "TB_CourseName";
            this.TB_CourseName.Size = new System.Drawing.Size(210, 20);
            this.TB_CourseName.TabIndex = 1;
            // 
            // LABEL_CourseName
            // 
            this.LABEL_CourseName.AutoSize = true;
            this.LABEL_CourseName.Location = new System.Drawing.Point(138, 55);
            this.LABEL_CourseName.Name = "LABEL_CourseName";
            this.LABEL_CourseName.Size = new System.Drawing.Size(81, 13);
            this.LABEL_CourseName.TabIndex = 0;
            this.LABEL_CourseName.Text = "<Course name>";
            // 
            // BUTTON_SaveFile
            // 
            this.BUTTON_SaveFile.Enabled = false;
            this.BUTTON_SaveFile.Location = new System.Drawing.Point(412, 326);
            this.BUTTON_SaveFile.Name = "BUTTON_SaveFile";
            this.BUTTON_SaveFile.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_SaveFile.TabIndex = 25;
            this.BUTTON_SaveFile.Text = "<Save course>";
            this.BUTTON_SaveFile.UseVisualStyleBackColor = true;
            this.BUTTON_SaveFile.Click += new System.EventHandler(this.BUTTON_SaveFile_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Enabled = false;
            this.BUTTON_Cancel.Location = new System.Drawing.Point(331, 326);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 24;
            this.BUTTON_Cancel.Text = "<Cancel>";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // CHECK_UploadReady
            // 
            this.CHECK_UploadReady.AutoSize = true;
            this.CHECK_UploadReady.Enabled = false;
            this.CHECK_UploadReady.Location = new System.Drawing.Point(187, 248);
            this.CHECK_UploadReady.Name = "CHECK_UploadReady";
            this.CHECK_UploadReady.Size = new System.Drawing.Size(101, 17);
            this.CHECK_UploadReady.TabIndex = 18;
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
            this.NUMERIC_CourseTimer.Location = new System.Drawing.Point(247, 112);
            this.NUMERIC_CourseTimer.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NUMERIC_CourseTimer.Name = "NUMERIC_CourseTimer";
            this.NUMERIC_CourseTimer.Size = new System.Drawing.Size(60, 20);
            this.NUMERIC_CourseTimer.TabIndex = 4;
            // 
            // LABEL_Timer
            // 
            this.LABEL_Timer.AutoSize = true;
            this.LABEL_Timer.Location = new System.Drawing.Point(244, 96);
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
            this.GroupBox_Scroll_Settings.Location = new System.Drawing.Point(10, 178);
            this.GroupBox_Scroll_Settings.Name = "GroupBox_Scroll_Settings";
            this.GroupBox_Scroll_Settings.Size = new System.Drawing.Size(160, 135);
            this.GroupBox_Scroll_Settings.TabIndex = 14;
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
            this.LABEL_LastItemPlaced.Location = new System.Drawing.Point(176, 276);
            this.LABEL_LastItemPlaced.Name = "LABEL_LastItemPlaced";
            this.LABEL_LastItemPlaced.Size = new System.Drawing.Size(144, 13);
            this.LABEL_LastItemPlaced.TabIndex = 12;
            this.LABEL_LastItemPlaced.Text = "<Last item placed (memory):>";
            // 
            // LABEL_ClearCheckStatus
            // 
            this.LABEL_ClearCheckStatus.AutoSize = true;
            this.LABEL_ClearCheckStatus.Location = new System.Drawing.Point(355, 74);
            this.LABEL_ClearCheckStatus.Name = "LABEL_ClearCheckStatus";
            this.LABEL_ClearCheckStatus.Size = new System.Drawing.Size(76, 13);
            this.LABEL_ClearCheckStatus.TabIndex = 14;
            this.LABEL_ClearCheckStatus.Text = "<Clear check>";
            // 
            // BUTTON_TimerMaximum
            // 
            this.BUTTON_TimerMaximum.Enabled = false;
            this.BUTTON_TimerMaximum.Location = new System.Drawing.Point(369, 109);
            this.BUTTON_TimerMaximum.Name = "BUTTON_TimerMaximum";
            this.BUTTON_TimerMaximum.Size = new System.Drawing.Size(50, 23);
            this.BUTTON_TimerMaximum.TabIndex = 6;
            this.BUTTON_TimerMaximum.Text = "<Max>";
            this.BUTTON_TimerMaximum.UseVisualStyleBackColor = true;
            this.BUTTON_TimerMaximum.Click += new System.EventHandler(this.BUTTON_TimerMaximum_Click);
            // 
            // BUTTON_TimerMinimum
            // 
            this.BUTTON_TimerMinimum.Enabled = false;
            this.BUTTON_TimerMinimum.Location = new System.Drawing.Point(313, 109);
            this.BUTTON_TimerMinimum.Name = "BUTTON_TimerMinimum";
            this.BUTTON_TimerMinimum.Size = new System.Drawing.Size(50, 23);
            this.BUTTON_TimerMinimum.TabIndex = 5;
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
            this.ComboBox_Style_Settings.Location = new System.Drawing.Point(10, 111);
            this.ComboBox_Style_Settings.Name = "ComboBox_Style_Settings";
            this.ComboBox_Style_Settings.Size = new System.Drawing.Size(125, 21);
            this.ComboBox_Style_Settings.TabIndex = 2;
            // 
            // ComboBox_Physics_Settings
            // 
            this.ComboBox_Physics_Settings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Physics_Settings.Enabled = false;
            this.ComboBox_Physics_Settings.FormattingEnabled = true;
            this.ComboBox_Physics_Settings.Location = new System.Drawing.Point(10, 71);
            this.ComboBox_Physics_Settings.Name = "ComboBox_Physics_Settings";
            this.ComboBox_Physics_Settings.Size = new System.Drawing.Size(125, 21);
            this.ComboBox_Physics_Settings.TabIndex = 0;
            // 
            // LABEL_Physics
            // 
            this.LABEL_Physics.AutoSize = true;
            this.LABEL_Physics.Location = new System.Drawing.Point(10, 55);
            this.LABEL_Physics.Name = "LABEL_Physics";
            this.LABEL_Physics.Size = new System.Drawing.Size(55, 13);
            this.LABEL_Physics.TabIndex = 17;
            this.LABEL_Physics.Text = "<Physics>";
            // 
            // LABEL_Style
            // 
            this.LABEL_Style.AutoSize = true;
            this.LABEL_Style.Location = new System.Drawing.Point(10, 95);
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
            this.ComboBox_Theme_Settings.Location = new System.Drawing.Point(10, 151);
            this.ComboBox_Theme_Settings.Name = "ComboBox_Theme_Settings";
            this.ComboBox_Theme_Settings.Size = new System.Drawing.Size(125, 21);
            this.ComboBox_Theme_Settings.TabIndex = 7;
            // 
            // LABEL_Theme
            // 
            this.LABEL_Theme.AutoSize = true;
            this.LABEL_Theme.Location = new System.Drawing.Point(10, 135);
            this.LABEL_Theme.Name = "LABEL_Theme";
            this.LABEL_Theme.Size = new System.Drawing.Size(52, 13);
            this.LABEL_Theme.TabIndex = 20;
            this.LABEL_Theme.Text = "<Theme>";
            // 
            // LABEL_LastItemOffset
            // 
            this.LABEL_LastItemOffset.AutoSize = true;
            this.LABEL_LastItemOffset.Location = new System.Drawing.Point(176, 295);
            this.LABEL_LastItemOffset.Name = "LABEL_LastItemOffset";
            this.LABEL_LastItemOffset.Size = new System.Drawing.Size(93, 13);
            this.LABEL_LastItemOffset.TabIndex = 21;
            this.LABEL_LastItemOffset.Text = "<Last item offset:>";
            // 
            // LABEL_CourseCreator
            // 
            this.LABEL_CourseCreator.AutoSize = true;
            this.LABEL_CourseCreator.Location = new System.Drawing.Point(138, 95);
            this.LABEL_CourseCreator.Name = "LABEL_CourseCreator";
            this.LABEL_CourseCreator.Size = new System.Drawing.Size(53, 13);
            this.LABEL_CourseCreator.TabIndex = 22;
            this.LABEL_CourseCreator.Text = "<Creator>";
            // 
            // TB_CourseCreator
            // 
            this.TB_CourseCreator.Enabled = false;
            this.TB_CourseCreator.Location = new System.Drawing.Point(141, 111);
            this.TB_CourseCreator.MaxLength = 10;
            this.TB_CourseCreator.Name = "TB_CourseCreator";
            this.TB_CourseCreator.Size = new System.Drawing.Size(100, 20);
            this.TB_CourseCreator.TabIndex = 3;
            // 
            // NUMERIC_CourseMonth
            // 
            this.NUMERIC_CourseMonth.Enabled = false;
            this.NUMERIC_CourseMonth.Location = new System.Drawing.Point(350, 192);
            this.NUMERIC_CourseMonth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NUMERIC_CourseMonth.Name = "NUMERIC_CourseMonth";
            this.NUMERIC_CourseMonth.Size = new System.Drawing.Size(50, 20);
            this.NUMERIC_CourseMonth.TabIndex = 19;
            // 
            // NUMERIC_CourseDay
            // 
            this.NUMERIC_CourseDay.Enabled = false;
            this.NUMERIC_CourseDay.Location = new System.Drawing.Point(416, 192);
            this.NUMERIC_CourseDay.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NUMERIC_CourseDay.Name = "NUMERIC_CourseDay";
            this.NUMERIC_CourseDay.Size = new System.Drawing.Size(50, 20);
            this.NUMERIC_CourseDay.TabIndex = 20;
            // 
            // NUMERIC_CourseHour
            // 
            this.NUMERIC_CourseHour.Enabled = false;
            this.NUMERIC_CourseHour.Location = new System.Drawing.Point(350, 231);
            this.NUMERIC_CourseHour.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NUMERIC_CourseHour.Name = "NUMERIC_CourseHour";
            this.NUMERIC_CourseHour.Size = new System.Drawing.Size(50, 20);
            this.NUMERIC_CourseHour.TabIndex = 21;
            // 
            // NUMERIC_CourseMinute
            // 
            this.NUMERIC_CourseMinute.Enabled = false;
            this.NUMERIC_CourseMinute.Location = new System.Drawing.Point(416, 231);
            this.NUMERIC_CourseMinute.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NUMERIC_CourseMinute.Name = "NUMERIC_CourseMinute";
            this.NUMERIC_CourseMinute.Size = new System.Drawing.Size(50, 20);
            this.NUMERIC_CourseMinute.TabIndex = 22;
            // 
            // CHECK_SetDateTimeNow
            // 
            this.CHECK_SetDateTimeNow.AutoSize = true;
            this.CHECK_SetDateTimeNow.Enabled = false;
            this.CHECK_SetDateTimeNow.Location = new System.Drawing.Point(325, 257);
            this.CHECK_SetDateTimeNow.Name = "CHECK_SetDateTimeNow";
            this.CHECK_SetDateTimeNow.Size = new System.Drawing.Size(120, 17);
            this.CHECK_SetDateTimeNow.TabIndex = 23;
            this.CHECK_SetDateTimeNow.Text = "<Set datetime now>";
            this.CHECK_SetDateTimeNow.UseVisualStyleBackColor = true;
            this.CHECK_SetDateTimeNow.CheckedChanged += new System.EventHandler(this.CHECK_SetDateTimeNow_CheckedChanged);
            // 
            // LABEL_CourseMonth
            // 
            this.LABEL_CourseMonth.AutoSize = true;
            this.LABEL_CourseMonth.Location = new System.Drawing.Point(351, 176);
            this.LABEL_CourseMonth.Name = "LABEL_CourseMonth";
            this.LABEL_CourseMonth.Size = new System.Drawing.Size(49, 13);
            this.LABEL_CourseMonth.TabIndex = 29;
            this.LABEL_CourseMonth.Text = "<Month>";
            // 
            // LABEL_CourseDay
            // 
            this.LABEL_CourseDay.AutoSize = true;
            this.LABEL_CourseDay.Location = new System.Drawing.Point(418, 176);
            this.LABEL_CourseDay.Name = "LABEL_CourseDay";
            this.LABEL_CourseDay.Size = new System.Drawing.Size(38, 13);
            this.LABEL_CourseDay.TabIndex = 30;
            this.LABEL_CourseDay.Text = "<Day>";
            // 
            // TwoDots
            // 
            this.TwoDots.AutoSize = true;
            this.TwoDots.Location = new System.Drawing.Point(403, 234);
            this.TwoDots.Name = "TwoDots";
            this.TwoDots.Size = new System.Drawing.Size(10, 13);
            this.TwoDots.TabIndex = 31;
            this.TwoDots.Text = ":";
            // 
            // LABEL_CourseHour
            // 
            this.LABEL_CourseHour.AutoSize = true;
            this.LABEL_CourseHour.Location = new System.Drawing.Point(351, 215);
            this.LABEL_CourseHour.Name = "LABEL_CourseHour";
            this.LABEL_CourseHour.Size = new System.Drawing.Size(42, 13);
            this.LABEL_CourseHour.TabIndex = 32;
            this.LABEL_CourseHour.Text = "<Hour>";
            // 
            // LABEL_CourseMinute
            // 
            this.LABEL_CourseMinute.AutoSize = true;
            this.LABEL_CourseMinute.Location = new System.Drawing.Point(419, 215);
            this.LABEL_CourseMinute.Name = "LABEL_CourseMinute";
            this.LABEL_CourseMinute.Size = new System.Drawing.Size(51, 13);
            this.LABEL_CourseMinute.TabIndex = 33;
            this.LABEL_CourseMinute.Text = "<Minute>";
            // 
            // NUMERIC_CourseYear
            // 
            this.NUMERIC_CourseYear.Enabled = false;
            this.NUMERIC_CourseYear.Location = new System.Drawing.Point(406, 151);
            this.NUMERIC_CourseYear.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NUMERIC_CourseYear.Name = "NUMERIC_CourseYear";
            this.NUMERIC_CourseYear.Size = new System.Drawing.Size(60, 20);
            this.NUMERIC_CourseYear.TabIndex = 13;
            // 
            // LABEL_CourseYear
            // 
            this.LABEL_CourseYear.AutoSize = true;
            this.LABEL_CourseYear.Location = new System.Drawing.Point(407, 135);
            this.LABEL_CourseYear.Name = "LABEL_CourseYear";
            this.LABEL_CourseYear.Size = new System.Drawing.Size(41, 13);
            this.LABEL_CourseYear.TabIndex = 35;
            this.LABEL_CourseYear.Text = "<Year>";
            // 
            // TB_CourseIDprefix
            // 
            this.TB_CourseIDprefix.Enabled = false;
            this.TB_CourseIDprefix.Location = new System.Drawing.Point(141, 151);
            this.TB_CourseIDprefix.MaxLength = 4;
            this.TB_CourseIDprefix.Name = "TB_CourseIDprefix";
            this.TB_CourseIDprefix.Size = new System.Drawing.Size(40, 20);
            this.TB_CourseIDprefix.TabIndex = 8;
            // 
            // TB_CourseIDsuffix1
            // 
            this.TB_CourseIDsuffix1.Enabled = false;
            this.TB_CourseIDsuffix1.Location = new System.Drawing.Point(187, 151);
            this.TB_CourseIDsuffix1.MaxLength = 4;
            this.TB_CourseIDsuffix1.Name = "TB_CourseIDsuffix1";
            this.TB_CourseIDsuffix1.Size = new System.Drawing.Size(40, 20);
            this.TB_CourseIDsuffix1.TabIndex = 9;
            // 
            // TB_CourseIDsuffix2
            // 
            this.TB_CourseIDsuffix2.Enabled = false;
            this.TB_CourseIDsuffix2.Location = new System.Drawing.Point(233, 151);
            this.TB_CourseIDsuffix2.MaxLength = 4;
            this.TB_CourseIDsuffix2.Name = "TB_CourseIDsuffix2";
            this.TB_CourseIDsuffix2.Size = new System.Drawing.Size(40, 20);
            this.TB_CourseIDsuffix2.TabIndex = 10;
            // 
            // TB_CourseIDsuffix3
            // 
            this.TB_CourseIDsuffix3.Enabled = false;
            this.TB_CourseIDsuffix3.Location = new System.Drawing.Point(279, 151);
            this.TB_CourseIDsuffix3.MaxLength = 4;
            this.TB_CourseIDsuffix3.Name = "TB_CourseIDsuffix3";
            this.TB_CourseIDsuffix3.Size = new System.Drawing.Size(40, 20);
            this.TB_CourseIDsuffix3.TabIndex = 11;
            // 
            // LABEL_CourseID
            // 
            this.LABEL_CourseID.AutoSize = true;
            this.LABEL_CourseID.Location = new System.Drawing.Point(138, 135);
            this.LABEL_CourseID.Name = "LABEL_CourseID";
            this.LABEL_CourseID.Size = new System.Drawing.Size(66, 13);
            this.LABEL_CourseID.TabIndex = 40;
            this.LABEL_CourseID.Text = "<Course ID>";
            // 
            // CHECK_CourseStatusDownloaded
            // 
            this.CHECK_CourseStatusDownloaded.AutoSize = true;
            this.CHECK_CourseStatusDownloaded.Enabled = false;
            this.CHECK_CourseStatusDownloaded.Location = new System.Drawing.Point(187, 192);
            this.CHECK_CourseStatusDownloaded.Name = "CHECK_CourseStatusDownloaded";
            this.CHECK_CourseStatusDownloaded.Size = new System.Drawing.Size(98, 17);
            this.CHECK_CourseStatusDownloaded.TabIndex = 15;
            this.CHECK_CourseStatusDownloaded.Text = "<Downloaded>";
            this.CHECK_CourseStatusDownloaded.UseVisualStyleBackColor = true;
            // 
            // CHECK_CourseStatusUploaded
            // 
            this.CHECK_CourseStatusUploaded.AutoSize = true;
            this.CHECK_CourseStatusUploaded.Enabled = false;
            this.CHECK_CourseStatusUploaded.Location = new System.Drawing.Point(187, 211);
            this.CHECK_CourseStatusUploaded.Name = "CHECK_CourseStatusUploaded";
            this.CHECK_CourseStatusUploaded.Size = new System.Drawing.Size(84, 17);
            this.CHECK_CourseStatusUploaded.TabIndex = 16;
            this.CHECK_CourseStatusUploaded.Text = "<Uploaded>";
            this.CHECK_CourseStatusUploaded.UseVisualStyleBackColor = true;
            // 
            // CHECK_CourseStatusRemoved
            // 
            this.CHECK_CourseStatusRemoved.AutoSize = true;
            this.CHECK_CourseStatusRemoved.Enabled = false;
            this.CHECK_CourseStatusRemoved.Location = new System.Drawing.Point(187, 230);
            this.CHECK_CourseStatusRemoved.Name = "CHECK_CourseStatusRemoved";
            this.CHECK_CourseStatusRemoved.Size = new System.Drawing.Size(84, 17);
            this.CHECK_CourseStatusRemoved.TabIndex = 17;
            this.CHECK_CourseStatusRemoved.Text = "<Removed>";
            this.CHECK_CourseStatusRemoved.UseVisualStyleBackColor = true;
            // 
            // LABEL_LastSFXplaced
            // 
            this.LABEL_LastSFXplaced.AutoSize = true;
            this.LABEL_LastSFXplaced.Location = new System.Drawing.Point(325, 295);
            this.LABEL_LastSFXplaced.Name = "LABEL_LastSFXplaced";
            this.LABEL_LastSFXplaced.Size = new System.Drawing.Size(145, 13);
            this.LABEL_LastSFXplaced.TabIndex = 44;
            this.LABEL_LastSFXplaced.Text = "<Last SFX placed (memory):>";
            // 
            // BUTTON_CopyID
            // 
            this.BUTTON_CopyID.Enabled = false;
            this.BUTTON_CopyID.Location = new System.Drawing.Point(325, 148);
            this.BUTTON_CopyID.Name = "BUTTON_CopyID";
            this.BUTTON_CopyID.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_CopyID.TabIndex = 12;
            this.BUTTON_CopyID.Text = "<Copy ID>";
            this.BUTTON_CopyID.UseVisualStyleBackColor = true;
            this.BUTTON_CopyID.Click += new System.EventHandler(this.BUTTON_CopyID_Click);
            // 
            // FORM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 361);
            this.Controls.Add(this.BUTTON_CopyID);
            this.Controls.Add(this.LABEL_LastSFXplaced);
            this.Controls.Add(this.CHECK_CourseStatusRemoved);
            this.Controls.Add(this.CHECK_CourseStatusUploaded);
            this.Controls.Add(this.CHECK_CourseStatusDownloaded);
            this.Controls.Add(this.LABEL_CourseID);
            this.Controls.Add(this.TB_CourseIDsuffix3);
            this.Controls.Add(this.TB_CourseIDsuffix2);
            this.Controls.Add(this.TB_CourseIDsuffix1);
            this.Controls.Add(this.TB_CourseIDprefix);
            this.Controls.Add(this.LABEL_CourseYear);
            this.Controls.Add(this.NUMERIC_CourseYear);
            this.Controls.Add(this.LABEL_CourseMinute);
            this.Controls.Add(this.LABEL_CourseHour);
            this.Controls.Add(this.TwoDots);
            this.Controls.Add(this.LABEL_CourseDay);
            this.Controls.Add(this.LABEL_CourseMonth);
            this.Controls.Add(this.CHECK_SetDateTimeNow);
            this.Controls.Add(this.NUMERIC_CourseMinute);
            this.Controls.Add(this.NUMERIC_CourseHour);
            this.Controls.Add(this.NUMERIC_CourseDay);
            this.Controls.Add(this.NUMERIC_CourseMonth);
            this.Controls.Add(this.LABEL_CourseCreator);
            this.Controls.Add(this.TB_CourseCreator);
            this.Controls.Add(this.LABEL_LastItemOffset);
            this.Controls.Add(this.LABEL_Theme);
            this.Controls.Add(this.ComboBox_Theme_Settings);
            this.Controls.Add(this.LABEL_Style);
            this.Controls.Add(this.LABEL_Physics);
            this.Controls.Add(this.ComboBox_Physics_Settings);
            this.Controls.Add(this.ComboBox_Style_Settings);
            this.Controls.Add(this.BUTTON_TimerMinimum);
            this.Controls.Add(this.BUTTON_TimerMaximum);
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
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.TB_CourseName, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseName, 0);
            this.Controls.SetChildIndex(this.BUTTON_SaveFile, 0);
            this.Controls.SetChildIndex(this.BUTTON_Cancel, 0);
            this.Controls.SetChildIndex(this.CHECK_UploadReady, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseTimer, 0);
            this.Controls.SetChildIndex(this.LABEL_Timer, 0);
            this.Controls.SetChildIndex(this.GroupBox_Scroll_Settings, 0);
            this.Controls.SetChildIndex(this.LABEL_LastItemPlaced, 0);
            this.Controls.SetChildIndex(this.LABEL_ClearCheckStatus, 0);
            this.Controls.SetChildIndex(this.BUTTON_TimerMaximum, 0);
            this.Controls.SetChildIndex(this.BUTTON_TimerMinimum, 0);
            this.Controls.SetChildIndex(this.ComboBox_Style_Settings, 0);
            this.Controls.SetChildIndex(this.ComboBox_Physics_Settings, 0);
            this.Controls.SetChildIndex(this.LABEL_Physics, 0);
            this.Controls.SetChildIndex(this.LABEL_Style, 0);
            this.Controls.SetChildIndex(this.ComboBox_Theme_Settings, 0);
            this.Controls.SetChildIndex(this.LABEL_Theme, 0);
            this.Controls.SetChildIndex(this.LABEL_LastItemOffset, 0);
            this.Controls.SetChildIndex(this.TB_CourseCreator, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseCreator, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseMonth, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseDay, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseHour, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseMinute, 0);
            this.Controls.SetChildIndex(this.CHECK_SetDateTimeNow, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseMonth, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseDay, 0);
            this.Controls.SetChildIndex(this.TwoDots, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseHour, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseMinute, 0);
            this.Controls.SetChildIndex(this.NUMERIC_CourseYear, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseYear, 0);
            this.Controls.SetChildIndex(this.TB_CourseIDprefix, 0);
            this.Controls.SetChildIndex(this.TB_CourseIDsuffix1, 0);
            this.Controls.SetChildIndex(this.TB_CourseIDsuffix2, 0);
            this.Controls.SetChildIndex(this.TB_CourseIDsuffix3, 0);
            this.Controls.SetChildIndex(this.LABEL_CourseID, 0);
            this.Controls.SetChildIndex(this.CHECK_CourseStatusDownloaded, 0);
            this.Controls.SetChildIndex(this.CHECK_CourseStatusUploaded, 0);
            this.Controls.SetChildIndex(this.CHECK_CourseStatusRemoved, 0);
            this.Controls.SetChildIndex(this.LABEL_LastSFXplaced, 0);
            this.Controls.SetChildIndex(this.BUTTON_CopyID, 0);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseTimer)).EndInit();
            this.GroupBox_Scroll_Settings.ResumeLayout(false);
            this.GroupBox_Scroll_Settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CourseYear)).EndInit();
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
        private System.Windows.Forms.ComboBox ComboBox_Theme_Settings;
        private System.Windows.Forms.Label LABEL_Theme;
        private System.Windows.Forms.Label LABEL_LastItemOffset;
        private System.Windows.Forms.Label LABEL_CourseCreator;
        private System.Windows.Forms.TextBox TB_CourseCreator;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseMonth;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseDay;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseHour;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseMinute;
        private System.Windows.Forms.CheckBox CHECK_SetDateTimeNow;
        private System.Windows.Forms.Label LABEL_CourseMonth;
        private System.Windows.Forms.Label LABEL_CourseDay;
        private System.Windows.Forms.Label TwoDots;
        private System.Windows.Forms.Label LABEL_CourseHour;
        private System.Windows.Forms.Label LABEL_CourseMinute;
        private System.Windows.Forms.NumericUpDown NUMERIC_CourseYear;
        private System.Windows.Forms.Label LABEL_CourseYear;
        private System.Windows.Forms.TextBox TB_CourseIDprefix;
        private System.Windows.Forms.TextBox TB_CourseIDsuffix1;
        private System.Windows.Forms.TextBox TB_CourseIDsuffix2;
        private System.Windows.Forms.TextBox TB_CourseIDsuffix3;
        private System.Windows.Forms.Label LABEL_CourseID;
        private System.Windows.Forms.CheckBox CHECK_CourseStatusDownloaded;
        private System.Windows.Forms.CheckBox CHECK_CourseStatusUploaded;
        private System.Windows.Forms.CheckBox CHECK_CourseStatusRemoved;
        private System.Windows.Forms.Label LABEL_LastSFXplaced;
        private System.Windows.Forms.Button BUTTON_CopyID;
    }
}

