using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    using NUnit.Framework;

    ///<summary>
    ///Tests the Movement class's public properties.
    ///</summary>
    [TestFixture]
    public class ModelPropertiesTest
    {
        BoardLocation zeroLoc;
        BoardLocation eastLoc;
        BoardLocation northLoc;
        BoardLocation westLoc;
        BoardLocation southLoc;
        BoardLocation testLoc;
        BoardVector eastVector;
        BoardVector northVector;
        BoardVector westVector;
        BoardVector southVector;
        BoardVector testVector;
        ///<summary>
        ///Constructor: Sets up the readonly properies.
        ///</summary>
        public ModelPropertiesTest()
        {
            zeroLoc = new BoardLocation();
            zeroLoc.X = 0;
            zeroLoc.Y = 0;

            eastLoc = new BoardLocation();
            eastLoc.X = Int32.MaxValue;
            eastLoc.Y = 0;

            northLoc = new BoardLocation();
            northLoc.X = 0;
            northLoc.Y = Int32.MaxValue;

            westLoc = new BoardLocation();
            westLoc.X = Int32.MinValue;
            westLoc.Y = 0;

            southLoc = new BoardLocation();
            southLoc.X = 0;
            southLoc.Y = Int32.MinValue;

            eastVector = new BoardVector();
            eastVector.Direction = MoveDirection.Right;
            eastVector.Length = Int32.MaxValue;

            northVector = new BoardVector();
            northVector.Direction = MoveDirection.Forward;
            northVector.Length = Int32.MaxValue;

            westVector = new BoardVector();
            westVector.Direction = MoveDirection.Left;
            westVector.Length = Int32.MaxValue;

            southVector = new BoardVector();
            southVector.Direction = MoveDirection.Backward;
            southVector.Length = Int32.MaxValue;
        }

        ///<summary>
        ///Tests the board location addition.
        ///</summary>
        [Test]
        public void BoardLocationAdditionTest()
        {
            testLoc = eastLoc + northLoc;
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testLoc.Y);
            testLoc = westLoc + southLoc;
            Assert.AreEqual(Constants.MIN_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(Constants.MIN_BOARD_DISTANCE, testLoc.Y);
            testLoc = eastLoc + westLoc;
            Assert.AreEqual(zeroLoc, testLoc);
            testLoc = southLoc + northLoc;
            Assert.AreEqual(zeroLoc, testLoc);
        }
        ///<summary>
        ///Tests the BoardVector fromOffset.
        ///</summary>
        [Test]
        public void BoardVectorFromOffsetTest()
        {
            testVector.fromOffset(eastLoc);
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Right, testVector.Direction);

            testVector.fromOffset(northLoc);
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Forward, testVector.Direction);

            testVector.fromOffset(westLoc);
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Left, testVector.Direction);

            testVector.fromOffset(southLoc);
            Assert.AreEqual(Constants.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Backward, testVector.Direction);
        }
        ///<summary>
        ///Tests the BoardVector toOffset.
        ///</summary>
        [Test]
        public void BoardVectorToOffsetTest()
        {
            testVector.Direction = MoveDirection.Right;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(eastLoc, testVector.toOffset());

            testVector.Direction = MoveDirection.Forward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(northLoc, testVector.toOffset());

            testVector.Direction = MoveDirection.Left;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(westLoc, testVector.toOffset());

            testVector.Direction = MoveDirection.Backward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(southLoc, testVector.toOffset());
        }
    }
    ///<summary>
    ///Tests the Movement class.
    ///</summary>
    [TestFixture]
    public class MovementTest
    {
        readonly BoardLocation startLoc;
        BoardLocation testLoc;
        Movement testMove;
        Random random = new Random();

        ///<summary>
        ///Tests the Movement class.
        ///</summary>
        public MovementTest()
        {
            startLoc.X = 0;
            startLoc.Y = 0;
        }

        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation.
        ///</summary>
        [Test]
        public void moveFromBoardLocationX()
        {
            testMove = new Movement(1, 0, false);
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);
            Console.Out.WriteLine("Positive X move passed.");

            testMove = new Movement(-1, 0, false);
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(-1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);
            Console.Out.WriteLine("Negative X move passed.");

            int randomX;
            for (int i = 0; i < 10; i++)
            {
                randomX = random.Next(Constants.MIN_BOARD_DISTANCE,
                    Constants.MAX_BOARD_DISTANCE);
                testMove = new Movement(randomX, 0, false);
                testLoc = startLoc;
                testLoc = testMove.moveFrom(startLoc);
                Assert.AreEqual(randomX, testLoc.X, "X: Random test failed on iteration: " + i);
                Assert.AreEqual(0, testLoc.Y);
            }

            Console.Out.WriteLine("Random X move passed.");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
        [Ignore("Fix the BoardVector implementation")]
        public void moveFromDistancePositiveY()
        {
            testMove = new Movement(0, 10, false);
            testLoc = testMove.moveFrom(startLoc, 1);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(1, testLoc.Y, "Y:Didn't move correct distance");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 5);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(5, testLoc.Y, "Y:Didn't move correct distance");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 20);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(20, testLoc.Y, "Y:Didn't move correct distance");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
        [Ignore ("Fix the BoardVector implementation")]
        public void moveFromDistancePositiveX()
        {
            testMove = new Movement(10, 0, false);
            testLoc = testMove.moveFrom(startLoc, 1);
            Assert.AreEqual(1, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 5);
            Assert.AreEqual(5, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 20);
            Assert.AreEqual(10, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
        }
    }
}