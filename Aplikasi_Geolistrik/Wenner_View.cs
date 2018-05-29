using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Aplikasi_Geolistrik
{
    public partial class Wenner_View : Form
    {
        double[] dis;
        double[] Rho;
        double[] a;
        public Wenner_View(double[] d, double[] r, double[] aa)
        {
            InitializeComponent();
            dis = d;
            Rho = r;
            a = aa;
        }

        private void Wenner_View_Load(object sender, EventArgs e)
        {
            try {
                chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
                chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
                chart1.ChartAreas[0].CursorY.Interval = 1e-10;//zoom resolution threshold
                                                              //chart1.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
                chart1.ChartAreas[0].AxisY.MinorGrid.Interval = 1;
                chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisY.IsReversed = true;
                chart1.ChartAreas[0].AxisX.Title = "Distance (m)";
                chart1.ChartAreas[0].AxisY.Title = "n";
                //chart1.ChartAreas[0].AxisY.IsLogarithmic = true;
                //chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart1.Series[0].IsVisibleInLegend = false;
                chart1.Series.Add("Datum View");
                chart1.Series["Datum View"].ChartType = SeriesChartType.Point;
                chart1.Series["Datum View"].Color = Color.Red;
                for (int i = 0; i < dis.Length; i++)
                {
                    chart1.Series["Datum View"].Points.AddXY(dis[i], a[i]);
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
