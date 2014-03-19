using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Position
    {
        public Point Point { get; set; }

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
                this.Point.X == comparisonPosition.Point.X &&
                this.Point.Y == comparisonPosition.Point.Y &&
                this.CardinalDirection == comparisonPosition.CardinalDirection;
        }

        public Position(int x, int y)
        {
            this.Point = new Point(x, y);
        }

        public Position(int x, int y, Movement.CardinalDirection cardinalDirection)
        {
            this.Point = new Point(x, y);
            this.CardinalDirection = cardinalDirection;
        }
    }
}
