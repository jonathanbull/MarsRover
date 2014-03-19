using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public interface IRover
    {
        Planet DeployedTo { get; set; }

        Position Position { get; set; }

        void Command(string commands);

        void DeployTo(Planet planet);
    }
}