using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPChess
{
    public static class Dice
    {
        static private Random random = new Random();

        static public int Roll(uint sides)
        {
            // 3 operations
            return random.Next((int)sides);
        }

        static public int RollD6()
        {
            return Roll(6);
        }
    }
}
