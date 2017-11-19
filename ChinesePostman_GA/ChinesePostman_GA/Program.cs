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

namespace ChinesePostman_GA
{
    class Program
    {
        static void Main(string[] args)
        {
            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            //var fitness = new MyProblemFitness();
            //var chromosome = new MyProblemChromosome();
            //var population = new Population(50, 70, chromosome);

            //var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            //ga.Termination = new GenerationNumberTermination(100);

            Console.WriteLine("GA running...");
            //ga.Start();

            //Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
        }
    }
}
