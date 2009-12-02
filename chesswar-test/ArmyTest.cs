using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NUnit.Framework;
using chesswar;

namespace chesswartest
{
    [TestFixture]
	public class ArmyTest
	{
        public ArmyTest()
        {
        }

        [Test]
        public void TestArmyXmlSerialization()
        {
            Piece[] parray = new Piece[1];
            parray[0] = new Piece(
                Piece.Identifier.Pawn, 
                "TestPawn", 
                1, 
                1, 
                1, 
                1, 
                1,
                null);
            Army original = new Army(
                "Test",
                1,
                parray,
                1,
                "A test Army");
            XmlWriter xw = XmlWriter.Create("Test.xml");
            XmlSerializer aserial = new XmlSerializer(typeof(Army));
            aserial.Serialize(xw, original);
            xw.Flush();
            xw.Close();
            Army test = new Army();
            XmlReaderSettings rs = new XmlReaderSettings();
            rs.ConformanceLevel = ConformanceLevel.Fragment;
            test.ReadXml(XmlReader.Create("Test.xml", rs));
            Assert.AreEqual(original.Name, test.Name);
            Assert.AreEqual(original.Initiative, test.Initiative);
            Assert.AreEqual(original.Wealth, test.Wealth);
            Assert.AreEqual(original.Description, test.Description);
            Assert.AreEqual(original.Staff.Count, test.Staff.Count);
            foreach (Piece p in original.Staff)
            {
                Assert.IsTrue(test.Staff.Contains(p));
            }
        }

        [Test]
        public void TestArmyDeserialization()
        {
            List<Army> armies = new List<Army>();
            XmlSerializer aserial = new XmlSerializer(typeof(Army));
            XmlReader xr = XmlReader.Create("C:\\home\\Robert\\workspace\\RPChess\\chesswar\\Chesswar Specification\\Armies.xml");
            while(aserial.CanDeserialize(xr))
            {
                armies.Add((Army)aserial.Deserialize(xr));
            }
            Assert.Greater(armies.Count, 0);
        }
	}
}
