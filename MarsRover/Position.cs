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

        public Movement.CardinalHeading CardinalHeading { get; set; }

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
                this.CardinalHeading == comparisonPosition.CardinalHeading;
        }

        public Position(int x, int y)
        {
            this.Point = new Point(x, y);
        }

        public Position(int x, int y, Movement.CardinalHeading cardinalHeading)
        {
            this.Point = new Point(x, y);
            this.CardinalHeading = cardinalHeading;
        }
    }
}
