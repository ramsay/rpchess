using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        Constants c = new Constants();
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
            Assert.AreEqual(c.MAX_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(c.MAX_BOARD_DISTANCE, testLoc.Y);
            testLoc = westLoc + southLoc;
            Assert.AreEqual(c.MIN_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(c.MIN_BOARD_DISTANCE, testLoc.Y);
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
            testVector.Length = Int32.MaxValue;
            StringBuilder sb = new StringBuilder();
            for ( int d = 0; d < 8; d++)
            {
                testVector.Direction = (MoveDirection)d;
                testLoc = testVector.toOffset();
                sb.AppendLine("testVector in direction: " + 
                    (MoveDirection)d + " => " + testLoc);
            }
            Console.Out.Write(sb.ToString());
            testVector.Direction = MoveDirection.Right;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(eastLoc, testVector.toOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Forward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(northLoc, testVector.toOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Left;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(westLoc, testVector.toOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Backward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(southLoc, testVector.toOffset(), sb.ToString());
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
            testLoc.X = 1;
            testLoc.Y = 0;
            testMove = new Movement(testLoc, false);
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);

            testMove = new Movement(-1, 0, false);
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(-1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);

            int randomX;
            for (int i = 0; i < 1000; i++)
            {
                randomX = random.Next(Constants.MIN_BOARD_DISTANCE,
                    Constants.MAX_BOARD_DISTANCE);
                testMove = new Movement(randomX, 0, false);
                testLoc = startLoc;
                testLoc = testMove.moveFrom(startLoc);
                Assert.AreEqual(randomX, testLoc.X, "X: Random test failed on iteration: " + i);
                Assert.AreEqual(0, testLoc.Y);
            }

            Console.Out.WriteLine("moveFromBoardLocationX()");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
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
            Assert.AreEqual(10, testLoc.Y, "Y:Didn't move correct distance");
            Console.Out.WriteLine("moveFromDistancePositiveY() passed!");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
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
            Console.Out.WriteLine("moveFromDistancePositiveX() passed!");
        }
        [Test]
        public void initializeTest()
        {
            Movement newMove = new Movement(startLoc);
            testMove = new Movement(startLoc);
            testMove.initialize();
            Assert.AreEqual(newMove, testMove);
            Console.Out.WriteLine("initializeTest() passed!");
        }
        [Test]
        public void toXMLTest()
        {
            
        }
        [Test]
        public void fromXMLTest()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml( 
                "<Movement>" +
                "<BoardLocation>" +
                "<X type=\"int\">1</X>" +
                "<Y type=\"int\">0</Y>" + 
                "</BoardLocation>" +
                "</Movement>");
            Assert.Fail();
            Console.Out.WriteLine("fromXMLTest() passed!");
        }
        [Test]
        public void ToStringTest()
        {
            Assert.Fail();
            Console.Out.WriteLine("toXMLTest() passed!");
        }
    }
}
//#endif