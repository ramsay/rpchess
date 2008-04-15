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
            b.Y = (int)Math.Sin(((double)((int)Direction / 4)) * Math.PI);
            b.X = (int)Math.Cos(((double)((int)Direction / 4)) * Math.PI);
            return b;
        }

        public void fromOffset(BoardLocation offset)
        {
            Length = (int) Math.Sqrt(offset.X ^ 2 + offset.Y ^ 2);
            Direction = (MoveDirection) (Math.Atan2((double)offset.Y, (double)offset.X) * 4/Math.PI) + 2;
        }
    }
    struct BoardLocation 
    {
        public int X;
        public int Y;

        public static BoardLocation operator +( BoardLocation c1,
                                                BoardLocation c2 )
        {
            BoardLocation b = c1;
            b.X += c2.X;
            b.Y += c2.Y;
            return b;
        }
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
            foreach (Move m in _moveSet)
            {
                m.initialize();
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
                repr += m.ToString();
            }
            return repr;
        }
    }

    interface Move
    {
        /// <summary>
        /// Type property </summary>
        /// <value>
        /// The type of move this object is an instance of.</value>
        MoveType Type
        {
            get;
        }
        /// <summary>
        /// This initializes any dynamic variables.</summary>
        void initialize();
        /// <summary>
        /// Exports the properites of the Move.</summary>
        /// <returns>A DTD complient XML fragment.</returns>
        XmlDocument toXML();
        /// <summary>
        /// Loads the properties of the Move from the XML.</summary>
        /// <param name="xml" type="XmlReader">The XML with Move
        /// information.</param>
        void fromXML(XmlReader xml);
    }

    abstract class Attack : Move
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
        public MoveType Type
        {
            get
            {
                return MoveType.Attack;
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

        public abstract void initialize();
        public abstract XmlDocument toXML();
        public abstract void fromXML(XmlReader xml);
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

        public override void initialize()
        {
            reset();
        }

        public override XmlDocument toXML()
        {
            XmlDocument xml = new XmlDocument();
            //xml.LoadXml(toXMLString());
            return xml;
        }

        public override void fromXML(XmlReader xml)
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
        //private bool _ranged;
        //private bool _stopable;

        public DirectionalAbility(String name, int points, 
            BoardVector boardVector, int damage)
        {
            _name = name;
            _MAX_POINTS = points;
            _boardVector = boardVector;
            _damage = damage;
        }

        public override void initialize()
        {
            reset();
        }

        public DirectionalAbility(XmlReader xml)
        {
            fromXML(xml);
        }

        public override XmlDocument toXML()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(toXMLString());
            return xml;
        }

        public override void fromXML(XmlReader xml)
        {
        }

        public String toXMLString()
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
    /// The movement class handles topological calculations.</summary>
    /// <remarks>
    /// A movement is a type of move that allows the Piece to
    /// move from one space to another on the board.
    /// </remarks>
    class Movement : Move
    {
        protected BoardLocation _offset;
        protected BoardVector _vector;
        protected bool _jump;
        public MoveType Type
        {
            get
            {
                return MoveType.Movement;
            }
        }

        public Movement(int forward, int right, bool jump)
        {
            _offset.Y = forward;
            _offset.X = right;
            _vector.fromOffset(_offset);
            _jump = jump;
        }

        public Movement(BoardVector boardVector)
        {
            _vector = boardVector;
            _jump = false;
        }

        public Movement(XmlReader xml)
        {
            //fromXML(xml);
        }

        public BoardLocation moveFrom(BoardLocation bLoc)
        {
            return bLoc + _offset;
        }

        public BoardLocation moveFrom(BoardLocation bLoc, int distance)
        {
            if (!_jump)
            {
                BoardVector v = _vector;
                if (distance < v.Length)
                {
                    v.Length = distance;
                }
                return bLoc + v.toOffset();
            }
            return bLoc;
        }

        // Move Interface methods
        public void initialize()
        {
        }

        public XmlDocument toXML()
        {
            XmlDocument xml = new XmlDocument();
            //xml.LoadXml(toXMLString());
            return xml;
        }

        public void fromXML(XmlReader xml)
        {
        }
    }
}