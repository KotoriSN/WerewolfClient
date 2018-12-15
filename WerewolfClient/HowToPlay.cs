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


namespace WerewolfClient
{
    public partial class HowToPlay : Form
    {
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Kotori\Source\Repos\WerewolfClient\WerewolfClient\Resources\PressButton.wav");
        
        public HowToPlay()
        {
            InitializeComponent();
        }



        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            sp.Play();
            this.Close();
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sp.Play();
            System.Diagnostics.Process.Start("https://werewolf.chat/Roles");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sp.Play();
        }
    }
}
