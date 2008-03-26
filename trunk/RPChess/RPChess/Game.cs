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
           _view = new View("text");
           _model = new Model();;
           _controller = new Controller("text");
       }
       
       public void run()
       {
       }
       
       public void end()
       {
       }
    }
}
