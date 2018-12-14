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
    public partial class Login_WF : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");
        Mainmanu f1 = new Mainmanu();

        public Login_WF()
        {
            player.URL = "MainMenuBgmusic.mp3";
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            sp.Play();
            player.controls.play();
            pictureBox12.Visible = false;
            pictureBox9.Visible = true;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            sp.Play();
            player.controls.pause();
            pictureBox9.Visible = false;
            pictureBox12.Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Hide();
            f1.ShowDialog();
            this.Close();


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }
    }
}
