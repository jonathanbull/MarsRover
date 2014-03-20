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
            iRover.DeployTo(iPlanet, 0, 0, Movement.CardinalDirection.North);
        }

        [TestCase(0, 0, Movement.CardinalDirection.North, "F", 0, 1, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "FFB", 0, 1, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "R", 0, 0, Movement.CardinalDirection.East)]
        [TestCase(0, 0, Movement.CardinalDirection.South, "L", 0, 0, Movement.CardinalDirection.East)]
        [TestCase(0, 0, Movement.CardinalDirection.West, "R", 0, 0, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "L", 0, 0, Movement.CardinalDirection.West)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "FFRFF", 2, 2, Movement.CardinalDirection.East)]
        [TestCase(0, 50, Movement.CardinalDirection.North, "F", 0, 0, Movement.CardinalDirection.North)]
        [TestCase(100, 0, Movement.CardinalDirection.East, "F", 0, 0, Movement.CardinalDirection.East)]
        public void IssueCommandAndVerifyPosition(
            int startingXPosition,
            int startingYPosition,
            Movement.CardinalDirection startingCardinalDirection,
            string command,
            int expectedXPosition,
            int expectedYPosition,
            Movement.CardinalDirection expectedCardinalDirection)
        {
            iRover.DeployTo(iPlanet, startingXPosition, startingYPosition, startingCardinalDirection);

            var expectedPosition = new Position(expectedXPosition, expectedYPosition, expectedCardinalDirection);

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
            iRover.DeployTo(planet, 0, 0, Movement.CardinalDirection.North);
            Assert.IsNotNull(iRover.DeployedTo);
        }

        [TestCase(0, 0, Movement.CardinalDirection.North)]
        [TestCase(10, 0, Movement.CardinalDirection.East)]
        [TestCase(0, 10, Movement.CardinalDirection.South)]
        [TestCase(30, 30, Movement.CardinalDirection.West)]
        public void VerifyDeployedPosition(int landingPointX, int landingPointY, Movement.CardinalDirection landingCardinalDirection)
        {
            var planet = new Planet();
            planet.SetSize(100, 50);
            iRover.DeployTo(planet, landingPointX, landingPointY, landingCardinalDirection);
            
            var expectedPosition = new Position(landingPointX, landingPointY, landingCardinalDirection);
            
            Assert.AreEqual(iRover.Position, expectedPosition);
        }
    }
}
