using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover : IRover
    {
        public Position Position { get; set; }

        private void Turn(Direction.Turning turn)
        {
            switch (turn)
            {
                case Direction.Turning.Right:
                    if (this.Position.CardinalDirection == Direction.Cardinal.West)
                    {
                        // Bounds wrapping
                        this.Position.CardinalDirection = Direction.Cardinal.North;
                    }
                    else
                    {
                        this.Position.CardinalDirection += 1;
                    }
                    break;
                case Direction.Turning.Left:
                    if (this.Position.CardinalDirection == Direction.Cardinal.North)
                    {
                        // Bounds wrapping
                        this.Position.CardinalDirection = Direction.Cardinal.West;
                    }
                    else
                    {
                        this.Position.CardinalDirection -= 1;
                    }
                    break;
            }
        }

        public void Command(string commands)
        {
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'F':
                        this.Position.Y += 1;
                        break;
                    case 'B':
                        this.Position.Y -= 1;
                        break;
                    case 'R':
                        this.Turn(Direction.Turning.Right);
                        break;
                    case 'L':
                        this.Turn(Direction.Turning.Left);
                        break;
                }
            }
        }

        public Rover()
        {
            this.Position = new Position();
        }
    }
}
