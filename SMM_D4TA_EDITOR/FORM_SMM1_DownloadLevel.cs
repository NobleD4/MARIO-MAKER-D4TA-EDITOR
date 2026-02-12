using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        const string API_EndpointSearchLevels= "searchLevels/";

        public class CONTROLLER_LevelDownloaderSMM1
        {
            private HttpClient client;

            public CONTROLLER_LevelDownloaderSMM1()
            {
                client = new HttpClient();
            }

            public async Task<List<MODEL_LevelDownloaderSMM1>> GetRandomLevel()
            {
                Random rnd = new Random();
                int randomNum =rnd.Next(1, 18118278);

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
        }

        //The main idea is to have a filter to choose between Nintendo Network, Pretendo Network and SMMDB

        //https://api.bobac-analytics.com/smm1/ping
        //https://api.bobac-analytics.com/smm1/searchRandomLevels/18118278
        //https://api.bobac-analytics.com/smm1/searchLevels/Idiom/1?coursename=1&courseid=0&creatorname=0&creatorid=0&searchexact=1

        private async void GetRndLvl()
        {
            var levels = await ControllerLevelDownloaderSMM1.GetRandomLevel();

            if (levels == null || levels.Count == 0)
            {
                MessageBox.Show("Not found.");
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

        private void BUTTON_SearchRandom_Click(object sender, EventArgs e)
        {
            GetRndLvl();
        }
    }
}
