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
            iPlanet.AddObstacle(new Obstacle(), 0, 0);

            Assert.IsTrue(iPlanet.Obstacles.Any());
        }

        [TestCase(5, 5)]
        public void AddObstacleAtPosition(int xPoint, int yPoint)
        {
            var expectedPosition = new Position(xPoint, yPoint);
            iPlanet.AddObstacle(new Obstacle(), xPoint, yPoint);

            Assert.AreEqual(iPlanet.Obstacles.First().Value, expectedPosition);
        }

        [TestCase(-1, 100)]
        [TestCase(100, -1)]
        public void SetSizeOutOfRange(int width, int height)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => iPlanet.SetSize(width, height));
        }
    }
}
