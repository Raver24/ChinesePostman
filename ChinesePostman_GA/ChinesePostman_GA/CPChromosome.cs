using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePostman_GA
{
    class CPChromosome : ChromosomeBase
    {
        #region Fields
        private int m_numberOfRoads;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Extensions.Tsp.TspChromosome"/> class.
        /// </summary>
        /// <param name="numberOfRoads">Number of roads.</param>
        public CPChromosome(int numberOfRoads) : base(numberOfRoads)
        {
            List<Road> roads = Program.roads;

            m_numberOfRoads = numberOfRoads;
            
            int[] roadsIndexes = new int[m_numberOfRoads]; //list to hold indexes of roads in order

            Random random = new Random(); //object to random mechanism
            roadsIndexes[0]= random.Next(roads.Count); //Random search for first road

            for (int i = 1; i < m_numberOfRoads; i++)
            {
                List<Road> filtered = roads.Where(e => e.cityFrom.Equals(roads[roadsIndexes[i - 1]].cityTo)).ToList(); //new list with filtered roads, that have cityFrom set as same as previous element cityTo
                int selectedFromFilter = random.Next(filtered.Count); //get random road from filtered ones
                roadsIndexes[i] = roads.IndexOf(filtered[selectedFromFilter]);//set this road as next                                                                                      

            }
            for (int i = 0; i < m_numberOfRoads; i++)
            {
                ReplaceGene(i, new Gene(roadsIndexes[i]));
            }
        }

       
        #endregion

        #region Properties
        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double Distance { get; internal set; }

        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>The distance.</value>

        #endregion

        #region implemented abstract members of ChromosomeBase
        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="geneIndex">Gene index.</param>
        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetInt(0, m_numberOfRoads));
        }

        /// <summary>
        /// Creates a new chromosome using the same structure of this.
        /// </summary>
        /// <returns>The new chromosome.</returns>
        public override IChromosome CreateNew()
        {
            return new CPChromosome(m_numberOfRoads);
        }

        /// <summary>
        /// Creates a clone.
        /// </summary>
        /// <returns>The chromosome clone.</returns>
        public override IChromosome Clone()
        {
            var clone = base.Clone() as CPChromosome;
            clone.Distance = Distance;

            return clone;
        }
        #endregion
    }
}
