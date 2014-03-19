using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover : IRover
    {
        public Planet DeployedTo { get; set; }

        public Position Position { get; set; }

        private void Move(Movement.Direction direction)
        {
            int gridPoints = direction == Movement.Direction.Forwards ? 1 : -1;

            switch (this.Position.CardinalDirection)
            {
                case Movement.CardinalDirection.North:
                    this.Position.Y += gridPoints;
                    break;
                case Movement.CardinalDirection.East:
                    this.Position.X += gridPoints;
                    break;
                case Movement.CardinalDirection.South:
                    this.Position.Y -= gridPoints;
                    break;
                case Movement.CardinalDirection.West:
                    this.Position.X -= gridPoints;
                    break;
            }
        }

        private void Turn(Movement.TurningDirection turn)
        {
            switch (turn)
            {
                case Movement.TurningDirection.Right:
                    if (this.Position.CardinalDirection == Movement.CardinalDirection.West)
                    {
                        // Bounds wrapping
                        this.Position.CardinalDirection = Movement.CardinalDirection.North;
                    }
                    else
                    {
                        this.Position.CardinalDirection += 1;
                    }
                    break;
                case Movement.TurningDirection.Left:
                    if (this.Position.CardinalDirection == Movement.CardinalDirection.North)
                    {
                        // Bounds wrapping
                        this.Position.CardinalDirection = Movement.CardinalDirection.West;
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
                        this.Move(Movement.Direction.Forwards);
                        break;
                    case 'B':
                        this.Move(Movement.Direction.Backwards);
                        break;
                    case 'R':
                        this.Turn(Movement.TurningDirection.Right);
                        break;
                    case 'L':
                        this.Turn(Movement.TurningDirection.Left);
                        break;
                }
            }
        }

        public void DeployTo(Planet planet)
        {

        }

        public Rover()
        {
            this.Position = new Position();
        }
    }
}
