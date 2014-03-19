using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Movement.CardinalDirection CardinalDirection { get; set; }

        public override bool Equals(object comparisonObject)
        {
            if (comparisonObject == null)
            {
                return false;
            }

            var comparisonPosition = comparisonObject as Position;
            if (comparisonPosition == null)
            {
                return false;
            }

            return
                this.X == comparisonPosition.X &&
                this.Y == comparisonPosition.Y &&
                this.CardinalDirection == comparisonPosition.CardinalDirection;
        }
    }
}
