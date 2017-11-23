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
            int index = 0; // variable  for checking correct values
            foreach (var g in genes)
            {
                var currentRoadIndex = Convert.ToInt32(g.Value, CultureInfo.InvariantCulture);
                if (!(index == ((CPChromosome)chromosome).notUsableIndex )) //if value equals indeks from not usable genes, return distance immediately
                {
                    distanceSum += listOfRoads[currentRoadIndex].cost; //if gene is usable then add cost
                    lastRoadIndex = currentRoadIndex;
                    index++;
                }
                else
                {
                    break;  //anyway break operation
                }
                
            }

            var fitness = 1.0 / (distanceSum);

            ((CPChromosome)chromosome).Distance = distanceSum;

            if (fitness < 0)
            {
                 fitness = 0;
            }

            return fitness;

        }
    }
}
