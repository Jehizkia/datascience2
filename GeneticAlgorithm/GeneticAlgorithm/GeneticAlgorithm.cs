using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class GeneticAlgorithm
    {
        int PopulationSize = 0;
        double CrossoverRate = 0;
        double MutationRate = 0;
        bool Elitism = false;
        int Iterations = 0;
        int IndiviualSize = 5;

        List<Individual> Population = new List<Individual>();

        public GeneticAlgorithm(int PopSize, double CrossRate, double MutRate, bool Eli, int Iterations)
        {
            this.PopulationSize = PopSize;
            this.CrossoverRate = CrossRate;
            this.MutationRate = MutRate;
            this.Elitism = Eli;
            this.Iterations = Iterations;
            start();
            PrintPopulation();
        }

        public void start()
        {
            InitialPopulation();
        }


        //Generates the populationd
        public void InitialPopulation()
        {
            Random rand = new Random();
            for (int i = 0; i < PopulationSize; i++)
            {
                Individual indi = new Individual();

                for (int x = 0; x < 5; x++)
                {
                    indi.Add(rand.Next(2));
                }
                indi.CalculateFitness();
                Population.Add(indi);
            }
        }

        public void newPopulation()
        {
            Selection();  
        }

        public void Selection()
        {

        }

        public void PrintPopulation()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                for (int x = 0; x < Population[i].Count; x++)
                {
                    Console.Write(Population[i][x]);
                }

                Console.WriteLine("===========");
            }
        }

    }
}
