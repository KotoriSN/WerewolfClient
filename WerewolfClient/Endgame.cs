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

namespace WerewolfClient
{
    public partial class Endgame : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private Form mainmanu;

        public Endgame(Mainmanu mainmanu)
        {
            this.mainmanu = mainmanu;
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
            SoundPlayer sp = new SoundPlayer(@"C:\Users\Kotori\Source\Repos\WerewolfClient\WerewolfClient\Resources\PressButton.wav");
            sp.Play();
            mainmanu.Visible = true;
            this.Visible = false;
            player.controls.stop();
        }
    }
}
