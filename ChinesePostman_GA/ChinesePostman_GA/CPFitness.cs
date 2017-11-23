using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePostman_GA
{
    class CPFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            List<Road> listOfRoads = Program.roads;
            var genes = chromosome.GetGenes();
            var distanceSum = 0.0;
            var lastRoadIndex = Convert.ToInt32(genes[0].Value, CultureInfo.InvariantCulture);
            var roadsIndexes = new List<int>();
            roadsIndexes.Add(lastRoadIndex);
            
            foreach (var g in genes)
            {
                var currentRoadIndex = Convert.ToInt32(g.Value, CultureInfo.InvariantCulture);
                distanceSum += listOfRoads[currentRoadIndex].cost;
                lastRoadIndex = currentRoadIndex;

                roadsIndexes.Add(lastRoadIndex);
            }

            var fitness = 1.0 / (distanceSum * genes.Length);

            ((CPChromosome)chromosome).Distance = distanceSum;

            if (fitness < 0)
            {
                 fitness = 0;
            }

            return fitness;

        }
    }
}
