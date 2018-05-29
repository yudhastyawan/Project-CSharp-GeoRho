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
    public partial class Dipole_Imaging : Form
    {
        string pathtofile;
        int inforadio;
        int[] selected;
        string MyDataString;
        int poz;
        public Dipole_Imaging(string ds, string qs, int ir, int[] se, int poz_int)
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
        double[] n;
        ILArray<float> matrixgen;
        ILArray<float> Xgen;
        ILArray<float> Ygen;

        private void Dipole_Imaging_Load(object sender, EventArgs e)
        {
            try {
                DataTable table = new DataTable();
                if (inforadio == 0)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("a (m)"),
                    new DataColumn("C1"),
                    new DataColumn("C2"),
                    new DataColumn("P1"),
                    new DataColumn("P2"),
                    new DataColumn("X (m)"),
                    new DataColumn("Z/n (m)"),
                    new DataColumn("V (mV)"),
                    new DataColumn("I (mA)"),
                    new DataColumn("R (Ω)"),
                    new DataColumn("K (m)"),
                    new DataColumn("ρa (Ω.m)"),
                    new DataColumn("Za (m)")});
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
                    double[] data_Z = new double[L];
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
                        data_n[i] = data_P1[i] - data_C2[i];
                        data_K[i] = data_n[i] * data_a[i] * pi * (data_n[i] + 2) * (data_n[i] + 1);
                        data_Rho[i] = data_R[i] * data_K[i];
                        data_dis[i] = (data_P1[i] + data_C2[i]) * data_a[i] / 2;
                        data_Z[i] = data_n[i] * data_a[i] / 3;
                        object[] Data_Row = {data_a[i], data_C1[i], data_C2[i],
                    data_P1[i], data_P2[i], data_dis[i], data_n[i], data_V[i], data_I[i], data_R[i],
                    data_K[i], data_Rho[i], data_Z[i]};
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    n = data_n;

                    double[] X = data_dis;
                    double[] Y = data_n;
                    double[] Z = data_Rho;
                    /*
                    double[] Z = new double[data_Rho.Length];
                    for (int i = 0; i < data_Rho.Length; i++)
                    {
                        Z[i] = Math.Log10(data_Rho[i]);
                    } 
                    */
                    double minX = X.Min();
                    double maxX = X.Max();
                    double minY = Y.Min();
                    double maxY = Y.Max();
                    double intX = 1;
                    double intY = 0.1;
                    int LX = Convert.ToInt32((maxX - minX) / intX);
                    int LY = Convert.ToInt32((maxY - minY) / intY);
                    double[] intpX = new double[LX];
                    double[] intpY = new double[LY];
                    for (int i = 0; i < LX; i++)
                    {
                        if (i == 0)
                        {
                            intpX[i] = minX;
                        }
                        else
                        {
                            intpX[i] = intpX[i - 1] + intX;
                        }
                    }
                    for (int i = 0; i < LY; i++)
                    {
                        if (i == 0)
                        {
                            intpY[i] = minY;
                        }
                        else
                        {
                            intpY[i] = intpY[i - 1] + intY;
                        }
                    }
                    // IDW
                    int nX = intpX.Length;
                    int nY = intpY.Length;
                    int N = nX * nY;
                    double[] intpZ = new double[N];
                    double[] susX = new double[N];
                    double[] susY = new double[N];
                    for (int i = 0; i < nY; i++)
                    {
                        for (int j = 0; j < nX; j++)
                        {
                            susX[j + (nX * (i))] = intpX[j];
                            susY[j + (nX * (i))] = intpY[i];
                        }
                    }
                    double k = 2;
                    for (int i = 0; i < N; i++)
                    {
                        double[] d_obs = new double[X.Length];
                        double[] d_ok = new double[X.Length];
                        double[] spd_ok = new double[X.Length];
                        double[] Zspd_ok = new double[X.Length];
                        double[] s_spd_ok = new double[X.Length];
                        double[] s_Zspd_ok = new double[X.Length];
                        for (int j = 0; j < X.Length; j++)
                        {
                            d_obs[j] = Math.Sqrt(Math.Pow(X[j] - susX[i], 2) + Math.Pow(Y[j] - susY[i], 2));
                            d_ok[j] = Math.Pow(d_obs[j], k);
                            spd_ok[j] = 1 / (d_ok[j] + 0.00001);
                            Zspd_ok[j] = Z[j] * spd_ok[j];
                            if (j == 0)
                            {
                                s_spd_ok[j] = spd_ok[j];
                                s_Zspd_ok[j] = Zspd_ok[j];
                            }
                            else
                            {
                                s_spd_ok[j] = s_spd_ok[j - 1] + spd_ok[j];
                                s_Zspd_ok[j] = s_Zspd_ok[j - 1] + Zspd_ok[j];
                            }
                        }
                        intpZ[i] = s_Zspd_ok[X.Length - 1] / s_spd_ok[X.Length - 1];
                    }
                    ILArray<double> ILintpZ = intpZ;
                    ILArray<double> matrix = ILMath.zeros(nY, nX);

                    for (int i = 0; i < nY; i++)
                    {
                        ILArray<int> rangeX = ILMath.vec<int>(0, nX - 1);
                        ILArray<int> rangeZ = ILMath.vec<int>(0 + (nX) * (i), (nX * (i + 1) - 1));
                        matrix[i, rangeX] = ILintpZ[rangeZ];
                    }

                    ILArray<float> matrixf = ILMath.convert<double, float>(matrix);
                    ILArray<float> matrixl = ILMath.log10(matrixf);
                    ILArray<float> Xf = ILMath.convert<double, float>(intpX);
                    ILArray<float> Yf = -1 * ILMath.convert<double, float>(intpY);

                    matrixgen = matrixf;
                    Xgen = X;
                    Ygen = Y;

                    // Example
                    ILArray<float> ZZZ = ILSpecialData.sincf(40, 50);
                    ILArray<float> XXX = ILMath.linspace<float>(1, 2, 50);
                    ILArray<float> YYY = ILMath.linspace<float>(1, 2, 40);
                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        },
                        YAxis =
                        {
                            Label =
                            {
                                Text = "n"
                            }
                        }
                    },
                        Children = {
                    new ILSurface(matrixf,Xf,Yf)
                    {
	                      // make thin transparent wireframes
	                      Wireframe = { Color = Color.FromArgb(50, Color.LightGray) },
		                    // choose a different colormap
		                    Colormap = new ILColormap(Colormaps.Jet), 
	                      // add a colorbar (see below)
	                      Children = { new ILColorbar() }

                    }
                    }
                    });
                }
                if (inforadio == 1)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("a (m)"),
                    new DataColumn("C1"),
                    new DataColumn("C2"),
                    new DataColumn("P1"),
                    new DataColumn("P2"),
                    new DataColumn("X (m)"),
                    new DataColumn("Z/n (m)"),
                    new DataColumn("R (Ω)"),
                    new DataColumn("K (m)"),
                    new DataColumn("ρa (Ω.m)"),
                    new DataColumn("Za (m)")});
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
                    double[] data_Z = new double[L];
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
                        data_n[i] = data_P1[i] - data_C2[i];
                        data_K[i] = data_n[i] * data_a[i] * pi * (data_n[i] + 2) * (data_n[i] + 1);
                        data_Rho[i] = data_R[i] * data_K[i];
                        data_dis[i] = (data_P1[i] + data_C2[i]) * data_a[i] / 2;
                        data_Z[i] = data_n[i] * data_a[i] / 3;
                        object[] Data_Row = {data_a[i], data_C1[i], data_C2[i],
                    data_P1[i], data_P2[i], data_dis[i], data_n[i], data_R[i],
                    data_K[i], data_Rho[i], data_Z[i]};
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    n = data_n;

                    double[] X = data_dis;
                    double[] Y = data_n;
                    double[] Z = data_Rho;
                    double minX = X.Min();
                    double maxX = X.Max();
                    double minY = Y.Min();
                    double maxY = Y.Max();
                    double intX = 1;
                    double intY = 0.1;
                    int LX = Convert.ToInt32((maxX - minX) / intX);
                    int LY = Convert.ToInt32((maxY - minY) / intY);
                    double[] intpX = new double[LX];
                    double[] intpY = new double[LY];
                    for (int i = 0; i < LX; i++)
                    {
                        if (i == 0)
                        {
                            intpX[i] = minX;
                        }
                        else
                        {
                            intpX[i] = intpX[i - 1] + intX;
                        }
                    }
                    for (int i = 0; i < LY; i++)
                    {
                        if (i == 0)
                        {
                            intpY[i] = minY;
                        }
                        else
                        {
                            intpY[i] = intpY[i - 1] + intY;
                        }
                    }
                    // IDW
                    int nX = intpX.Length;
                    int nY = intpY.Length;
                    int N = nX * nY;
                    double[] intpZ = new double[N];
                    double[] susX = new double[N];
                    double[] susY = new double[N];
                    for (int i = 0; i < nY; i++)
                    {
                        for (int j = 0; j < nX; j++)
                        {
                            susX[j + (nX * (i))] = intpX[j];
                            susY[j + (nX * (i))] = intpY[i];
                        }
                    }
                    double k = 2;
                    for (int i = 0; i < N; i++)
                    {
                        double[] d_obs = new double[X.Length];
                        double[] d_ok = new double[X.Length];
                        double[] spd_ok = new double[X.Length];
                        double[] Zspd_ok = new double[X.Length];
                        double[] s_spd_ok = new double[X.Length];
                        double[] s_Zspd_ok = new double[X.Length];
                        for (int j = 0; j < X.Length; j++)
                        {
                            d_obs[j] = Math.Sqrt(Math.Pow(X[j] - susX[i], 2) + Math.Pow(Y[j] - susY[i], 2));
                            d_ok[j] = Math.Pow(d_obs[j], k);
                            spd_ok[j] = 1 / (d_ok[j] + 0.00001);
                            Zspd_ok[j] = Z[j] * spd_ok[j];
                            if (j == 0)
                            {
                                s_spd_ok[j] = spd_ok[j];
                                s_Zspd_ok[j] = Zspd_ok[j];
                            }
                            else
                            {
                                s_spd_ok[j] = s_spd_ok[j - 1] + spd_ok[j];
                                s_Zspd_ok[j] = s_Zspd_ok[j - 1] + Zspd_ok[j];
                            }
                        }
                        intpZ[i] = s_Zspd_ok[X.Length - 1] / s_spd_ok[X.Length - 1];
                    }
                    ILArray<double> ILintpZ = intpZ;
                    ILArray<double> matrix = ILMath.zeros(nY, nX);

                    for (int i = 0; i < nY; i++)
                    {
                        ILArray<int> rangeX = ILMath.vec<int>(0, nX - 1);
                        ILArray<int> rangeZ = ILMath.vec<int>(0 + (nX) * (i), (nX * (i + 1) - 1));
                        matrix[i, rangeX] = ILintpZ[rangeZ];
                    }

                    ILArray<float> matrixf = ILMath.convert<double, float>(matrix);
                    ILArray<float> Xf = ILMath.convert<double, float>(intpX);
                    ILArray<float> Yf = -1 * ILMath.convert<double, float>(intpY);

                    matrixgen = matrixf;
                    Xgen = X;
                    Ygen = Y;

                    // Example
                    ILArray<float> ZZZ = ILSpecialData.sincf(40, 50);
                    ILArray<float> XXX = ILMath.linspace<float>(1, 2, 50);
                    ILArray<float> YYY = ILMath.linspace<float>(1, 2, 40);
                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        },
                        YAxis =
                        {
                            Label =
                            {
                                Text = "n"
                            }
                        }
                    },
                        Children = {
                    new ILSurface(matrixf,Xf,Yf)
                    {
	                      // make thin transparent wireframes
	                      Wireframe = { Color = Color.FromArgb(50, Color.LightGray) },
		                    // choose a different colormap
		                    Colormap = new ILColormap(Colormaps.Jet), 
	                      // add a colorbar (see below)
	                      Children = { new ILColorbar() }

                    }
                    }
                    });
                }
                if (inforadio == 2)
                {
                    table.Columns.AddRange(new DataColumn[] { new DataColumn("Distance (m)"),
                    new DataColumn("Za (m)"),
                    new DataColumn("ρa (Ω.m)")});
                    string[] MyDataLines = MyDataString.Split('\n');
                    L = MyDataLines.Length;
                    double[] data_dis = new double[L];
                    double[] data_Z = new double[L];
                    double[] data_Rho = new double[L];


                    for (int i = 0; i < L; i++)
                    {
                        string[] MyDataColumns = MyDataLines[i].Split('\t');
                        data_Z[i] = Convert.ToDouble(MyDataColumns[selected[1]]);
                        data_dis[i] = Convert.ToDouble(MyDataColumns[selected[0]]);
                        data_Rho[i] = Convert.ToDouble(MyDataColumns[selected[2]]);
                        object[] Data_Row = { data_dis[i], data_Z[i], data_Rho[i] };
                        table.Rows.Add(Data_Row);
                    }
                    dataGridView1.DataSource = table;
                    dis = data_dis;
                    Rho = data_Rho;
                    n = data_Z;

                    double[] X = data_dis;
                    double[] Y = data_Z;
                    double[] Z = data_Rho;
                    double minX = X.Min();
                    double maxX = X.Max();
                    double minY = Y.Min();
                    double maxY = Y.Max();
                    double intX = 1;
                    double intY = 1;
                    int LX = Convert.ToInt32((maxX - minX) / intX);
                    int LY = Convert.ToInt32((maxY - minY) / intY);
                    double[] intpX = new double[LX];
                    double[] intpY = new double[LY];
                    for (int i = 0; i < LX; i++)
                    {
                        if (i == 0)
                        {
                            intpX[i] = minX;
                        }
                        else
                        {
                            intpX[i] = intpX[i - 1] + intX;
                        }
                    }
                    for (int i = 0; i < LY; i++)
                    {
                        if (i == 0)
                        {
                            intpY[i] = minY;
                        }
                        else
                        {
                            intpY[i] = intpY[i - 1] + intY;
                        }
                    }
                    // IDW
                    int nX = intpX.Length;
                    int nY = intpY.Length;
                    int N = nX * nY;
                    double[] intpZ = new double[N];
                    double[] susX = new double[N];
                    double[] susY = new double[N];
                    for (int i = 0; i < nY; i++)
                    {
                        for (int j = 0; j < nX; j++)
                        {
                            susX[j + (nX * (i))] = intpX[j];
                            susY[j + (nX * (i))] = intpY[i];
                        }
                    }
                    double k = 2;
                    for (int i = 0; i < N; i++)
                    {
                        double[] d_obs = new double[X.Length];
                        double[] d_ok = new double[X.Length];
                        double[] spd_ok = new double[X.Length];
                        double[] Zspd_ok = new double[X.Length];
                        double[] s_spd_ok = new double[X.Length];
                        double[] s_Zspd_ok = new double[X.Length];
                        for (int j = 0; j < X.Length; j++)
                        {
                            d_obs[j] = Math.Sqrt(Math.Pow(X[j] - susX[i], 2) + Math.Pow(Y[j] - susY[i], 2));
                            d_ok[j] = Math.Pow(d_obs[j], k);
                            spd_ok[j] = 1 / (d_ok[j] + 0.00001);
                            Zspd_ok[j] = Z[j] * spd_ok[j];
                            if (j == 0)
                            {
                                s_spd_ok[j] = spd_ok[j];
                                s_Zspd_ok[j] = Zspd_ok[j];
                            }
                            else
                            {
                                s_spd_ok[j] = s_spd_ok[j - 1] + spd_ok[j];
                                s_Zspd_ok[j] = s_Zspd_ok[j - 1] + Zspd_ok[j];
                            }
                        }
                        intpZ[i] = s_Zspd_ok[X.Length - 1] / s_spd_ok[X.Length - 1];
                    }
                    ILArray<double> ILintpZ = intpZ;
                    ILArray<double> matrix = ILMath.zeros(nY, nX);

                    for (int i = 0; i < nY; i++)
                    {
                        ILArray<int> rangeX = ILMath.vec<int>(0, nX - 1);
                        ILArray<int> rangeZ = ILMath.vec<int>(0 + (nX) * (i), (nX * (i + 1) - 1));
                        matrix[i, rangeX] = ILintpZ[rangeZ];
                    }

                    ILArray<float> matrixf = ILMath.convert<double, float>(matrix);
                    ILArray<float> Xf = ILMath.convert<double, float>(intpX);
                    ILArray<float> Yf = -1 * ILMath.convert<double, float>(intpY);

                    matrixgen = matrixf;
                    Xgen = X;
                    Ygen = Y;

                    // Example
                    ILArray<float> ZZZ = ILSpecialData.sincf(40, 50);
                    ILArray<float> XXX = ILMath.linspace<float>(1, 2, 50);
                    ILArray<float> YYY = ILMath.linspace<float>(1, 2, 40);
                    ilPanel1.Scene.Add(new ILPlotCube
                    {
                        Axes =
                    {
                        XAxis =
                        {
                            Label =
                            {
                                Text = "Distance (m)"
                            }
                        },
                        YAxis =
                        {
                            Label =
                            {
                                Text = "Z"
                            }
                        }
                    },
                        Children = {
                    new ILSurface(matrixf,Xf,Yf)
                    {
	                      // make thin transparent wireframes
	                      Wireframe = { Color = Color.FromArgb(50, Color.LightGray) },
		                    // choose a different colormap
		                    Colormap = new ILColormap(Colormaps.Jet), 
	                      // add a colorbar (see below)
	                      Children = { new ILColorbar() }

                    }
                    }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void datumLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Dipole_View dip1 = new Dipole_View(dis, n);
                dip1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void logaritmicScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
