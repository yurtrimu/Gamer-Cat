using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace Program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);

        public bool active = false;
        public void Form1_Load(object sender, EventArgs e)
        {
            checkFiles();
            placeImages();
            if (Convert.ToBoolean(File.ReadAllLines(@"settings.txt").Where(x => x.Contains("BottomTab")).ToArray()[0].Split('"')[1].Trim()))
            {
                exit_button bb = new exit_button();
                bb.f1 = this;
                new Thread(() => { bb.ShowDialog(); }).Start();
            }
        }
        public void startListen(Bitmap bmp, Graphics graphics, Bitmap tableImage, Bitmap catImage, Bitmap equipmentsImage, Bitmap backgroundImage, Bitmap openhandlImage, Bitmap closedhandlImage, Bitmap openhandrImage, Bitmap closedhandrImage)
        {
            while (true)
            {
                if (active)
                {
                    bmp = new Bitmap(350, 250);
                    graphics = Graphics.FromImage(bmp);
                    bool keyboardPressed = true;
                    for (int i = 32; i < 127; i++)
                    {
                        if (GetAsyncKeyState(i) != 0)
                        {
                            keyboardPressed = true;
                            break;
                        }
                        else
                        {
                            keyboardPressed = false;
                        }
                    }

                    bool leftClicked = true;
                    if (GetAsyncKeyState(0x01) != 0 | GetAsyncKeyState(0x02) != 0)
                    {
                        leftClicked = true;
                    }
                    else
                    {
                        leftClicked = false;
                    }


                    graphics.DrawImage(backgroundImage, Point.Empty);
                    graphics.DrawImage(catImage, Point.Empty);
                    graphics.DrawImage(tableImage, Point.Empty);
                    graphics.DrawImage(equipmentsImage, Point.Empty);
                    if (leftClicked)
                    {
                        graphics.DrawImage(closedhandlImage, Point.Empty);
                    }
                    else
                    {
                        graphics.DrawImage(openhandlImage, Point.Empty);
                    }

                    if (keyboardPressed)
                    {
                        graphics.DrawImage(closedhandrImage, Point.Empty);
                    }
                    else
                    {
                        graphics.DrawImage(openhandrImage, Point.Empty);
                    }
                    pictureBox1.Image = bmp;
                    Thread.Sleep(50);
                }
            }
        }

        public void placeImages()
        {
            Bitmap bmp = new Bitmap(350, 250);
            Bitmap tableImage = new Bitmap(@"PATHS\table.png");
            Bitmap catImage = new Bitmap(@"PATHS\cat.png");
            Bitmap equipmentsImage = new Bitmap(@"PATHS\equipments.png");
            Bitmap backgroundImage = new Bitmap(@"PATHS\background.png");
            Bitmap openhandlImage = new Bitmap(@"PATHS\openhandl.png");
            Bitmap closedhandlImage = new Bitmap(@"PATHS\closedhandl.png");
            Bitmap openhandrImage = new Bitmap(@"PATHS\openhandr.png");
            Bitmap closedhandrImage = new Bitmap(@"PATHS\closedhandr.png");
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawImage(backgroundImage, Point.Empty);
            graphics.DrawImage(catImage, Point.Empty);
            graphics.DrawImage(tableImage, Point.Empty);
            graphics.DrawImage(equipmentsImage, Point.Empty);
            graphics.DrawImage(openhandlImage, Point.Empty);
            graphics.DrawImage(openhandrImage, Point.Empty);
            pictureBox1.Image = bmp;

            new Thread(() => startListen(bmp, graphics, tableImage, catImage, equipmentsImage, backgroundImage, openhandlImage, closedhandlImage, openhandrImage, closedhandrImage)).Start();
        }
        public void checkFiles()
        {
            WebClient client = new WebClient();
            if (!Directory.Exists(@"PATHS"))
            {
                Directory.CreateDirectory(@"PATHS");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/cat.png", @"PATHS\cat.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/table.png", @"PATHS\table.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/background.png", @"PATHS\background.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/openhandr.png", @"PATHS\openhandr.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/openhandl.png", @"PATHS\openhandl.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/closedhandr.png", @"PATHS\closedhandr.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/closedhandl.png", @"PATHS\closedhandl.png");
                client.DownloadFile("https://github.com/yurtrimu/Gamer-Cat/raw/main/PATHS/equipments.png", @"PATHS\equipments.png");
            }
        }

        bool move;
        Point lastloc;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            lastloc = e.Location;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            move = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!move) return;
            Location = new Point(
                (Location.X - lastloc.X) + e.X, (Location.Y - lastloc.Y) + e.Y);

            Update();
        }
    }
}
