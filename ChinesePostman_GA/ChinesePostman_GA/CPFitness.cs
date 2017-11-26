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
        public bool everyRoadIsTraveled(List<Road> roads)
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
            bool completed = false; //variable to verify, if algorithm made full cycle
            int notUsableIndex = -1;
            int startingPoint = listOfRoads[0].cityFrom; // point from where we start
            for (int i = 0; i < genes.Length; i++)
            {
                var roadIndex = Convert.ToInt32(genes[i].Value, CultureInfo.InvariantCulture);
                listOfRoads[roadIndex].isTravelled = true; // set that this road is travelled
                Road returnRoad = listOfRoads.Find(e => e.index.Equals(listOfRoads[roadIndex].cityTo.ToString() + listOfRoads[roadIndex].cityFrom.ToString())); // getting return road object
                if (returnRoad != null) // resisting exceptions when return road is not present in file
                {
                    returnRoad.isTravelled = true; // set that the reverce of the road is travelled
                }
                if (completed && notUsableIndex == -1) // if algorithm completed all paths, remaining genes does not count in fitness function
                {
                    notUsableIndex = index;
                }
                if (listOfRoads[roadIndex].cityTo == startingPoint && everyRoadIsTraveled(listOfRoads)) // checking if the current end point is exact as starting point and if all roads were travelled at least once
                {
                    completed = true;
                }
                if (!(index == notUsableIndex)) //if value equals indeks from not usable genes, return distance immediately
                {
                    if (i != 0 && listOfRoads[roadIndex].cityFrom != listOfRoads[Convert.ToInt32(genes[i-1].Value, CultureInfo.InvariantCulture)].cityTo)
                    {
                        distanceSum += listOfRoads[roadIndex].cost * 1000; //if road is not correct multiply the cost
                        lastRoadIndex = roadIndex;
                        index++;
                    }
                    else
                    {
                        distanceSum += listOfRoads[roadIndex].cost; //if gene is usable then add cost
                        lastRoadIndex = roadIndex;
                        index++;
                    }
                    
                }
                else
                {
                    break;  //anyway break operation
                }
            }
            foreach (var g in genes)
            {
                
                
            }
            foreach (Road road in Program.roads)
            {
                road.isTravelled = false;
            }
            var fitness = 1.0 / distanceSum;

            ((CPChromosome)chromosome).Distance = distanceSum;

            if (fitness < 0)
            {
                 fitness = 0;
            }

            return fitness;

        }
    }
}
