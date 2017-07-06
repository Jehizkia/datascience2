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
        double bestSolutionFound = double.MaxValue;

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

            //Amount of iterations
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

            postProcess();

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

            //Each customer gets an cluster assigned
            for (int i = 0; i < customers.Count; i++)
            {
                //Distances are saved for each customer
                Dictionary<int, double> distances = new Dictionary<int, double>();

                //Distance is calculated for each centroid. 
                //The centroid index and the actual distance are being saved
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

        //Sum of the squares of the error of each point.  
        //Error of a point = distance to the nearest cluster center

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

            if(sumSquaredOfError < bestSolutionFound)
            {
                bestSolutionFound = sumSquaredOfError;
            }
            //Console.WriteLine("SSE: " + sumSquaredOfError);
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
                Console.WriteLine(" ");
                Console.WriteLine("======.:|| Cluster " + i + "||:.======" + centroids[i].customerList.Count);
                for (int x = 0; x < centroids[i].customerList.Count; x++)
                {
                    count++;
                
                    foreach (var item in centroids[i].customerList[x])
                    {
                        Console.Write(item);
                    }
                    Console.WriteLine();
                }               
            }        
        }

        public void postProcess()
        {

            Console.WriteLine("Best solution: " + bestSolutionFound);
            Console.WriteLine("");
            Console.WriteLine("=========.:[CLUSTERS]:.=========");
            printClusters();

            //post process the clusters
            Console.WriteLine("");
            Console.WriteLine("=========.:[RESULTS]:.=========");


            int centroidCount = 0;
            foreach (var centroid in centroids)
            {
                int[] offers = new int[centroid.Count];

                Console.WriteLine("");
                Console.WriteLine("=========.:[Cluster " + centroidCount +"]:.=========");
                foreach (var customer in centroid.customerList)
                {
                    int itemCount = 0;
                    foreach (var item in customer)
                    {
                        if(item == 1)
                        {
                            offers[itemCount]++;
                        }

                        itemCount++;
                    }
                }

                for (int i = 0; i < offers.Length; i++)
                {                    

                    if(offers[i] > 1)
                    {
                        Console.WriteLine("Offer " + i + " -> bought " + offers[i] + " times");
                    } else if (offers[i] == 1)
                    {
                        Console.WriteLine("Offer " + i + " -> bought " + offers[i] + " time");
                    }

                }

                centroidCount++;
            } 
        }
    }
}
