namespace SMM_D4TA_EDITOR
{
    partial class DIALOG_ExtractMii
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
            this.TB_MiiName = new System.Windows.Forms.TextBox();
            this.BUTTON_Save = new System.Windows.Forms.Button();
            this.BUTTON_Cancel = new System.Windows.Forms.Button();
            this.LABEL_MiiName = new System.Windows.Forms.Label();
            this.CHECK_SaveMiiAsFFSD = new System.Windows.Forms.CheckBox();
            this.NUMERIC_CountryCode = new System.Windows.Forms.NumericUpDown();
            this.LABEL_Country = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CountryCode)).BeginInit();
            this.SuspendLayout();
            // 
            // TB_MiiName
            // 
            this.TB_MiiName.Location = new System.Drawing.Point(15, 25);
            this.TB_MiiName.Name = "TB_MiiName";
            this.TB_MiiName.Size = new System.Drawing.Size(218, 20);
            this.TB_MiiName.TabIndex = 0;
            // 
            // BUTTON_Save
            // 
            this.BUTTON_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BUTTON_Save.Location = new System.Drawing.Point(197, 101);
            this.BUTTON_Save.Name = "BUTTON_Save";
            this.BUTTON_Save.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Save.TabIndex = 4;
            this.BUTTON_Save.Text = "<Save>";
            this.BUTTON_Save.UseVisualStyleBackColor = true;
            this.BUTTON_Save.Click += new System.EventHandler(this.BUTTON_Save_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BUTTON_Cancel.Location = new System.Drawing.Point(116, 101);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 3;
            this.BUTTON_Cancel.Text = "<Cancel>";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // LABEL_MiiName
            // 
            this.LABEL_MiiName.AutoSize = true;
            this.LABEL_MiiName.Location = new System.Drawing.Point(12, 9);
            this.LABEL_MiiName.Name = "LABEL_MiiName";
            this.LABEL_MiiName.Size = new System.Drawing.Size(47, 13);
            this.LABEL_MiiName.TabIndex = 3;
            this.LABEL_MiiName.Text = "<Name>";
            // 
            // CHECK_SaveMiiAsFFSD
            // 
            this.CHECK_SaveMiiAsFFSD.AutoSize = true;
            this.CHECK_SaveMiiAsFFSD.Enabled = false;
            this.CHECK_SaveMiiAsFFSD.Location = new System.Drawing.Point(81, 67);
            this.CHECK_SaveMiiAsFFSD.Name = "CHECK_SaveMiiAsFFSD";
            this.CHECK_SaveMiiAsFFSD.Size = new System.Drawing.Size(123, 17);
            this.CHECK_SaveMiiAsFFSD.TabIndex = 2;
            this.CHECK_SaveMiiAsFFSD.Text = "<Save Mii as FFSD>";
            this.CHECK_SaveMiiAsFFSD.UseVisualStyleBackColor = true;
            // 
            // NUMERIC_CountryCode
            // 
            this.NUMERIC_CountryCode.Location = new System.Drawing.Point(15, 64);
            this.NUMERIC_CountryCode.Name = "NUMERIC_CountryCode";
            this.NUMERIC_CountryCode.Size = new System.Drawing.Size(60, 20);
            this.NUMERIC_CountryCode.TabIndex = 1;
            // 
            // LABEL_Country
            // 
            this.LABEL_Country.AutoSize = true;
            this.LABEL_Country.Location = new System.Drawing.Point(12, 48);
            this.LABEL_Country.Name = "LABEL_Country";
            this.LABEL_Country.Size = new System.Drawing.Size(55, 13);
            this.LABEL_Country.TabIndex = 8;
            this.LABEL_Country.Text = "<Country>";
            // 
            // DIALOG_ExtractMii
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 136);
            this.Controls.Add(this.LABEL_Country);
            this.Controls.Add(this.NUMERIC_CountryCode);
            this.Controls.Add(this.CHECK_SaveMiiAsFFSD);
            this.Controls.Add(this.LABEL_MiiName);
            this.Controls.Add(this.BUTTON_Cancel);
            this.Controls.Add(this.BUTTON_Save);
            this.Controls.Add(this.TB_MiiName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DIALOG_ExtractMii";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "<_TITLE>";
            ((System.ComponentModel.ISupportInitialize)(this.NUMERIC_CountryCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_MiiName;
        private System.Windows.Forms.Button BUTTON_Save;
        private System.Windows.Forms.Button BUTTON_Cancel;
        private System.Windows.Forms.Label LABEL_MiiName;
        private System.Windows.Forms.CheckBox CHECK_SaveMiiAsFFSD;
        private System.Windows.Forms.NumericUpDown NUMERIC_CountryCode;
        private System.Windows.Forms.Label LABEL_Country;
    }
}