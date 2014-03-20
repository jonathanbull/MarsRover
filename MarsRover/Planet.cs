using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Planet : IPlanet
    {
        public Size Size { get; set; }

        public IDictionary<Obstacle, Position> Obstacles { get; set; }

        public void SetSize(int width, int height)
        {
            if (width < 1 || height < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.Size = new Size(width, height);
        }

        public void AddObstacle(Obstacle obstacle, int xPoint, int yPoint)
        {
            if (this.Obstacles == null)
            {
                this.Obstacles = new Dictionary<Obstacle, Position>();
            }

            if (xPoint < 0 ||
                yPoint < 0 ||
                xPoint > this.Size.Width ||
                yPoint > this.Size.Height)
            {
                throw new ArgumentException("Obstacle cannot be placed outside the bounds of a planet.");
            }

            var position = new Position(xPoint, yPoint);

            this.Obstacles.Add(obstacle, position);
        }

        public bool HasObstacleAtPosition(Position position)
        {
            return
                this.Obstacles != null &&
                this.Obstacles.Any(o => o.Value.Point.X == position.Point.X && o.Value.Point.Y == position.Point.Y);
        }
    }
}
