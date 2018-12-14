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
    public partial class Endgame : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();

        public Endgame()
        {
            InitializeComponent();
            player.URL = "EndgameBackgroundmusic.mpc";
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }
        
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");
            sp.Play();
            Mainmanu f1 = new Mainmanu();
            this.Hide();
            f1.ShowDialog();
            this.Close();
            player.controls.stop();
        }
    }
}
