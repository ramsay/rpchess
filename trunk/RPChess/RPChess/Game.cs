using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    struct Constants
    {
        public const int MAX_BOARD_DISTANCE = 46340;
        public const int MIN_BOARD_DISTANCE = -46340;
    }

    class Game
    {
       private Log _movelog;
       private View _view;
       private Model _model;
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
           _model = new Model();
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
            //if (args.Length > 0)
                
                //if (args[0].Equals("test", StringComparison.CurrentCultureIgnoreCase))
                {
                    ModelPropertiesTest m = new ModelPropertiesTest();
                    m.BoardLocationAdditionTest();
                    m.BoardVectorFromOffsetTest();
                    m.BoardVectorToOffsetTest();
                }
            //}
            //else
                chessgame.run();
        }
    }
}
