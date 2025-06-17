using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Statistics;
using ScottPlot.StarAxes;
using ScottPlot.DataGenerators;
using ScottPlot.Stylers;
using ScottPlot.Plottables;
namespace NMR_QuantumLab
{
    public partial class FormMain : Form
    {
        public Button buttonSend => button1;
        public RichTextBox RichTextBox_log => richTextBox1;
        public ComboBox ComboBox_COMPort => comboBox1;
       
        public FormMain()
        {
            
            InitializeComponent();
            CargarGraficos();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }


        private void CargarGraficos()
        {
            // Generación manual de datos FID: senoidal con decaimiento exponencial
            int puntos = 10000;
            double dt = 0.01; // ms entre puntos
            double[] tiempo = new double[puntos];
            double[] senoide = new double[puntos];
            double[] decaimiento = new double[puntos];
            double[] fid = new double[puntos];


           
            for (int i = 0; i < puntos; i++)
            {
                tiempo[i] = i * dt;
                senoide[i] = Math.Sin(2 * Math.PI * 6 * i / puntos); // 6 ciclos
                decaimiento[i] = Math.Exp(-0.01 * i);
                fid[i] = senoide[i] * decaimiento[i];
            }

            // Simulación de un modelo ajustado (fit)
            double frecuencia = 6190.27; // Hz
            double A = 1119.7683; // mV
            double[] modelo = new double[puntos];
            for (int i = 0; i < puntos; i++)
            {
                modelo[i] = A * Math.Sin(2 * Math.PI * frecuencia * tiempo[i] * 1e-3);
            }

            // Configuración del gráfico
            var plt = formsPlot1.Plot;
            plt.Clear();

            plt.Add.Scatter(tiempo, fid, color: Colors.Yellow);
            plt.Add.Scatter(tiempo, modelo, color: Colors.Cyan);

            plt.Axes.Title.Label.Text = "FID y Modelo Senoidal Ajustado";
            plt.Axes.Bottom.Label.Text = "Tiempo (ms)";
            plt.Axes.Left.Label.Text = "Amplitud (mV)";

            plt.Legend.IsVisible = true;
            PlotStyle style = new PlotStyle();
            style.FigureBackgroundColor = Colors.Blue;
            plt.SetStyle(style);

            formsPlot1.Refresh();
        }
    }
}

