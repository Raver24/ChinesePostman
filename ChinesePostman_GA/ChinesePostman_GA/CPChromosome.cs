using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePostman_GA
{
    class CPChromosome : ChromosomeBase
    {
        #region Fields
        private int m_numberOfCities;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Extensions.Tsp.TspChromosome"/> class.
        /// </summary>
        /// <param name="numberOfCities">Number of cities.</param>
        public CPChromosome(int numberOfCities) : base(numberOfCities)
        {
            m_numberOfCities = numberOfCities;
            var citiesIndexes = RandomizationProvider.Current.GetUniqueInts(numberOfCities, 0, numberOfCities);

            for (int i = 0; i < numberOfCities; i++)
            {
                ReplaceGene(i, new Gene(citiesIndexes[i]));
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double Distance { get; internal set; }
        #endregion

        #region implemented abstract members of ChromosomeBase
        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="geneIndex">Gene index.</param>
        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetInt(0, m_numberOfCities));
        }

        /// <summary>
        /// Creates a new chromosome using the same structure of this.
        /// </summary>
        /// <returns>The new chromosome.</returns>
        public override IChromosome CreateNew()
        {
            return new CPChromosome(m_numberOfCities);
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
