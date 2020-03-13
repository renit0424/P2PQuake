using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PQuake
{
    public partial class P2PQuake : Form
    {
        public P2PQuake()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string json;
            using (var wc = new System.Net.WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                json = wc.DownloadString("http://api.p2pquake.net/v1/human-readable");
            }
            List<EQData5610> eqds = JsonConvert.DeserializeObject<List<EQData5610>>(json);
            eqds.RemoveAll(v => v.code != 5610);

            if (json.Contains(""))
            {
                var times = DateTime.Parse(eqds[0].time);
                string retime = times.ToString("HH時mm分ss秒");
                label2.Text = $"開始：{retime}";
                label3.Text = $"感知数：{eqds[0].count.ToString()}件";
            }
        }
        class EQData5610
        {
            public string time { get; set; }
            public int code { get; set; }
            public int count { get; set; }

            public Dictionary<string, int> regions { get; set; }
            public Dictionary<string, int> prefs { get; set; }
            public Dictionary<string, int> areas { get; set; }
        }
    }
}