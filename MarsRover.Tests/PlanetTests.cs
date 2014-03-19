using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests
{
    [TestFixture]
    public class PlanetTests
    {
        [TestCase(100, 100)]
        public void CreatePlanetAndVerifySize(int width, int height)
        {
            var expectedSize = new Size(width, height);

            var planet = new Planet();
            planet.SetSize(width, height);
            Assert.AreEqual(planet.Size, expectedSize);
        }
    }
}
