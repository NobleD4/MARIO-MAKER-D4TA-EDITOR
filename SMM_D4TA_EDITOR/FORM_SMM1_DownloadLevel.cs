using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMM_D4TA_EDITOR.SMM1FileFormats;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_SMM1_DownloadLevel : BaseForm
    {
        public FORM_SMM1_DownloadLevel()
        {
            InitializeComponent();
            ControllerLevelDownloaderSMM1 = new CONTROLLER_LevelDownloaderSMM1();
            ControllerMiiData = new CONTROLLER_MiiData();
        }

        private CONTROLLER_LevelDownloaderSMM1 ControllerLevelDownloaderSMM1;
        private CONTROLLER_MiiData ControllerMiiData;

        const string API_LevelDownloaderSMM1 = "https://api.bobac-analytics.com/smm1/";
        const string API_EndpointSearchRandom = "searchRandomLevels/";
        const string API_EndpointSearchLevels = "searchLevels/";

        byte SearchPageNUM = 1;

        const string API_SMM1_Mii = "https://mii-unsecure.ariankordi.net/";

        private async void FORM_SMM1_DownloadLevel_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_LevelDownloader");
            ComboBox_ServerSearch.Items.AddRange(LanguageManager.GetList("ComboBox_Server").ToArray());
            ComboBox_FilterSearch.Items.AddRange(LanguageManager.GetList("ComboBox_Filter").ToArray());
            Activate();

            ComboBox_ServerSearch.SelectedIndex = 0;
            ComboBox_FilterSearch.SelectedIndex = 0;

            TB_DisplayPage.Text = SearchPageNUM.ToString();

            CHECK_DecompressASH0.Checked = true;
            CHECK_DownloadMii.Checked = true;

            var isAPIworking = await ControllerLevelDownloaderSMM1.IsAPIWorking();
            var isMiiAPIworking = await ControllerMiiData.IsAPIWorking();
            LABEL_IsLevelAPIWorking.Text = isAPIworking;
            LABEL_IsMiiAPIWorking.Text = isMiiAPIworking;
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

            public List<byte[]> SplitAshFile(byte[] data)
            {
                byte[] separator = new byte[] { 0x41, 0x53, 0x48, 0x30 }; // "ASH0"

                List<byte[]> parts = new List<byte[]>();

                int lastIndex = 0;

                while (true)
                {
                    int index = FindPattern(data, separator, lastIndex);
                    if (index == -1)
                        break;

                    int nextIndex = FindPattern(data, separator, index + separator.Length);
                    if (nextIndex == -1)
                        nextIndex = data.Length;

                    int length = nextIndex - index;

                    byte[] part = new byte[length];
                    Array.Copy(data, index, part, 0, length);

                    parts.Add(part);

                    lastIndex = nextIndex;
                }
                
                return parts;
            }

            private int FindPattern(byte[] data, byte[] pattern, int startIndex)
            {
                for (int i = startIndex; i <= data.Length - pattern.Length; i++)
                {
                    bool match = true;

                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if (data[i + j] != pattern[j])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                        return i;
                }

                return -1;
            }

            public async Task DownloadLevel(MODEL_LevelDownloaderSMM1 level, string outputPath, bool DecompressASH0)
            {
                string timestamp = await GetArchiveTimestamp(level.url);

                if (string.IsNullOrEmpty(timestamp))
                {
                    string text = LanguageManager.Get("FORM_LevelDownloader", "msgNotFound");
                    MessageBox.Show(text);
                    return;
                }

                string archiveUrl = $"https://web.archive.org/web/{timestamp}if_/{level.url}";

                string fileName = Path.GetFileName(new Uri(level.url).AbsolutePath);

                var response = await client.GetAsync(archiveUrl);
                response.EnsureSuccessStatusCode();

                //1. Download ASH0
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                //2. Save
                File.WriteAllBytes(outputPath, fileBytes);

                if (DecompressASH0)
                {
                    //3. Create a new directory with the same name without extension
                    string folderPath = Path.Combine(
                        Path.GetDirectoryName(outputPath),
                        Path.GetFileNameWithoutExtension(outputPath)
                    );

                    Directory.CreateDirectory(folderPath);

                    //4. SPLIT
                    var parts = SplitAshFile(fileBytes);

                    string[] partNamesFirst = {
                    "thumbnail0",
                    "course_data",
                    "course_data_sub",
                    "thumbnail1"
                };

                    string[] finalNames = {
                    "thumbnail0.tnl",
                    "course_data.cdt",
                    "course_data_sub.cdt",
                    "thumbnail1.tnl"
                };

                    //5. Save parts
                    for (int i = 0; i < parts.Count && i < partNamesFirst.Length; i++)
                    {
                        string path = Path.Combine(folderPath, partNamesFirst[i]);
                        File.WriteAllBytes(path, parts[i]);
                    }

                    //6. Execute ASH Extractor
                    for (int i = 0; i < partNamesFirst.Length; i++)
                    {
                        string inputPath = Path.Combine(folderPath, partNamesFirst[i]);

                        var process = new Process();
                        process.StartInfo.FileName = "ASH.exe";
                        process.StartInfo.Arguments = $"\"{inputPath}\"";
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.UseShellExecute = false;

                        process.Start();
                        process.WaitForExit();

                        string arcFile = inputPath + ".arc";

                        if (File.Exists(arcFile))
                        {
                            string finalPath = Path.Combine(folderPath, finalNames[i]);
                            File.Move(arcFile, finalPath);

                            File.Delete(inputPath);
                        }
                    }

                    //7. Create sound.bwv
                    CreateSoundbwvFile(Path.Combine(folderPath, "sound.bwv"));

                    //8. Clear status to uncleared
                    string file1 = Path.Combine(folderPath, "course_data.cdt");
                    string file2 = Path.Combine(folderPath, "course_data_sub.cdt");

                    if (!File.Exists(file1) || !File.Exists(file2))
                    {
                        string text = LanguageManager.Get("FORM_LevelDownloader", "msgCourseDataFileNotFound");
                        MessageBox.Show(text);
                        return;
                    }

                    byte[] fileBytes1 = File.ReadAllBytes(file1);
                    byte[] fileBytes2 = File.ReadAllBytes(file2);

                    //Not uploaded
                    fileBytes1[UploadedCourseOffset] = 0x00;
                    fileBytes2[UploadedCourseOffset] = 0x00;

                    //Uncleared
                    fileBytes1[ClearCheckOffset] = 0x00;
                    fileBytes2[ClearCheckOffset] = 0x00;

                    WriteChecksumCRC32(fileBytes1);
                    WriteChecksumCRC32(fileBytes2);

                    //Save and overwrites .cdt file
                    File.WriteAllBytes(file1, fileBytes1);
                    File.WriteAllBytes(file2, fileBytes2);

                    //9. Delete original ASH0
                    File.Delete(outputPath);
                }

                string caption = LanguageManager.Get("FORM_LevelDownloader", "msgLvlDownloadedSuccess");
                MessageBox.Show(caption);
            }

            //A "sound.bwv" file halways as only the first 4 bytes filled with the same thing, so I don't need a whole "sound.bwv" file to copy and paste to downloaded level
            //After writing header, this function fill with zeros skiping directly to the end
            public void CreateSoundbwvFile(string outputPath)
            {
                using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(new byte[] { 0x76, 0x24, 0x6A, 0xAE }, 0, 4);

                    //Size of "sound.bwv" is 0xD808
                    fs.SetLength(0xD808);
                }
            }
        }

        public class CONTROLLER_MiiData
        {
            private HttpClient client;
            public CONTROLLER_MiiData()
            {
                client = new HttpClient();
            }

            public async Task<string> IsAPIWorking()
            {
                HttpResponseMessage response = await client.GetAsync(API_SMM1_Mii + "jobs");

                if (response.IsSuccessStatusCode)
                {
                    string statusTrue = LanguageManager.Get("FORM_LevelDownloader", "API_StatusTrue");
                    return statusTrue;
                }

                else
                {
                    string statusFalse = LanguageManager.Get("FORM_LevelDownloader", "API_StatusFalse");
                    return statusFalse;
                }
            }

            public async Task<MODEL_MiiData> GetMiiDataFromLevel(string NNID)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, API_SMM1_Mii + "mii_data/" + NNID);

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                string responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<MODEL_MiiData>(responseJson);
            }
        }

        public async Task DownloadMiiForLevel(int levelid, string MiiBase64, string outputPath)
        {
            byte[] MiiBytes = Convert.FromBase64String(MiiBase64);

            string levelidHex = levelid.ToString("X12");
            byte[] levelidBytes = Enumerable.Range(0, levelidHex.Length).Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(levelidHex.Substring(x, 2), 16)).ToArray();

            string file1 = Path.Combine(outputPath, "course_data.cdt");
            string file2 = Path.Combine(outputPath, "course_data_sub.cdt");

            if (!File.Exists(file1) || !File.Exists(file2))
            {
                string text = LanguageManager.Get("FORM_LevelDownloader", "msgCourseDataFileNotFound");
                MessageBox.Show(text);
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(file1);
            byte[] fileBytes2 = File.ReadAllBytes(file2);

            fileBytes[CourseIDsuffixStartOffset] = levelidBytes[0];
            fileBytes[0x1B] = levelidBytes[1];
            fileBytes[0x1C] = levelidBytes[2];
            fileBytes[0x1D] = levelidBytes[3];
            fileBytes[0x1E] = levelidBytes[4];
            fileBytes[0x1F] = levelidBytes[5];

            fileBytes2[CourseIDsuffixStartOffset] = levelidBytes[0];
            fileBytes2[0x1B] = levelidBytes[1];
            fileBytes2[0x1C] = levelidBytes[2];
            fileBytes2[0x1D] = levelidBytes[3];
            fileBytes2[0x1E] = levelidBytes[4];
            fileBytes2[0x1F] = levelidBytes[5];

            //Downloaded status 01
            fileBytes[DownloadedCourseOffset] = 0x01;
            fileBytes2[DownloadedCourseOffset] = 0x01;

            //Add Mii bytes to the file
            Array.Copy(MiiBytes, 0, fileBytes, CourseMiiOffset, CourseMiiSize);
            Array.Copy(MiiBytes, 0, fileBytes2, CourseMiiOffset, CourseMiiSize);

            WriteChecksumCRC32(fileBytes);
            WriteChecksumCRC32(fileBytes2);

            //Save and overwrites .cdt file
            File.WriteAllBytes(file1, fileBytes);
            File.WriteAllBytes(file2, fileBytes2);
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

            if(DataGridView_LevelResults.Rows.Count <= 0)
            InitializeLevelResultsGrid();

            foreach (var level in levels)
            {
                DataGridView_LevelResults.Rows.Add(
                    isNBD4Profile(level.creator),
                    level.name,
                    level.stars,
                    isNBD4Profile(level.world_record_holder_nnid) + " " + ConvertMillisecondsToMinutes(level.world_record_ms),
                    (level.clearrate * 100).ToString("F4") + "%" + " " + level.clears + "/" + level.total_attempts,
                    FormatCourseID(level.levelid),
                    level.creatorid
                );
            }
        }

        public static string isNBD4Profile(string creator) //Extremely important function, one of the main reasons I made my own level downloader
        {
            if (creator == "D4Pro10") return "NobleD4";
            else return creator;
        }

        public static string ConvertMillisecondsToMinutes(string milliseconds)
        {
            if (milliseconds == null || !milliseconds.All(char.IsDigit))
            {
                return "--:--.---";
            }
            else
            {
                TimeSpan t = TimeSpan.FromMilliseconds(Convert.ToInt32(milliseconds));
                string answer = string.Format("{0:D2}:{1:D2}:{2:D3}", (t.Hours * 60) + t.Minutes, t.Seconds, t.Milliseconds);

                return answer;
            }
        }

        public static string FormatCourseID(int levelId)
        {
            string prefix = GenerateCourseIdPrefix((ulong)levelId);

            string suffixHex = levelId.ToString("X12");

            string fullHex = prefix + suffixHex;

            return $"{fullHex.Substring(0, 4)}-" + $"{fullHex.Substring(4, 4)}-" + $"{fullHex.Substring(8, 4)}-" + $"{fullHex.Substring(12, 4)}";
        }

        static string GenerateCourseIdPrefix(ulong suffix) //This needs to be a global function
        {
            byte[] baseKey = Encoding.ASCII.GetBytes(KeyAccessSMM1); //Yeah, I'm going to make key access a flobal variable and also all constants //Aug 8th 2026: Done
            using (var md5 = MD5.Create())
            {
                baseKey = md5.ComputeHash(baseKey);
            }
            byte[] data = BitConverter.GetBytes(suffix);
            byte[] checksum;
            using (var hmac = new HMACMD5(baseKey))
            {
                checksum = hmac.ComputeHash(data);
            }
            string prefix = checksum[3].ToString("X2") + checksum[2].ToString("X2");
            return prefix;
        }

        public void CopyID()
        {
            if (DataGridView_LevelResults.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridView_LevelResults.SelectedRows[0];
                string textToCopy = selectedRow.Cells["LevelID"].Value.ToString();

                Thread thread = new Thread(() => Clipboard.SetText(textToCopy));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
        }

        private async void SMM1_GetLvlsSearch(string SMM1_LvlTxtSearch, byte pageNUM, byte coursename, byte courseid, byte creatorname, byte creatorid, byte searchexact)
        {
            if (courseid == 1 && !SMM1_LvlTxtSearch.All(char.IsDigit))
            {
                SMM1_LvlTxtSearch = SMM1_LvlTxtSearch.Replace("%20", ""); //I'm using this instead of trim because gets into this function after uri escape data
                SMM1_LvlTxtSearch = SMM1_LvlTxtSearch.Replace("-", ""); //Searching works by using numbers, so it's necessary remove the "-" before converting

                if (SMM1_LvlTxtSearch.Length >= 16) //First 4 digits of a SMM1 ID doesn't matters for searching
                SMM1_LvlTxtSearch = SMM1_LvlTxtSearch.Substring(SMM1_LvlTxtSearch.Length - 12);
                
                if(!SMM1_LvlTxtSearch.All(char.IsDigit))
                SMM1_LvlTxtSearch = Convert.ToInt32(SMM1_LvlTxtSearch, 16).ToString();
            }

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
                    isNBD4Profile(level.creator),
                    level.name,
                    level.stars,
                    isNBD4Profile(level.world_record_holder_nnid) + " " + ConvertMillisecondsToMinutes(level.world_record_ms),
                    (level.clearrate * 100).ToString("F4") + "%" + " " + level.clears + "/" + level.total_attempts,
                    FormatCourseID(level.levelid),
                    level.creatorid
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
            SearchSMM1Levels();
            TB_DisplayPage.Text = SearchPageNUM.ToString();
        }

        private void BUTTON_NextPage_Click(object sender, EventArgs e)
        {
            SearchPageNUM++;
            SearchSMM1Levels();
            TB_DisplayPage.Text = SearchPageNUM.ToString();
        }

        public void SearchSMM1Levels()
        {
            InitializeLevelResultsGrid();
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

        private void InitializeLevelResultsGrid()
        {
            DataGridView_LevelResults.Columns.Clear();

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CreatorName",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsCreatorName"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LevelName",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsLevelName"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LevelStars",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsLevelStars"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "WorldRecordHolder",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsWorldRecordHolder"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ClearRate",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsClearRate"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "LevelID",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsLevelID"),
            });

            DataGridView_LevelResults.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "CreatorID",
                HeaderText = LanguageManager.Get("FORM_LevelDownloader", "DGV_LevelResultsCreatorID"),
            });
        }

        private void BUTTON_Search_Click(object sender, EventArgs e)
        {
            SearchPageNUM = 1;
            TB_DisplayPage.Text = SearchPageNUM.ToString();
            SearchSMM1Levels();
        }

        private async void BUTTON_DownloadLevel_Click(object sender, EventArgs e)
        {
            var row = DataGridView_LevelResults.CurrentRow;

            if (row == null || row.Tag == null)
            {
                string caption = LanguageManager.Get("FORM_LevelDownloader", "msgNoSelectedRow");
                MessageBox.Show(caption);
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
                //I added an extension to avoid errors at the part of the code which creates a folder with exactly the same name
                await ControllerLevelDownloaderSMM1.DownloadLevel(level, SaveFileDialog_SMM1Level.FileName + ".tmp", CHECK_DecompressASH0.Checked);

                if (CHECK_DownloadMii.Checked) {
                    var MiiData = await ControllerMiiData.GetMiiDataFromLevel(level.creator);
                    await DownloadMiiForLevel(level.levelid, MiiData.data, SaveFileDialog_SMM1Level.FileName);
                }
            }
        }

        private void BUTTON_CopyID_Click(object sender, EventArgs e)
        {
            CopyID();
        }
    }
}