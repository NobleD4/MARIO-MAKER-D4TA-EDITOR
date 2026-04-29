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

            public async Task<string> GetArchiveTimestamp(string SMM1_LvlObjUrl)
            {
                string encodedUrl = Uri.EscapeDataString(SMM1_LvlObjUrl); //uri stuff is just in case to avoid errors from spaces/symbols
                string waybackURL = $"https://web.archive.org/__wb/sparkline?output=json&url={encodedUrl}&collection=web";

                var request = new HttpRequestMessage(HttpMethod.Get, waybackURL);

                request.Headers.Add("User-Agent", "Mozilla/5.0");
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("Referer", $"https://web.archive.org/web/*/{SMM1_LvlObjUrl}");
                request.Headers.Add("Cache-Control", "no-cache");

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                string json = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(json);

                return data?.first_ts;
            }

            public async Task DownloadLevelFile(string archiveUrl, string outputPath)
            {
                var response = await client.GetAsync(archiveUrl);
                response.EnsureSuccessStatusCode();

                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                File.WriteAllBytes(outputPath, fileBytes);
            }

            public string BuildArchiveDownloadUrl(string timestamp, string SMM1_LvlObjUrl)
            {
                return $"https://web.archive.org/web/{timestamp}if_/{SMM1_LvlObjUrl}";
            }

            public async Task DownloadLevel(MODEL_LevelDownloaderSMM1 level, string outputPath)
            {
                string timestamp = await GetArchiveTimestamp(level.url);

                if (string.IsNullOrEmpty(timestamp))
                {
                    string text = LanguageManager.Get("FORM_LevelDownloader", "msgNotFound");
                    MessageBox.Show(text);
                    return;
                }

                string archiveUrl = BuildArchiveDownloadUrl(timestamp, level.url);

                string fileName = Path.GetFileName(new Uri(level.url).AbsolutePath);

                await DownloadLevelFile(archiveUrl, outputPath);

                MessageBox.Show("Level downloaded successfully");
            }
        }

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
                    level.clearrate * 100
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
                int rowIndex = DataGridView_LevelResults.Rows.Add(
                    level.name,
                    level.levelid,
                    level.creator,
                    level.creatorid,
                    level.clearrate * 100
                );

                DataGridView_LevelResults.Rows[rowIndex].Tag = level;
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
                    SMM1_GetLvlsSearch(Uri.EscapeDataString(TB_LevelSearch.Text), SearchPageNUM, 1, 0, 0, 0, 1);
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 1)
                {
                    SMM1_GetLvlsSearch(Uri.EscapeDataString(TB_LevelSearch.Text), SearchPageNUM, 0, 1, 0, 0, 1);
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 2)
                {
                    if (TB_LevelSearch.Text == "NobleD4")
                    {
                        SMM1_GetLvlsSearch("D4Pro10", SearchPageNUM, 0, 0, 1, 0, 1);
                    }
                    else
                    {
                        SMM1_GetLvlsSearch(Uri.EscapeDataString(TB_LevelSearch.Text), SearchPageNUM, 0, 0, 1, 0, 1);
                    }
                }
                else if (ComboBox_FilterSearch.SelectedIndex == 3)
                {
                    SMM1_GetLvlsSearch(Uri.EscapeDataString(TB_LevelSearch.Text), SearchPageNUM, 0, 0, 0, 1, 1);
                }
            }
        }

        private async void BUTTON_DownloadLevel_Click(object sender, EventArgs e)
        {
            var row = DataGridView_LevelResults.CurrentRow;

            if (row == null || row.Tag == null)
            {
                MessageBox.Show("There's no selected row");
                return;
            }

            var level = (MODEL_LevelDownloaderSMM1)row.Tag;

            if (level == null)
            {
                string text = LanguageManager.Get("FORM_LevelDownloader", "msgNotFound");
                MessageBox.Show(text);
                return;
            }

            if (SaveFileDialog_SMM1Level.ShowDialog() == DialogResult.OK)
            {
                await ControllerLevelDownloaderSMM1.DownloadLevel(level, SaveFileDialog_SMM1Level.FileName);
            }
        }
    }
}