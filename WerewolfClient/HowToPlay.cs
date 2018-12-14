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


namespace WindowsFormsApp2
{
    public partial class HowToPlay : Form
    {
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");
        
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
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sp.Play();
        }
    }
}
