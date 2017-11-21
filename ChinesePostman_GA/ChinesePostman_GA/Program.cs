using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Terminations;
using System.IO;

namespace ChinesePostman_GA
{
    class Program
    {
        public static List<Road> roads;
        public static int[,] LoadDataFromFile(string path)
        {
            string[] fileLineContent = File.ReadAllLines(path);
            int[,] dataContent = new int[fileLineContent.Length, 4];
            for (int i = 0; i < fileLineContent.Length; i++)
            {
                string[] splitted = fileLineContent[i].Split(' ');
                for (int j = 0; j < 4; j++)
                {
                    dataContent[i, j] = Int32.Parse(splitted[j]);
                }
            }
            return dataContent;
        }

        static void Main(string[] args)
        {
            #region Loading data from File

            string location = "C:\\9 semestr\\ISK\\PostmanProblem\\ChinesePostman_GA\\ChinesePostman_GA\\sample_data\\easy_sample_data.txt";
            int[,] data = LoadDataFromFile(location);
            roads = new List<Road>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Road newRoad = new Road(data[i, 0], data[i, 1], data[i, 2]);// one direction cost
                roads.Add(newRoad);
            }

            #endregion

            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new CPFitness();
            var chromosome = new CPChromosome(roads.Count);
            var population = new Population(50, 70, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            ga.Start();

            Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
        }
    }
}
