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
        IPlanet iPlanet;

        [SetUp]
        public void SetUp()
        {
            iPlanet = new Planet();
            iPlanet.SetSize(100, 50);

            iRover = new Rover();
            iRover.DeployTo(iPlanet, 0, 0, Movement.CardinalHeading.North);
        }

        [TestCase(0, 0, Movement.CardinalHeading.North, "F", 0, 1, Movement.CardinalHeading.North)]
        [TestCase(0, 0, Movement.CardinalHeading.North, "FFB", 0, 1, Movement.CardinalHeading.North)]
        [TestCase(0, 0, Movement.CardinalHeading.North, "R", 0, 0, Movement.CardinalHeading.East)]
        [TestCase(0, 0, Movement.CardinalHeading.South, "L", 0, 0, Movement.CardinalHeading.East)]
        [TestCase(0, 0, Movement.CardinalHeading.West, "R", 0, 0, Movement.CardinalHeading.North)]
        [TestCase(0, 0, Movement.CardinalHeading.North, "L", 0, 0, Movement.CardinalHeading.West)]
        [TestCase(0, 0, Movement.CardinalHeading.North, "FFRFF", 2, 2, Movement.CardinalHeading.East)]
        [TestCase(0, 50, Movement.CardinalHeading.North, "F", 0, 0, Movement.CardinalHeading.North)]
        [TestCase(100, 0, Movement.CardinalHeading.East, "F", 0, 0, Movement.CardinalHeading.East)]
        public void IssueCommandAndVerifyPosition(
            int startingXPosition,
            int startingYPosition,
            Movement.CardinalHeading startingCardinalHeading,
            string command,
            int expectedXPosition,
            int expectedYPosition,
            Movement.CardinalHeading expectedCardinalHeading)
        {
            iRover.DeployTo(iPlanet, startingXPosition, startingYPosition, startingCardinalHeading);

            var expectedPosition = new Position(expectedXPosition, expectedYPosition, expectedCardinalHeading);

            iRover.Command(command);

            Assert.AreEqual(expectedPosition.Point.X, iRover.Position.Point.X);
        }

        [TestCase("RF", 1, 0)]
        [TestCase("B", 0, 50)]
        [TestCase("LFFF", 100, 0)]
        [TestCase("RFLFFFLF", 1, 3)]
        [TestCase("BLF", 100, 50)]
        public void IssueCommandAndCheckObstacleDetection(string command, int obstacleAtPositionX, int obstacleAtPositionY)
        {
            iPlanet.AddObstacle(new Obstacle(), obstacleAtPositionX, obstacleAtPositionY);

            Assert.Throws<DetectedObstacleException>(() => iRover.Command(command));
        }

        [TestCase("F6BBB")]
        [TestCase(" F ")]
        [TestCase("ABC")]
        [TestCase("012345")]
        [TestCase("FFBB<FFBB")]
        public void IssueErroneousCommand(string command)
        {
            Assert.Throws<ArgumentException>(() => iRover.Command(command));
        }

        [Test]
        public void DeployToPlanet()
        {
            var planet = new Planet();
            planet.SetSize(100, 50);
            iRover.DeployTo(planet, 0, 0, Movement.CardinalHeading.North);
            Assert.IsNotNull(iRover.DeployedTo);
        }

        [TestCase(0, 0, Movement.CardinalHeading.North)]
        [TestCase(10, 0, Movement.CardinalHeading.East)]
        [TestCase(0, 10, Movement.CardinalHeading.South)]
        [TestCase(30, 30, Movement.CardinalHeading.West)]
        public void VerifyDeployedPosition(int landingPointX, int landingPointY, Movement.CardinalHeading landingCardinalHeading)
        {
            var planet = new Planet();
            planet.SetSize(100, 50);
            iRover.DeployTo(planet, landingPointX, landingPointY, landingCardinalHeading);
            
            var expectedPosition = new Position(landingPointX, landingPointY, landingCardinalHeading);
            
            Assert.AreEqual(iRover.Position, expectedPosition);
        }

        [TestCase(-1, 10)]
        [TestCase(10, -1)]
        [TestCase(99999, 10)]
        [TestCase(10, 99999)]
        public void DeployToErroneousPosition(int landingPositionX, int landingPositionY)
        {
            Assert.Throws<ArgumentException>(() => iRover.DeployTo(iPlanet, landingPositionX, landingPositionY, Movement.CardinalHeading.North));
        }
    }
}
