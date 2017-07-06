using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kmeans
{
    class Reader
    {
        List<Customer> listOfCustomers = new List<Customer>();
        bool firstrun = true;

        public Reader()
        {
            this.ReadFile();
        }

        public void ReadFile()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("C:/Users/Jehizkia/Documents/WineData.csv"))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        this.InsertUsers(values);
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

        public void InsertUsers(string[] values)
        {
            //Initializing the list of customers with the first value of each customer
            if (firstrun)
            {
                //Inserts a customer
                for (int i = 0; i < values.Length; i++)
                {
                    Customer customer = new Customer();
                    customer.Add(Convert.ToDouble(values[i]));
                    listOfCustomers.Add(customer);
                }

                firstrun = false;
            }

            //After initialization the values will be added to the corresponding customer
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    listOfCustomers[i].Add(Convert.ToDouble(values[i]));
                }
            }

        }

        public List<Customer> getCustomers()
        {
            return listOfCustomers;
        }

    }
}
