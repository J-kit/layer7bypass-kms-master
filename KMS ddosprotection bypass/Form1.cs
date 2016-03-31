using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace KMS_ddosprotection_bypass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bypasscookie = "",byco2="";
            int request1 = 0, request2 = 0;
            string site1 = get_kmsprotected("http://kms-manager.com/",ref bypasscookie,ref request1);
            byco2 = bypasscookie;
            string site2 = get_kmsprotected("http://kms-manager.com/", ref bypasscookie, ref request2);

            File.WriteAllText("r1.html", site1);
            File.WriteAllText("r2.html", site2);


            MessageBox.Show("Request1:"+byco2+" "+request1.ToString() + "\n"+
                            "Request2:" + bypasscookie + " " + request2.ToString());

        }
  

        string get_kmsprotected(string url, ref string cookie,ref int requests )
        {
            WebClient wc = new WebClient();
            if (cookie != "")
            {
                wc.Headers.Add(HttpRequestHeader.Cookie, "layer7-antiddos=" + cookie);
            }
            wc.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36");
            string creq = wc.DownloadString(url);
            requests++;

            if (creq.Contains("function|document|cookie|layer7-antiddos"))
            {
                string[] split = creq.Split(new Char[] { '|' });
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i] == "layer7-antiddos")
                    {
                        cookie = split[++i];
                        break;
                    }

                }
                wc.Headers.Add(HttpRequestHeader.Cookie, "layer7-antiddos=" + cookie);
                requests++;
                return wc.DownloadString(url);
            }
            else
            {
                return creq;
            }

        }
    }
}
