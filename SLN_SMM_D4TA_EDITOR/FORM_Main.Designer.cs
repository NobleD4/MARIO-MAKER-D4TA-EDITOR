namespace MakerUtilidades
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
            this.CHECK_RemoveFlags = new System.Windows.Forms.CheckBox();
            this.BUTTON_SaveFile = new System.Windows.Forms.Button();
            this.BUTTON_Cancel = new System.Windows.Forms.Button();
            this.CHECK_UploadReady = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(314, 24);
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
            this.ToolStripMenuItem_BYML_To_XML.Size = new System.Drawing.Size(84, 20);
            this.ToolStripMenuItem_BYML_To_XML.Text = "byml -> xml";
            this.ToolStripMenuItem_BYML_To_XML.Click += new System.EventHandler(this.ToolStripMenuItem_BYML_To_XML_Click);
            // 
            // ToolStripMenuItem_XML_To_BYML
            // 
            this.ToolStripMenuItem_XML_To_BYML.Name = "ToolStripMenuItem_XML_To_BYML";
            this.ToolStripMenuItem_XML_To_BYML.Size = new System.Drawing.Size(84, 20);
            this.ToolStripMenuItem_XML_To_BYML.Text = "xml -> byml";
            this.ToolStripMenuItem_XML_To_BYML.Click += new System.EventHandler(this.ToolStripMenuItem_XML_To_BYML_Click);
            // 
            // SaveFileDialog_BYML_To_XML
            // 
            this.SaveFileDialog_BYML_To_XML.Filter = "File|*.xml";
            // 
            // OpenFileDialog_BYML_To_XML
            // 
            this.OpenFileDialog_BYML_To_XML.FileName = "openFileDialog1";
            this.OpenFileDialog_BYML_To_XML.Filter = "File|*.BYML;*.Byml;*.byml;*.byaml|All files|*.*";
            // 
            // SaveFileDialog_XML_To_BYML
            // 
            this.SaveFileDialog_XML_To_BYML.Filter = "File|*.byml";
            // 
            // OpenFileDialog_XML_To_BYML
            // 
            this.OpenFileDialog_XML_To_BYML.FileName = "openFileDialog1";
            this.OpenFileDialog_XML_To_BYML.Filter = "File|*.XML;*.Xml;*.xml|All files|*.*";
            // 
            // OpenFileDialog_cdtFile
            // 
            this.OpenFileDialog_cdtFile.FileName = "OpenFileDialog1";
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
            // CHECK_RemoveFlags
            // 
            this.CHECK_RemoveFlags.AutoSize = true;
            this.CHECK_RemoveFlags.Enabled = false;
            this.CHECK_RemoveFlags.Location = new System.Drawing.Point(15, 53);
            this.CHECK_RemoveFlags.Name = "CHECK_RemoveFlags";
            this.CHECK_RemoveFlags.Size = new System.Drawing.Size(191, 17);
            this.CHECK_RemoveFlags.TabIndex = 1;
            this.CHECK_RemoveFlags.Text = "Remove Downloaded/Deleted flag";
            this.CHECK_RemoveFlags.UseVisualStyleBackColor = true;
            // 
            // BUTTON_SaveFile
            // 
            this.BUTTON_SaveFile.Enabled = false;
            this.BUTTON_SaveFile.Location = new System.Drawing.Point(227, 76);
            this.BUTTON_SaveFile.Name = "BUTTON_SaveFile";
            this.BUTTON_SaveFile.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_SaveFile.TabIndex = 4;
            this.BUTTON_SaveFile.Text = "Save course";
            this.BUTTON_SaveFile.UseVisualStyleBackColor = true;
            this.BUTTON_SaveFile.Click += new System.EventHandler(this.BUTTON_SaveFile_Click);
            // 
            // BUTTON_Cancel
            // 
            this.BUTTON_Cancel.Enabled = false;
            this.BUTTON_Cancel.Location = new System.Drawing.Point(146, 76);
            this.BUTTON_Cancel.Name = "BUTTON_Cancel";
            this.BUTTON_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BUTTON_Cancel.TabIndex = 3;
            this.BUTTON_Cancel.Text = "Cancel";
            this.BUTTON_Cancel.UseVisualStyleBackColor = true;
            this.BUTTON_Cancel.Click += new System.EventHandler(this.BUTTON_Cancel_Click);
            // 
            // CHECK_UploadReady
            // 
            this.CHECK_UploadReady.AutoSize = true;
            this.CHECK_UploadReady.Enabled = false;
            this.CHECK_UploadReady.Location = new System.Drawing.Point(211, 53);
            this.CHECK_UploadReady.Name = "CHECK_UploadReady";
            this.CHECK_UploadReady.Size = new System.Drawing.Size(89, 17);
            this.CHECK_UploadReady.TabIndex = 2;
            this.CHECK_UploadReady.Text = "Upload ready";
            this.CHECK_UploadReady.UseVisualStyleBackColor = true;
            this.CHECK_UploadReady.Visible = false;
            // 
            // FORM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 111);
            this.Controls.Add(this.CHECK_UploadReady);
            this.Controls.Add(this.BUTTON_Cancel);
            this.Controls.Add(this.BUTTON_SaveFile);
            this.Controls.Add(this.CHECK_RemoveFlags);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_CourseName);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FORM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "v0.0.1 D4TA EDITOR";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.CheckBox CHECK_RemoveFlags;
        private System.Windows.Forms.Button BUTTON_SaveFile;
        private System.Windows.Forms.Button BUTTON_Cancel;
        private System.Windows.Forms.CheckBox CHECK_UploadReady;
    }
}

