using System;
using System.Collections.Generic;
using System.Text;

namespace SMM_D4TA_EDITOR
{
    public static class LanguageManager
    {

        public const string tooltipIdentifier = ".tooltip";

        private static Dictionary<string, Dictionary<string, string>> Contents;
        private static Dictionary<string, List<string>> Lists;

        public static void Load(string[] LangFile)
        {
            Contents = new Dictionary<string, Dictionary<string, string>>();
            Lists = new Dictionary<string, List<string>>();

            bool ListMode = false;
            Dictionary<string, string> CurrentSection = null;
            List<string> CurrentList = null;

            foreach (string Line in LangFile)
            {
                string CheckLine = Line.Trim();
                if (CheckLine == "") continue;
                if (CheckLine[0] == ';') continue;

                if (CheckLine.StartsWith("[") && CheckLine.EndsWith("]"))
                {
                    string SectionName = CheckLine.Substring(1, CheckLine.Length - 2);

                    if (SectionName.StartsWith("LIST_"))
                    {
                        CurrentList = new List<string>();
                        Lists[SectionName.Substring(5)] = CurrentList;
                        ListMode = true;

                    }
                    else
                    {
                        if (Contents.ContainsKey(SectionName))
                        {
                            CurrentSection = Contents[SectionName];
                        }
                        else
                        {
                            CurrentSection = new Dictionary<string, string>();
                            Contents[SectionName] = CurrentSection;
                        }
                        ListMode = false;
                    }

                }
                else
                {
                    if (ListMode)
                    {
                        CurrentList.Add(CheckLine);

                    }
                    else
                    {
                        if (CheckLine.Contains("="))
                        {
                            int EqPos = CheckLine.IndexOf('=');
                            CurrentSection[CheckLine.Substring(0, EqPos)] = CheckLine.Substring(EqPos + 1).Replace("\\n", "\n");
                        }
                    }
                }
            }
        }

        public static string Get(string Area, string Key)
        {
            if (Contents == null) return "<NOT LOADED>";

            if (Contents.ContainsKey(Area))
            {
                Dictionary<string, string> Referred = Contents[Area];
                if (Referred.ContainsKey(Key))
                {
                    return Referred[Key];
                }
            }

            return string.Format("<ERROR {0}:{1}>", Area, Key);
        }

        public static string Get(string Area, int Number)
        {
            if (Contents == null) return "<NOT LOADED>";

            if (Contents.ContainsKey(Area))
            {
                string[] keys = new string[Contents[Area].Keys.Count];
                Contents[Area].Keys.CopyTo(keys, 0);
                return (Contents[Area][keys[Number]]);
            }

            return string.Format("<ERROR {0}:{1}>", Area, Number);
        }

        public static List<string> GetList(string Name)
        {
            if (Lists == null) return new List<string>(new string[] { "<NOT LOADED>" });

            if (Lists.ContainsKey(Name))
            {
                return Lists[Name];
            }

            return new List<string>(new string[] { string.Format("<ERROR {0}>", Name) });
        }

        public static void ApplyToContainer(System.Windows.Forms.Control Container, string Area, System.Windows.Forms.ToolTip tooltip = null)
        {
            if (Contents == null) return;

            if (Contents.ContainsKey(Area))
            {
                Dictionary<string, string> Referred = Contents[Area];

                ApplyToContainer(Container, Referred, tooltip);

                if (Referred.ContainsKey("_TITLE"))
                {
                    Container.Text = Referred["_TITLE"];
                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show(
                    Area + " was missing from the language file.\nThe editor cannot continue.",
                    "NSMB Editor 4",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        private static void ApplyToContainer(System.Windows.Forms.Control Container, Dictionary<string, string> Referred, System.Windows.Forms.ToolTip tooltip = null)
        {
            foreach (System.Windows.Forms.Control Control in Container.Controls)
            {
                //Console.Out.WriteLine(Control.Name + " " + Control);
                if (Referred.ContainsKey(Control.Name))
                {
                    Control.Text = Referred[Control.Name];
                }
                //Applies tooltip text to a control
                if (tooltip != null && Referred.ContainsKey(Control.Name + tooltipIdentifier))
                {
                    tooltip.SetToolTip(Control, Referred[Control.Name + tooltipIdentifier]);
                }

                if (Control is System.Windows.Forms.ToolStrip)
                {
                    System.Windows.Forms.ToolStrip TS = Control as System.Windows.Forms.ToolStrip;
                    foreach (System.Windows.Forms.ToolStripItem TSItem in TS.Items)
                    {
                        if (Referred.ContainsKey(TSItem.Name))
                        {
                            TSItem.Text = Referred[TSItem.Name];
                        }
                        //Sets tooltip on a toolstrip
                        if (Referred.ContainsKey(TSItem.Name + tooltipIdentifier))
                        {
                            TSItem.ToolTipText = Referred[TSItem.Name + tooltipIdentifier];
                        }
                        //Language to DropDownItems
                        if (TSItem is System.Windows.Forms.ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
                        {
                            ApplyToToolStripMenuItemRecursive(menuItem, Referred);
                        }
                    }
                }

                if (Control.Controls.Count > 0)
                {
                    ApplyToContainer(Control, Referred, tooltip);
                }
            }
        }

        //New recursive function to apply language to DropDownMenu from ToolStripMenuItem
        private static void ApplyToToolStripMenuItemRecursive(System.Windows.Forms.ToolStripMenuItem parent, Dictionary<string, string> Referred)
        {
            foreach (System.Windows.Forms.ToolStripItem subItem in parent.DropDownItems)
            {
                if (!string.IsNullOrEmpty(subItem.Name) && Referred.ContainsKey(subItem.Name))
                {
                    subItem.Text = Referred[subItem.Name];
                }

                if (subItem is System.Windows.Forms.ToolStripMenuItem subMenuItem && subMenuItem.HasDropDownItems)
                {
                    ApplyToToolStripMenuItemRecursive(subMenuItem, Referred);
                }
            }
        }
    }
}
