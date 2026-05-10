using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM_D4TA_EDITOR
{
    public class MODEL_LevelDownloaderSMM1
    {
        public string url { get; set; }
        public string name { get; set; }
        public string creator { get; set; }
        public int levelid { get; set; }
        public int creatorid { get; set; }
        public int clears { get; set; }
        public int failures { get; set; }
        public int total_attempts { get; set; }
        public double clearrate { get; set; }
        public DateTime uploadTime { get; set; }
        public string world_record_ms { get; set; } //Sometimes the API throws "null" here, but Int32 doesn't knows what is "null"
        public string world_record_holder_nnid { get; set; }
        public int stars { get; set; }
    }
    public class ERROR_LevelDownloaderSMM1
    {
        public string error { get; set; }
    }

}
