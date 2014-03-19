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
            iRover.DeployTo(new Planet(100, 50));
        }

        [TestCase(0, 0, Movement.CardinalDirection.North, "F", 0, 1, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "FFB", 0, 1, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "R", 0, 0, Movement.CardinalDirection.East)]
        [TestCase(0, 0, Movement.CardinalDirection.South, "L", 0, 0, Movement.CardinalDirection.East)]
        [TestCase(0, 0, Movement.CardinalDirection.West, "R", 0, 0, Movement.CardinalDirection.North)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "L", 0, 0, Movement.CardinalDirection.West)]
        [TestCase(0, 0, Movement.CardinalDirection.North, "FFRFF", 2, 2, Movement.CardinalDirection.East)]
        [TestCase(0, 50, Movement.CardinalDirection.North, "F", 0, 0, Movement.CardinalDirection.North)]
        public void IssueCommandAndVerifyPosition(
            int startingXPosition,
            int startingYPosition,
            Movement.CardinalDirection startingCardinalDirection,
            string command,
            int expectedXPosition,
            int expectedYPosition,
            Movement.CardinalDirection expectedCardinalDirection)
        {
            iRover.Position = new Position
            {
                X = startingXPosition,
                Y = startingYPosition,
                CardinalDirection = startingCardinalDirection
            };

            var expectedPosition = new Position
            {
                X = expectedXPosition,
                Y = expectedYPosition,
                CardinalDirection = expectedCardinalDirection
            };

            iRover.Command(command);

            Assert.AreEqual(expectedPosition, iRover.Position);
        }

        [Test]
        public void DeployToPlanet()
        {
            iRover.DeployTo(new Planet(50, 50));
            Assert.IsNotNull(iRover.DeployedTo);
        }
    }
}
