//-----------------------------------------------------------------------
// <copyright file="ModelTest.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;

    [TestFixture]
    public class ModelTest
    {
        Model testModel;
        XmlDocument xmlDoc;
        public ModelTest()
        {
            testModel = new Model();
        }

        [TestFixtureSetUp]
        public void ModelTestSetup()
        {
            testModel.Initialize();
        }

        [TestFixtureTearDown]
        public void ModelTestTeardown()
        {
        }

        [Test]
        public void DefaultConstructorTest()
        {
            //TODO:
            Assert.Fail("Not implemented!");
        }

        [Test]
        public void InitializeTest()
        {
            //TODO:
            Assert.Fail("Not implemented!");
        }

        [Test]
        public void ToXmlDocumentTest()
        {
            //TODO:
            xmlDoc = testModel.ToXmlDocument();
            Assert.Fail("Not implemented!");
        }

        [Test]
        public void FromXmlDocument()
        {
            //TODO:
            Assert.Fail("Not implemented!");
        }
    }

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
            Assert.AreEqual((int)BoardLocation.BoardLimit, testLoc.X);
            Assert.AreEqual((int)BoardLocation.BoardLimit, testLoc.Y);
            testLoc = westLoc + southLoc;
            Assert.AreEqual(-(int)BoardLocation.BoardLimit, testLoc.X);
            Assert.AreEqual(-(int)BoardLocation.BoardLimit, testLoc.Y);
            testLoc = eastLoc + westLoc;
            Assert.AreEqual(zeroLoc, testLoc);
            testLoc = southLoc + northLoc;
            Assert.AreEqual(zeroLoc, testLoc);
        }
        ///<summary>
        ///Tests the BoardVector FromOffset.
        ///</summary>
        [Test]
        public void BoardVectorFromOffsetTest()
        {
            testVector.FromOffset(eastLoc);
            Assert.AreEqual((int)BoardLocation.BoardLimit, testVector.Length);
            Assert.AreEqual(MoveDirection.Right, testVector.Direction);

            testVector.FromOffset(northLoc);
            Assert.AreEqual((int)BoardLocation.BoardLimit, testVector.Length);
            Assert.AreEqual(MoveDirection.Forward, testVector.Direction);

            testVector.FromOffset(westLoc);
            Assert.AreEqual((int)BoardLocation.BoardLimit, testVector.Length);
            Assert.AreEqual(MoveDirection.Left, testVector.Direction);

            testVector.FromOffset(southLoc);
            Assert.AreEqual((int)BoardLocation.BoardLimit, testVector.Length);
            Assert.AreEqual(MoveDirection.Backward, testVector.Direction);
        }
        ///<summary>
        ///Tests the BoardVector ToOffset.
        ///</summary>
        [Test]
        public void BoardVectorToOffsetTest()
        {
            testVector.Length = Int32.MaxValue;
            StringBuilder sb = new StringBuilder();
            for (int d = 0; d < 8; d++)
            {
                testVector.Direction = (MoveDirection)d;
                testLoc = testVector.ToOffset();
                sb.AppendLine("testVector in direction: " +
                    (MoveDirection)d + " => " + testLoc);
            }
            Console.Out.Write(sb.ToString());
            testVector.Direction = MoveDirection.Right;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(eastLoc, testVector.ToOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Forward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(northLoc, testVector.ToOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Left;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(westLoc, testVector.ToOffset(), sb.ToString());

            testVector.Direction = MoveDirection.Backward;
            testVector.Length = Int32.MaxValue;
            Assert.AreEqual(southLoc, testVector.ToOffset(), sb.ToString());
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

            zeroBL = new BoardLocation(0, 0);

            testBL = new BoardLocation(0, 0);

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
            testDA = new DirectionalAbility("test", 5, bv, 1);
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
            System.Console.Write("DirectionalAbility ConstructorTest passed.");
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
            Assert.AreEqual(zeroDAString, zeroDA.ToString());

            String testDAString = "RPChess.Model.DirectionalAbility( \"test\", ( 10, BoardDirection.Forward ), 10, 0, 1)";
            Assert.AreEqual(testDAString, testDA.ToString());

            Console.WriteLine("DirectionalAbility.ToString Test passed.");
        }
        /// <summary>
        /// Test the ToXmlDocument Method.
        /// </summary>
        [Test]
        public void ToXmlDocumentTest()
        {
            //TODO; modify to properly test DirectionalAbility.ToXmlDocument()            
            string expectedXML =
                    "<DirectionalAbility name=\"\">" +
                    "<Vector type=\"RPChess.BoardVector\">" +
                    "<Length type=\"int\">0</Length>" +
                    "<Direction type=\"RPChess.MoveDirection\">Right</Direction>" +
                    "</Vector>" +
                    "<Damage type=\"int\">0</Damage>" +
                    "</DirectionalAbility>";
            Assert.AreEqual(expectedXML, zeroDA.ToXmlDocument().OuterXml);

            testBV = new BoardVector();
            for (int i = 0; i < 100; i++)
            {
                testBV.Length = random.Next((int)-(int)BoardLocation.BoardLimit,
                    (int)(int)BoardLocation.BoardLimit);
                testBV.Direction = (MoveDirection)random.Next((int)MoveDirection.Right,
                    (int)MoveDirection.BackwardRight);
                testDA = new DirectionalAbility(name, points, testBV, damage);
                expectedXML =
                    "<DirectionalAbility name=\"" + testDA.Name + "\">" +
                    "<Vector type=\"RPChess.BoardVector\">" +
                    "<Length type=\"int\">" + testDA.Vector.Length + "</Length>" +
                    "<Direction type=\"RPChess.MoveDirection\">" + testDA.Vector.Direction.ToString() + "</Direction>" +
                    "</Vector>" +
                    "<Damage type=\"int\">" + testDA.Damage + "</Damage>" +
                    "</DirectionalAbility>";
                Assert.AreEqual(expectedXML, testDA.ToXmlDocument().OuterXml);
            }
            Console.Out.WriteLine(this.ToString() + ".ToXmlDocumentTest() passed!");
        }
        /// <summary>
        /// Test the FromXmlDocument Method.
        /// </summary>
        [Test]
        public void FromXmlDocumentTest()
        {
            //TODO: Beef up DirectionalAbility FromXmlDocument Test.
            XmlDocument xmlDoc = new XmlDocument();
            String zeroXML =
                    "<DirectionalAbility name=\"\">" +
                    "<Vector type=\"RPChess.BoardVector\">" +
                    "<Length type=\"int\">0</Length>" +
                    "<Direction type=\"RPChess.MoveDirection\">Right</Direction>" +
                    "</Vector>" +
                    "<Damage type=\"int\">0</Damage>" +
                    "</DirectionalAbility>";
            xmlDoc.LoadXml(zeroXML);
            DirectionalAbility zeroDAFromXmlDocument = new DirectionalAbility(xmlDoc);
            Assert.AreEqual(zeroDAFromXmlDocument, zeroDA);
            String testXML =
                    "<DirectionalAbility name=\"test\">" +
                    "<Vector type=\"RPChess.BoardVector\">" +
                    "<Length type=\"int\">10</Length>" +
                    "<Direction type=\"RPChess.MoveDirection\">Forward</Direction>" +
                    "</Vector>" +
                    "<Damage type=\"int\">1</Damage>" +
                    "</DirectionalAbility>";
            xmlDoc.LoadXml(testXML);
            DirectionalAbility testDAFromXmlDocument = new DirectionalAbility(xmlDoc);
            Assert.AreEqual(testDAFromXmlDocument, testDA);

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
            cross = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; i < 3; i++)
                {
                    if (i == j)
                        cross[i, j] = 1;
                    else if (3 - i == j)
                        cross[i, j] = 1;
                    else
                        cross[i, j] = 0;
                }
            }
            empty = new int[3, 3];
            empty[1, 1] = 0;
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
            testAoEA = new AreaOfEffectAbility("AreaOfEffect", 0, cross);
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
            Assert.AreEqual("AreaOfEffectAbility{ \"\", {0}, 0, 0}",
                            blankAoEA.ToString());

            Assert.AreEqual("AreaOfEffectAbility{ \"test\", {{0,1,0},{1,1,1},{0,1,0}}, 1, 1}",
                            testAoEA.ToString());
        }
        /// <summary>
        /// Test the ToXmlDocument Method.
        /// </summary>
        [Test]
        public void ToXmlDocumentTest()
        {
            //TODO; Modify to properly test AreaOfEffectAbility.ToXmlDocument test and modify.
            string expectedXML =
                    "<Attack type=\"RPChess.Model.AreaOfEffectAbility\">" +
                    "<Name type=\"String\"></Name>" +
                    "<Points type=\"Integer\">0</Points>" +
                    "<AreaOfEffect type=\"Integer[,]\" rows=" + 1 + " columns=" + 1 + ">" +
                    "<row index=\"0\">" +
                    "<column index=\"0\">0</column>" +
                    "</row>" +
                    "</Attack>";
            Assert.AreEqual(expectedXML, testAoEA.ToXmlDocument().OuterXml);

            testAoEA = new AreaOfEffectAbility("Cross", 3, cross);
            expectedXML =
                    "<Movement>" +
                    "<Offset type=\"RPChess.BoardLocation\">" +
                    "<X type=\"int\">0</X>" +
                    "<Y type=\"int\">0</Y>" +
                    "</Offset>" +
                    "<Jump type=\"bool\">False</Jump>" +
                    "</Movement>";
            Assert.AreEqual(expectedXML, testAoEA.ToXmlDocument().OuterXml);

            /*testLoc = new BoardLocation();
            bool jump = false; 
            for (int i = 0; i < 100; i++)
            {
                testLoc.X = random.Next(-(int)BoardLocation.BoardLimit,
                    (int)BoardLocation.BoardLimit);
                testLoc.Y = random.Next(-(int)BoardLocation.BoardLimit,
                    (int)BoardLocation.BoardLimit);
                jump =  (i % 2) == 0;
                testMove = new Movement(testLoc, jump);
                expectedXML = "<Movement>" +
                              "<Offset type=\"RPChess.BoardLocation\">" +
                              "<X type=\"int\">" + testLoc.X + "</X>" +
                              "<Y type=\"int\">" + testLoc.Y + "</Y>" +
                              "</Offset>" +
                              "<Jump type=\"bool\">" + jump + "</Jump>" +
                              "</Movement>";
                Assert.AreEqual(expectedXML, testMove.ToXmlDocument().OuterXml);
            }*/
            Console.Out.WriteLine("AreaOfEffectAbility.ToXmlDocument() Test passed!");
        }
        /// <summary>
        /// Test the FromXmlDocument Method.
        /// </summary>
        [Test]
        public void FromXmlDocumentTest()
        {
            //TODO;
            Assert.Fail();
        }
    }
}
