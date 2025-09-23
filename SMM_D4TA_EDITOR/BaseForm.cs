using SMM_D4TA_EDITOR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLN_SMM_D4TA_EDITOR
{
    public class BaseForm : Form
    {
        protected MenuStrip SMM1_MainMenu;

        public BaseForm()
        {
            SMM1_MainMenu = new MenuStrip();
            var ToolStripMenuItem_SMM1_EditLevel = new ToolStripMenuItem("Edit SMM1 level");
            var ToolStripMenuItem_SMM1_SaveFile = new ToolStripMenuItem("Edit SMM1 Save file");
            var ToolStripMenuItem_About = new ToolStripMenuItem("About");

            ToolStripMenuItem_SMM1_SaveFile.Enabled = false;

            ToolStripMenuItem_SMM1_EditLevel.Click += (s, e) => {
                var x = new Thread(() => Application.Run(new FORM_Main()));
                //THIS NEXT LINE IS IMPORTANT, MAIN THREAD IS "STA" AND IF I DON'T ADD IT MAIN THREAD WOULD BE "MTA" NOW
                x.SetApartmentState(ApartmentState.STA); //IF THE PROGRAM IS AT "MTA" THREAD, WILL CRASH IF YOU TRY TO OPEN A FILE
                this.Close();
                x.Start();
            };
            ToolStripMenuItem_SMM1_SaveFile.Click += (s, e) =>
            {

            };
            ToolStripMenuItem_About.Click += (s, e) =>
            {
                var x = new Thread(() => Application.Run(new FORM_About()));
                //THIS NEXT LINE IS IMPORTANT, MAIN THREAD IS "STA" AND IF I DON'T ADD IT MAIN THREAD WOULD BE "MTA" NOW
                x.SetApartmentState(ApartmentState.STA); //IF THE PROGRAM IS AT "MTA" THREAD, WILL CRASH IF YOU TRY TO OPEN A FILE
                this.Close();
                x.Start();
            };

            SMM1_MainMenu.Items.Add(ToolStripMenuItem_SMM1_EditLevel);
            SMM1_MainMenu.Items.Add(ToolStripMenuItem_SMM1_SaveFile);
            SMM1_MainMenu.Items.Add(ToolStripMenuItem_About);

            this.MainMenuStrip = SMM1_MainMenu;
            this.Controls.Add(SMM1_MainMenu);
        }
    }
}