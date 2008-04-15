using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    using NUnit.Framework;


    ///<summary>
    ///Tests the Movement class.
    ///</summary>
    [TestFixture]
    public class MovementTest
    {
        readonly BoardLocation start;

        ///<summary>
        ///Tests the Movement class.
        ///</summary>
        public MovementTest()
        {
            start.X = 0;
            start.Y = 0;
        }

        ///<summary>
        ///Tests the Movement class.
        ///</summary>
        [Test]
        public void moveFrom()
        {
            Movement move = new Movement(1, 0, false);
            BoardLocation test;
            test = move.moveFrom(start);
            Assert.AreEqual(0, test.X);
            Assert.AreEqual(1, test.Y);
        }
    }
}
