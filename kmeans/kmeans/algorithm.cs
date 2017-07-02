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
            start();

        }

        public void start()
        {
            intializeCentroids();

            for (int i = 0; i < iterations; i++)
            {
                assignCustomerToCentroid();
                recomputeCentroid();
                SSE();
                clearCustomerListFromCentroids();

                if (i == (iterations - 1))
                {
                    assignCustomerToCentroid();
                }
            }
            printClusters();

        }

        //This functions is called after a recomputation of a centroid. The customers will be removed from the centroid.
        public void clearCustomerListFromCentroids()
        {
            for (int i = 0; i < centroids.Count; i++)
            {
                centroids[i].customerList.Clear();
            }
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
                Dictionary<int, double> distances = new Dictionary<int, double>();

                for (int x = 0; x < centroids.Count; x++)
                {
                    distances.Add(x, distance.euclidean(customers[i], centroids[x]));
                }

                //This function takes the smallest distance and assigns it to a cluster
                var order = distances.OrderBy(x => x.Value);
                var last = order.First();

                centroids[last.Key].customerList.Add(customers[i]);
            }
        }

        public void recomputeCentroid()
        {
            for (int i = 0; i < centroids.Count; i++)
            {
                centroids[i].updateCentroid();
            }
        }

        public void SSE()
        {
            double sumSquaredOfError = 0;
            Distance distance = new Distance();
            for (int i = 0; i < centroids.Count; i++)
            {
                for (int x = 0; x < centroids[i].customerList.Count; x++)
                {
                    double dist = distance.euclidean(centroids[i].customerList[x], centroids[i]);
                    sumSquaredOfError += Math.Pow(dist, 2);

                }
            }

            Console.WriteLine("SSE: " + sumSquaredOfError);
        }

        public void displayCentroids()
        {
            int count = 0;
            foreach (var point in centroids)
            {
                count++;
                Console.WriteLine(count + ": " + point.customerList.Count);
            }
        }

        public void printClusters()
        {
            int count = 0;
            for (int i = 0; i < centroids.Count; i++)
            {
                Console.WriteLine("================ Cluster " + i + "=============");
                for (int x = 0; x < centroids[i].customerList.Count; x++)
                {
                    count++;
                }

                Console.WriteLine(centroids[i].customerList.Count);
            }

            Console.WriteLine(count);
        }
    }
}
