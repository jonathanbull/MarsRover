using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests
{
    [TestFixture]
    public class RoverTests
    {
        IRover iRover;

        [SetUp]
        public void SetUp()
        {
            iRover = new Rover();
        }
    }
}
