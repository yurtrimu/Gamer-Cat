using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            string settings = @"settings.txt";
            if (Convert.ToBoolean(File.ReadAllLines(settings).Where(x => x.Contains("HideAfterStart")).ToArray()[0].Split('"')[1].Trim()))
            {
                Hide();
            }
            f1.TopMost = Convert.ToBoolean(File.ReadAllLines(settings).Where(x => x.Contains("TopMost")).ToArray()[0].Split('"')[1].Trim());
            f1.active = Convert.ToBoolean(File.ReadAllLines(settings).Where(x => x.Contains("Active")).ToArray()[0].Split('"')[1].Trim());
            f1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string settings = @"settings.txt";
            try
            {
                Text = File.ReadAllLines(settings).Where(x => x.Contains("FormName")).ToArray()[0].Split('"')[1].Trim();
            } catch
            {
                StreamWriter sw = new StreamWriter(File.Create("settings.txt"));
                sw.Write(client.DownloadString("https://raw.githubusercontent.com/yurtrimu/Gamer-Cat/main/settings.txt"));
                sw.Close();
                Text = File.ReadAllLines(settings).Where(x => x.Contains("FormName")).ToArray()[0].Split('"')[1].Trim();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/yurtrimu");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://youtu.be/3qIBpbDvjlU");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
