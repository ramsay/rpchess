using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RPChess
{
    /// <summary>
    /// This enum matches words to cardinal directions (Forward, ForwardRight, 
    /// BackwardLeft, ...). This adheres to the standards of trigonometry:
    /// Each direction is the radian angle * 4/PI
    /// </summary>
    public enum MoveDirection { 
        /// <summary>
        /// Right = 0
        /// </summary>
        Right, 
        /// <summary>
        /// ForwardRight = 1
        /// </summary>
        FowardRight,
        /// <summary>
        /// Forward = 2
        /// </summary>
        Forward, 
        /// <summary>
        /// ForwardLeft = 3
        /// </summary>
        ForwardLeft, 
        /// <summary>
        /// Left = 4
        /// </summary>
        Left, 
        /// <summary>
        /// BackwardLeft = 5
        /// </summary>
        BackwardLeft, 
        /// <summary>
        /// Backward = 6
        /// </summary>
        Backward, 
        /// <summary>
        /// BackwardRight = 7
        /// </summary>
        BackwardRight };
    /// <summary>
    /// Enum for determining which type of move the class is implementing.
    /// </summary>
    public enum MoveType { 
        /// <summary>
        /// Capture is the standard chess move.
        /// </summary>
        Capture, 
        /// <summary>
        /// Movement handles a pieces movement across the board.
        /// </summary>
        Movement, 
        /// <summary>
        /// Attack has no movement only deals/heals damage to pieces.
        /// </summary>
        Attack };
    /// <summary>
    /// This is a struct that matches a distance and a direction
    /// for use on board topology.
    /// </summary>
    public struct BoardVector
    {
        /// <summary>
        /// The direction of the vector, enum.
        /// </summary>
        public MoveDirection Direction;
        private uint _Length;
        /// <summary>
        /// The length of the vector. It can only be positive and maxes at
        /// the sqrt(Int32.MaxValue) for use of pythagorean theorem.
        /// </summary>
        public int Length
        {
            get
            {
                return (int)_Length;
            }
            set
            {
                if (value > BoardLocation.MAX_BOARD_DISTANCE)
                    _Length = (uint)BoardLocation.MAX_BOARD_DISTANCE;
                else if (value < 0)
                    _Length = 0;
                else
                    _Length = (uint)value;
            }
        }
        /// <summary>
        /// Converts the Vector to an offset stored in BoardLocation,
        /// uses Sin and Cos to form the X and Y offsets.
        /// </summary>
        /// <returns type="BoardLocation">
        /// the offset relatively equivalent to this vector</returns>
        public BoardLocation toOffset()
        {
            BoardLocation b = new BoardLocation();
            double theta = (double)Direction * Math.PI / 4.0F;
            double y = Math.Sin(theta) * (double) Length;
            b.Y = (int)y;
            double x = Math.Cos(theta) * (double) Length;
            b.X = (int)x;
            return b;
        }
        /// <summary>
        /// Creates a vector from an X,Y offset.
        /// </summary>
        /// <param name="offset" type="BoardLocation">
        /// The offset to convert this vector to.</param>
        public void fromOffset(BoardLocation offset)
        {
            Length = (int)Math.Sqrt( (double)( (offset.X * offset.X)
                + (offset.Y * offset.Y) ) );
            Direction = (MoveDirection) 
                ( ( ( Math.Atan2( 
                (double)offset.Y, (double)offset.X) * 4/Math.PI) + 8 )%8);
        }
    }
    /// <summary>
    /// This is a simple X,Y pair that is used for storing locations and
    /// offsets.
    /// </summary>
    public struct BoardLocation 
    {
        /// <summary>
        /// The maximum allowed distance across the board.
        /// </summary>
        public const int MAX_BOARD_DISTANCE = 46340;
        /// <summary>
        /// The minimum allowed distance across the board.
        /// </summary>
        public const int MIN_BOARD_DISTANCE = -46340;
        private int _X;
        /// <summary>
        /// The X axis(horizontal) offset. Positive goes right.
        /// The maximum absolute Value is the sqrt(Int32.MaxValue).
        /// </summary>
        public int X
        {
            get
            {
                return _X;
            }
            set
            {
                if (value > MAX_BOARD_DISTANCE)
                    _X = MAX_BOARD_DISTANCE;
                else if (value < MIN_BOARD_DISTANCE)
                    _X = MIN_BOARD_DISTANCE;
                else
                    _X = value;
            }
        }
        private int _Y;
        /// <summary>
        /// The Y axis(vertical) offset. Positive goes up.
        /// The maximum absolute Value is the sqrt(Int32.MaxValue).
        /// </summary>
        public int Y
        {
            get
            {
                return _Y;
            }
            set
            {
                if (value > MAX_BOARD_DISTANCE)
                    _Y = MAX_BOARD_DISTANCE;
                else if (value < MIN_BOARD_DISTANCE)
                    _Y = MIN_BOARD_DISTANCE;
                else
                    _Y = value;
            }
        }
        /// <summary>
        /// Adds two BoardLocations together.
        /// </summary>
        /// <param name="b1" type="BoardLocation">
        /// One BoardLocation</param>
        /// <param name="b2" type="BoardLocation">
        /// A second BoardLocation.</param>
        /// <returns>The sum BoardLocation</returns>
        public static BoardLocation operator +( BoardLocation b1,
                                                BoardLocation b2 )
        {
            BoardLocation sum = b1;
            sum.X += b2.X;
            sum.Y += b2.Y;
            return sum;
        }
        /// <summary>
        /// An equality comparison.
        /// </summary>
        /// <param name="b1" type="BoardLocation">
        /// One BoardLocation</param>
        /// <param name="b2" type="BoardLocation">
        /// Second BoardLocation</param>
        /// <returns type="bool">
        /// True if the X and Y values are both equal.</returns>
        public static bool operator ==(BoardLocation b1,
                               BoardLocation b2)
        {
            return false;
        }
        /// <summary>
        /// An inequality comparison.
        /// </summary>
        /// <param name="b1" type="BoardLocation">
        /// One BoardLocation</param>
        /// <param name="b2" type="BoardLocation">
        /// Second BoardLocation</param>
        /// <returns type="bool">
        /// False if the X and Y values are both equal.</returns>
        public static bool operator !=(BoardLocation b1,
                                       BoardLocation b2)
        {
            return true;
        }
        /// <summary>
        /// Objet equals override
        /// </summary>
        /// <param name="obj">Another Object</param>
        /// <returns>
        /// True if these are the same object.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// hashcode override
        /// </summary>
        /// <returns>The hashcode of this object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// To String override for easier debugging and testing.
        /// </summary>
        /// <returns>
        /// A string representation of the form: ( int, int )</returns>
        public override string ToString()
        {
            return "( " + X + ", " + Y + " )";
        }
    }
    /// <summary>
    /// The interface Model enforces the Board class to adhere to the
    /// Model-View-Controller design.
    /// </summary>
    public interface Model
    {
        /// <summary>
        /// An array that shows what the current state of the
        /// Board is as far as piece placement.
        /// </summary>
        BoardSpace[] BoardState
        {
            get;
        }
        /// <summary>
        /// Take care of any initialization that the class may have.
        /// </summary>
        void initialize();
        /// <summary>
        /// Put all the data stored in this class into an XmlDocument
        /// for human readable/editable file storage.
        /// </summary>
        /// <returns>The data in a well formatted XML document</returns>
        XmlDocument toXML();
        /// <summary>
        /// Load all of the data to this board from a well formatted XML
        /// document.
        /// </summary>
        /// <param name="xml" type="XmlReader">
        /// An XmlReader that points to a xml document with the data in a
        /// specific format.</param>
        void fromXML(XmlReader xml);
    }
    /// <summary>
    /// The implementation of the Model interface.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The pieces on the White team.
        /// </summary>
        public Piece[] TeamWhite
        {
            get
            {
                return TeamWhite;
            }
        }
        /// <summary>
        /// The pieces on the Black team.
        /// </summary>
        public Piece[] TeamBlack
        {
            get
            {
                return TeamBlack;
            }
        }
        /// <summary>
        /// The width of the board.
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// The length of the board.
        /// </summary>
        public readonly int Length;
        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="width">Width of the board.</param>
        /// <param name="length">Length of the board.</param>
        public Board(int width, int length)
        {
            Width = width;
            Length = length;
            initialize();
        }
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        protected void initialize()
        {
        }
    }
    /// <summary>
    /// An interface to hold empty board spaces and pieces.
    /// </summary>
    public interface BoardSpace
    {
        /// <summary>
        /// Simple property to check if it is an empty space.
        /// </summary>
        bool isEmpty
        {
            get;
        }
    }
    /// <summary>
    /// A class that holds the stats and other data for a
    /// Game Piece that is univeral.
    /// </summary>
    public class Piece
    {
        protected int _MAX_HP;
        /// <summary>
        /// The Maximum HP of the piece.
        /// </summary>
        public int MAX_HP
        {
            get
            {
                return _MAX_HP;
            }
        }
        protected String _name;
        /// <summary>
        /// User defined name of the piece.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
        }
        protected Move[] _moveSet;
        /// <summary>
        /// A set of the piece's available moves.
        /// </summary>
        public Move[] MoveSet
        {
            get
            {
                return _moveSet;
            }
        }
        private int _HP;
        /// <summary>
        /// The current HP of the piece.
        /// </summary>
        public int HP
        {
            get
            {
                return _HP;
            }
        }
        /// <summary>
        /// BoardSpace interface property. For a piece it is
        /// always false.
        /// </summary>
        public bool isEmpty
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="name">Name for piece</param>
        /// <param name="hp">Maximum HP</param>
        /// <param name="moveSet">Set of moves</param>
        public Piece ( String name, int hp, Move[] moveSet )
        {
            _name = name;
            _MAX_HP = hp;
            _moveSet = moveSet;
            initialize();
        }
        /// <summary>
        /// Constructs a piece from XML.
        /// </summary>
        /// <param name="xml">XmlReader of piece data.</param>
        public Piece(XmlReader xml)
        {
            _fromXml(xml);
        }
        /// <summary>
        /// Initialize any values for the Piece.
        /// Resets the HP to Maximum.
        /// Initializes all Moves in MoveSet.
        /// </summary>
        /// <returns>
        /// The max hp that the current hp is reset to.</returns>
        public int initialize()
        {
            _HP = MAX_HP;
            foreach (Move m in _moveSet)
            {
                m.initialize();
            }
            return _HP;
        }
        /// <summary>
        /// Reduce the HP by the damage amount.
        /// </summary>
        /// <param name="damage">
        /// A positive amount to decrease this Piece's hp</param>
        /// <returns>
        /// Current HP after damage has been taken.</returns>
        public int takeDamage( uint damage )
        {
            _HP = _HP - (int)damage;
            return _HP;
        }
        /// <summary>
        /// Recover HP.
        /// </summary>
        /// <param name="heal">
        /// A positive amount to heal the piece.</param>
        /// <returns>Current HP after healing.</returns>
        public int healHP(uint heal)
        {
            _HP = _HP + (int)heal;
            return _HP;
        }
        protected void _fromXml(XmlReader xml)
        {
        }
        /// <summary>
        /// Write all the piece data to a well formatted 
        /// XML document for human readable storage.
        /// </summary>
        /// <returns>An XmlDocument</returns>
        public XmlDocument toXmlDocument()
        {
            StringBuilder repr = new StringBuilder();
            repr.AppendLine( "<piece name=\"" + _name +
                             "\"HP=\"" + _MAX_HP +
                             "\"/>\r\n" );
            foreach ( Move m in _moveSet )
            {
                repr.Append( m.ToString() );
            }
            repr.AppendLine("</Piece>");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(repr.ToString());
            return xml;
        }
    }
    /// <summary>
    /// An interface for the different actions a Piece can do.
    /// </summary>
    public interface Move
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
    /// <summary>
    /// A base class for attacks.
    /// <Implements>Move</Implements>
    /// </summary>
    public abstract class Attack : Move
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
    /// <summary>
    /// An area of effect attack for wide spread multispace attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class AreaOfEffectAbility : Attack
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
    /// <summary>
    /// Directional attack for ranged attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class DirectionalAbility : Attack
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
    public class Movement : Move
    {
        // properties
        protected BoardLocation _offset;
        public BoardLocation Offset
        {
            get
            {
                return _offset;
            }
        }
        //protected BoardVector _vector;
        protected bool _jump;
        // constructors
        [Obsolete ("Use the BoardLocation constructor instead.")]
        public Movement(int right, int forward, bool jump)
        {
            _offset.Y = forward;
            _offset.X = right;
            //_vector.fromOffset(_offset);
            _jump = jump;
        }
        public Movement(BoardLocation offset)
        {
            _setUp(offset, false);
        }
        public Movement(BoardLocation offset, bool jump)
        {
            _setUp(offset, jump);
        }
        public Movement(XmlReader xml)
        {
            //fromXML(xml);
        }
        private void _setUp(BoardLocation offset, bool jump)
        {
            _offset = offset;
            //_vector.fromOffset(_offset);
            _jump = jump;
        }
        // public methods
        public BoardLocation moveFrom(BoardLocation bLoc)
        {
            return bLoc + _offset;
        }
        public BoardLocation moveFrom(BoardLocation bLoc, int distance)
        {
            if (!_jump)
            {
                BoardVector v = new BoardVector();
                v.fromOffset(_offset);
                if (distance < v.Length)
                {
                    v.Length = distance;
                }
                return bLoc + v.toOffset();
            }
            return bLoc;
        }
        // Move Interface methods and properties
        public MoveType Type
        {
            get
            {
                return MoveType.Movement;
            }
        }
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
        // Overriden Object Methods
        public override string ToString()
        {

            return base.ToString() + _offset;
        }
    }
}