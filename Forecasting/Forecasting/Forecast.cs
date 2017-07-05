using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecasting
{
    class Forecast
    {
        double smoothingFactor = 0.8;
        double trendFactor = 0.6;
        public Reader read = new Reader();
        public List<double> SES = new List<double>();
        public List<double> DES = new List<double>();

        public double[] bestSESData = new double[2];
        public double[] bestDESData = new double[3];

        int forecast = 11;

        public Forecast()
        {
            start();
        }

        public void start()
        {
            getBestSES();
            getBestDES();
        }

        public void calcSES()
        {
            //initialize: Calculate the average of the first 12 values
            double sum_first_12 = 0;

            for (int i = 0; i < 12; i++)
            {
                sum_first_12 += read.dataset2[i];
            }

            SES.Add((sum_first_12 / 12));

            //Smoothing
            for (int x = 1; x < read.dataset2.Count; x++)
            {
                //𝒔𝒕 = 𝜶 * 𝒙𝒕−𝟏 + (𝟏 − 𝜶) * 𝒔𝒕−𝟏
                double smoothed = ((smoothingFactor * read.dataset2[x - 1]) + ((1 - smoothingFactor) * SES[x - 1]));
                SES.Add(smoothed);
            }

            //Forecasting
            for (int y = 0; y < forecast; y++)
            {
                SES.Add(SES.Last());
            }

        }

        public void calcDES()
        {
            List<double> local_SES = new List<double>();
            List<double> local_DES = new List<double>();

            //initialization
            local_SES.Add(read.dataset2[0]);
            local_DES.Add((read.dataset2[1] - read.dataset2[0]));

            //formulas
            for (int x = 1; x < read.dataset2.Count; x++)
            {
                //𝒔𝒕 = 𝜶 * 𝒙𝒕 + (𝟏 − 𝜶) (𝒔𝒕−𝟏 + 𝒃𝒕−𝟏)
                double ses_smoothed = ((smoothingFactor * read.dataset2[x]) + ((1 - smoothingFactor) * (local_SES[x - 1] + local_DES[x - 1])));
                local_SES.Add(ses_smoothed);


                //𝒃𝒕 = 𝜷 * (𝒔𝒕 − 𝒔𝒕−𝟏) + (𝟏 − 𝜷) * 𝒃𝒕−𝟏
                double des_smoothed = (trendFactor * (local_SES[x] - local_SES[x - 1])) + ((1 - trendFactor) * local_DES[x - 1]);
                local_DES.Add(des_smoothed);
            }

            for (int y = 0; y < local_SES.Count; y++)
            {
                DES.Add(local_SES[y] + local_DES[y]);
            }

            //forecast
            for (int e = 2; e < forecast; e++)
            {
                double forecst = local_SES.Last() + (e * local_DES.Last());
                DES.Add(forecst);
            }

        }

        public void SquaredErrors()
        {
            double sum_SES = 0;
            double sum_DES = 0;

            for (int i = 0; i < read.dataset2.Count; i++)
            {
                sum_SES += Math.Pow((SES[i] - read.dataset2[i]), 2);
                sum_DES += Math.Pow((DES[i] - read.dataset2[i]), 2);
            }

            double SSE_SES = Math.Sqrt(sum_SES / (read.dataset2.Count - 1));
            double SSE_DES = Math.Sqrt(sum_DES / (read.dataset2.Count - 2));
        }

        public double SSE_SES()
        {
            double sum_SES = 0;

            for (int i = 0; i < read.dataset2.Count; i++)
            {
                sum_SES += Math.Pow((SES[i] - read.dataset2[i]), 2);
            }

            double SSE_SES = Math.Sqrt(sum_SES / (read.dataset2.Count - 1));

            return SSE_SES;
        }

        public double SSE_DES()
        {
            double sum_DES = 0;

            for (int i = 0; i < read.dataset2.Count; i++)
            {
                sum_DES += Math.Pow((DES[i] - read.dataset2[i]), 2);
            }
            double SSE_DES = Math.Sqrt(sum_DES / (read.dataset2.Count - 2));

            return SSE_DES;
        }

        public void getBestSES()
        {
            //SSE , Smoothingfactor
            double[] bestSmoothingFactor = new double[2];

            //used to get the smallest value
            bestSmoothingFactor[0] = double.MaxValue;

            //
            for (double i = 0; i <= 1; i += 0.1)
            {
                SES = new List<double>();
                smoothingFactor = i;
                calcSES();
                double currentSSE = SSE_SES();

                if (currentSSE < bestSmoothingFactor[0])
                {
                    bestSmoothingFactor[0] = currentSSE;
                    bestSmoothingFactor[1] = i;
                }
            }

            //final 
            smoothingFactor = bestSmoothingFactor[1];
            bestSESData = bestSmoothingFactor;
            SES = new List<double>();
            calcSES();
        }

        public void getBestDES()
        {
            //SSE, SmoothingFactor, Trendfactor
            double[] bestSmoothAndTrend = new double[3];
            bestSmoothAndTrend[0] = double.MaxValue;

            for (double i = 0; i < 1; i += 0.1)
            {
                for (double x = 0; x < 1; x += 0.1)
                {
                    DES = new List<double>();
                    smoothingFactor = i;
                    trendFactor = x;
                    calcDES();
                    double currentSSE = SSE_DES();

                    if (currentSSE < bestSmoothAndTrend[0])
                    {
                        bestSmoothAndTrend[0] = currentSSE;
                        bestSmoothAndTrend[1] = i;
                        bestSmoothAndTrend[2] = x;
                    }
                }
            }

            //Final
            smoothingFactor = bestSmoothAndTrend[1];
            trendFactor = bestSmoothAndTrend[2];
            bestDESData = bestSmoothAndTrend;
            DES = new List<double>();
            calcDES();
        }
    }
}
