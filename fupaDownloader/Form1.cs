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

            String url = textBox2.Text;

            try
            {
                int[] ids = getPicIDs(url);
                downloadPics(ids, textBox1.Text);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        //Check for right URL
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void downloadPics(int[] ids, String path)
        {
            using (WebClient client = new WebClient())
            {
                int length = ids.Length;
                progressBar1.Visible = true;
                label2.Visible = true;
                progressBar1.Maximum = length;
                for (int i = 0; i < length; i++)
                {
                    progressBar1.Increment(1);
                    label2.Text = i + 1 + " von " + length + " heruntergeladen";
                    label2.Update();
                    label2.Focus();
                    client.DownloadFile("https://www.fupa.net/fupa/images/galerie/big/" + ids[i] + ".jpg", @path + (i + 1) + ".jpg");
                }
                MessageBox.Show(length + " Bilder erfolgreich heruntergeladen!", "Herunterladen erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                progressBar1.Visible = false;
                progressBar1.Value = 0;
                label2.Visible = false;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
