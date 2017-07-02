using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans
{
    class Program
    {
        static void Main(string[] args)
        {
            init();
        }

        public static void init()
        {
            Reader reader = new Reader();
            Algorithm alg = new Algorithm(5, 30, reader.getCustomers());
        }
    }
}
