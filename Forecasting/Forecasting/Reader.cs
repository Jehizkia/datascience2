using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Forecasting
{
    class Reader
    {
        public Dictionary<int, int> dataset = new Dictionary<int, int>();
        public List<int> dataset2 = new List<int>();
        public Reader()
        {
            readFile();
        }

        public void readFile()
        {

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("C:/Users/Jehizkia/Documents/datasetforecasting.csv"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    bool firstline = true;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!firstline)
                        {
                            string[] values = line.Split(';');                     
                            dataset.Add(Convert.ToInt16(values[0]), Convert.ToInt16(values[1]));
                            dataset2.Add(Convert.ToInt16(values[1]));
                        }

                        firstline = false;
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public Dictionary<int, int> getDataset()
        {
            return dataset;
        }

    }
}
