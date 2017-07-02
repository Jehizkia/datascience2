using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans
{
    class Distance
    {
        public double euclidean(Customer customer, Centroid centroid)
        {
            double sum = 0;

            for (int i = 0; i < customer.Count; i++)
            {
                sum += Math.Pow((customer[i] - centroid[i]), 2);
            }
             
            return Math.Sqrt(sum);
        }
    }
}
