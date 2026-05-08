using System;
using System.Data;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public partial class DIALOG_ExtractMii : Form
    {
        DataTable MiiData_XML;
        string MiiBytesBase64;

        public DIALOG_ExtractMii(string MiiBytes, string MiiName, int Country, bool state)
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "DIALOG_ExctractMii");
            MiiBytesBase64 = MiiBytes;

            TB_MiiName.Text = MiiName;
            NUMERIC_CountryCode.Value = Country;

            if (state) CHECK_SaveMiiAsFFSD.Enabled = true;
            else CHECK_SaveMiiAsFFSD.Enabled = false;
        }
        
        private void BUTTON_Save_Click(object sender, EventArgs e)
        {
            if (CHECK_SaveMiiAsFFSD.Checked) {

            }
            else {
                //Create "Data.xml" manually in the same path as .exe file to avoid a crash
                MiiData_XML = new DataTable("MiiData");
                MiiData_XML.Columns.Add("SaveName");
                MiiData_XML.Columns.Add("MiiBase64");
                MiiData_XML.Columns.Add("CountryID");
                MiiData_XML.ReadXml("Data.xml");

                MiiData_XML.Rows.Add(TB_MiiName.Text, MiiBytesBase64, NUMERIC_CountryCode.Value);

                MiiData_XML.GetChanges().WriteXml("Data.xml");
            }

            string text = LanguageManager.Get("DIALOG_ExctractMii", "msgMiiExtracted");
            MessageBox.Show(text);
            
            Close();
        }

        private void BUTTON_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
