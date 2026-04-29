using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_SMM1_DownloadLevel : BaseForm
    {
        public FORM_SMM1_DownloadLevel()
        {
            InitializeComponent();
            ControllerLevelDownloaderSMM1 = new CONTROLLER_LevelDownloaderSMM1();
        }

        private CONTROLLER_LevelDownloaderSMM1 ControllerLevelDownloaderSMM1;

        const string API_LevelDownloaderSMM1 = "https://api.bobac-analytics.com/smm1/";
        const string API_EndpointSearchRandom = "searchRandomLevels/";
        const string API_EndpointSearchLevels = "searchLevels/";
        const string WebArchiveSMM1FileString1 = "https://web.archive.org/__wb/sparkline?output=json&url=$"; string UrlLevelObjSMM1 = null; const string WebArchiveSMM1FileString2 = "&collection=web";

        byte SearchPageNUM = 1;

        private async void FORM_SMM1_DownloadLevel_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_LevelDownloader");
            ComboBox_ServerSearch.Items.AddRange(LanguageManager.GetList("ComboBox_Server").ToArray());
            ComboBox_FilterSearch.Items.AddRange(LanguageManager.GetList("ComboBox_Filter").ToArray());
            Activate();

            ComboBox_ServerSearch.SelectedIndex = 0;
            ComboBox_FilterSearch.SelectedIndex = 0;

            TB_DisplayPage.Text = SearchPageNUM.ToString();

            var isAPIworking = await ControllerLevelDownloaderSMM1.IsAPIWorking();
            LABEL_IsAPIWorking.Text = isAPIworking;
        }

        public class CONTROLLER_LevelDownloaderSMM1
        {
            private HttpClient client;
            public CONTROLLER_LevelDownloaderSMM1()
            {
                client = new HttpClient();
            }

            public async Task<string> IsAPIWorking()
            {
                HttpResponseMessage response = await client.GetAsync(API_LevelDownloaderSMM1 + "ping");

                if (response.IsSuccessStatusCode)
                {
                    string statusTrue = LanguageManager.Get("FORM_LevelDownloader", "API_StatusTrue");
                    return statusTrue;
                }

                else {
                    string statusFalse = LanguageManager.Get("FORM_LevelDownloader", "API_StatusFalse");
                    return statusFalse;
                }
            }


            public async Task<List<MODEL_LevelDownloaderSMM1>> SMM1_GetRandomLevel()
            {
                Random rnd = new Random();
                int randomNum = rnd.Next(1, 18118278);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(API_LevelDownloaderSMM1 + API_EndpointSearchRandom + randomNum);
                    response.EnsureSuccessStatusCode();

                    string responseJson = await response.Content.ReadAsStringAsync();

                    if (responseJson.TrimStart().StartsWith("{"))
                    {
                        return new List<MODEL_LevelDownloaderSMM1>(); //Empty list
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<MODEL_LevelDownloaderSMM1>>(responseJson);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public async Task<List<MODEL_LevelDownloaderSMM1>> SMM1_GetLevels(string LvlTxtSearch, byte pageNUM, byte coursename, byte courseid, byte creatorname, byte creatorid, byte searchexact)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(API_LevelDownloaderSMM1 + API_EndpointSearchLevels + LvlTxtSearch + $"/{pageNUM}?coursename={coursename}&courseid={courseid}&creatorname={creatorname}&creatorid={creatorid}&searchexact={searchexact}");
                    response.EnsureSuccessStatusCode();

                    string responseJson = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<List<MODEL_LevelDownloaderSMM1>>(responseJson);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //The main idea is to have a filter to choose between Nintendo Network, Pretendo Network and SMMDB

        //https://api.bobac-analytics.com/smm1/ping
        //https://api.bobac-analytics.com/smm1/searchRandomLevels/18118278
        //https://api.bobac-analytics.com/smm1/searchLevels/Idiom/1?coursename=1&courseid=0&creatorname=0&creatorid=0&searchexact=1

        private async void SMM1_GetRndLvl()
        {
            var levels = await ControllerLevelDownloaderSMM1.SMM1_GetRandomLevel();

            if (levels == null || levels.Count == 0)
            {
                string text = LanguageManager.Get("FORM_LevelDownloader", "msgNotFound");
                MessageBox.Show(text);
                return;
            }

            foreach (var level in levels)
            {
                DataGridView_LevelResults.Rows.Add(
                    level.name,
                    level.levelid,
                    level.creator,
                    level.creatorid,
                    level.clearrate
                );
            }
        }

        private async void SMM1_GetLvlsSearch(string SMM1_LvlTxtSearch, byte pageNUM, byte coursename, byte courseid, byte creatorname, byte creatorid, byte searchexact)
        {
            var levels = await ControllerLevelDownloaderSMM1.SMM1_GetLevels(SMM1_LvlTxtSearch, pageNUM, coursename, courseid, creatorname, creatorid, searchexact);

            if (levels == null || levels.Count == 0)
            {
                string text = LanguageManager.Get("FORM_LevelDownloader", "msgNotFound");
                MessageBox.Show(text);
                return;
            }

            foreach (var level in levels)
            {
                DataGridView_LevelResults.Rows.Add(
                    level.name,
                    level.levelid,
                    level.creator,
                    level.creatorid,
                    level.clearrate * 100
                );
            }
        }

        private void BUTTON_SearchRandom_Click(object sender, EventArgs e)
        {
            SMM1_GetRndLvl();
        }

        private void BUTTON_PreviousPage_Click(object sender, EventArgs e)
        {
            SearchPageNUM--;
            TB_DisplayPage.Text = SearchPageNUM.ToString();
        }

        private void BUTTON_NextPage_Click(object sender, EventArgs e)
        {
            SearchPageNUM++;
            TB_DisplayPage.Text = SearchPageNUM.ToString();
        }

        private void BUTTON_Search_Click(object sender, EventArgs e)
        {
            DataGridView_LevelResults.Rows.Clear();

            if (ComboBox_ServerSearch.SelectedIndex == 0)
            {
                if (ComboBox_FilterSearch.SelectedIndex == 0)
                {
                    SMM1_GetLvlsSearch(TB_LevelSearch.Text, SearchPageNUM, 1, 0, 0, 0, 1);
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 1)
                {
                    SMM1_GetLvlsSearch(TB_LevelSearch.Text, SearchPageNUM, 0, 1, 0, 0, 1);
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 2)
                {
                    if (TB_LevelSearch.Text == "NobleD4")
                    {
                        SMM1_GetLvlsSearch("D4Pro10", SearchPageNUM, 0, 0, 1, 0, 1);
                    }
                    else
                    {
                        SMM1_GetLvlsSearch(TB_LevelSearch.Text, SearchPageNUM, 0, 0, 1, 0, 1);
                    }
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 3)
                {
                    SMM1_GetLvlsSearch(TB_LevelSearch.Text, SearchPageNUM, 0, 0, 0, 1, 1);
                }
            }
        }
    }
}