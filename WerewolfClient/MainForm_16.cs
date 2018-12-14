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
    public partial class MainForm_16 : Form
    {
        WindowsMediaPlayer player_day = new WindowsMediaPlayer();
        WindowsMediaPlayer player_night = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");

        public MainForm_16()
        {
            player_day.URL = "GameplayBgmusic.mp3";
            player_night.URL = "Nightplaygamemusicbg.mp3";
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //player_day.controls.play(); if it to day.
            //player_day.controls.stop(); if it switch day to night
            //player_night.controls.play(); if it to night
            //player_day.controls.stop(); if it switch night to day
        }
    }
}
