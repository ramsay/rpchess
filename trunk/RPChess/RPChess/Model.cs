using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RPChess
{
    enum MoveDirection { Right, FowardRight, Forward, ForwardLeft, 
            Left, BackwardLeft, Backward, BackwardRight };
    enum MoveType { Capture, Movement, Attack };

    struct BoardVector
    {
        public MoveDirection Direction;
        public int Length;

        public BoardLocation toOffset()
        {
            BoardLocation b;
            b.Y = (int)Math.Sin((double)((int)Direction / 4));
            b.X = (int)Math.Cos((double)((int)Direction / 4));
        }
    }

    struct BoardLocation 
    {
        public int X;
        public int Y;
    }

    class Model
    {
        private Board _board;
        public Board GameBoard
        {
            get
            {
                return _board;
            }
        }

        public Model()
        {
            _board = new Board(8,8);
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Board
    {
        public Piece[] TeamWhite
        {
            get
            {
                return TeamWhite;
            }
        }

        public Piece[] TeamBlack
        {
            get
            {
                return TeamBlack;
            }
        }
        public readonly int Width;
        public readonly int Length;

        public Board(int width, int length)
        {
            Width = width;
            Length = length;
            initialize();
        }

        protected void initialize()
        {
        }
    }

    class Piece
    {
        protected int _MAX_HP;
        public int MAX_HP
        {
            get
            {
                return _MAX_HP;
            }
        }
        protected String _name;
        public String Name
        {
            get
            {
                return _name;
            }
        }
        protected Move[] _moveSet;
        public Move[] MoveSet
        {
            get
            {
                return _moveSet;
            }
        }
        private int _HP;
        public int HP
        {
            get
            {
                return _HP;
            }
        }

        public Piece ( String name, int hp, Move[] moveSet )
        {
            _name = name;
            _MAX_HP = hp;
            _moveSet = moveSet;
            initialize();
        }

        public int initialize()
        {
            _HP = MAX_HP;
            foreach (Ability a in abilityList)
            {
                a.initialize();
            }
            return _HP;
        }

        public int takeDamage( uint damage )
        {
            _HP = _HP - (int)damage;
            return _HP;
        }

        public int healHP(uint heal)
        {
            _HP = _HP + (int)heal;
            return _HP;
        }

        public String ToXMLString()
        {
            String repr = "<piece name=\"" + _name +
                               "\"HP=\"" + _MAX_HP +
                               "\"/>\r\n";
            foreach ( Move m in _moveSet )
            {
                repr << m.ToString();
            }
            return repr;
        }
    }

    interface Move
    {
        MoveType getType();
        XmlElement toXML();
        void fromXML(XmlElement xml);
    }

    class Attack : Move
    {
        protected String _name;
        public String Name
        {
            get
            {
                return _name;
            }
        }
        protected int _MAX_POINTS;
        public int MAX_POINTS
        {
            get
            {
                return _MAX_POINTS;
            }
        }   
        protected int _points;
        public int Points
        {
            get
            {
                return _points;
            }
        }

        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int use()
        {
            _points = _points - 1;
            return _points;
        }

        /// <summary>
        /// Resets the points to MAX_POINTS.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int reset()
        {
            _points = _MAX_POINTS;
            return _points;
        }

        public MoveType getType()
        {
            return MoveType.Attack;
        }

        //public virtual XmlElement toXML();
        //public virtual void fromXML(XmlElement xml);
    }

    class AreaOfEffectAbility : Attack
    {
        private int[][] _areaOfEffect;
        public int[][] AreaOfEffect
        {
            get
            {
                return _areaOfEffect;
            }
        }

        public AreaOfEffectAbility(String name, int points, int[][] areaOfEffect)
        {
            _name = name;
            _MAX_POINTS = points;
            _areaOfEffect = areaOfEffect;
            reset();
        }

        public XmlElement toXML()
        {
            XmlElement xml;
            return xml;
        }

        public void fromXML(XmlElement xml)
        {
        }
    }

    class DirectionalAbility : Attack
    {
        private BoardVector _boardVector;
        public BoardVector BoardVector
        {
            get
            {
                return _boardVector;
            }
        }
        private int _damage;
        public int Damage
        {
            get
            {
                return _damage;
            }
        }


        public DirectionalAbility(String name, int points, BoardVector boardVector)
        {
            _name = name;
            _MAX_POINTS = points;
            _boardVector = boardVector;
        }

        public DirectionalAbility(XmlElement xml)
        {
            fromXML(xml);
        }

        public XmlElement toXML()
        {
            XmlElement xml;
            return xml;
        }

        public void fromXML(XmlElement xml)
        {
        }

        public String ToXMLString()
        {
            String repr = "<attack name=\"" + _name +
                "\" direction=\"" + _boardVector.Direction +
                "\" length=" + _boardVector.Length +
                "\" damage=" + _damage +
                "\"/>";
            return repr;
        }
    }
    /// <summary>
    /// Class level summary documentation goes here.</summary>
    /// <remarks>
    /// Longer comments can be associated with a type or member 
    /// through the remarks tag</remarks>
    class Movement : Move
    {
        private readonly MoveDirection Direction;
        private readonly int Length;
        public readonly int Forward;
        public readonly int Right;

        public Movement(int forward, int right)
        {
            Forward = forward;
            Right = right;
        }

        public Movement(MoveDirection direction, int length)
        {
            Direction = direction;
            Length = length;
            int forward = (int)Math.Sin((double)((int)Direction / 4));
            int right = (int)Math.Cos((double)((int)Direction / 4));

            if (length == -1)
            {
                right *= Int32.MaxValue;
                forward *= Int32.MaxValue;
            }
            else
            {
                right *= Length;
                forward *= Length;
            }

            Forward = forward;
            Right = right;
        }

        public Movement(XmlReader xml)
        {
            fromXML(xml);
        }

        public BoardLocation moveFrom(BoardLocation bLoc)
        {
            bLoc.X += Right;
            bLoc.Y += Forward;
            return bLoc;
        }

        public MoveType getType()
        {
            return MoveType.Movement;
        }

        public XmlElement toXML()
        {
            XmlElement xml;
            return xml;
        }

        public void fromXML(XmlElement xml)
        {
        }
    }
}