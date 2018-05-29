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
    public partial class VES_View : Form
    {
        string pathtofile;
        
        public VES_View(string ptf)
        {
            InitializeComponent();
            pathtofile = ptf;
            
        }

        private void VES_View_Load(object sender, EventArgs e)
        {
            try {
                string MyDataString = File.ReadAllText(pathtofile);
                richTextBox1.Text = MyDataString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
