using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SS_OpenCV
{
    public partial class HistogramForm : Form
    {
        public HistogramForm(int [,] matrix)
        {
            InitializeComponent();
            /*
            DataPointCollection [] list1 = { };
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                list1[i] = (chart1.Series[i].Points);
            }
            */
            /*
            switch (matrix.GetLength(0))
            {
                case 1:
                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        chart1.Series[0].Points.AddXY(i, matrix[0, i]);
                    }
                    chart1.Series[0].Color = Color.Gray;
                    chart1.Series.RemoveAt(1);
                    chart1.Series.RemoveAt(2);
                    chart1.Series.RemoveAt(3);
                    break;
                case 3:
                    for (int i = 0;i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            chart1.Series[i].Points.AddXY(j, matrix[i, j]);
                        }
                    }
                    chart1.Series[0].Color = Color.Red;
                    chart1.Series[1].Color = Color.Green;
                    chart1.Series[2].Color = Color.Blue;
                    chart1.Series.RemoveAt(3);
                    break;
                case 4:
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            chart1.Series[i].Points.AddXY(j, matrix[i, j]);
                        }
                    }
                    chart1.Series[0].Color = Color.Gray;
                    chart1.Series[1].Color = Color.Red;
                    chart1.Series[2].Color = Color.Green;
                    chart1.Series[3].Color = Color.Blue;
                    break;
            }
            */
            
            //Color[] colorsArr = new Color[] { Color.Gray, Color.Red, Color.Green, Color.Blue };
            
            for (int i=0; i<matrix.GetLength(0); i++)
            {
                for (int j=0; j<matrix.GetLength(1); j++)
                {
                    chart1.Series[i].Points.AddXY(j, matrix[i, j]);
                }
                //chart1.Series[i].Color = colorsArr[i];
            }

            if (matrix.GetLength(0) == 1)
            {
                chart1.Series.RemoveAt(1);
                chart1.Series.RemoveAt(2);
                //chart1.Series.RemoveAt(3);
                chart1.Series[0].Color = Color.Gray;
            }
            else if (matrix.GetLength(0) == 3)
            {
                chart1.Series.RemoveAt(3);
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[1].Color = Color.Green;
                chart1.Series[2].Color = Color.Red;
            }
            else
            {
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[1].Color = Color.Green;
                chart1.Series[2].Color = Color.Red;
                chart1.Series[3].Color = Color.Gray;
            }

            //chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Maximum = matrix.GetLength(1);
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Title = "Pixel Numbers";
            chart1.ChartAreas[0].AxisX.Title = "Intensities";
            chart1.ResumeLayout();
        }
    }
}
