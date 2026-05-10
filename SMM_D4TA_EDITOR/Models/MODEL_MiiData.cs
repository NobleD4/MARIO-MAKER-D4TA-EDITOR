using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMM_D4TA_EDITOR
{
    public class MODEL_MiiData
    {
        public string data { get; set; }
        public MODEL_MiiDataImages images { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
        public string studio_url_data { get; set; }
        public string user_id { get; set; }

    }

    public class MODEL_MiiDataImages
    {
        public DateTime last_modified { get; set; }

    }
}
