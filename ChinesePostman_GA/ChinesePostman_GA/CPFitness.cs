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
        public static bool everyRoadIsTraveled(List<Road> roads)
        {
            foreach (var road in roads)
            {
                if (road.isTravelled == false)
                {
                    return false;
                }
            }
            return true;
        }
        public double Evaluate(IChromosome chromosome)
        {
            List<Road> listOfRoads = Program.roads;
            var genes = chromosome.GetGenes();
            var distanceSum = 0.0;
            var lastRoadIndex = Convert.ToInt32(genes[0].Value, CultureInfo.InvariantCulture);
            int index = 0; // variable  for checking correct values

            int startingPoint = listOfRoads[Convert.ToInt32(genes[0].Value, CultureInfo.InvariantCulture)].cityFrom; // point from where we start
            for (int i = 0; i < genes.Length; i++)
            {
                var roadIndex = Convert.ToInt32(genes[i].Value, CultureInfo.InvariantCulture);
                Road returnRoad = listOfRoads.Find(e => e.index.Equals(listOfRoads[roadIndex].cityTo.ToString() + "-" + listOfRoads[roadIndex].cityFrom.ToString())); // getting return road object
                listOfRoads[roadIndex].isTravelled = true; // set that this road is travelled
                returnRoad.isTravelled = true; // set that the reverce of the road is travelled

                if (i != 0 && listOfRoads[roadIndex].cityFrom != listOfRoads[Convert.ToInt32(genes[i - 1].Value, CultureInfo.InvariantCulture)].cityTo)
                {
                    distanceSum += listOfRoads[roadIndex].cost * 1000; //if road is not correct multiply the cost
                    lastRoadIndex = roadIndex;
                    index++;
                }
                else
                {
                    distanceSum += listOfRoads[roadIndex].cost;
                    lastRoadIndex = roadIndex;
                    index++;
                }
                if (everyRoadIsTraveled(listOfRoads)) // checking if the current end point is exact as starting point and if all roads were travelled at least once
                {
                    if (listOfRoads[roadIndex].cityTo == listOfRoads[Convert.ToInt32(genes[0].Value, CultureInfo.InvariantCulture)].cityFrom)
                    {
                        break;
                    }

                }
            }
            if (!everyRoadIsTraveled(listOfRoads))
            {
                distanceSum *= 1000;
            }
            foreach (Road road in Program.roads)
            {
                road.isTravelled = false;
            }

            var fitness = 1.0 / distanceSum;


            return fitness;

        }
    }
}
