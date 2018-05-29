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
    public partial class VES_Options : Form
    {
        Main_Form main1;
        public VES_Options(Main_Form main)
        {
            InitializeComponent();
            main1 = main;
        }
        int inforadio;
        int[] selected = new int[4];
        string pathtofile;
        string MyDataString;
        private void VES_Options_Load(object sender, EventArgs e)
        {
            try {
                pathtofile = "";
                OpenFileDialog TheDialog = new OpenFileDialog();
                TheDialog.Title = "Open Text File";
                TheDialog.Filter = "Text Files (*.txt)|*.txt|CSV files (*.csv)|*.csv|Dat files (*.dat)|*.dat|All files (*.*)|*.*";
                TheDialog.InitialDirectory = @"C:\";

                if (TheDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(TheDialog.FileName.ToString());
                    pathtofile = TheDialog.FileName;
                }
                if (File.Exists(pathtofile))
                {
                    StreamReader Data = new StreamReader(pathtofile);
                    MyDataString = Data.ReadToEnd();
                    Data.Close();
                    string[] MyDataLines = MyDataString.Split('\n');
                    string[] MyDataColumns = MyDataLines[0].Split('\t');
                    int Col_length = MyDataColumns.Length;
                    object[] range = new object[Col_length];
                    for (int i = 0; i < Col_length; i++)
                    {
                        range[i] = i + 1;
                    }
                    comboBox1.Items.AddRange(range);
                    comboBox2.Items.AddRange(range);
                    comboBox3.Items.AddRange(range);
                    comboBox4.Items.AddRange(range);
                    comboBox5.Items.AddRange(range);
                    comboBox6.Items.AddRange(range);

                    if (radioButton1.Checked == true)
                    {
                        label1.Enabled = true;
                        label2.Enabled = true;
                        label3.Enabled = true;
                        label4.Enabled = true;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        comboBox3.Enabled = true;
                        comboBox4.Enabled = true;
                    }
                    else
                    {
                        label1.Enabled = false;
                        label2.Enabled = false;
                        label3.Enabled = false;
                        label4.Enabled = false;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox3.Enabled = false;
                        comboBox4.Enabled = false;
                    }
                    if (radioButton2.Checked == true)
                    {
                        label5.Enabled = true;
                        label6.Enabled = true;
                        comboBox5.Enabled = true;
                        comboBox6.Enabled = true;
                    }
                    else
                    {
                        label5.Enabled = false;
                        label6.Enabled = false;
                        comboBox5.Enabled = false;
                        comboBox6.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    label4.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                }
                else
                {
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    label4.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try {
                if (radioButton2.Checked == true)
                {
                    label5.Enabled = true;
                    label6.Enabled = true;
                    comboBox5.Enabled = true;
                    comboBox6.Enabled = true;
                }
                else
                {
                    label5.Enabled = false;
                    label6.Enabled = false;
                    comboBox5.Enabled = false;
                    comboBox6.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (radioButton1.Checked == true)
                {
                    inforadio = 0;
                    //int[] information = {0,1,2,3};
                    int[] information = {comboBox1.SelectedIndex, comboBox2.SelectedIndex, comboBox3.SelectedIndex,
                comboBox4.SelectedIndex};
                    for (int i = 0; i < information.Length; i++)
                    {

                        selected[i] = information[i];

                    }
                }
                if (radioButton2.Checked == true)
                {
                    inforadio = 1;
                    int[] information = { comboBox5.SelectedIndex, comboBox6.SelectedIndex, 0, 0 };
                    selected = information;
                }
                VES_Form ves2 = new VES_Form(MyDataString, pathtofile, inforadio, selected);
                ves2.MdiParent = main1;
                ves2.WindowState = FormWindowState.Maximized;
                ves2.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                VES_View ves3 = new VES_View(pathtofile);
                ves3.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
