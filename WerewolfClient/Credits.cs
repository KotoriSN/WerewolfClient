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
    public partial class Credits : Form
    {
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Attachai\Source\Repos\FormWerewolf\WindowsFormsApp2\Resources\PressButton.wav");

        public Credits()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            sp.Play();
            
        }
    }
}
