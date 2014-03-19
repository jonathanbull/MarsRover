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

        public void Command(string commands)
        {
            foreach (char command in commands)
            {
                switch (command)
                {
                    case 'F':
                        this.Position.Y += 1;
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
