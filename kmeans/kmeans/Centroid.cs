using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans
{
    class Centroid : Customer
    {
        public List<Customer> customerList = new List<Customer>();


        //Takes the mean of the customers and reasigns the points of the centroid
        public void updateCentroid()
        {
            Centroid newCentroid = new Centroid();
            newCentroid.customerList = customerList;

            for (int i = 0; i < this.Count; i++)
            {
                double sum_values = 0;
                for (int x = 0; x < customerList.Count; x++)
                {
                    sum_values += customerList[x][i];
                }

                newCentroid.Add(sum_values / this.Count);
            }

            for (int y = 0; y < newCentroid.Count; y++)
            {
                this[y] = newCentroid[y];
            }
        }
    }
}
