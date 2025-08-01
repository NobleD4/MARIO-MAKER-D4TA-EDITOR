using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string langDir = Path.Combine(Application.StartupPath, "Languages");
            string langFileName = Path.Combine(langDir, Properties.Settings.Default.LanguageFile + ".ini");
            if (File.Exists(langFileName))
            {
                StreamReader rdr = new StreamReader(langFileName);
                LanguageManager.Load(rdr.ReadToEnd().Split('\n'));
                rdr.Close();
            }
            else
            {
                MessageBox.Show("File " + langFileName + " could not be found, so the language has defaulted to English.");
                LanguageManager.Load(Properties.Resources.English.Split('\n'));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FORM_Main());
        }
    }
}
