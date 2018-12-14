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

namespace WindowsFormsApp2
{
    public partial class Mainmanu : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");
        
        
        public Mainmanu()
        {
            
            InitializeComponent();
            player.URL = "MainMenuBgmusic.mp3";
        }

        HowToPlay f2 = new HowToPlay();
        Credits f3 = new Credits();

        private void Form1_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }

        private void pictureBox3_Click(object sender, EventArgs e) 
        {
            sp.Play();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Close();
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Hide();
            f2.ShowDialog();
            this.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Hide();
            f3.ShowDialog();
            this.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sp.Play();
          
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            sp.Play();
            player.controls.pause();
            pictureBox9.Visible = false;
            pictureBox12.Visible = true;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            sp.Play();
            player.controls.play();
            pictureBox12.Visible = false;
            pictureBox9.Visible = true;

        }
    }
}
