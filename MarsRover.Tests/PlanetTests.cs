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
        IPlanet iPlanet;

        [SetUp]
        public void SetUp()
        {
            iPlanet = new Planet();
        }

        [TestCase(100, 100)]
        public void CreatePlanetAndVerifySize(int width, int height)
        {
            var expectedSize = new Size(width, height);

            iPlanet.SetSize(width, height);
            Assert.AreEqual(iPlanet.Size, expectedSize);
        }

        [Test]
        public void AddObstacle()
        {
            iPlanet.AddObstacle(new Obstacle());

            Assert.IsTrue(iPlanet.Obstacles.Any());
        }
    }
}
