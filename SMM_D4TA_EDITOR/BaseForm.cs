using System.Threading;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public class BaseForm : Form
    {
        private MenuStrip SMM1_MainMenu;
        private ToolStripMenuItem ToolStripMenuItem_SMM1_EditLevel;
        private ToolStripMenuItem ToolStripMenuItem_SMM1_SaveFile;
        private ToolStripMenuItem ToolStripMenuItem_About;

        public BaseForm()
        {
            SMM1_MainMenu = new MenuStrip();
            ToolStripMenuItem_SMM1_EditLevel = new ToolStripMenuItem("<Edit SMM1 level>");
            ToolStripMenuItem_SMM1_SaveFile = new ToolStripMenuItem("<Edit SMM1 Save file>");
            ToolStripMenuItem_About = new ToolStripMenuItem("<About>");

            //WHAAAAT?! Windows Forms always worked with a ".Name" property instead of actual variable name... I feel disappointed...
            //That's why Language class wasn't working only here
            SMM1_MainMenu.Name = "SMM1_MainMenu";
            ToolStripMenuItem_SMM1_EditLevel.Name = "ToolStripMenuItem_SMM1_EditLevel";
            ToolStripMenuItem_SMM1_SaveFile.Name = "ToolStripMenuItem_SMM1_SaveFile";
            ToolStripMenuItem_About.Name = "ToolStripMenuItem_About";

            ToolStripMenuItem_SMM1_EditLevel.Click += (s, e) => {
                OpenNewForm<FORM_Main>();
            };
            ToolStripMenuItem_SMM1_SaveFile.Click += (s, e) =>
            {
                OpenNewForm<FORM_SMM1_SaveFile>();
            };
            ToolStripMenuItem_About.Click += (s, e) =>
            {
                OpenNewForm<FORM_About>();
            };

            SMM1_MainMenu.Items.Add(ToolStripMenuItem_SMM1_EditLevel);
            SMM1_MainMenu.Items.Add(ToolStripMenuItem_SMM1_SaveFile);
            SMM1_MainMenu.Items.Add(ToolStripMenuItem_About);

            this.MainMenuStrip = SMM1_MainMenu;
            this.Controls.Add(SMM1_MainMenu);

            LanguageManager.ApplyToContainer(this, "FORM_BaseForm");
        }

        private void OpenNewForm<T>() where T : Form, new()
        {
            var x = new Thread(() =>
            {
                Application.Run(new T());
            });
            //THIS NEXT LINE IS IMPORTANT, MAIN THREAD IS "STA" AND IF I DON'T ADD IT MAIN THREAD WOULD BE "MTA" NOW
            x.SetApartmentState(ApartmentState.STA); //IF THE PROGRAM IS AT "MTA" THREAD, WILL CRASH IF YOU TRY TO OPEN A FILE
            this.Close();
            x.Start();
        }
    }
}