using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePostman_GA
{
    public class Road
    {
        public int cityFrom { get; set; }
        public int cityTo { get; set; }
        public int cost { get; set; }
        public bool isTravelled { get; set; }

        public Road(int cityFrom, int cityTo, int cost, bool isTravelled = false)
        {
            this.cityFrom = cityFrom;
            this.cityTo = cityTo;
            this.cost = cost;
            this.isTravelled = isTravelled;
        }
    }

    
}
