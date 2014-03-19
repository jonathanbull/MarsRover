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
            this.Size = new Size(width, height);
        }

        public void AddObstacle(Obstacle obstacle, int xPoint, int yPoint)
        {
            if (this.Obstacles == null)
            {
                this.Obstacles = new Dictionary<Obstacle, Position>();
            }

            var position = new Position(xPoint, yPoint);

            this.Obstacles.Add(obstacle, position);
        }

        public bool HasObstacleAtPosition(Position position)
        {
            return this.Obstacles != null && this.Obstacles.Any(o => o.Value.X == position.X && o.Value.Y == position.Y);
        }
    }
}
