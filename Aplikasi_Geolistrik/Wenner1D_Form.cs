using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace Aplikasi_Geolistrik
{
    public partial class Wenner1D_Form : Form
    {
        string pathtofile;
        int inforadio;
        int[] selected;
        string MyDataString;
        int poz;
        public Wenner1D_Form(string ds, string qs, int ir, int[] se, int poz_int)
        {
            InitializeComponent();
            this.pathtofile = qs;
            this.inforadio = ir;
            this.selected = se;
            this.MyDataString = ds;
            this.poz = poz_int;
        }
        int L;
        double[] dis;
        double[] Rho;
        double[] a;

        private void Wenner1D_Form_Load(object sender, EventArgs e)
        {
            try {
                DataTable table = new DataTable();
                if (inforadio == 0)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("a (m)"),
                    new DataColumn("Distance (m)"),
                    new DataColumn("C1"),
                    new DataColumn("C2"),
                    new DataColumn("P1"),
                    new DataColumn("P2"),
                    new DataColumn("Z/n (m)"),
                    new DataColumn("V (mV)"),
                    new DataColumn("I (mA)"),
                    new DataColumn("R (Ω)"),
                    new DataColumn("K (m)"),
                    new DataColumn("ρa (Ω.m)")});
                    string[] MyDataLines = MyDataString.Split('\n');
                    L = MyDataLines.Length;
                    double[] data_a = new double[L];
                    double[] data_C1 = new double[L];
                    double[] data_C2 = new double[L];
                    double[] data_P1 = new double[L];
                    double[] data_P2 = new double[L];
                    double[] data_V = new double[L];
                    double[] data_I = new double[L];
                    double[] data_R = new double[L];
                    double[] data_K = new double[L];
                    double[] data_Rho = new double[L];
                    double[] data_dis = new double[L];
                    double[] data_n = new double[L];
                    double pi = 3.14;

                    for (int i = 0; i < L; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        data_a[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        data_C1[i] = Convert.ToDouble(MyDataColumns[selected[1]]);
                        data_C2[i] = Convert.ToDouble(MyDataColumns[selected[2]]);
                        data_P1[i] = Convert.ToDouble(MyDataColumns[selected[3]]);
                        data_P2[i] = Convert.ToDouble(MyDataColumns[selected[4]]);
                        data_V[i] = Convert.ToDouble(MyDataColumns[selected[5]]);
                        data_I[i] = Convert.ToDouble(MyDataColumns[selected[6]]);
                        data_R[i] = data_V[i] / data_I[i];
                        data_K[i] = 2 * pi * data_a[i];
                        data_n[i] = data_P2[i] - data_P1[i];
                        data_Rho[i] = data_R[i] * data_K[i];
                        data_dis[i] = (data_C1[i] - poz - 1) * data_a[0] + data_a[i] + (data_a[i] / 2);
                        object[] Data_Row = {data_a[i], data_dis[i], data_C1[i], data_C2[i],
                    data_P1[i], data_P2[i], data_n[i], data_V[i], data_I[i], data_R[i],
                    data_K[i], data_Rho[i]};
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    a = data_n;

                    ILArray<double> dat_dis = data_dis;
                    ILArray<double> dat_Rho = data_Rho;
                    ILArray<float> ddis = ILMath.convert<double, float>(dat_dis);
                    ILArray<float> drho = ILMath.convert<double, float>(dat_Rho);
                    ILArray<float> position = ILMath.zeros<float>(3, data_dis.Length);
                    position[0, ":"] = ddis[":"];
                    position[1, ":"] = drho[":"];

                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        YAxis =
                        {
                            Label =
                            {
                                Text = "ρa (Ω.m)"
                            }
                        },
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        }

                    },
                        Children = { new ILPoints
                    {
                        Positions = position,
                        Size = 4
                    } }

                    });

                }
                if (inforadio == 1)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("a (m)"),
                    new DataColumn("Distance (m)"),
                    new DataColumn("C1"),
                    new DataColumn("C2"),
                    new DataColumn("P1"),
                    new DataColumn("P2"),
                    new DataColumn("Z/n (m)"),
                    new DataColumn("R (Ω)"),
                    new DataColumn("K (m)"),
                    new DataColumn("ρa (Ω.m)")});
                    string[] MyDataLines = MyDataString.Split('\n');
                    L = MyDataLines.Length;
                    double[] data_a = new double[L];
                    double[] data_C1 = new double[L];
                    double[] data_C2 = new double[L];
                    double[] data_P1 = new double[L];
                    double[] data_P2 = new double[L];
                    double[] data_R = new double[L];
                    double[] data_K = new double[L];
                    double[] data_Rho = new double[L];
                    double[] data_dis = new double[L];
                    double[] data_n = new double[L];
                    double pi = 3.14;

                    for (int i = 0; i < L; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        data_a[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        data_C1[i] = Convert.ToDouble(MyDataColumns[selected[1]]);
                        data_C2[i] = Convert.ToDouble(MyDataColumns[selected[2]]);
                        data_P1[i] = Convert.ToDouble(MyDataColumns[selected[3]]);
                        data_P2[i] = Convert.ToDouble(MyDataColumns[selected[4]]);
                        data_R[i] = Convert.ToDouble(MyDataColumns[selected[5]]);
                        data_K[i] = 2 * pi * data_a[i];
                        data_Rho[i] = data_R[i] * data_K[i];
                        data_n[i] = data_P2[i] - data_P1[i];
                        data_dis[i] = (data_C1[i] - poz - 1) * data_a[0] + data_a[i] + (data_a[i] / 2);
                        object[] Data_Row = {data_a[i], data_dis[i], data_C1[i], data_C2[i],
                    data_P1[i], data_P2[i], data_n[i], data_R[i],
                    data_K[i], data_Rho[i]};
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    a = data_n;

                    ILArray<double> dat_dis = data_dis;
                    ILArray<double> dat_Rho = data_Rho;
                    ILArray<float> ddis = ILMath.convert<double, float>(dat_dis);
                    ILArray<float> drho = ILMath.convert<double, float>(dat_Rho);
                    ILArray<float> position = ILMath.zeros<float>(3, data_dis.Length);
                    position[0, ":"] = ddis[":"];
                    position[1, ":"] = drho[":"];

                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        YAxis =
                        {
                            Label =
                            {
                                Text = "ρa (Ω.m)"
                            }
                        },
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        }

                    },
                        Children = { new ILPoints
                    {
                        Positions = position,
                        Size = 4
                    } }

                    });

                }
                if (inforadio == 2)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("Distance (m)"),
                    new DataColumn("a (m)"),
                    new DataColumn("ρa (Ω.m)")});
                    string[] MyDataLines = MyDataString.Split('\n');
                    L = MyDataLines.Length;
                    double[] data_dis = new double[L];
                    double[] data_a = new double[L];
                    double[] data_Rho = new double[L];


                    for (int i = 0; i < L; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        data_a[i] = Convert.ToDouble(MyDataColumns[selected[1]]);
                        data_dis[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        data_Rho[i] = Convert.ToDouble(MyDataColumns[selected[2]]);
                        object[] Data_Row = { data_dis[i], data_a[i], data_Rho[i] };
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    a = data_a;

                    ILArray<double> dat_dis = data_dis;
                    ILArray<double> dat_Rho = data_Rho;
                    ILArray<float> ddis = ILMath.convert<double, float>(dat_dis);
                    ILArray<float> drho = ILMath.convert<double, float>(dat_Rho);
                    ILArray<float> position = ILMath.zeros<float>(3, data_dis.Length);
                    position[0, ":"] = ddis[":"];
                    position[1, ":"] = drho[":"];

                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        YAxis =
                        {
                            Label =
                            {
                                Text = "ρa (Ω.m)"
                            }
                        },
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        }

                    },
                        Children = { new ILPoints
                    {
                        Positions = position,
                        Size = 4
                    } }

                    });

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
