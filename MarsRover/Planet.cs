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

        public IList<Obstacle> Obstacles { get; set; }

        public void SetSize(int width, int height)
        {
            this.Size = new Size(width, height);
        }

        public void AddObstacle(Obstacle obstacle)
        {

        }
    }
}
