using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Movement
    {
        public enum Direction
        {
            Forwards,
            Backwards
        }

        public enum CardinalHeading
        {
            North,
            East,
            South,
            West
        }

        public enum TurningDirection
        {
            Right,
            Left
        }
    }
}
