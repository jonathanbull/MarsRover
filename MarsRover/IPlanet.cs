using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public interface IPlanet
    {
        Size Size { get; set; }

        IDictionary<Obstacle, Position> Obstacles { get; set; }

        void SetSize(int width, int height);

        void AddObstacle(Obstacle obstacle, int xPoint, int yPoint);

        bool HasObstacleAtPosition(Position position);
    }
}
