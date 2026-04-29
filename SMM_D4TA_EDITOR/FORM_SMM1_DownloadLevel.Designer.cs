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
            this.DataGridView_LevelResults = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICTURE_thumbnail0 = new System.Windows.Forms.PictureBox();
            this.PICTURE_thumbnail1 = new System.Windows.Forms.PictureBox();
            this.TB_LevelSearch = new System.Windows.Forms.TextBox();
            this.ComboBox_ServerSearch = new System.Windows.Forms.ComboBox();
            this.BUTTON_Search = new System.Windows.Forms.Button();
            this.LABEL_ServerSearch = new System.Windows.Forms.Label();
            this.ComboBox_FilterSearch = new System.Windows.Forms.ComboBox();
            this.LABEL_FilterSearch = new System.Windows.Forms.Label();
            this.BUTTON_SearchRandom = new System.Windows.Forms.Button();
            this.LABEL_IsAPIWorking = new System.Windows.Forms.Label();
            this.BUTTON_DownloadLevel = new System.Windows.Forms.Button();
            this.BUTTON_PreviousPage = new System.Windows.Forms.Button();
            this.BUTTON_NextPage = new System.Windows.Forms.Button();
            this.TB_DisplayPage = new System.Windows.Forms.TextBox();
            this.SaveFileDialog_SMM1Level = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_LevelResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTURE_thumbnail1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView_LevelResults
            // 
            this.DataGridView_LevelResults.AllowUserToAddRows = false;
            this.DataGridView_LevelResults.AllowUserToDeleteRows = false;
            this.DataGridView_LevelResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_LevelResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DataGridView_LevelResults.Location = new System.Drawing.Point(12, 107);
            this.DataGridView_LevelResults.Name = "DataGridView_LevelResults";
            this.DataGridView_LevelResults.ReadOnly = true;
            this.DataGridView_LevelResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_LevelResults.Size = new System.Drawing.Size(500, 186);
            this.DataGridView_LevelResults.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Creator";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Creator ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Clear rate";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
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
            this.TB_LevelSearch.Location = new System.Drawing.Point(139, 41);
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
            this.ComboBox_ServerSearch.TabIndex = 5;
            // 
            // BUTTON_Search
            // 
            this.BUTTON_Search.Location = new System.Drawing.Point(355, 39);
            this.BUTTON_Search.Name = "BUTTON_Search";
            this.BUTTON_Search.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Search.TabIndex = 6;
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
            this.LABEL_ServerSearch.TabIndex = 10;
            this.LABEL_ServerSearch.Text = "<Server>";
            // 
            // ComboBox_FilterSearch
            // 
            this.ComboBox_FilterSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_FilterSearch.FormattingEnabled = true;
            this.ComboBox_FilterSearch.Location = new System.Drawing.Point(12, 80);
            this.ComboBox_FilterSearch.Name = "ComboBox_FilterSearch";
            this.ComboBox_FilterSearch.Size = new System.Drawing.Size(121, 21);
            this.ComboBox_FilterSearch.TabIndex = 4;
            // 
            // LABEL_FilterSearch
            // 
            this.LABEL_FilterSearch.AutoSize = true;
            this.LABEL_FilterSearch.Location = new System.Drawing.Point(12, 64);
            this.LABEL_FilterSearch.Name = "LABEL_FilterSearch";
            this.LABEL_FilterSearch.Size = new System.Drawing.Size(41, 13);
            this.LABEL_FilterSearch.TabIndex = 14;
            this.LABEL_FilterSearch.Text = "<Filter>";
            // 
            // BUTTON_SearchRandom
            // 
            this.BUTTON_SearchRandom.Location = new System.Drawing.Point(139, 78);
            this.BUTTON_SearchRandom.Name = "BUTTON_SearchRandom";
            this.BUTTON_SearchRandom.Size = new System.Drawing.Size(94, 23);
            this.BUTTON_SearchRandom.TabIndex = 15;
            this.BUTTON_SearchRandom.Text = "<Random level>";
            this.BUTTON_SearchRandom.UseVisualStyleBackColor = true;
            this.BUTTON_SearchRandom.Click += new System.EventHandler(this.BUTTON_SearchRandom_Click);
            // 
            // LABEL_IsAPIWorking
            // 
            this.LABEL_IsAPIWorking.AutoSize = true;
            this.LABEL_IsAPIWorking.Location = new System.Drawing.Point(436, 44);
            this.LABEL_IsAPIWorking.Name = "LABEL_IsAPIWorking";
            this.LABEL_IsAPIWorking.Size = new System.Drawing.Size(92, 13);
            this.LABEL_IsAPIWorking.TabIndex = 18;
            this.LABEL_IsAPIWorking.Text = "<API WORKING>";
            // 
            // BUTTON_DownloadLevel
            // 
            this.BUTTON_DownloadLevel.Location = new System.Drawing.Point(239, 78);
            this.BUTTON_DownloadLevel.Name = "BUTTON_DownloadLevel";
            this.BUTTON_DownloadLevel.Size = new System.Drawing.Size(110, 23);
            this.BUTTON_DownloadLevel.TabIndex = 19;
            this.BUTTON_DownloadLevel.Text = "<Download level>";
            this.BUTTON_DownloadLevel.UseVisualStyleBackColor = true;
            this.BUTTON_DownloadLevel.Click += new System.EventHandler(this.BUTTON_DownloadLevel_Click);
            // 
            // BUTTON_PreviousPage
            // 
            this.BUTTON_PreviousPage.Location = new System.Drawing.Point(355, 78);
            this.BUTTON_PreviousPage.Name = "BUTTON_PreviousPage";
            this.BUTTON_PreviousPage.Size = new System.Drawing.Size(20, 23);
            this.BUTTON_PreviousPage.TabIndex = 20;
            this.BUTTON_PreviousPage.Text = "<";
            this.BUTTON_PreviousPage.UseVisualStyleBackColor = true;
            this.BUTTON_PreviousPage.Click += new System.EventHandler(this.BUTTON_PreviousPage_Click);
            // 
            // BUTTON_NextPage
            // 
            this.BUTTON_NextPage.Location = new System.Drawing.Point(412, 78);
            this.BUTTON_NextPage.Name = "BUTTON_NextPage";
            this.BUTTON_NextPage.Size = new System.Drawing.Size(20, 23);
            this.BUTTON_NextPage.TabIndex = 21;
            this.BUTTON_NextPage.Text = ">";
            this.BUTTON_NextPage.UseVisualStyleBackColor = true;
            this.BUTTON_NextPage.Click += new System.EventHandler(this.BUTTON_NextPage_Click);
            // 
            // TB_DisplayPage
            // 
            this.TB_DisplayPage.Location = new System.Drawing.Point(381, 80);
            this.TB_DisplayPage.Name = "TB_DisplayPage";
            this.TB_DisplayPage.ReadOnly = true;
            this.TB_DisplayPage.Size = new System.Drawing.Size(25, 20);
            this.TB_DisplayPage.TabIndex = 22;
            // 
            // SaveFileDialog_SMM1Level
            // 
            this.SaveFileDialog_SMM1Level.Filter = "File|*.*";
            // 
            // FORM_SMM1_DownloadLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 361);
            this.Controls.Add(this.TB_DisplayPage);
            this.Controls.Add(this.BUTTON_NextPage);
            this.Controls.Add(this.BUTTON_PreviousPage);
            this.Controls.Add(this.BUTTON_DownloadLevel);
            this.Controls.Add(this.LABEL_IsAPIWorking);
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
            this.Controls.SetChildIndex(this.LABEL_IsAPIWorking, 0);
            this.Controls.SetChildIndex(this.BUTTON_DownloadLevel, 0);
            this.Controls.SetChildIndex(this.BUTTON_PreviousPage, 0);
            this.Controls.SetChildIndex(this.BUTTON_NextPage, 0);
            this.Controls.SetChildIndex(this.TB_DisplayPage, 0);
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
        private System.Windows.Forms.Label LABEL_IsAPIWorking;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button BUTTON_DownloadLevel;
        private System.Windows.Forms.Button BUTTON_PreviousPage;
        private System.Windows.Forms.Button BUTTON_NextPage;
        private System.Windows.Forms.TextBox TB_DisplayPage;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog_SMM1Level;
    }
}