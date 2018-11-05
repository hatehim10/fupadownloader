using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace fupaDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String url = textBox1.Text;
            int[] ids = getPicIDs(url);

            progressBar1.Visible = true;
            downloadPics(ids,"D:\\Test\\Pics2\\");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void downloadPics(int[] ids, String path)
        {
            using (WebClient client = new WebClient())
            {
                progressBar1.Maximum = ids.Length;
                for(int i = 0; i < ids.Length;i++)
                {
                    client.DownloadFile("https://www.fupa.net/fupa/images/galerie/big/" + ids[i]+ ".jpg", @path + i + ".jpg");
                    progressBar1.Increment(1);
                }
                progressBar1.Visible = false;
            }
        }

        private int[] getPicIDs(String url)
        {
            String htmlCode = "";
            int[] picIDs;

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }
            String[] split = Regex.Split(htmlCode, "<span id='bild_comment_");
            split = split.Skip(1).ToArray();
            picIDs = new int[split.Length];
            for (int i = 0; i < split.Length - 1; i++)
            {
                String[] subSplit = Regex.Split(split[i], "' style='display:none;'></span>");
                picIDs[i] = int.Parse(subSplit[0]);
            }
            String last = split[split.Length - 1].Split('\'')[0];
            picIDs[split.Length - 1] = int.Parse(last);

            return picIDs;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
