using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Individual : List<int>
    {
        public double fitness = 0;

        // Evaluate the fitness 𝑓(𝑥) of each individual 𝑥 in the population 
        public void CalculateFitness()
        {
            //convert list to binary string
            string binaryString = string.Join("", this.ToArray());
            int binary = Convert.ToInt32(binaryString, 2);

            fitness = (Math.Pow(-binary, 2)) + (7 * binary);
        }     

    }
}
