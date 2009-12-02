namespace chesswartest
{
	using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;
	using chesswar;
	
	[TestFixture]
	public class DiceTest
	{
		
		public DiceTest()
		{
		}
		
        /*[TestFixtureSetUp]
        public void DiceTestSetup()
        {
        }

        [TestFixtureTearDown]
        public void DiceTestTeardown()
        {
        }*/

		
        [Test]
        public void RollD6Test()
        {
			int t = Dice.RollD6();
			Assert.Greater(7,t);
			Assert.Less(0,t);
        }

        [Test]
        public void RollD20Test()
        {
            int t = Dice.Roll(20);
            Assert.GreaterOrEqual(20, t);
            Assert.Less(1, t);
        }
	}
}
