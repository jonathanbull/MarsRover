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
            iPlanet.SetSize(100, 100);
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
        public void AddObstacleAtPosition(int atPointX, int atPointY)
        {
            var expectedPosition = new Position(atPointX, atPointY);
            iPlanet.AddObstacle(new Obstacle(), atPointX, atPointY);

            Assert.AreEqual(iPlanet.Obstacles.First().Value, expectedPosition);
        }

        [TestCase(-1, 10)]
        [TestCase(10, -1)]
        [TestCase(101, 10)]
        [TestCase(10, 101)]
        public void AddObstacleAtErroneousPosition(int atPointX, int atPointY)
        {
            Assert.Throws<ArgumentException>(() => iPlanet.AddObstacle(new Obstacle(), atPointX, atPointY));
        }

        [TestCase(-1, 100)]
        [TestCase(100, -1)]
        public void SetSizeOutOfRange(int width, int height)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => iPlanet.SetSize(width, height));
        }
    }
}
