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

        [TestCase(0, 0, Direction.Cardinal.North, "F", 0, 1, Direction.Cardinal.North)]
        [TestCase(0, 0, Direction.Cardinal.North, "FFB", 0, 1, Direction.Cardinal.North)]
        [TestCase(0, 0, Direction.Cardinal.North, "R", 0, 0, Direction.Cardinal.East)]
        [TestCase(0, 0, Direction.Cardinal.South, "L", 0, 0, Direction.Cardinal.East)]
        [TestCase(0, 0, Direction.Cardinal.West, "R", 0, 0, Direction.Cardinal.North)]
        public void IssueCommandAndVerifyPosition(
            int startingXPosition,
            int startingYPosition,
            Direction.Cardinal startingCardinalDirection,
            string command,
            int expectedXPosition,
            int expectedYPosition,
            Direction.Cardinal expectedCardinalDirection)
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
    }
}
