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
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testLoc.Y);
            testLoc = westLoc + southLoc;
            Assert.AreEqual(BoardLocation.MIN_BOARD_DISTANCE, testLoc.X);
            Assert.AreEqual(BoardLocation.MIN_BOARD_DISTANCE, testLoc.Y);
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
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Right, testVector.Direction);

            testVector.fromOffset(northLoc);
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Forward, testVector.Direction);

            testVector.fromOffset(westLoc);
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testVector.Length);
            Assert.AreEqual(MoveDirection.Left, testVector.Direction);

            testVector.fromOffset(southLoc);
            Assert.AreEqual(BoardLocation.MAX_BOARD_DISTANCE, testVector.Length);
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
        XmlDocument xmlDoc;

        ///<summary>
        ///Constructor sets up common variables.
        ///</summary>
        public MovementTest()
        {
            startLoc = new BoardLocation(0, 0);
            xmlDoc = new XmlDocument();
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation.
        ///</summary>
        [Test]
        public void moveFromBoardLocationX()
        {
            testLoc = new BoardLocation(1, 0);
            testMove = new Movement(testLoc, false);
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);

            testMove = new Movement(new BoardLocation(-1,0));
            testLoc = testMove.moveFrom(startLoc);
            Assert.AreEqual(-1, testLoc.X);
            Assert.AreEqual(0, testLoc.Y);

            int randomX;
            for (int i = 0; i < 1000; i++)
            {
                randomX = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
                testMove = new Movement(new BoardLocation(randomX, 0));
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
            testMove = new Movement(new BoardLocation(0,10));
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
            testLoc = testMove.moveFrom(startLoc, -1);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -5);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -20);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            Console.Out.WriteLine("moveFromDistancePositiveY() passed!");
        }///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
        public void moveFromDistanceNegativeY()
        {
            testMove = new Movement(new BoardLocation(0,-10));
            testLoc = testMove.moveFrom(startLoc, 1);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(-1, testLoc.Y, "Y:Didn't move correct distance");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 5);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(-5, testLoc.Y, "Y:Didn't move correct distance");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 20);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(-10, testLoc.Y, "Y:Didn't move correct distance");
            testLoc = testMove.moveFrom(startLoc, -1);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -5);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -20);
            Assert.AreEqual(0, testLoc.X, "X:Moved in Wrong Direction");
            Assert.AreEqual(0, testLoc.Y, "Y:Shouldn't accept negative lengths.");
            Console.Out.WriteLine("moveFromDistanceNegitiveY() passed!");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
        public void moveFromDistancePositiveX()
        {
            testMove = new Movement(new BoardLocation(10,0));
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
            testLoc = testMove.moveFrom(startLoc, -1);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -5);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -20);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            Console.Out.WriteLine("moveFromDistancePositiveX() passed!");
        }
        ///<summary>
        ///Tests the Movement.moveFrom() using BoardLocation and
        ///distance parameters.
        ///</summary>
        [Test]
        public void moveFromDistanceNegativeX()
        {
            testMove = new Movement(new BoardLocation(-10,0));
            testLoc = testMove.moveFrom(startLoc, 1);
            Assert.AreEqual(-1, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 5);
            Assert.AreEqual(-5, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, 20);
            Assert.AreEqual(-10, testLoc.X, "X:Didn't move correct distance");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = testMove.moveFrom(startLoc, -1);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -5);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            testLoc = startLoc;
            testLoc = testMove.moveFrom(startLoc, -20);
            Assert.AreEqual(0, testLoc.X, "X:Shouldn't accept negative distances.");
            Assert.AreEqual(0, testLoc.Y, "Y:Moved in Wrong Direction");
            Console.Out.WriteLine("moveFromDistanceNegitiveX() passed!");
        }
        ///<summary>
        ///Tests that the Initialize method changes nothing.
        ///</summary>
        [Test]
        public void InitializeTest()
        {
            Movement newMove = new Movement(startLoc);
            testMove = newMove;
            testMove.Initialize();
            Assert.AreEqual(newMove, testMove);
            Console.Out.WriteLine("InitializeTest() passed!");
        }
        ///<summary>
        ///Tests that the Movement class correctly exports it's data to
        ///an XML element.
        ///</summary>
        [Test]
        public void ToXMLTest()
        {
            testMove = new Movement(startLoc);
			string expectedXML =
				    "<Movement>" +
					"<Offset type=\"RPChess.BoardLocation\">" +
					"<X type=\"int\">0</X>" +
					"<Y type=\"int\">0</Y>" +
					"</Offset>" +
					"<Jump type=\"bool\">False</Jump>" +
					"</Movement>";
            Assert.AreEqual(expectedXML, testMove.ToXML().OuterXml );
            testLoc = new BoardLocation();
			bool jump = false; 
            for (int i = 0; i < 100; i++)
            {
                testLoc.X = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
                testLoc.Y = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
				jump =  (i % 2) == 0;
                testMove = new Movement(testLoc, jump);
                expectedXML = "<Movement>" +
				              "<Offset type=\"RPChess.BoardLocation\">" +
				              "<X type=\"int\">" + testLoc.X + "</X>" +
				              "<Y type=\"int\">" + testLoc.Y + "</Y>" +
				              "</Offset>" +
				              "<Jump type=\"bool\">" + jump + "</Jump>" +
				              "</Movement>";
                Assert.AreEqual(expectedXML, testMove.ToXML().OuterXml);
            }
            Console.Out.WriteLine("ToXMLTest() passed!");
        }
        ///<summary>
        ///Tests that the Movment class can load data from XML
        ///correctly.
        ///</summary>
        [Test]
        public void fromXMLTest()
        {
            xmlDoc.LoadXml("<Movement>" +
			               "<Offset type=\"RPChess.BoardLocation\">" +
			               "<X type=\"int\">1</X>" +
			               "<Y type=\"int\">0</Y>" +
			               "</Offset>" +
			               "<Jump type=\"bool\">False</Jump>" +
			               "</Movement>");
            testMove = new Movement( xmlDoc );
            Movement right1 = new Movement(new BoardLocation(1,0));
            Assert.AreEqual(right1, testMove);
            Console.Out.WriteLine("fromXMLTest() passed!");
        }
        ///<summary>
        ///A simple test that returns a string represtentation of the 
        ///Movement object.
        ///</summary>
        [Test]
        public void ToStringTest()
        {
            testMove = new Movement(startLoc);
            Assert.AreEqual("RPChess.Movement( 0, 0 )", testMove.ToString());
            testLoc = new BoardLocation(5, 0);
            Console.Out.WriteLine("ToStringTest() passed!");
        }
        ///<summary>
        ///Test for equivalency operations.
        ///</summary>
        [Test]
        public void equalsTest()
        {
        	Movement newMove = new Movement(startLoc, false);
        	testMove = new Movement(startLoc, false);
        	Assert.IsTrue(testMove.Equals(testMove), 
        	              "x.Equals(x) returned False.");
        	Assert.IsFalse(testMove.Equals(null),
        	              "x.Equals(null) did not return false.");
        	Assert.AreEqual(testMove.Equals(newMove), 
        	                newMove.Equals(testMove),
        	               "x.Equals(y) didn't the same value as y.Equals(x).");
        	Assert.AreEqual(testMove.Equals(newMove), 
        	                newMove.Equals(testMove),
        	               "Successive invocations of x.Equals(y) differed.");
        	int randX, randY;
        	for (int i = 0; i < 50; i++)
        	{
        		randX = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
        		randY = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
        		testMove = new Movement( new BoardLocation( randX, randY ), (i%2==0) );
        		newMove  = new Movement( new BoardLocation( randX, randY ), (i%2==0) );
        		Assert.IsTrue(testMove.Equals(testMove), 
        	              "x.Equals(x) returned False.");
        		Assert.IsFalse(testMove.Equals(null),
        		               "x.Equals(null) did not return false.");
        		Assert.AreEqual(testMove.Equals(newMove),
        		                newMove.Equals(testMove),
        		                "x.Equals(y) didn't the same value as y.Equals(x).");
        		Assert.AreEqual(testMove.Equals(newMove),
        		                newMove.Equals(testMove),
        		                "Successive invocations of x.Equals(y) differed.");
        	}
        	Assert.AreEqual(testMove, newMove,
        	                "Assert.AreEqual() returned false.");
        	Console.Out.WriteLine("equalsTest passed.");
        }
    }

    ///<summary>
    /// Tests the Attack class.
    /// The Attack class is a base class, with only a few real (non-virtual)
    /// members and methods.
    ///</summary>
    [TestFixture]
    public class AttackTest
    {
    	Attack blank = new Attack("",0);
    	Attack a;
        Random random = new Random();
        ///<summary>
        ///Constructor sets up common variables.
        ///</summary>
        public AttackTest()
        {
        }        
        /// <summary>
        /// Tests all the constructor.
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
        	Assert.AreEqual("", blank.Name);
        	Assert.AreEqual(0, blank.MAX_POINTS);
        	Assert.AreEqual(0, blank.Points);
        	String name = "name";
        	int points = 10;
        	a = new Attack(name, points);
        	Assert.AreEqual(name, a.Name);
        	Assert.AreEqual(points, a.MAX_POINTS);
        	Assert.AreEqual(points, a.Points);
        }
        /// <summary>
        /// Test the method use, make sure it respects bounds.
        /// And only acts on the right members.
        /// </summary>
        [Test]
        public void useTest()
        {
        	Assert.AreEqual(0, blank.MAX_POINTS);
        	Assert.AreEqual(0, blank.Points);
        	blank.use();
        	Assert.AreEqual(0, blank.MAX_POINTS,
        	                "MAX_POINTS should not change.");
        	Assert.AreEqual(0, blank.Points, 
        	                "Points shouldn't be less than zero.");
        	
        	String name = "name";
        	int points = 10;
        	a = new Attack(name, points);
        	Assert.AreEqual(points, a.MAX_POINTS);
        	Assert.AreEqual(points, a.Points);
        	a.use();
        	Assert.AreEqual(points, a.MAX_POINTS,        	               
        	                "MAX_POINTS should not change.");
        	Assert.AreEqual(points-1, a.Points,
        	                "Points should decrement one after Attack.use()");
        	a.use(10);
        	Assert.AreEqual(0, a.Points, 
        	                "Points shouldn't be less than zero.");
        	a.use();
        	Assert.AreEqual(0, a.Points, 
        	                "Points shouldn't be less than zero.");
        }
        /// <summary>
        /// Ensure that reset only sets Points to MAX_POINTS and nothing else.
        /// </summary>
        [Test]
        public void resetTest()
        {
        	Assert.AreEqual("", blank.Name);
        	Assert.AreEqual(0, blank.MAX_POINTS);
        	Assert.AreEqual(0, blank.Points);
        	blank.reset();
        	Assert.AreEqual("", blank.Name);
        	Assert.AreEqual(0, blank.MAX_POINTS);
        	Assert.AreEqual(0, blank.Points);
        	
        	String name = "name";
        	int points = 10;
        	a = new Attack(name, points);
        	Assert.AreEqual(name, a.Name);
        	Assert.AreEqual(points, a.MAX_POINTS);
        	Assert.AreEqual(points, a.Points);
        	a.use();
        	// Assume these are true.
        	/*
        	Assert.AreEqual(name, a.Name);
        	Assert.AreEqual(points, a.MAX_POINTS);
        	Assert.IsFalse(points == a.Points);
        	*/
        	a.reset();
        	Assert.AreEqual(points, a.Points,
        	               "Attack.reset() didn't work.");
        	a.reset();
        	Assert.AreEqual(points, a.Points,
        	               "Successive use of Attack.reset() were different.");
        	
        }
    }
    
    ///<summary>
    /// Tests the DirectionalAbility class.
    /// Tests the String and XML methods, as well as the constructors.
    /// Leaves the equality, and actual implementation of the ablility
    /// ingame to a higher class test.
    ///</summary>
    [TestFixture]
    public class DirectionalAbilityTest
    {
    	readonly DirectionalAbility zeroDA;
    	DirectionalAbility testDA;
    	readonly BoardVector zeroBV;
    	BoardVector testBV;
		readonly BoardLocation zeroBL;
		BoardLocation testBL;
    	Random random = new Random();
    	string name;
    	int points;
    	int damage;
    	
    	/// <summary>
    	/// Empty default constructor.
    	/// </summary>
    	public DirectionalAbilityTest()
    	{
    		zeroBV = new BoardVector();
    		zeroBV.Direction = MoveDirection.Right;
    		zeroBV.Length = 0;
    		
    		testBV = new BoardVector();
			
			zeroBL = new BoardLocation(0,0);
			
			testBL = new BoardLocation(0,0);
    		
    		zeroDA = new DirectionalAbility("", 0, zeroBV, 0);
    		
    		random = new Random();
    	}
    	/// <summary>
    	/// Sets up a blank DirectionalAbility and sets basic
    	/// testable DirectionalAbility
    	/// </summary>
    	[TestFixtureSetUp]
    	public void CreateDirectionalAbilities()
    	{
    		BoardVector bv = new BoardVector();
    		bv.Direction = MoveDirection.Forward;
    		bv.Length = 10;
    		testDA  = new DirectionalAbility("test", 5, bv, 1);
    		name = "";
    		points = 0;
    		damage = 0;
    	}
    	/// <summary>
    	/// Tests the default and standard constructors.
    	/// </summary>
    	[Test]
    	public void ConstructorsTest()
    	{
    		Assert.AreEqual("", zeroDA.Name);
    		Assert.AreEqual(zeroBV, zeroDA.Vector);
    		Assert.AreEqual(0, zeroDA.Damage);
    		
			testBV.Direction = MoveDirection.Forward;
			testBV.Length = 10;
    		Assert.AreEqual("test", testDA.Name);
    		Assert.AreEqual(testBV, testDA.Vector);
    		Assert.AreEqual(1, testDA.Damage);
			//TODO: Beef up constructor test.
			System.Console.Write( "DirectionalAbility ConstructorTest passed." );
    	}    	
    	/// <summary>
    	/// Test the Initialize Method.
    	/// </summary>
    	[Test]
    	public void InitializeTest()
    	{
    		zeroDA.Initialize();
    		Assert.AreEqual("", zeroDA.Name);
    		Assert.AreEqual(zeroBV, zeroDA.Vector);
    		Assert.AreEqual(0, zeroDA.Damage);
    		
			testBV.Direction = MoveDirection.Forward;
			testBV.Length = 10;
    		testDA.Initialize();
			Assert.AreEqual("test", testDA.Name);
    		Assert.AreEqual(testBV, testDA.Vector);
    		Assert.AreEqual(1, testDA.Damage);
    	}
    	/// <summary>
    	/// Test the ToString Method.
    	/// </summary>
    	[Test]
    	public void ToStringTest()
    	{
    		//TODO: Decide correct usage here, and beef up.
			String zeroDAString = "RPChess.Model.DirectionalAbility( \"\", ( 0, BoardDirection.Right ), 0, 0, 0)";
			Assert.AreEqual( zeroDAString, zeroDA.ToString() );
			
			String testDAString = "RPChess.Model.DirectionalAbility( \"test\", ( 10, BoardDirection.Forward ), 10, 0, 1)";
			Assert.AreEqual( testDAString, testDA.ToString() );
			
			Console.WriteLine( "DirectionalAbility.ToString Test passed." );
    	}
    	/// <summary>
    	/// Test the ToXML Method.
    	/// </summary>
    	[Test]
    	public void ToXMLTest()
    	{
    		//TODO; modify to properly test DirectionalAbility.ToXML()            
			string expectedXML =
				    "<DirectionalAbility name=\"\">" +
					"<Vector type=\"RPChess.BoardVector\">" +
					"<Length type=\"int\">0</Length>" +
					"<Direction type=\"RPChess.MoveDirection\">Right</Direction>" +
					"</Vector>" +
					"<Damage type=\"int\">0</Damage>" +
					"</DirectionalAbility>";
            Assert.AreEqual(expectedXML, zeroDA.ToXML().OuterXml );
            
            testBV = new BoardVector();
            for (int i = 0; i < 100; i++)
            {
                testBV.Length = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
            	testBV.Direction = (MoveDirection)random.Next( (int)MoveDirection.Right,
            	    (int)MoveDirection.BackwardRight);
                testDA = new DirectionalAbility(name, points, testBV, damage);
                expectedXML =
				    "<DirectionalAbility name=\"" +testDA.Name + "\">" +
					"<Vector type=\"RPChess.BoardVector\">" +
					"<Length type=\"int\">" + testDA.Vector.Length + "</Length>" +
                	"<Direction type=\"RPChess.MoveDirection\">" + testDA.Vector.Direction.ToString() + "</Direction>" +
					"</Vector>" +
					"<Damage type=\"int\">" + testDA.Damage + "</Damage>" +
					"</DirectionalAbility>";
                Assert.AreEqual(expectedXML, testDA.ToXML().OuterXml);
            }
            Console.Out.WriteLine(this.ToString() + ".ToXMLTest() passed!");
    	}    	
    	/// <summary>
    	/// Test the FromXML Method.
    	/// </summary>
    	[Test]
    	public void FromXMLTest()
    	{
    		//TODO: Beef up DirectionalAbility FromXML Test.
			XmlDocument xmlDoc = new XmlDocument();
			String zeroXML =
				    "<DirectionalAbility name=\"\">" +
					"<Vector type=\"RPChess.BoardVector\">" +
					"<Length type=\"int\">0</Length>" +
                	"<Direction type=\"RPChess.MoveDirection\">Right</Direction>" +
					"</Vector>" +
					"<Damage type=\"int\">0</Damage>" +
					"</DirectionalAbility>";
			xmlDoc.LoadXml( zeroXML );
			DirectionalAbility zeroDAfromXml = new DirectionalAbility( xmlDoc );
			Assert.AreEqual( zeroDAfromXml, zeroDA );
			String testXML =
				    "<DirectionalAbility name=\"test\">" +
					"<Vector type=\"RPChess.BoardVector\">" +
					"<Length type=\"int\">10</Length>" +
                	"<Direction type=\"RPChess.MoveDirection\">Forward</Direction>" +
					"</Vector>" +
					"<Damage type=\"int\">1</Damage>" +
					"</DirectionalAbility>";
			xmlDoc.LoadXml( testXML );
			DirectionalAbility testDAfromXml = new DirectionalAbility( xmlDoc );
			Assert.AreEqual( testDAfromXml, testDA );
			
    	}
    }
    
    ///<summary>
    /// Tests the DirectionalAbility class.
    /// Tests the String and XML methods, as well as the constructors.
    /// Leaves the equality, and actual implementation of the ablility
    /// ingame to a higher class test.
    ///</summary>
    [TestFixture]
    public class AreaOfEffectAbilityTest
    {
    	readonly AreaOfEffectAbility blankAoEA;
    	AreaOfEffectAbility testAoEA;
    	int[,] cross;
    	int[,] empty;
    	/// <summary>
    	/// Empty default constructor.
    	/// </summary>
    	public AreaOfEffectAbilityTest()
    	{
    		cross = new int[3,3];
    		for (int i = 0; i < 3; i++)
    		{
    			for (int j = 0; i < 3; i++)
    			{
    				if ( i == j )
    					cross[i,j] = 1;
    				else if ( 3-i == j )
    					cross[i,j] = 1;
    				else
    					cross[i,j] = 0;
    			}
    		}
    		empty = new int[3,3];
    		empty[1,1]=0;
    		blankAoEA = new AreaOfEffectAbility("", 0, empty);
    	}
    	/// <summary>
    	/// Sets up a blank DirectionalAbility and sets basic
    	/// testable DirectionalAbility
    	/// </summary>
    	[TestFixtureSetUp]
    	public void CreateAreaOfEffectAbilities()
    	{
    		blankAoEA.Initialize();
    		testAoEA  = new AreaOfEffectAbility("AreaOfEffect", 0, cross);
    	}
    	/// <summary>
    	/// Tests the default and standard constructors.
    	/// </summary>
    	[Test]
    	public void ConstructorsTest()
    	{
			//TODO: Separate Multidemensional arrays into single dimension arrays or create function for doing it.
    		Assert.AreEqual("", blankAoEA.Name);
    		Assert.AreEqual(empty, blankAoEA.AreaOfEffect);
			
    		Assert.AreEqual("AreaOfEffect", testAoEA.Name);
			Assert.AreEqual(cross.ToString(), testAoEA.AreaOfEffect.ToString());
    	}    	
    	/// <summary>
    	/// Test the Initialize Method.
    	/// </summary>
    	[Test]
    	public void InitializeTest()
    	{
    		blankAoEA.Initialize();
    		Assert.AreEqual("", blankAoEA.Name);
    		Assert.AreEqual(empty, blankAoEA.AreaOfEffect);
    		
    		testAoEA.Initialize();
    		Assert.AreEqual("AreaOfEffect", testAoEA.Name);
    		Assert.AreEqual(cross, testAoEA.AreaOfEffect);
    	}
    	/// <summary>
    	/// Test the ToString Method.
    	/// </summary>
    	[Test]
    	public void ToStringTest()
    	{
			Assert.AreEqual( "AreaOfEffectAbility{ \"\", {0}, 0, 0}", 
			                blankAoEA.ToString() );
			
			Assert.AreEqual( "AreaOfEffectAbility{ \"test\", {{0,1,0},{1,1,1},{0,1,0}}, 1, 1}", 
			                testAoEA.ToString() );
    	}
    	/// <summary>
    	/// Test the ToXML Method.
    	/// </summary>
    	[Test]
    	public void ToXMLTest()
    	{
    		//TODO; Modify to properly test AreaOfEffectAbility.ToXML test and modify.
    		string expectedXML =
				    "<Attack type=\"RPChess.Model.AreaOfEffectAbility\">" +
				    "<Name type=\"String\"></Name>" +
				    "<Points type=\"Integer\">0</Points>" +
    			    "<AreaOfEffect type=\"Integer[][]\" rows=" + 1 + " columns=" + 1 + ">" +
    			    "<row index=\"0\">" +
    			    "<column index=\"0\">0</column>" +
    			    "</row>" +
				    "</Attack>";
			Assert.AreEqual(expectedXML, testAoEA.ToXML().OuterXml );
			
			testAoEA = new AreaOfEffectAbility("Cross", 3, cross);
			expectedXML =
				    "<Movement>" +
					"<Offset type=\"RPChess.BoardLocation\">" +
					"<X type=\"int\">0</X>" +
					"<Y type=\"int\">0</Y>" +
					"</Offset>" +
					"<Jump type=\"bool\">False</Jump>" +
					"</Movement>";
            Assert.AreEqual(expectedXML, testAoEA.ToXML().OuterXml );
            
            /*testLoc = new BoardLocation();
			bool jump = false; 
            for (int i = 0; i < 100; i++)
            {
                testLoc.X = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
                testLoc.Y = random.Next(BoardLocation.MIN_BOARD_DISTANCE,
                    BoardLocation.MAX_BOARD_DISTANCE);
				jump =  (i % 2) == 0;
                testMove = new Movement(testLoc, jump);
                expectedXML = "<Movement>" +
				              "<Offset type=\"RPChess.BoardLocation\">" +
				              "<X type=\"int\">" + testLoc.X + "</X>" +
				              "<Y type=\"int\">" + testLoc.Y + "</Y>" +
				              "</Offset>" +
				              "<Jump type=\"bool\">" + jump + "</Jump>" +
				              "</Movement>";
                Assert.AreEqual(expectedXML, testMove.ToXML().OuterXml);
            }*/
            Console.Out.WriteLine("AreaOfEffectAbility.ToXML() Test passed!");
    	}    	
    	/// <summary>
    	/// Test the FromXML Method.
    	/// </summary>
    	[Test]
    	public void FromXMLTest()
    	{
    		//TODO;
    		Assert.Fail();
    	}
    }
}
