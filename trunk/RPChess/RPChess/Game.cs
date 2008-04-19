using System;
using System.Collections.Generic;
using System.Text;


namespace RPChess
{
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
