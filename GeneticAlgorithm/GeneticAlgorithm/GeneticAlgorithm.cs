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
        Individual bestindi = null;

        List<Individual> Population = new List<Individual>();
        List<Individual> newPopulation = new List<Individual>();

        Random rand = new Random();

        public GeneticAlgorithm(int PopSize, double CrossRate, double MutRate, bool Eli, int Iterations)
        {
            this.PopulationSize = PopSize;
            this.CrossoverRate = CrossRate;
            this.MutationRate = MutRate;
            this.Elitism = Eli;
            this.Iterations = Iterations;
            start();            
        }

        public void start()
        {
            InitialPopulation();

            for (int i = 0; i < Iterations; i++)
            {
                generateNewPopulation();
            }
            postProcess();
        }

        public void generateNewPopulation()
        {
            CalculateFitness();

            if (Elitism == true)
            {
                for (int i = 0; i < (Population.Count / 2) - 1; i++)
                {
                    Individual parent1 = TournamentSelection();
                    Individual parent2 = TournamentSelection();
                    Crossover(parent1, parent2);
                }


                Dictionary<int, double> listOfFitnesses = new Dictionary<int, double>();

                Individual[] EliteIndi = new Individual[2];

                for (int y = 0; y < Population.Count; y++)
                {
                    listOfFitnesses.Add(y, Population[y].fitness);

                    if (EliteIndi[0] == null || EliteIndi[0].fitness < Population[y].fitness)
                    {
                        EliteIndi[0] = Population[y];
                    }
                    else if (EliteIndi[1] == null || EliteIndi[1].fitness < Population[y].fitness)
                    {
                        EliteIndi[1] = Population[y];
                    }
                }


                for (int x = 0; x < EliteIndi.Length; x++)
                {
                    newPopulation.Add(EliteIndi[x]);
                }
            }
            else
            {
                for (int i = 0; i < Population.Count / 2; i++)
                {
                    Individual parent1 = TournamentSelection();
                    Individual parent2 = TournamentSelection();
                    Crossover(parent1, parent2);
                }
            }

            mutation();
            AcceptNewPopulation();
        }

        public void CalculateFitness()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                Population[i].CalculateFitness();
            }
        }


        //Generates the populationd
        public void InitialPopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                Individual indi = new Individual();

                for (int x = 0; x < 5; x++)
                {
                    indi.Add(rand.Next(2));
                }
                // indi.CalculateFitness();
                Population.Add(indi);
            }
        }

        public void AcceptNewPopulation()
        {
            Population = newPopulation;
            newPopulation = new List<Individual>();
        }

        public Individual TournamentSelection()
        {
            Individual best = null;


            for (int i = 1; i < 5; i++)
            {
                Individual indi = Population[rand.Next(Population.Count)];

                if (best == null || indi.fitness > best.fitness)
                {
                    best = indi;
                }
            }

            return best;
        }

        public void Crossover(Individual parent1, Individual parent2)
        {
            Individual child1 = new Individual();
            Individual child2 = new Individual();

            int length = parent1.Count;

            if (CrossoverRate < rand.NextDouble())
            {
                for (int i = 0; i < length; i++)
                {
                    //first half
                    if (i < length / 2)
                    {
                        child1.Add(parent1[i]);
                        child2.Add(parent2[i]);
                    } //Second half
                    else
                    {
                        child1.Add(parent2[i]);
                        child2.Add(parent1[i]);
                    }
                }

                newPopulation.Add(child1);
                newPopulation.Add(child2);
            }
            else
            {
                newPopulation.Add(parent1);
                newPopulation.Add(parent2);
            }
        }

        public void mutation()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                if (rand.NextDouble() < MutationRate)
                {
                    Individual indi = Population[i];
                    int RandomBit = rand.Next(indi.Count);

                    if (indi[RandomBit] == 0)
                    {
                        indi[RandomBit] = 1;
                    }
                    else
                    {
                        indi[RandomBit] = 0;
                    }

                }
            }
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

        public void postProcess()
        {
            double sum_fitness = 0;
            Individual theBestIndi = null;

            foreach (var individual in Population)
            {
                sum_fitness += individual.fitness;

                if (theBestIndi == null || theBestIndi.fitness < individual.fitness)
                {
                    theBestIndi = individual;
                }

            }


            Console.WriteLine("Average Fitness Of last population: " + (sum_fitness / Population.Count));
            Console.WriteLine("Best fitness: " + theBestIndi.fitness);
            Console.WriteLine("Best Individual: ");

            foreach (var item in theBestIndi)
            {
                Console.WriteLine(item);
            }


        }

    }
}
