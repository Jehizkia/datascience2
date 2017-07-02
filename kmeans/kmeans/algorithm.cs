using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans
{
    class Algorithm
    {
        int clustersAmount;
        int iterations;
        int customersize = 32;
        List<Customer> customers = new List<Customer>();
        List<Centroid> centroids = new List<Centroid>();

        public Algorithm(int Kclusters, int Kiterations, List<Customer> listOfCustomers)
        {
            this.clustersAmount = Kclusters;
            this.iterations = Kiterations;
            this.customers = listOfCustomers;

            intializeCentroids();
            assignCustomerToCentroid();
        }

        //creates the random centroids
        public void intializeCentroids()
        {
            Random rnd = new Random();
            for (int i = 0; i < clustersAmount; i++)
            {
                Centroid centroid = new Centroid();
                
                for (int x = 0; x < customersize; x++)
                {
                    int value = rnd.Next(2);
                    centroid.Add(value);
                }
                centroids.Add(centroid);
            }
        }


        public void assignCustomerToCentroid()
        {
            Distance distance = new Distance();
            for (int i = 0; i < customers.Count; i++)
            {
                //List<double> distances = new List<double>();
                Dictionary<int ,double> distances = new Dictionary<int ,double>();

                for (int x = 0; x < centroids.Count; x++)
                {
                    distances.Add(x ,distance.euclidean(customers[i], centroids[x]));
                }

                var order = distances.OrderBy(x => x.Value);
                var last = order.Last();       
                
                
            }
        }
    }
}
