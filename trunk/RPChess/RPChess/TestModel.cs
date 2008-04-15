using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    using NUnit.Framework;
    
    [TestFixture]
    class MovementTest
    {
        readonly BoardLocation start;
        [Setup]
        public MovementTest()
        {
            start.X = 0;
            start.Y = 0;
        }

        [Test]
        public void moveFrom()
        {
            Movement move = new Movement(1, 0, false);
            BoardLocation test;
            test = move.moveFrom(start);
            Assert(0, test.X);
            Assert(1, test.Y);
        }
    }
}
