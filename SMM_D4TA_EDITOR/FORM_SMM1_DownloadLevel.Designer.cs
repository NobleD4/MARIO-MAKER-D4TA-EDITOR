namespace SMM_D4TA_EDITOR
{
    partial class FORM_SMM1_DownloadLevel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_SMM1_DownloadLevel));
            this.DataGridView_LevelResults = new System.Windows.Forms.DataGridView();
            this.PICTURE_thumbnail0 = new System.Windows.Forms.PictureBox();
            this.PICTURE_thumbnail1 = new System.Windows.Forms.PictureBox();
            this.TB_LevelSearch = new System.Windows.Forms.TextBox();
            this.ComboBox_ServerSearch = new System.Windows.Forms.ComboBox();
            this.BUTTON_Search = new System.Windows.Forms.Button();
            this.LABEL_ServerSearch = new System.Windows.Forms.Label();
            this.ComboBox_FilterSearch = new System.Windows.Forms.ComboBox();
            this.LABEL_FilterSearch = new System.Windows.Forms.Label();
            this.BUTTON_SearchRandom = new System.Windows.Forms.Button();
            this.LABEL_IsLevelAPIWorking = new System.Windows.Forms.Label();
            this.BUTTON_DownloadLevel = new System.Windows.Forms.Button();
            this.BUTTON_PreviousPage = new System.Windows.Forms.Button();
            this.BUTTON_NextPage = new System.Windows.Forms.Button();
            this.TB_DisplayPage = new System.Windows.Forms.TextBox();
            this.SaveFileDialog_SMM1Level = new System.Windows.Forms.SaveFileDialog();
            this.CHECK_DecompressASH0 = new System.Windows.Forms.CheckBox();
            this.CHECK_DownloadMii = new System.Windows.Forms.CheckBox();
            this.LABEL_IsMiiAPIWorking = new System.Windows.Forms.Label();
            this.BUTTON_CopyID = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_LevelResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_LevelResults
            // 
            this.DataGridView_LevelResults.AllowUserToAddRows = false;
            this.DataGridView_LevelResults.AllowUserToDeleteRows = false;
            this.DataGridView_LevelResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_LevelResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView_LevelResults.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView_LevelResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_LevelResults.Location = new System.Drawing.Point(12, 107);
            this.DataGridView_LevelResults.Name = "DataGridView_LevelResults";
            this.DataGridView_LevelResults.ReadOnly = true;
            this.DataGridView_LevelResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_LevelResults.Size = new System.Drawing.Size(500, 186);
            this.DataGridView_LevelResults.TabIndex = 11;
            // 
            // PICTURE_thumbnail0
            // 
            this.PICTURE_thumbnail0.Location = new System.Drawing.Point(84, 299);
            this.PICTURE_thumbnail0.Name = "PICTURE_thumbnail0";
            this.PICTURE_thumbnail0.Size = new System.Drawing.Size(428, 50);
            this.PICTURE_thumbnail0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PICTURE_thumbnail0.TabIndex = 2;
            this.PICTURE_thumbnail0.TabStop = false;
            // 
            // PICTURE_thumbnail1
            // 
            this.PICTURE_thumbnail1.Location = new System.Drawing.Point(12, 299);
            this.PICTURE_thumbnail1.Name = "PICTURE_thumbnail1";
            this.PICTURE_thumbnail1.Size = new System.Drawing.Size(70, 50);
            this.PICTURE_thumbnail1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PICTURE_thumbnail1.TabIndex = 3;
            this.PICTURE_thumbnail1.TabStop = false;
            // 
            // TB_LevelSearch
            // 
            this.TB_LevelSearch.Location = new System.Drawing.Point(12, 67);
            this.TB_LevelSearch.MaxLength = 32;
            this.TB_LevelSearch.Name = "TB_LevelSearch";
            this.TB_LevelSearch.Size = new System.Drawing.Size(210, 20);
            this.TB_LevelSearch.TabIndex = 4;
            // 
            // ComboBox_ServerSearch
            // 
            this.ComboBox_ServerSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_ServerSearch.FormattingEnabled = true;
            this.ComboBox_ServerSearch.Location = new System.Drawing.Point(12, 40);
            this.ComboBox_ServerSearch.Name = "ComboBox_ServerSearch";
            this.ComboBox_ServerSearch.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_ServerSearch.TabIndex = 0;
            // 
            // BUTTON_Search
            // 
            this.BUTTON_Search.Location = new System.Drawing.Point(228, 65);
            this.BUTTON_Search.Name = "BUTTON_Search";
            this.BUTTON_Search.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Search.TabIndex = 5;
            this.BUTTON_Search.Text = "<Search>";
            this.BUTTON_Search.UseVisualStyleBackColor = true;
            this.BUTTON_Search.Click += new System.EventHandler(this.BUTTON_Search_Click);
            // 
            // LABEL_ServerSearch
            // 
            this.LABEL_ServerSearch.AutoSize = true;
            this.LABEL_ServerSearch.Location = new System.Drawing.Point(12, 24);
            this.LABEL_ServerSearch.Name = "LABEL_ServerSearch";
            this.LABEL_ServerSearch.Size = new System.Drawing.Size(50, 13);
            this.LABEL_ServerSearch.TabIndex = 0;
            this.LABEL_ServerSearch.Text = "<Server>";
            // 
            // ComboBox_FilterSearch
            // 
            this.ComboBox_FilterSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_FilterSearch.FormattingEnabled = true;
            this.ComboBox_FilterSearch.Location = new System.Drawing.Point(139, 40);
            this.ComboBox_FilterSearch.Name = "ComboBox_FilterSearch";
            this.ComboBox_FilterSearch.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_FilterSearch.TabIndex = 1;
            // 
            // LABEL_FilterSearch
            // 
            this.LABEL_FilterSearch.AutoSize = true;
            this.LABEL_FilterSearch.Location = new System.Drawing.Point(139, 24);
            this.LABEL_FilterSearch.Name = "LABEL_FilterSearch";
            this.LABEL_FilterSearch.Size = new System.Drawing.Size(41, 13);
            this.LABEL_FilterSearch.TabIndex = 0;
            this.LABEL_FilterSearch.Text = "<Filter>";
            // 
            // BUTTON_SearchRandom
            // 
            this.BUTTON_SearchRandom.Location = new System.Drawing.Point(266, 38);
            this.BUTTON_SearchRandom.Name = "BUTTON_SearchRandom";
            this.BUTTON_SearchRandom.Size = new System.Drawing.Size(94, 23);
            this.BUTTON_SearchRandom.TabIndex = 2;
            this.BUTTON_SearchRandom.Text = "<Random level>";
            this.BUTTON_SearchRandom.UseVisualStyleBackColor = true;
            this.BUTTON_SearchRandom.Click += new System.EventHandler(this.BUTTON_SearchRandom_Click);
            // 
            // LABEL_IsLevelAPIWorking
            // 
            this.LABEL_IsLevelAPIWorking.AutoSize = true;
            this.LABEL_IsLevelAPIWorking.Location = new System.Drawing.Point(437, 43);
            this.LABEL_IsLevelAPIWorking.Name = "LABEL_IsLevelAPIWorking";
            this.LABEL_IsLevelAPIWorking.Size = new System.Drawing.Size(24, 13);
            this.LABEL_IsLevelAPIWorking.TabIndex = 0;
            this.LABEL_IsLevelAPIWorking.Text = "API";
            // 
            // BUTTON_DownloadLevel
            // 
            this.BUTTON_DownloadLevel.Location = new System.Drawing.Point(309, 65);
            this.BUTTON_DownloadLevel.Name = "BUTTON_DownloadLevel";
            this.BUTTON_DownloadLevel.Size = new System.Drawing.Size(110, 23);
            this.BUTTON_DownloadLevel.TabIndex = 6;
            this.BUTTON_DownloadLevel.Text = "<Download level>";
            this.BUTTON_DownloadLevel.UseVisualStyleBackColor = true;
            this.BUTTON_DownloadLevel.Click += new System.EventHandler(this.BUTTON_DownloadLevel_Click);
            // 
            // BUTTON_PreviousPage
            // 
            this.BUTTON_PreviousPage.Location = new System.Drawing.Point(425, 65);
            this.BUTTON_PreviousPage.Name = "BUTTON_PreviousPage";
            this.BUTTON_PreviousPage.Size = new System.Drawing.Size(20, 23);
            this.BUTTON_PreviousPage.TabIndex = 7;
            this.BUTTON_PreviousPage.Text = "<";
            this.BUTTON_PreviousPage.UseVisualStyleBackColor = true;
            this.BUTTON_PreviousPage.Click += new System.EventHandler(this.BUTTON_PreviousPage_Click);
            // 
            // BUTTON_NextPage
            // 
            this.BUTTON_NextPage.Location = new System.Drawing.Point(482, 65);
            this.BUTTON_NextPage.Name = "BUTTON_NextPage";
            this.BUTTON_NextPage.Size = new System.Drawing.Size(20, 23);
            this.BUTTON_NextPage.TabIndex = 9;
            this.BUTTON_NextPage.Text = ">";
            this.BUTTON_NextPage.UseVisualStyleBackColor = true;
            this.BUTTON_NextPage.Click += new System.EventHandler(this.BUTTON_NextPage_Click);
            // 
            // TB_DisplayPage
            // 
            this.TB_DisplayPage.Location = new System.Drawing.Point(451, 67);
            this.TB_DisplayPage.Name = "TB_DisplayPage";
            this.TB_DisplayPage.ReadOnly = true;
            this.TB_DisplayPage.Size = new System.Drawing.Size(25, 20);
            this.TB_DisplayPage.TabIndex = 8;
            // 
            // SaveFileDialog_SMM1Level
            // 
            this.SaveFileDialog_SMM1Level.Filter = "File|*.*";
            // 
            // CHECK_DecompressASH0
            // 
            this.CHECK_DecompressASH0.AutoSize = true;
            this.CHECK_DecompressASH0.Location = new System.Drawing.Point(12, 88);
            this.CHECK_DecompressASH0.Name = "CHECK_DecompressASH0";
            this.CHECK_DecompressASH0.Size = new System.Drawing.Size(134, 17);
            this.CHECK_DecompressASH0.TabIndex = 10;
            this.CHECK_DecompressASH0.Text = "< Decompress ASH0 >";
            this.CHECK_DecompressASH0.UseVisualStyleBackColor = true;
            // 
            // CHECK_DownloadMii
            // 
            this.CHECK_DownloadMii.AutoSize = true;
            this.CHECK_DownloadMii.Location = new System.Drawing.Point(152, 88);
            this.CHECK_DownloadMii.Name = "CHECK_DownloadMii";
            this.CHECK_DownloadMii.Size = new System.Drawing.Size(108, 17);
            this.CHECK_DownloadMii.TabIndex = 11;
            this.CHECK_DownloadMii.Text = "< Download Mii >";
            this.CHECK_DownloadMii.UseVisualStyleBackColor = true;
            // 
            // LABEL_IsMiiAPIWorking
            // 
            this.LABEL_IsMiiAPIWorking.AutoSize = true;
            this.LABEL_IsMiiAPIWorking.Location = new System.Drawing.Point(260, 89);
            this.LABEL_IsMiiAPIWorking.Name = "LABEL_IsMiiAPIWorking";
            this.LABEL_IsMiiAPIWorking.Size = new System.Drawing.Size(24, 13);
            this.LABEL_IsMiiAPIWorking.TabIndex = 0;
            this.LABEL_IsMiiAPIWorking.Text = "API";
            // 
            // BUTTON_CopyID
            // 
            this.BUTTON_CopyID.Location = new System.Drawing.Point(366, 38);
            this.BUTTON_CopyID.Name = "BUTTON_CopyID";
            this.BUTTON_CopyID.Size = new System.Drawing.Size(70, 23);
            this.BUTTON_CopyID.TabIndex = 3;
            this.BUTTON_CopyID.Text = "<Copy ID>";
            this.BUTTON_CopyID.UseVisualStyleBackColor = true;
            this.BUTTON_CopyID.Click += new System.EventHandler(this.BUTTON_CopyID_Click);
            // 
            // FORM_SMM1_DownloadLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 361);
            this.Controls.Add(this.BUTTON_CopyID);
            this.Controls.Add(this.LABEL_IsMiiAPIWorking);
            this.Controls.Add(this.CHECK_DownloadMii);
            this.Controls.Add(this.CHECK_DecompressASH0);
            this.Controls.Add(this.TB_DisplayPage);
            this.Controls.Add(this.BUTTON_NextPage);
            this.Controls.Add(this.BUTTON_PreviousPage);
            this.Controls.Add(this.BUTTON_DownloadLevel);
            this.Controls.Add(this.LABEL_IsLevelAPIWorking);
            this.Controls.Add(this.BUTTON_SearchRandom);
            this.Controls.Add(this.LABEL_FilterSearch);
            this.Controls.Add(this.ComboBox_FilterSearch);
            this.Controls.Add(this.LABEL_ServerSearch);
            this.Controls.Add(this.BUTTON_Search);
            this.Controls.Add(this.ComboBox_ServerSearch);
            this.Controls.Add(this.TB_LevelSearch);
            this.Controls.Add(this.PICTURE_thumbnail1);
            this.Controls.Add(this.PICTURE_thumbnail0);
            this.Controls.Add(this.DataGridView_LevelResults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FORM_SMM1_DownloadLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<_TITLE>";
            this.Load += new System.EventHandler(this.FORM_SMM1_DownloadLevel_Load);
            this.Controls.SetChildIndex(this.DataGridView_LevelResults, 0);
            this.Controls.SetChildIndex(this.PICTURE_thumbnail0, 0);
            this.Controls.SetChildIndex(this.PICTURE_thumbnail1, 0);
            this.Controls.SetChildIndex(this.TB_LevelSearch, 0);
            this.Controls.SetChildIndex(this.ComboBox_ServerSearch, 0);
            this.Controls.SetChildIndex(this.BUTTON_Search, 0);
            this.Controls.SetChildIndex(this.LABEL_ServerSearch, 0);
            this.Controls.SetChildIndex(this.ComboBox_FilterSearch, 0);
            this.Controls.SetChildIndex(this.LABEL_FilterSearch, 0);
            this.Controls.SetChildIndex(this.BUTTON_SearchRandom, 0);
            this.Controls.SetChildIndex(this.LABEL_IsLevelAPIWorking, 0);
            this.Controls.SetChildIndex(this.BUTTON_DownloadLevel, 0);
            this.Controls.SetChildIndex(this.BUTTON_PreviousPage, 0);
            this.Controls.SetChildIndex(this.BUTTON_NextPage, 0);
            this.Controls.SetChildIndex(this.TB_DisplayPage, 0);
            this.Controls.SetChildIndex(this.CHECK_DecompressASH0, 0);
            this.Controls.SetChildIndex(this.CHECK_DownloadMii, 0);
            this.Controls.SetChildIndex(this.LABEL_IsMiiAPIWorking, 0);
            this.Controls.SetChildIndex(this.BUTTON_CopyID, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_LevelResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridView_LevelResults;
        private System.Windows.Forms.PictureBox PICTURE_thumbnail0;
        private System.Windows.Forms.PictureBox PICTURE_thumbnail1;
        private System.Windows.Forms.TextBox TB_LevelSearch;
        private System.Windows.Forms.ComboBox ComboBox_ServerSearch;
        private System.Windows.Forms.Button BUTTON_Search;
        private System.Windows.Forms.Label LABEL_ServerSearch;
        private System.Windows.Forms.ComboBox ComboBox_FilterSearch;
        private System.Windows.Forms.Label LABEL_FilterSearch;
        private System.Windows.Forms.Button BUTTON_SearchRandom;
        private System.Windows.Forms.Label LABEL_IsLevelAPIWorking;
        private System.Windows.Forms.Button BUTTON_DownloadLevel;
        private System.Windows.Forms.Button BUTTON_PreviousPage;
        private System.Windows.Forms.Button BUTTON_NextPage;
        private System.Windows.Forms.TextBox TB_DisplayPage;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_SMM1Level;
        private System.Windows.Forms.CheckBox CHECK_DecompressASH0;
        private System.Windows.Forms.CheckBox CHECK_DownloadMii;
        private System.Windows.Forms.Label LABEL_IsMiiAPIWorking;
        private System.Windows.Forms.Button BUTTON_CopyID;
    }
}