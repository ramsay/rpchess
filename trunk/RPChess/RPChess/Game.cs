using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    /// <summary>
    /// Constants for the game.
    /// </summary>
    public struct Constants
    {
        /// <summary>
        /// The maximum allowed distance across the board.
        /// </summary>
        public const int MAX_BOARD_DISTANCE = 46340;
        /// <summary>
        /// The minimum allowed distance across the board.
        /// </summary>
        public const int MIN_BOARD_DISTANCE = -46340;
    }

    class Game
    {
       private Log _movelog;
       private View _view;
       private Board _board;
       private Controller _controller;

       /****************************************************
        * Default Constructor
        ****************************************************/
       public Game( string[] args )
       {
           // Initialization
           initialize();
       }

       /****************************************************
        * Initialize Members
        ****************************************************/
       protected void initialize( )
       {
           _movelog = new Log();
           _view = new TextView();
           _board = new Board(8,8);
           _controller = new TextController();
       }
       
       public void run()
       {
       }
       
       public void end()
       {
       }
       
        static void Main(String[] args)
        {
            Game chessgame = new Game(args);
            chessgame.run();
        }
    }
}
