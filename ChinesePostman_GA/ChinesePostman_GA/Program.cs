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
using GeneticSharp.Domain.Chromosomes;
using System.Globalization;
using System.Diagnostics;

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

            string location = Directory.GetCurrentDirectory() + "\\Data\\graf4_kod.txt";

            int[,] data = LoadDataFromFile(location);
            roads = new List<Road>();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Road newRoad = new Road(data[i, 0], data[i, 1], data[i, 2]);// one direction cost
                roads.Add(newRoad);
                Road reverseRoad = new Road(data[i, 1], data[i, 0], data[i, 3]); // read reverse road
                roads.Add(reverseRoad);
            }

            #endregion
            var selection = new EliteSelection();
            var crossover = new ThreeParentCrossover();
            var mutation = new TworsMutation();
            var fitness = new CPFitness();
            var chromosome = new CPChromosome(3*roads.Count);
            var population = new Population(200, 400, chromosome);

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new GenerationNumberTermination(400);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine("GA running...");
            ga.Start();
            Console.WriteLine();
            timer.Stop();
            bool first = true;
            int startCityIndex = Convert.ToInt32(ga.BestChromosome.GetGenes()[0].Value, CultureInfo.InvariantCulture);
            int startCity = roads[startCityIndex].cityFrom;
            int totalCost = 0;
            foreach (Gene gene in ga.BestChromosome.GetGenes())
            {
                int ind = Convert.ToInt32(gene.Value, CultureInfo.InvariantCulture);
                totalCost += roads[ind].cost;
                if (CPFitness.everyRoadIsTraveled(roads) && roads[ind].cityTo == startCity)
                {
                    Console.Write("-" + roads[ind].cityTo.ToString());
                    break;
                }
                else
                {
                    if (first)
                    {
                        Console.Write(roads[ind].cityFrom.ToString() + "-" + roads[ind].cityTo.ToString());
                        first = false;
                    }
                    else
                    {
                        Console.Write("-" + roads[ind].cityTo.ToString());
                    }
                    roads[ind].isTravelled = true;
                    Road returnRoad = roads.Find(e => e.index.Equals(roads[ind].cityTo.ToString() + "-" + roads[ind].cityFrom.ToString()));
                    returnRoad.isTravelled = true;
                }
            }
            TimeSpan ts = timer.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine();
            Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
            Console.WriteLine("Best solution has total cost: {0}", totalCost);
            Console.WriteLine("Time running: {0}", elapsedTime);
            Console.ReadKey();
        }
    }
}
