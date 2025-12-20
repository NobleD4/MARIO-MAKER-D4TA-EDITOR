using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_About : BaseForm
    {
        public FORM_About()
        {
            InitializeComponent();
            LABEL_Version.Text = VersionControl.GetString();
        }

        private void COMBOBOX_Language_Settings_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = COMBOBOX_Language_Settings.SelectedItem.ToString();
            
            if (selected == Properties.Settings.Default.LanguageFile)
            {
                return;
            }

            Properties.Settings.Default.LanguageFile = selected;
            Properties.Settings.Default.Save();

            string caption = LanguageManager.Get("FORM_Main", "LangChangedTitle");
            string text = LanguageManager.Get("FORM_Main", "LangChanged");
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FORM_About_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_About");
            Activate();
            //Get Language Files
            string langDir = Path.Combine(Application.StartupPath, "Languages");
            if (Directory.Exists(langDir))
            {
                string[] files = Directory.GetFiles(langDir);
                for (int l = 0; l < files.Length; l++)
                {
                    if (files[l].EndsWith(".ini"))
                    {
                        int startPos = files[l].LastIndexOf(Path.DirectorySeparatorChar) + 1;
                        COMBOBOX_Language_Settings.Items.Add(files[l].Substring(startPos, files[l].LastIndexOf('.') - startPos));
                    }
                }
            }
            COMBOBOX_Language_Settings.DropDownStyle = ComboBoxStyle.DropDownList;
            COMBOBOX_Language_Settings.SelectedIndexChanged -= new EventHandler(COMBOBOX_Language_Settings_SelectedIndexChanged);
            COMBOBOX_Language_Settings.SelectedItem = Properties.Settings.Default.LanguageFile;
            COMBOBOX_Language_Settings.SelectedIndexChanged += new EventHandler(COMBOBOX_Language_Settings_SelectedIndexChanged);
        }

        private void LINK_GitHub_SMMD4TAEDITOR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/NobleD4/MARIO-MAKER-D4TA-EDITOR");
        }

        private void LINK_GitHub_PointlessMaker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/aboood40091/PointlessMaker");
        }

        private void LINK_GitHub_NSMBeMaintained_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/MammaMiaTeam/NSMB-Editor");
        }

        private void LINK_GitHub_NSMBeOriginal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Dirbaio/NSMB-Editor");
        }

        private void LINK_GitHub_SMM1LevelDownloader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/HerobrineTV/SMM1-Level-Downloader");
        }

        private void LINK_GitHub_TheFourthDimension_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/exelix11/TheFourthDimension");
        }
    }
}
