using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace C0504A
{
    public partial class Form1 : Form
    {
        PictureBox[] fire = new PictureBox[64];
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        SoundPlayer sp2 = new SoundPlayer("bgm.wav");
        SoundPlayer sp3 = new SoundPlayer("bossmusic.wav");
        SoundPlayer sp4 = new SoundPlayer("victary_vitmu.wav");
        SoundPlayer sp6 = new SoundPlayer("gameover.wav");

        int mmx, pp;
        int timerTime = 0;
        int score = 0;
        int BossHealth = 50;

        public Form1()
        {
            InitializeComponent();
            sp2.Play();
        }
        private void playexplosion() //播放撞擊音樂方法
        {
            var player1 = new WMPLib.WindowsMediaPlayer();
            player1.URL = "explosion.wav"; 
        }
        private void playshoot() //播放撞擊音樂方法
        {
            var player2 = new WMPLib.WindowsMediaPlayer();
            player2.URL = "shoot.wav";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mmx = 0; pp = 0;
            pictureBox1.Top = -5685; pictureBox1.Left = 0;
            pictureBox2.Top = 6300; pictureBox2.Left = 200;
            pictureBox3.Top = 3000; pictureBox3.Left = 300;
            pictureBox4.Dispose();
            pictureBox1.Controls.Add(this.pictureBox2);
            pictureBox1.Controls.Add(this.pictureBox3);
            pictureBox1.Controls.Add(this.pictureBox5);
            DoubleBuffered = true; timer1.Enabled = true; timer2.Enabled = true; timer3.Enabled = true; timer4.Enabled = true; timer5.Enabled = true;
            label2.Visible = false; label3.Visible = false;
            this.label1.Text = "遊戲時間：" + timerTime.ToString();
            this.label4.Text = "分數：" + score.ToString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random ran = new Random();
            int top = ran.Next(0, 5000);
            int left = ran.Next(35, 360);
            int leftmove = ran.Next(-50, 50);

            pictureBox6.Top -= 50;
            pictureBox5.Top -= 50;
            pictureBox2.Top -= 50;
            pictureBox1.Top += 50;

            if (pictureBox1.Top >= -800)
            {
                pictureBox1.Top = -5685; pictureBox1.Left = 0;
                pictureBox2.Top = 6300; pictureBox2.Left = 200;
                pictureBox3.Top = top; pictureBox3.Left = left;
                pictureBox5.Top = pictureBox2.Top - 500;
            }

            //pictureBox2.Left += mmx;

            this.label4.Text = "分數：" + score.ToString();

            pictureBox5.Left = pictureBox2.Left + leftmove;

            if(pictureBox5.Image == C0504A.Properties.Resources.fire)
            {
                score += 10;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Right) && (pictureBox2.Left <= 360 )) { pictureBox2.Left += 5; }
            if ((e.KeyCode == Keys.Left) && (pictureBox2.Left >= 35)) { pictureBox2.Left -= 5; }
            if (e.KeyCode == Keys.Up) { pictureBox2.Top -= 5; }
            if (e.KeyCode == Keys.Down) { pictureBox2.Top += 5; }
            //if (e.Modifiers.CompareTo(Keys.Left) == 0 && e.KeyCode == Keys.Up) { pictureBox2.Left -= 5; pictureBox2.Top -= 5; }
            if (e.KeyCode == Keys.Space)    {
                if (pp <= 15) {
                    Create_bullet_normal();
                    playshoot();
                }
            }
        }

        private void Create_bullet_normal()
        {
            PictureBox ss = new PictureBox();
            ss.Image = C0504A.Properties.Resources.bullet;
            ss.Size = new System.Drawing.Size(10, 10);
            ss.SizeMode = PictureBoxSizeMode.StretchImage;
            ss.BringToFront();
            ss.BackColor = System.Drawing.Color.Transparent;
            ss.Left = pictureBox2.Left + 20;
            ss.Top = pictureBox2.Top;
            fire[pp] = ss;
            pictureBox1.Controls.Add(fire[pp]);
            pp += 1;
            if (pp >= 31) { pp = 31; }

            PictureBox ss2 = new PictureBox();
            ss2.Image = C0504A.Properties.Resources.bullet;
            ss2.Size = new System.Drawing.Size(10, 10);
            ss2.SizeMode = PictureBoxSizeMode.StretchImage;
            ss2.BringToFront();
            ss2.BackColor = System.Drawing.Color.Transparent;
            ss2.Left = pictureBox2.Left + 85;
            ss2.Top = pictureBox2.Top;
            fire[pp] = ss2;
            pictureBox1.Controls.Add(fire[pp]);
            pp += 1;
            if (pp >= 31) { pp = 31; }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timerTime += 1;
            this.label1.Text = "遊戲時間："+ timerTime.ToString();
            if(score == 75)
            {
                
                pictureBox1.Image = C0504A.Properties.Resources.sence;
                label1.Image = C0504A.Properties.Resources.sence;
                label4.Image = C0504A.Properties.Resources.sence;
                pictureBox5.Visible = true;
                pictureBox5.Top = pictureBox2.Top-500;
                sp3.Play();
            }
            else if (score >= 100)
            {
                label2.Visible = true;
                sp4.Play();
                MessageBox.Show("勝利! 遊戲結束", "遊戲結束");
                this.Close();
            }
            if (pictureBox2.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                pictureBox2.Image = C0504A.Properties.Resources.fire;
                playexplosion();
                label3.Visible = true;
                sp6.Play();
                MessageBox.Show("失敗! 遊戲結束", "遊戲結束");
                this.Close();
            }
        }
    

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            score += 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pp > 0) {
                for (int i = 0; i < pp; i++)
                {
                    fire[i].Top -= 30;
                    if (pictureBox3.Bounds.IntersectsWith(fire[i].Bounds))
                    {
                        pictureBox1.Controls.Remove(fire[pp - 1]);
                        fire[pp - 1].Dispose();
                        pp -= 1;
                        score += 1;
                        pictureBox3.Image = C0504A.Properties.Resources.fire;
                        playexplosion();
                    }
                    if (fire[i].Top <= pictureBox2.Top - 700)
                    {
                        pictureBox1.Controls.Remove(fire[pp-1]);
                        fire[pp-1].Dispose();
                        pp -= 1;
                    }
                    if (pictureBox5.Bounds.IntersectsWith(fire[i].Bounds))
                    {
                        pictureBox1.Controls.Remove(fire[pp - 1]);
                        fire[pp - 1].Dispose();
                        pp -= 1;
                        BossHealth -= 1;
                        pictureBox5.Image = C0504A.Properties.Resources.bossDamaged;
                        playexplosion();
                        pictureBox5.Image = C0504A.Properties.Resources.boss;
                        if (BossHealth < 1)
                        {
                            score += 50;
                            pictureBox5.Image = C0504A.Properties.Resources.fire;
                            playexplosion();
                        }
                    }
                }
            }
        }
    }
}
