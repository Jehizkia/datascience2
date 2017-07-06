using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forecasting
{
    public partial class Form1 : Form
    {

        Forecast fc = new Forecast();
        public Form1()
        {
            InitializeComponent();
            displayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        public void displayData()
        {

            //Normal Data
            foreach (var item in fc.read.dataset2)
            {
                this.chart1.Series["Data"].Points.Add(item);
            }

            //SES Data
            foreach (var item in fc.SES)
            {
                this.chart1.Series["SES"].Points.Add(item);
            }

            //DES Data
            foreach (var item in fc.DES)
            {
                this.chart1.Series["DES"].Points.Add(item);
            }

            //Important data
            this.bestValSES.Text = "SES| best Smoothing factor: " + fc.bestSESData[1] + " SSE: " + fc.bestSESData[0];
            this.bestValDES.Text = "DES| best Smoothing factor: " + fc.bestDESData[1] + " Trend factor: " + fc.bestDESData[2] + " SSE: " + fc.bestDESData[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bestValDES_Click(object sender, EventArgs e)
        {

        }
    }
}
