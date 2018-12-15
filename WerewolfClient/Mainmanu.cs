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
using CommandEnum = WerewolfClient.WerewolfCommand.CommandEnum;
using EventEnum = WerewolfClient.WerewolfModel.EventEnum;

namespace WerewolfClient
{
    public partial class Mainmanu : Form, View
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Kotori\Source\Repos\WerewolfClient\WerewolfClient\Resources\PressButton.wav");
        private Form _login;
        private Form game;
        private WerewolfController controller;


        public Mainmanu()
        {
            
            InitializeComponent();
            player.URL = "MainMenuBgmusic.mp3";
        }

        public void setGame(MainForm_16 game)
        {
            this.game = game;
        }

        public void setLogin(Login_WF _login)
        {
            this._login = _login;
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case EventEnum.JoinGame:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            game.Visible = true;
                            this.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("You can't join the game, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case EventEnum.SignOut:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            _login.Visible = true;
                            this.Visible = false;
                        }
                        break;
                }
                // need to reset event
                wm.Event = EventEnum.NOP;
            }
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
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.JoinGame;
            controller.ActionPerformed(wcmd);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sp.Play();
            this.Close();
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            sp.Play();
            System.Diagnostics.Process.Start("https://werewolf.chat/Roles");
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
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignOut;
            wcmd.Payloads = new Dictionary<string, string>();
            controller.ActionPerformed(wcmd);
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
