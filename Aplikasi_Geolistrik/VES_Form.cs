using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Aplikasi_Geolistrik
{
    public partial class VES_Form : Form
    {
        string pathtofile;
        int inforadio;
        int[] selected;
        string MyDataString;
        public VES_Form(string ds, string qs, int ir, int[] se)
        {
            InitializeComponent();
            this.pathtofile = qs;
            this.inforadio = ir;
            this.selected = se;
            this.MyDataString = ds;
        }
        //public static int inforadio = VES_Options.inforadio;
        
        //public static int[] selected = VES_Options.selected;
        int L;
        private void VES_Form_Load(object sender, EventArgs e)
        {
            try {
                DataTable table_ves = new DataTable();
                table_ves.Columns.AddRange(new DataColumn[] { new DataColumn("MN/2 (m)"), new DataColumn("AB/2 (m)"),
                new DataColumn("K (m)"),new DataColumn("I (mA)"),new DataColumn("V (mV)"),new DataColumn("ρa (Ω.m)")});
                if (inforadio == 0)
                {

                    //StreamReader Data = new StreamReader(pathtofile);
                    //string MyDataString = Data.ReadToEnd();
                    //Data.Close();
                    string[] MyDataLines = MyDataString.Split('\n');
                    double[] Data_AB = new double[MyDataLines.Length];
                    double[] Data_MN = new double[MyDataLines.Length];
                    double[] Data_delV = new double[MyDataLines.Length];
                    double[] Data_I = new double[MyDataLines.Length];
                    double[] Data_K = new double[MyDataLines.Length];
                    double[] Data_Rho = new double[MyDataLines.Length];
                    double pi = 3.14;
                    L = MyDataLines.Length;

                    for (int i = 0; i < MyDataLines.Length; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        Data_AB[i] = Convert.ToDouble(MyDataColumns[selected[1]]);
                        Data_MN[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        Data_delV[i] = Convert.ToDouble(MyDataColumns[selected[3]]);
                        Data_I[i] = Convert.ToDouble(MyDataColumns[selected[2]]);
                        Data_K[i] = pi * (Data_AB[i] * Data_AB[i] - Data_MN[i] * Data_MN[i]) / (2 * Data_MN[i]);
                        Data_Rho[i] = Data_K[i] * Data_delV[i] / Data_I[i];

                    }
                    for (int j = 0; j < MyDataLines.Length; j++)
                    {
                        object[] Data_Row = { Data_MN[j], Data_AB[j], Data_K[j], Data_I[j], Data_delV[j], Data_Rho[j] };
                        table_ves.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table_ves;
                    chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                    chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                    chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                    chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
                    chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
                    chart1.ChartAreas[0].CursorY.Interval = 1e-10;//zoom resolution threshold
                    chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
                    chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 1;
                    chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                    chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                    chart1.ChartAreas[0].AxisX.Title = "AB/2";
                    chart1.ChartAreas[0].AxisY.Title = "Rho App";
                    chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
                    chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                    chart1.Series[0].IsVisibleInLegend = false;
                    chart1.Series.Add("VES Data");
                    chart1.Series["VES Data"].ChartType = SeriesChartType.Point;
                    chart1.Series["VES Data"].Color = Color.Red;
                    for (int i = 0; i < L; i++)
                    {
                        chart1.Series["VES Data"].Points.AddXY(Data_AB[i], Data_Rho[i]);
                    }
                }
                if (inforadio == 1)
                {

                    StreamReader Data = new StreamReader(pathtofile);
                    string MyDataString = Data.ReadToEnd();
                    Data.Close();
                    string[] MyDataLines = MyDataString.Split('\n');
                    double[] Data_AB = new double[MyDataLines.Length];
                    double[] Data_Rho = new double[MyDataLines.Length];
                    L = MyDataLines.Length;
                    for (int i = 0; i < MyDataLines.Length; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        Data_AB[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        Data_Rho[i] = Convert.ToDouble(MyDataColumns[selected[1]]);

                    }
                    for (int j = 0; j < MyDataLines.Length; j++)
                    {
                        object[] Data_Row = { "-", Data_AB[j], "-", "-", "-", Data_Rho[j] };
                        table_ves.Rows.Add(Data_Row);

                    }
                    dataGridView1.DataSource = table_ves;
                    chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                    chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                    chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                    chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
                    chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
                    chart1.ChartAreas[0].CursorY.Interval = 1e-10;//zoom resolution threshold
                    chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
                    chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 1;
                    chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                    chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                    chart1.ChartAreas[0].AxisX.Title = "AB/2";
                    chart1.ChartAreas[0].AxisY.Title = "Rho App";
                    chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
                    chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                    chart1.Series[0].IsVisibleInLegend = false;
                    chart1.Series.Add("VES Data");
                    chart1.Series["VES Data"].ChartType = SeriesChartType.Point;
                    chart1.Series["VES Data"].Color = Color.Red;
                    for (int i = 0; i < L; i++)
                    {
                        chart1.Series["VES Data"].Points.AddXY(Data_AB[i], Data_Rho[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
