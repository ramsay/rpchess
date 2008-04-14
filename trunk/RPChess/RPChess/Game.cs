using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    class Game
    {
       private Log _movelog;
       private View _view;
       private Model _model;
       private Controller _controller;

       /****************************************************
        * Default Constructor
        ****************************************************/
       public Game( int argc, string[] args )
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
           _model = new Model();
           _controller = new TextController();
       }
       
       public void run()
       {
       }
       
       public void end()
       {
       }
       
        static void Main()
        {
            String[] argv = new String[1];
            argv[0] = "Text";
            Game chessgame = new Game(1, argv);
            chessgame.run();
        }
    }
}
