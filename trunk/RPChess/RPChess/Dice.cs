
namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Wraps random.Next(int) with RPG familiar Dice such as D6 or D20. 
    /// </summary>
    public static class Dice
    {
        static private Random random = new Random();

        /// <summary>
        /// Gives a random positive number from 1 to sides.
        /// </summary>
        /// <param name="sides">Upperbound of the number set.</param>
        /// <returns>An integer from 1 to [sides]</returns>
        static public int Roll(uint sides)
        {
            // 3 operations
            return random.Next(1, (int)sides);
        }

        /// <summary>
        /// Simulates a D6 roll.
        /// </summary>
        /// <returns>An integer from 1 to 6</returns>
        static public int RollD6()
        {
            return Roll(6);
        }
    }
}
