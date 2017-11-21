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
            var lastCityIndex = Convert.ToInt32(genes[0].Value, CultureInfo.InvariantCulture);
            var citiesIndexes = new List<int>();
            citiesIndexes.Add(lastCityIndex);
            
            foreach (var g in genes)
            {
                var currentCityIndex = Convert.ToInt32(g.Value, CultureInfo.InvariantCulture);
                //distanceSum += CalcDistanceTwoCities(Cities[currentCityIndex], Cities[lastCityIndex]); // wczytywanie odleglosci z klasy Cities
                lastCityIndex = currentCityIndex;

                citiesIndexes.Add(lastCityIndex);
            }

            //distanceSum += CalcDistanceTwoCities(Cities[citiesIndexes.Last()], Cities[citiesIndexes.First()]);

            //var fitness = 1.0 - (distanceSum / (Cities.Count * 1000.0));

            ((CPChromosome)chromosome).Distance = distanceSum;

            // There is repeated cities on the indexes?
            //var diff = Cities.Count - citiesIndexes.Distinct().Count();

            //if (diff > 0)
            //{
            //   fitness /= diff;
            // }

            //if (fitness < 0)
            //{
            //     fitness = 0;
            //}

            // return fitness;
            return 0;

        }
    }
}
