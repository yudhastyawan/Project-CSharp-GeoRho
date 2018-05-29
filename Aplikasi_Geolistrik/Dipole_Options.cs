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
    public partial class Dipole_Options : Form
    {
        Main_Form main1;
        public Dipole_Options(Main_Form main)
        {
            InitializeComponent();
            main1 = main;
        }
        int inforadio;
        int[] selected;
        string pathtofile;
        string MyDataString;

        private void Dipole_Options_Load(object sender, EventArgs e)
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
                    comboBox7.Items.AddRange(range);
                    comboBox8.Items.AddRange(range);
                    comboBox9.Items.AddRange(range);
                    comboBox10.Items.AddRange(range);
                    comboBox11.Items.AddRange(range);
                    comboBox12.Items.AddRange(range);
                    comboBox13.Items.AddRange(range);
                    comboBox14.Items.AddRange(range);
                    comboBox15.Items.AddRange(range);
                    comboBox16.Items.AddRange(range);

                    if (radioButton1.Checked == true)
                    {
                        label1.Enabled = true;
                        label2.Enabled = true;
                        label3.Enabled = true;
                        label4.Enabled = true;
                        label5.Enabled = true;
                        label6.Enabled = true;
                        label7.Enabled = true;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        comboBox3.Enabled = true;
                        comboBox4.Enabled = true;
                        comboBox5.Enabled = true;
                        comboBox6.Enabled = true;
                        comboBox7.Enabled = true;
                    }
                    else
                    {
                        label1.Enabled = false;
                        label2.Enabled = false;
                        label3.Enabled = false;
                        label4.Enabled = false;
                        label5.Enabled = false;
                        label6.Enabled = false;
                        label7.Enabled = false;
                        comboBox1.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox3.Enabled = false;
                        comboBox4.Enabled = false;
                        comboBox5.Enabled = false;
                        comboBox6.Enabled = false;
                        comboBox7.Enabled = false;
                    }
                    if (radioButton3.Checked == true)
                    {
                        label9.Enabled = true;
                        label10.Enabled = true;
                        label11.Enabled = true;
                        label12.Enabled = true;
                        label13.Enabled = true;
                        label14.Enabled = true;
                        comboBox9.Enabled = true;
                        comboBox10.Enabled = true;
                        comboBox11.Enabled = true;
                        comboBox12.Enabled = true;
                        comboBox13.Enabled = true;
                        comboBox14.Enabled = true;
                    }
                    else
                    {
                        label9.Enabled = false;
                        label10.Enabled = false;
                        label11.Enabled = false;
                        label12.Enabled = false;
                        label13.Enabled = false;
                        label14.Enabled = false;
                        comboBox9.Enabled = false;
                        comboBox10.Enabled = false;
                        comboBox11.Enabled = false;
                        comboBox12.Enabled = false;
                        comboBox13.Enabled = false;
                        comboBox14.Enabled = false;
                    }
                    if (radioButton2.Checked == true)
                    {
                        label8.Enabled = true;
                        label15.Enabled = true;
                        label16.Enabled = true;
                        comboBox8.Enabled = true;
                        comboBox15.Enabled = true;
                        comboBox16.Enabled = true;
                    }
                    else
                    {
                        label8.Enabled = false;
                        label15.Enabled = false;
                        label16.Enabled = false;
                        comboBox8.Enabled = false;
                        comboBox15.Enabled = false;
                        comboBox16.Enabled = false;
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
            try {
                if (radioButton1.Checked == true)
                {
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    label4.Enabled = true;
                    label5.Enabled = true;
                    label6.Enabled = true;
                    label7.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    comboBox5.Enabled = true;
                    comboBox6.Enabled = true;
                    comboBox7.Enabled = true;
                }
                else
                {
                    label1.Enabled = false;
                    label2.Enabled = false;
                    label3.Enabled = false;
                    label4.Enabled = false;
                    label5.Enabled = false;
                    label6.Enabled = false;
                    label7.Enabled = false;
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;
                    comboBox6.Enabled = false;
                    comboBox7.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try {
                if (radioButton3.Checked == true)
                {
                    label9.Enabled = true;
                    label10.Enabled = true;
                    label11.Enabled = true;
                    label12.Enabled = true;
                    label13.Enabled = true;
                    label14.Enabled = true;
                    comboBox9.Enabled = true;
                    comboBox10.Enabled = true;
                    comboBox11.Enabled = true;
                    comboBox12.Enabled = true;
                    comboBox13.Enabled = true;
                    comboBox14.Enabled = true;
                }
                else
                {
                    label9.Enabled = false;
                    label10.Enabled = false;
                    label11.Enabled = false;
                    label12.Enabled = false;
                    label13.Enabled = false;
                    label14.Enabled = false;
                    comboBox9.Enabled = false;
                    comboBox10.Enabled = false;
                    comboBox11.Enabled = false;
                    comboBox12.Enabled = false;
                    comboBox13.Enabled = false;
                    comboBox14.Enabled = false;
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
                    label8.Enabled = true;
                    label15.Enabled = true;
                    label16.Enabled = true;
                    comboBox8.Enabled = true;
                    comboBox15.Enabled = true;
                    comboBox16.Enabled = true;
                }
                else
                {
                    label8.Enabled = false;
                    label15.Enabled = false;
                    label16.Enabled = false;
                    comboBox8.Enabled = false;
                    comboBox15.Enabled = false;
                    comboBox16.Enabled = false;
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                if (radioButton1.Checked == true)
                {
                    inforadio = 0;
                    int[] info = {comboBox1.SelectedIndex,
                comboBox2.SelectedIndex,
                comboBox3.SelectedIndex,
                comboBox4.SelectedIndex,
                comboBox5.SelectedIndex,
                comboBox6.SelectedIndex,
                comboBox7.SelectedIndex};
                    selected = info;
                }
                if (radioButton3.Checked == true)
                {
                    inforadio = 1;
                    int[] info = {comboBox14.SelectedIndex,
                comboBox13.SelectedIndex,
                comboBox12.SelectedIndex,
                comboBox11.SelectedIndex,
                comboBox10.SelectedIndex,
                comboBox9.SelectedIndex};
                    selected = info;
                }
                if (radioButton2.Checked == true)
                {
                    inforadio = 2;
                    int[] info = {comboBox16.SelectedIndex,
                comboBox15.SelectedIndex,
                comboBox8.SelectedIndex};
                    selected = info;
                }
                string poz = textBox1.Text;
                int poz_int = Convert.ToInt32(poz);
                Dipole_Imaging dip1 = new Dipole_Imaging(MyDataString, pathtofile, inforadio, selected, poz_int);
                dip1.MdiParent = main1;
                dip1.WindowState = FormWindowState.Maximized;
                dip1.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
