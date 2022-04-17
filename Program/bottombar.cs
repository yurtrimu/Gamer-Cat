using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public partial class exit_button : Form
    {
        public exit_button()
        {
            InitializeComponent();
        }

        public Form1 f1 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            f1.Close();
            Form2 f2 = new Form2();
            Hide();
            f2.ShowDialog();
        }

        private void exit_button_Load(object sender, EventArgs e)
        {
            if (!Convert.ToBoolean(File.ReadAllLines(@"settings.txt").Where(x => x.Contains("BottomTabTopMost")).ToArray()[0].Split('"')[1].Trim()))
            {
                TopMost = false;
            }
            if (Convert.ToBoolean(File.ReadAllLines(@"settings.txt").Where(x => x.Contains("BottomTabBackgroundTransparent")).ToArray()[0].Split('"')[1].Trim()))
            {
                TransparencyKey = Color.Gainsboro;
            }
            if (Convert.ToBoolean(File.ReadAllLines(@"settings.txt").Where(x => x.Contains("Active")).ToArray()[0].Split('"')[1].Trim()))
            {
                checkBox1.Checked = true;
            }
            Opacity = Convert.ToDouble(File.ReadAllLines(@"settings.txt").Where(x => x.Contains("BottomTabFormOpacity")).ToArray()[0].Split('"')[1].Trim()) / 100;
            Form f1 = Application.OpenForms[Application.OpenForms.Count - 2];
            CheckForIllegalCrossThreadCalls = false;
            new Thread(() =>
            {
                while (true)
                {
                    Location = new Point(f1.Location.X, f1.Location.Y + 249);
                }
            }).Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                f1.active = true;
            } else
            {
                f1.active = false;
            }
        }
    }
}
