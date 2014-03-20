using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover : IRover
    {
        public IPlanet DeployedTo { get; set; }

        public Position Position { get; set; }

        private void Move(Movement.Direction direction)
        {
            int gridPoints = direction == Movement.Direction.Forwards ? 1 : -1;

            switch (this.Position.CardinalDirection)
            {
                case Movement.CardinalDirection.North:
                    this.Position.Point = new Point(this.Position.Point.X, this.Position.Point.Y + gridPoints);
                    break;
                case Movement.CardinalDirection.East:
                    this.Position.Point = new Point(this.Position.Point.X + gridPoints, this.Position.Point.Y);
                    break;
                case Movement.CardinalDirection.South:
                    this.Position.Point = new Point(this.Position.Point.X, this.Position.Point.Y - gridPoints);
                    break;
                case Movement.CardinalDirection.West:
                    this.Position.Point = new Point(this.Position.Point.X - gridPoints, this.Position.Point.Y);
                    break;
            }

            // Bounds wrapping
            if (this.Position.Point.X > this.DeployedTo.Size.Width)
            {
                this.Position.Point = new Point(0, this.Position.Point.Y);
            }
            else if (this.Position.Point.X < 0)
            {
                this.Position.Point = new Point(this.DeployedTo.Size.Width, this.Position.Point.Y);
            }
            else if (this.Position.Point.Y > this.DeployedTo.Size.Height)
            {
                this.Position.Point = new Point(this.Position.Point.X, 0);
            }
            else if (this.Position.Point.Y < 0)
            {
                this.Position.Point = new Point(this.Position.Point.X, this.DeployedTo.Size.Height);
            }

            // Detect collision
            if (this.DeployedTo.HasObstacleAtPosition(this.Position))
            {
                throw new DetectedObstacleException();
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
                    default:
                        throw new ArgumentException(String.Format("'{0}' is an invalid command.", command));
                }
            }
        }

        public void DeployTo(IPlanet planet, int landingPositionX, int landingPositionY, Movement.CardinalDirection landingCardinalDirection)
        {
            this.DeployedTo = planet;

            if (landingPositionX < 0 ||
                landingPositionY < 0 ||
                landingPositionX > planet.Size.Width ||
                landingPositionY > planet.Size.Height)
            {
                throw new ArgumentException("Rover cannot be deployed to a point outside the bounds of the planet.");
            }
            this.Position = new Position(landingPositionX, landingPositionY, landingCardinalDirection);
        }

        public Rover()
        {
            this.Position = new Position(0, 0);
        }
    }
}
