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

            switch (this.Position.CardinalHeading)
            {
                case Movement.CardinalHeading.North:
                    this.Position.Point = new Point(this.Position.Point.X, this.Position.Point.Y + gridPoints);
                    break;
                case Movement.CardinalHeading.East:
                    this.Position.Point = new Point(this.Position.Point.X + gridPoints, this.Position.Point.Y);
                    break;
                case Movement.CardinalHeading.South:
                    this.Position.Point = new Point(this.Position.Point.X, this.Position.Point.Y - gridPoints);
                    break;
                case Movement.CardinalHeading.West:
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
                    if (this.Position.CardinalHeading == Movement.CardinalHeading.West)
                    {
                        // Bounds wrapping
                        this.Position.CardinalHeading = Movement.CardinalHeading.North;
                    }
                    else
                    {
                        this.Position.CardinalHeading += 1;
                    }
                    break;
                case Movement.TurningDirection.Left:
                    if (this.Position.CardinalHeading == Movement.CardinalHeading.North)
                    {
                        // Bounds wrapping
                        this.Position.CardinalHeading = Movement.CardinalHeading.West;
                    }
                    else
                    {
                        this.Position.CardinalHeading -= 1;
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

        public void DeployTo(IPlanet planet, int landingPositionX, int landingPositionY, Movement.CardinalHeading landingCardinalHeading)
        {
            this.DeployedTo = planet;

            if (landingPositionX < 0 ||
                landingPositionY < 0 ||
                landingPositionX > planet.Size.Width ||
                landingPositionY > planet.Size.Height)
            {
                throw new ArgumentException("Rover cannot be deployed to a point outside the bounds of the planet.");
            }
            this.Position = new Position(landingPositionX, landingPositionY, landingCardinalHeading);
        }

        public Rover()
        {
            this.Position = new Position(0, 0);
        }
    }
}
