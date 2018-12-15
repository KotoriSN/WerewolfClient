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
    public partial class Login_WF : Form, View
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Kotori\Source\Repos\WerewolfClient\WerewolfClient\Resources\PressButton.wav");
        private WerewolfController controller;
        private Form _mainForm;

        public Login_WF(Mainmanu mainForm)
        {
            player.URL = "MainMenuBgmusic.mp3";
            InitializeComponent();
            _mainForm = mainForm;
        }

        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case WerewolfModel.EventEnum.SignIn:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            _mainForm.Visible = true;
                            this.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case WerewolfModel.EventEnum.SignUp:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            MessageBox.Show("Sign up successfuly, please login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
            }
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
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

            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignIn;
            wcmd.Payloads = new Dictionary<string, string>() { { "Login", textBox2.Text }, { "Password", textBox3.Text }, { "Server", textBox1.Text } };
            controller.ActionPerformed(wcmd);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            sp.Play();

            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignUp;
            wcmd.Payloads = new Dictionary<string, string>() { { "Login", textBox2.Text }, { "Password", textBox3.Text }, { "Server", textBox1.Text } };
            controller.ActionPerformed(wcmd);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            player.controls.play();
        }
    }
}
