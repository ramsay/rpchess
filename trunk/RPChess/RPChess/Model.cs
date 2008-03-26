using System;
using System.Collections.Generic;
using System.Text;

namespace RPChess
{
    class Model
    {
        private Board _board;
        public Model()
        {
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Board
    {
        private Piece[] _TeamWhite, _TeamBlack;
        public Board()
        {
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Piece
    {
        private int _MAX_HP;
        private int _HP;
        private Ability[] _abilityList;
        private String _name;
        private Move[] _moveSet;

        public Piece ( )
        {
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Ability
    {
    }
    
    class Move
    {
    }
}