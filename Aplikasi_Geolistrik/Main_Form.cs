using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Aplikasi_Geolistrik
{
    public partial class Main_Form : Form
    {
        Main_Form a;
        public Main_Form()
        {
            InitializeComponent();
            a = this;
        }
       
        //public static string pathtofile;
        private void vESSchlumbergerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                VES_Options ves1 = new VES_Options(a);
                ves1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void wennerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Wenner2D_Option wen1 = new Wenner2D_Option(a);
                wen1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void dipoleDipoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Dipole_Options dip1 = new Dipole_Options(a);
                dip1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void mappingWennerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Wenner1D_Options wen1 = new Wenner1D_Options(a);
                wen1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void Main_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            //exit application when form is closed
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                About abt = new About();
                abt.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
