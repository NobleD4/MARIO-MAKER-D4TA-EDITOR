namespace SLN_SMM_D4TA_EDITOR
{
    partial class FORM_SMM1_SaveFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_SMM1_SaveFile));
            this.LISTBOX_Coursebot = new System.Windows.Forms.ListBox();
            this.BUTTON_DESC_Sort = new System.Windows.Forms.Button();
            this.BUTTON_ASC_Sort = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_SelectFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog_datFile = new System.Windows.Forms.OpenFileDialog();
            this.BUTTON_RemoveCourse = new System.Windows.Forms.Button();
            this.CHECKLISTBOX_ItemsOrder = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CHECK_ItemDelivery1 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery2 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery3 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery4 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery5 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery6 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery7 = new System.Windows.Forms.CheckBox();
            this.CHECK_ItemDelivery8 = new System.Windows.Forms.CheckBox();
            this.GroupBox_Controls = new System.Windows.Forms.GroupBox();
            this.RADIO_DashJump = new System.Windows.Forms.RadioButton();
            this.RADIO_JumpDash = new System.Windows.Forms.RadioButton();
            this.GroupBox_GamePad3DAudio = new System.Windows.Forms.GroupBox();
            this.RADIO_GamePad3DAudio_On = new System.Windows.Forms.RadioButton();
            this.RADIO_GamePad3DAudio_Off = new System.Windows.Forms.RadioButton();
            this.GroupBox_NotifyCreators = new System.Windows.Forms.GroupBox();
            this.RADIO_NotifyCreators_On = new System.Windows.Forms.RadioButton();
            this.RADIO_NotifyCreators_Off = new System.Windows.Forms.RadioButton();
            this.GroupBox_NotifyMyCourses = new System.Windows.Forms.GroupBox();
            this.RADIO_NotifyMyCourses_On = new System.Windows.Forms.RadioButton();
            this.RADIO_NotifyMyCourses_Off = new System.Windows.Forms.RadioButton();
            this.BUTTON_SaveFile = new System.Windows.Forms.Button();
            this.BUTTON_Cancel = new System.Windows.Forms.Button();
            this.BUTTON_ConfirmSort = new System.Windows.Forms.Button();
            this.GroupBox_SortCoursebot = new System.Windows.Forms.GroupBox();
            this.RADIO_SortBySlot = new System.Windows.Forms.RadioButton();
            this.RADIO_SortByCourseNumber = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.GroupBox_Controls.SuspendLayout();
            this.GroupBox_GamePad3DAudio.SuspendLayout();
            this.GroupBox_NotifyCreators.SuspendLayout();
            this.GroupBox_NotifyMyCourses.SuspendLayout();
            this.GroupBox_SortCoursebot.SuspendLayout();
            this.SuspendLayout();
            // 
            // LISTBOX_Coursebot
            // 
            this.LISTBOX_Coursebot.AllowDrop = true;
            this.LISTBOX_Coursebot.FormattingEnabled = true;
            this.LISTBOX_Coursebot.Location = new System.Drawing.Point(138, 51);
            this.LISTBOX_Coursebot.Name = "LISTBOX_Coursebot";
            this.LISTBOX_Coursebot.Size = new System.Drawing.Size(266, 121);
            this.LISTBOX_Coursebot.TabIndex = 4;
            this.LISTBOX_Coursebot.DragDrop += new System.Windows.Forms.DragEventHandler(this.LISTBOX_Coursebot_DragDrop);
            this.LISTBOX_Coursebot.DragOver += new System.Windows.Forms.DragEventHandler(this.LISTBOX_Coursebot_DragOver);
            this.LISTBOX_Coursebot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LISTBOX_Coursebot_MouseDown);
            // 
            // BUTTON_DESC_Sort
            // 
            this.BUTTON_DESC_Sort.Enabled = false;
            this.BUTTON_DESC_Sort.Location = new System.Drawing.Point(32, 153);
            this.BUTTON_DESC_Sort.Name = "BUTTON_DESC_Sort";
            this.BUTTON_DESC_Sort.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_DESC_Sort.TabIndex = 3;
            this.BUTTON_DESC_Sort.Text = "<Desc. Sort>";
            this.BUTTON_DESC_Sort.UseVisualStyleBackColor = true;
            this.BUTTON_DESC_Sort.Click += new System.EventHandler(this.BUTTON_DESC_Sort_Click);
            // 
            // BUTTON_ASC_Sort
            // 
            this.BUTTON_ASC_Sort.Enabled = false;
            this.BUTTON_ASC_Sort.Location = new System.Drawing.Point(32, 124);
            this.BUTTON_ASC_Sort.Name = "BUTTON_ASC_Sort";
            this.BUTTON_ASC_Sort.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_ASC_Sort.TabIndex = 2;
            this.BUTTON_ASC_Sort.Text = "<Asc. Sort>";
            this.BUTTON_ASC_Sort.UseVisualStyleBackColor = true;
            this.BUTTON_ASC_Sort.Click += new System.EventHandler(this.BUTTON_ASC_Sort_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SelectFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_SelectFile
            // 
            this.ToolStripMenuItem_SelectFile.Name = "ToolStripMenuItem_SelectFile";
            this.ToolStripMenuItem_SelectFile.Size = new System.Drawing.Size(53, 20);
            this.ToolStripMenuItem_SelectFile.Text = "<File>";
            this.ToolStripMenuItem_SelectFile.Click += new System.EventHandler(this.ToolStripMenuItem_SelectFile_Click);
            // 
            // OpenFileDialog_datFile
            // 
            this.OpenFileDialog_datFile.Filter = "File|*.dat";
            // 
            // BUTTON_RemoveCourse
            // 
            this.BUTTON_RemoveCourse.Enabled = false;
            this.BUTTON_RemoveCourse.Location = new System.Drawing.Point(410, 124);
            this.BUTTON_RemoveCourse.Name = "BUTTON_RemoveCourse";
            this.BUTTON_RemoveCourse.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_RemoveCourse.TabIndex = 5;
            this.BUTTON_RemoveCourse.Text = "<Remove>";
            this.BUTTON_RemoveCourse.UseVisualStyleBackColor = true;
            this.BUTTON_RemoveCourse.Click += new System.EventHandler(this.BUTTON_RemoveCourse_Click);
            // 
            // CHECKLISTBOX_ItemsOrder
            // 
            this.CHECKLISTBOX_ItemsOrder.Enabled = false;
            this.CHECKLISTBOX_ItemsOrder.FormattingEnabled = true;
            this.CHECKLISTBOX_ItemsOrder.Location = new System.Drawing.Point(158, 308);
            this.CHECKLISTBOX_ItemsOrder.Name = "CHECKLISTBOX_ItemsOrder";
            this.CHECKLISTBOX_ItemsOrder.Size = new System.Drawing.Size(120, 169);
            this.CHECKLISTBOX_ItemsOrder.TabIndex = 6;
            this.CHECKLISTBOX_ItemsOrder.Visible = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(158, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Set default";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(203, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Set as new";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // CHECK_ItemDelivery1
            // 
            this.CHECK_ItemDelivery1.AutoSize = true;
            this.CHECK_ItemDelivery1.Location = new System.Drawing.Point(7, 178);
            this.CHECK_ItemDelivery1.Name = "CHECK_ItemDelivery1";
            this.CHECK_ItemDelivery1.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery1.TabIndex = 9;
            this.CHECK_ItemDelivery1.Text = "<Items Delivery 1>";
            this.CHECK_ItemDelivery1.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery1.Visible = false;
            // 
            // CHECK_ItemDelivery2
            // 
            this.CHECK_ItemDelivery2.AutoSize = true;
            this.CHECK_ItemDelivery2.Location = new System.Drawing.Point(7, 201);
            this.CHECK_ItemDelivery2.Name = "CHECK_ItemDelivery2";
            this.CHECK_ItemDelivery2.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery2.TabIndex = 10;
            this.CHECK_ItemDelivery2.Text = "<Items Delivery 2>";
            this.CHECK_ItemDelivery2.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery2.Visible = false;
            // 
            // CHECK_ItemDelivery3
            // 
            this.CHECK_ItemDelivery3.AutoSize = true;
            this.CHECK_ItemDelivery3.Location = new System.Drawing.Point(7, 224);
            this.CHECK_ItemDelivery3.Name = "CHECK_ItemDelivery3";
            this.CHECK_ItemDelivery3.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery3.TabIndex = 11;
            this.CHECK_ItemDelivery3.Text = "<Items Delivery 3>";
            this.CHECK_ItemDelivery3.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery3.Visible = false;
            // 
            // CHECK_ItemDelivery4
            // 
            this.CHECK_ItemDelivery4.AutoSize = true;
            this.CHECK_ItemDelivery4.Location = new System.Drawing.Point(7, 247);
            this.CHECK_ItemDelivery4.Name = "CHECK_ItemDelivery4";
            this.CHECK_ItemDelivery4.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery4.TabIndex = 12;
            this.CHECK_ItemDelivery4.Text = "<Items Delivery 4>";
            this.CHECK_ItemDelivery4.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery4.Visible = false;
            // 
            // CHECK_ItemDelivery5
            // 
            this.CHECK_ItemDelivery5.AutoSize = true;
            this.CHECK_ItemDelivery5.Location = new System.Drawing.Point(7, 269);
            this.CHECK_ItemDelivery5.Name = "CHECK_ItemDelivery5";
            this.CHECK_ItemDelivery5.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery5.TabIndex = 13;
            this.CHECK_ItemDelivery5.Text = "<Items Delivery 5>";
            this.CHECK_ItemDelivery5.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery5.Visible = false;
            // 
            // CHECK_ItemDelivery6
            // 
            this.CHECK_ItemDelivery6.AutoSize = true;
            this.CHECK_ItemDelivery6.Location = new System.Drawing.Point(7, 292);
            this.CHECK_ItemDelivery6.Name = "CHECK_ItemDelivery6";
            this.CHECK_ItemDelivery6.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery6.TabIndex = 14;
            this.CHECK_ItemDelivery6.Text = "<Items Delivery 6>";
            this.CHECK_ItemDelivery6.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery6.Visible = false;
            // 
            // CHECK_ItemDelivery7
            // 
            this.CHECK_ItemDelivery7.AutoSize = true;
            this.CHECK_ItemDelivery7.Location = new System.Drawing.Point(7, 315);
            this.CHECK_ItemDelivery7.Name = "CHECK_ItemDelivery7";
            this.CHECK_ItemDelivery7.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery7.TabIndex = 15;
            this.CHECK_ItemDelivery7.Text = "<Items Delivery 7>";
            this.CHECK_ItemDelivery7.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery7.Visible = false;
            // 
            // CHECK_ItemDelivery8
            // 
            this.CHECK_ItemDelivery8.AutoSize = true;
            this.CHECK_ItemDelivery8.Location = new System.Drawing.Point(7, 338);
            this.CHECK_ItemDelivery8.Name = "CHECK_ItemDelivery8";
            this.CHECK_ItemDelivery8.Size = new System.Drawing.Size(113, 17);
            this.CHECK_ItemDelivery8.TabIndex = 16;
            this.CHECK_ItemDelivery8.Text = "<Items Delivery 8>";
            this.CHECK_ItemDelivery8.UseVisualStyleBackColor = true;
            this.CHECK_ItemDelivery8.Visible = false;
            // 
            // GroupBox_Controls
            // 
            this.GroupBox_Controls.Controls.Add(this.RADIO_DashJump);
            this.GroupBox_Controls.Controls.Add(this.RADIO_JumpDash);
            this.GroupBox_Controls.Enabled = false;
            this.GroupBox_Controls.Location = new System.Drawing.Point(12, 182);
            this.GroupBox_Controls.Name = "GroupBox_Controls";
            this.GroupBox_Controls.Size = new System.Drawing.Size(221, 67);
            this.GroupBox_Controls.TabIndex = 6;
            this.GroupBox_Controls.TabStop = false;
            this.GroupBox_Controls.Text = "<Controls>";
            // 
            // RADIO_DashJump
            // 
            this.RADIO_DashJump.AutoSize = true;
            this.RADIO_DashJump.Location = new System.Drawing.Point(6, 19);
            this.RADIO_DashJump.Name = "RADIO_DashJump";
            this.RADIO_DashJump.Size = new System.Drawing.Size(212, 17);
            this.RADIO_DashJump.TabIndex = 1;
            this.RADIO_DashJump.TabStop = true;
            this.RADIO_DashJump.Text = "<Dash/Jump (Dash: X, Y - Jump: B, A)>";
            this.RADIO_DashJump.UseVisualStyleBackColor = true;
            // 
            // RADIO_JumpDash
            // 
            this.RADIO_JumpDash.AutoSize = true;
            this.RADIO_JumpDash.Location = new System.Drawing.Point(6, 42);
            this.RADIO_JumpDash.Name = "RADIO_JumpDash";
            this.RADIO_JumpDash.Size = new System.Drawing.Size(212, 17);
            this.RADIO_JumpDash.TabIndex = 0;
            this.RADIO_JumpDash.TabStop = true;
            this.RADIO_JumpDash.Text = "<Jump/Dash (Dash: Y, B - Jump: X, A)>";
            this.RADIO_JumpDash.UseVisualStyleBackColor = true;
            // 
            // GroupBox_GamePad3DAudio
            // 
            this.GroupBox_GamePad3DAudio.Controls.Add(this.RADIO_GamePad3DAudio_On);
            this.GroupBox_GamePad3DAudio.Controls.Add(this.RADIO_GamePad3DAudio_Off);
            this.GroupBox_GamePad3DAudio.Enabled = false;
            this.GroupBox_GamePad3DAudio.Location = new System.Drawing.Point(239, 182);
            this.GroupBox_GamePad3DAudio.Name = "GroupBox_GamePad3DAudio";
            this.GroupBox_GamePad3DAudio.Size = new System.Drawing.Size(130, 67);
            this.GroupBox_GamePad3DAudio.TabIndex = 7;
            this.GroupBox_GamePad3DAudio.TabStop = false;
            this.GroupBox_GamePad3DAudio.Text = "<GamePad 3D Audio>";
            // 
            // RADIO_GamePad3DAudio_On
            // 
            this.RADIO_GamePad3DAudio_On.AutoSize = true;
            this.RADIO_GamePad3DAudio_On.Location = new System.Drawing.Point(6, 19);
            this.RADIO_GamePad3DAudio_On.Name = "RADIO_GamePad3DAudio_On";
            this.RADIO_GamePad3DAudio_On.Size = new System.Drawing.Size(51, 17);
            this.RADIO_GamePad3DAudio_On.TabIndex = 1;
            this.RADIO_GamePad3DAudio_On.TabStop = true;
            this.RADIO_GamePad3DAudio_On.Text = "<On>";
            this.RADIO_GamePad3DAudio_On.UseVisualStyleBackColor = true;
            // 
            // RADIO_GamePad3DAudio_Off
            // 
            this.RADIO_GamePad3DAudio_Off.AutoSize = true;
            this.RADIO_GamePad3DAudio_Off.Location = new System.Drawing.Point(6, 42);
            this.RADIO_GamePad3DAudio_Off.Name = "RADIO_GamePad3DAudio_Off";
            this.RADIO_GamePad3DAudio_Off.Size = new System.Drawing.Size(51, 17);
            this.RADIO_GamePad3DAudio_Off.TabIndex = 0;
            this.RADIO_GamePad3DAudio_Off.TabStop = true;
            this.RADIO_GamePad3DAudio_Off.Text = "<Off>";
            this.RADIO_GamePad3DAudio_Off.UseVisualStyleBackColor = true;
            // 
            // GroupBox_NotifyCreators
            // 
            this.GroupBox_NotifyCreators.Controls.Add(this.RADIO_NotifyCreators_On);
            this.GroupBox_NotifyCreators.Controls.Add(this.RADIO_NotifyCreators_Off);
            this.GroupBox_NotifyCreators.Enabled = false;
            this.GroupBox_NotifyCreators.Location = new System.Drawing.Point(375, 182);
            this.GroupBox_NotifyCreators.Name = "GroupBox_NotifyCreators";
            this.GroupBox_NotifyCreators.Size = new System.Drawing.Size(130, 67);
            this.GroupBox_NotifyCreators.TabIndex = 8;
            this.GroupBox_NotifyCreators.TabStop = false;
            this.GroupBox_NotifyCreators.Text = "<Notify to creators>";
            // 
            // RADIO_NotifyCreators_On
            // 
            this.RADIO_NotifyCreators_On.AutoSize = true;
            this.RADIO_NotifyCreators_On.Location = new System.Drawing.Point(6, 19);
            this.RADIO_NotifyCreators_On.Name = "RADIO_NotifyCreators_On";
            this.RADIO_NotifyCreators_On.Size = new System.Drawing.Size(64, 17);
            this.RADIO_NotifyCreators_On.TabIndex = 1;
            this.RADIO_NotifyCreators_On.TabStop = true;
            this.RADIO_NotifyCreators_On.Text = "<Notify>";
            this.RADIO_NotifyCreators_On.UseVisualStyleBackColor = true;
            // 
            // RADIO_NotifyCreators_Off
            // 
            this.RADIO_NotifyCreators_Off.AutoSize = true;
            this.RADIO_NotifyCreators_Off.Location = new System.Drawing.Point(6, 42);
            this.RADIO_NotifyCreators_Off.Name = "RADIO_NotifyCreators_Off";
            this.RADIO_NotifyCreators_Off.Size = new System.Drawing.Size(90, 17);
            this.RADIO_NotifyCreators_Off.TabIndex = 0;
            this.RADIO_NotifyCreators_Off.TabStop = true;
            this.RADIO_NotifyCreators_Off.Text = "<Don\'t notify>";
            this.RADIO_NotifyCreators_Off.UseVisualStyleBackColor = true;
            // 
            // GroupBox_NotifyMyCourses
            // 
            this.GroupBox_NotifyMyCourses.Controls.Add(this.RADIO_NotifyMyCourses_On);
            this.GroupBox_NotifyMyCourses.Controls.Add(this.RADIO_NotifyMyCourses_Off);
            this.GroupBox_NotifyMyCourses.Enabled = false;
            this.GroupBox_NotifyMyCourses.Location = new System.Drawing.Point(12, 255);
            this.GroupBox_NotifyMyCourses.Name = "GroupBox_NotifyMyCourses";
            this.GroupBox_NotifyMyCourses.Size = new System.Drawing.Size(151, 67);
            this.GroupBox_NotifyMyCourses.TabIndex = 9;
            this.GroupBox_NotifyMyCourses.TabStop = false;
            this.GroupBox_NotifyMyCourses.Text = "<Notify about my courses>";
            // 
            // RADIO_NotifyMyCourses_On
            // 
            this.RADIO_NotifyMyCourses_On.AutoSize = true;
            this.RADIO_NotifyMyCourses_On.Location = new System.Drawing.Point(6, 19);
            this.RADIO_NotifyMyCourses_On.Name = "RADIO_NotifyMyCourses_On";
            this.RADIO_NotifyMyCourses_On.Size = new System.Drawing.Size(77, 17);
            this.RADIO_NotifyMyCourses_On.TabIndex = 1;
            this.RADIO_NotifyMyCourses_On.TabStop = true;
            this.RADIO_NotifyMyCourses_On.Text = "<Receive>";
            this.RADIO_NotifyMyCourses_On.UseVisualStyleBackColor = true;
            // 
            // RADIO_NotifyMyCourses_Off
            // 
            this.RADIO_NotifyMyCourses_Off.AutoSize = true;
            this.RADIO_NotifyMyCourses_Off.Location = new System.Drawing.Point(6, 42);
            this.RADIO_NotifyMyCourses_Off.Name = "RADIO_NotifyMyCourses_Off";
            this.RADIO_NotifyMyCourses_Off.Size = new System.Drawing.Size(100, 17);
            this.RADIO_NotifyMyCourses_Off.TabIndex = 0;
            this.RADIO_NotifyMyCourses_Off.TabStop = true;
            this.RADIO_NotifyMyCourses_Off.Text = "<Don\'t receive>";
            this.RADIO_NotifyMyCourses_Off.UseVisualStyleBackColor = true;
            // 
            // BUTTON_SaveFile
            // 
            this.BUTTON_SaveFile.Enabled = false;
            this.BUTTON_SaveFile.Location = new System.Drawing.Point(412, 326);
            this.BUTTON_SaveFile.Name = "BUTTON_SaveFile";
            this.BUTTON_SaveFile.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_SaveFile.TabIndex = 11;
            this.BUTTON_SaveFile.Text = "<Save data>";
            this.BUTTON_SaveFile.UseVisualStyleBackColor = true;
            this.BUTTON_SaveFile.Click += new System.EventHandler(this.BUTTON_SaveFile_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Enabled = false;
            this.BUTTON_Cancel.Location = new System.Drawing.Point(331, 326);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 10;
            this.BUTTON_Cancel.Text = "<Cancel>";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // BUTTON_ConfirmSort
            // 
            this.BUTTON_ConfirmSort.Enabled = false;
            this.BUTTON_ConfirmSort.Location = new System.Drawing.Point(410, 153);
            this.BUTTON_ConfirmSort.Name = "BUTTON_ConfirmSort";
            this.BUTTON_ConfirmSort.Size = new System.Drawing.Size(100, 23);
            this.BUTTON_ConfirmSort.TabIndex = 6;
            this.BUTTON_ConfirmSort.Text = "<Confirm order>";
            this.BUTTON_ConfirmSort.UseVisualStyleBackColor = true;
            this.BUTTON_ConfirmSort.Click += new System.EventHandler(this.BUTTON_ConfirmSort_Click);
            // 
            // GroupBox_SortCoursebot
            // 
            this.GroupBox_SortCoursebot.Controls.Add(this.RADIO_SortBySlot);
            this.GroupBox_SortCoursebot.Controls.Add(this.RADIO_SortByCourseNumber);
            this.GroupBox_SortCoursebot.Enabled = false;
            this.GroupBox_SortCoursebot.Location = new System.Drawing.Point(12, 51);
            this.GroupBox_SortCoursebot.Name = "GroupBox_SortCoursebot";
            this.GroupBox_SortCoursebot.Size = new System.Drawing.Size(120, 67);
            this.GroupBox_SortCoursebot.TabIndex = 1;
            this.GroupBox_SortCoursebot.TabStop = false;
            this.GroupBox_SortCoursebot.Text = "<Sort by>";
            // 
            // RADIO_SortBySlot
            // 
            this.RADIO_SortBySlot.AutoSize = true;
            this.RADIO_SortBySlot.Location = new System.Drawing.Point(6, 19);
            this.RADIO_SortBySlot.Name = "RADIO_SortBySlot";
            this.RADIO_SortBySlot.Size = new System.Drawing.Size(55, 17);
            this.RADIO_SortBySlot.TabIndex = 1;
            this.RADIO_SortBySlot.TabStop = true;
            this.RADIO_SortBySlot.Text = "<Slot>";
            this.RADIO_SortBySlot.UseVisualStyleBackColor = true;
            // 
            // RADIO_SortByCourseNumber
            // 
            this.RADIO_SortByCourseNumber.AutoSize = true;
            this.RADIO_SortByCourseNumber.Location = new System.Drawing.Point(6, 42);
            this.RADIO_SortByCourseNumber.Name = "RADIO_SortByCourseNumber";
            this.RADIO_SortByCourseNumber.Size = new System.Drawing.Size(108, 17);
            this.RADIO_SortByCourseNumber.TabIndex = 0;
            this.RADIO_SortByCourseNumber.TabStop = true;
            this.RADIO_SortByCourseNumber.Text = "<Course number>";
            this.RADIO_SortByCourseNumber.UseVisualStyleBackColor = true;
            // 
            // FORM_SMM1_SaveFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 361);
            this.Controls.Add(this.GroupBox_SortCoursebot);
            this.Controls.Add(this.BUTTON_ConfirmSort);
            this.Controls.Add(this.GroupBox_GamePad3DAudio);
            this.Controls.Add(this.GroupBox_NotifyCreators);
            this.Controls.Add(this.BUTTON_Cancel);
            this.Controls.Add(this.BUTTON_SaveFile);
            this.Controls.Add(this.GroupBox_NotifyMyCourses);
            this.Controls.Add(this.GroupBox_Controls);
            this.Controls.Add(this.CHECK_ItemDelivery8);
            this.Controls.Add(this.CHECK_ItemDelivery7);
            this.Controls.Add(this.CHECK_ItemDelivery6);
            this.Controls.Add(this.CHECK_ItemDelivery5);
            this.Controls.Add(this.CHECK_ItemDelivery4);
            this.Controls.Add(this.CHECK_ItemDelivery3);
            this.Controls.Add(this.CHECK_ItemDelivery2);
            this.Controls.Add(this.CHECK_ItemDelivery1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CHECKLISTBOX_ItemsOrder);
            this.Controls.Add(this.BUTTON_RemoveCourse);
            this.Controls.Add(this.BUTTON_ASC_Sort);
            this.Controls.Add(this.BUTTON_DESC_Sort);
            this.Controls.Add(this.LISTBOX_Coursebot);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FORM_SMM1_SaveFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<_TITLE>";
            this.Load += new System.EventHandler(this.FORM_SMM1_SaveFile_Load);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.LISTBOX_Coursebot, 0);
            this.Controls.SetChildIndex(this.BUTTON_DESC_Sort, 0);
            this.Controls.SetChildIndex(this.BUTTON_ASC_Sort, 0);
            this.Controls.SetChildIndex(this.BUTTON_RemoveCourse, 0);
            this.Controls.SetChildIndex(this.CHECKLISTBOX_ItemsOrder, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.button2, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery1, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery2, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery3, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery4, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery5, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery6, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery7, 0);
            this.Controls.SetChildIndex(this.CHECK_ItemDelivery8, 0);
            this.Controls.SetChildIndex(this.GroupBox_Controls, 0);
            this.Controls.SetChildIndex(this.GroupBox_NotifyMyCourses, 0);
            this.Controls.SetChildIndex(this.BUTTON_SaveFile, 0);
            this.Controls.SetChildIndex(this.BUTTON_Cancel, 0);
            this.Controls.SetChildIndex(this.GroupBox_NotifyCreators, 0);
            this.Controls.SetChildIndex(this.GroupBox_GamePad3DAudio, 0);
            this.Controls.SetChildIndex(this.BUTTON_ConfirmSort, 0);
            this.Controls.SetChildIndex(this.GroupBox_SortCoursebot, 0);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GroupBox_Controls.ResumeLayout(false);
            this.GroupBox_Controls.PerformLayout();
            this.GroupBox_GamePad3DAudio.ResumeLayout(false);
            this.GroupBox_GamePad3DAudio.PerformLayout();
            this.GroupBox_NotifyCreators.ResumeLayout(false);
            this.GroupBox_NotifyCreators.PerformLayout();
            this.GroupBox_NotifyMyCourses.ResumeLayout(false);
            this.GroupBox_NotifyMyCourses.PerformLayout();
            this.GroupBox_SortCoursebot.ResumeLayout(false);
            this.GroupBox_SortCoursebot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LISTBOX_Coursebot;
        private System.Windows.Forms.Button BUTTON_DESC_Sort;
        private System.Windows.Forms.Button BUTTON_ASC_Sort;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SelectFile;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog_datFile;
        private System.Windows.Forms.Button BUTTON_RemoveCourse;
        private System.Windows.Forms.CheckedListBox CHECKLISTBOX_ItemsOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery1;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery2;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery3;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery4;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery5;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery6;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery7;
        private System.Windows.Forms.CheckBox CHECK_ItemDelivery8;
        private System.Windows.Forms.GroupBox GroupBox_Controls;
        private System.Windows.Forms.RadioButton RADIO_DashJump;
        private System.Windows.Forms.RadioButton RADIO_JumpDash;
        private System.Windows.Forms.GroupBox GroupBox_GamePad3DAudio;
        private System.Windows.Forms.RadioButton RADIO_GamePad3DAudio_On;
        private System.Windows.Forms.RadioButton RADIO_GamePad3DAudio_Off;
        private System.Windows.Forms.GroupBox GroupBox_NotifyCreators;
        private System.Windows.Forms.RadioButton RADIO_NotifyCreators_On;
        private System.Windows.Forms.RadioButton RADIO_NotifyCreators_Off;
        private System.Windows.Forms.GroupBox GroupBox_NotifyMyCourses;
        private System.Windows.Forms.RadioButton RADIO_NotifyMyCourses_On;
        private System.Windows.Forms.RadioButton RADIO_NotifyMyCourses_Off;
        private System.Windows.Forms.Button BUTTON_SaveFile;
        private System.Windows.Forms.Button BUTTON_Cancel;
        private System.Windows.Forms.Button BUTTON_ConfirmSort;
        private System.Windows.Forms.GroupBox GroupBox_SortCoursebot;
        private System.Windows.Forms.RadioButton RADIO_SortBySlot;
        private System.Windows.Forms.RadioButton RADIO_SortByCourseNumber;
    }
}