using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace RPChess
{
    /// <summary>
    /// The root of all [evil!] RPChess objects all classes must implement
    /// this interface.  All other interfaces implement this interface.  
    /// Guarantees a uniform XML Serialization behavior.
    /// </summary>
    public interface Object
    {
        /// <summary>
        /// Returns the Object to the state it would be as if the game had not
        /// started.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Makes all objects comply to RPChess proprietary XML Serialization.
        /// </summary>
        /// <param name="xml"></param>
        Object FromXmlDocument(XmlDocument xml);
        /// <summary>
        /// Makes all objects comply to RPChess proprietary XML Serialization.
        /// </summary>
        XmlDocument ToXmlDocument();
    }
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
    public struct BoardVector : Object
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
        public void Initialize()
        {
        }
        public Object FromXmlDocument(XmlDocument xml)
        {
            return (Object)new BoardVector();
        }
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }
    /// <summary>
    /// This is a simple X,Y pair that is used for storing locations and
    /// offsets.
    /// </summary>
    public struct BoardLocation : Object
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
        /// An easy constructor.
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        public BoardLocation(int x, int y)
        {
            _X = 0;
            _Y = 0;
            X = x;
            Y = y;
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
        /// Object value equals override
        /// </summary>
        /// <param name="obj">Another Object</param>
        /// <returns>
        /// True if the X and Y values are the same.</returns>
        public override bool Equals(object obj)
        {
			// the gimmes
			if (obj == null)
				return false;			
			if (base.Equals(obj))
			    return true;			
			//if (this.GetType() != obj.GetType()) 
			//	return false;
			
			BoardLocation b = (BoardLocation) obj;
			return this.X == b.X && this.Y == b.Y;
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
        public void Initialize()
        {
        }
        public Object FromXmlDocument(XmlDocument xml)
        {
            return (Object)new BoardLocation();
        }
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }
    /// <summary>
    /// An interface to hold empty board spaces and pieces.
    /// </summary>
    public interface BoardSpace : Object
    {
        /// <summary>
        /// Simple property to check if it is an empty space.
        /// </summary>
        bool isEmpty
        {
            get;
        }
    }
    ///<summary>
    /// An EmptySpace placeholder. Returns true to isEmpty... always.
    ///</summary>
    public sealed class EmptySpace : BoardSpace
    {
        static readonly EmptySpace instance = new EmptySpace();
        static EmptySpace()
        {
        }
        public static EmptySpace Instance
        {
            get
            {
                return instance;
            }
        }
        public bool isEmpty
        {
            get
            {
                return true;
            }
        }
        public void Initialize()
        {
        }
        public Object FromXmlDocument(XmlDocument xml)
        {
            return (Object)new EmptySpace();
        }
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }
    /// <summary>
    /// I don't know anymore.
    /// Model-View-Controller design.
    /// </summary>
    public class Model : Object
    {
        private Board _ChessBoard;
        private Piece[] _WhiteTeam;
        private Piece[] _BlackTeam;
        /// <summary>
        /// Default Constructor. Default Chessboard, Default White Team, 
        /// Default Black Team.
        /// The default Chessboard is flat 10 by 10 square.
        /// The default Teams are 4 Pawns, 1 Rook, 1 Bishop, 1 Queen and 1 King
        /// [ , , ,R,Q,K,B, , , ] Black
        /// [ , , ,P,P,P,p, , , ]
        /// [ , , , , , , , , , ]
        /// [ , , , , , , , , , ]
        /// [ , , , , , , , , , ]
        /// [ , , , , , , , , , ]
        /// [ , , , , , , , , , ]
        /// [ , , , , , , , , , ]
        /// [ , , ,P,P,P,P, , , ]
        /// [ , , ,B,Q,K,R, , , ] White
        /// </summary>
        public Model( )
        {
            _ChessBoard = new Board();
            _WhiteTeam = new Piece[8];
            _BlackTeam = new Piece[8];
            _WhiteTeam[0] = new Piece( new XmlDocument() );
            _BlackTeam[0] = new Piece( new XmlDocument() );
            // TODO: Add all pieces (built-ins) to each team.
        }
        /// <summary>
        /// Take care of any initialization that the class may have.
        /// </summary>
        public void Initialize()
        {
            _ChessBoard.Initialize();
            _WhiteTeam.Initialize();
            _BlackTeam.Initialize();
        }
        /// <summary>
        /// Put all the data stored in this class into an XmlDocument
        /// for human readable/editable file storage.
        /// </summary>
        /// <returns>The data in a well formatted XML document</returns>
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
        /// <summary>
        /// Load all of the data to this board from a well formatted XML
        /// document.
        /// </summary>
        /// <param name="xml" type="XmlDocument">
        /// An XmlDocument that points to a xml document with the data in a
        /// specific format.</param>
        public Object FromXmlDocument(XmlDocument xml)
        {
            Initialize();
            if (xml.FirstChild.Name == "Model")
            {
                return (Object)new Model();
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a RPChess.Model");
                return (Object)new Model();
            }
        }
    }
    /// <summary>
    /// The implementation of the Model interface.
    /// </summary>
    public class Board : Object
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
        public BoardSpace[][] BoardState
        {
            get
            {
                return _state;
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
        private BoardSpace[][] _state;
        /// <summary>
        /// Basic constructor.
        /// </summary>
        /// <param name="width">Width of the board.</param>
        /// <param name="length">Length of the board.</param>
        public Board()
        {
            // static 10 For now dynamic later:
            Width = 10; // width;
            Length = 10; // length;
            Initialize();
        }
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        public void Initialize()
        {
            _state = new BoardSpace[Length][];
            for (int row = 0; row < Length; row++ )
            {
                BoardSpace[] b = new BoardSpace[Width];
                for (int col = 0; col < Width; col++)
                {
                    BoardSpace s = (BoardSpace)EmptySpace.Instance;
                }
            }
            TeamWhite.Initialize();
            TeamBlack.Initialize();
        }
        public Object FromXmlDocument(XmlDocument xml)
        {
            Initialize();
            if (xml.FirstChild.Name == "Board")
            {
                //TODO                
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a RPChess.Board");
            }
            return (Object)new Board();
        }
        public XmlDocument ToXmlDocument( )
        {
            StringBuilder repr = new StringBuilder();
            repr.AppendLine("<Board ");
            repr.AppendLine("</Board>");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(repr.ToString());
            return xml;
        }
    }
    public class Team : Object
    {
        private Piece _King;
        // private ArrayList _Pieces;
        public Piece King
        {
            get
            {
                return _King;
            }
        }
        public Team()
        {
            _King = new Piece();
        }
        public void Initialize()
        {
        }
        public Object FromXmlDocument(XmlDocument xml)
        {
            Initialize();
            if (xml.FirstChild.Name == "Team")
            {
                //TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a piece");
            }
            return (Object)new Team();
        }
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }
    /// <summary>
    /// A class that holds the stats and other data for a
    /// Game Piece that is univeral.
    /// </summary>
    public class Piece : BoardSpace
    {
    	/// <summary>
    	/// The maximum amount for the piece
    	/// Hit Points. Pseudo constant.
    	/// Inheritable, protected int.
    	/// </summary>
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
        /// <summary>
        /// The customizable piece name, this is protected
        /// and inheritable string.
        /// </summary>
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
        /// <summary>
        /// The internal Move set field.
        /// Holds a list of all the moves
        /// the piece can make.
        /// </summary>
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
        public Piece()
        { 
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
            Initialize();
        }
        /// <summary>
        /// Constructs a piece from XML.
        /// </summary>
        /// <param name="xml">XmlDocument of piece data.</param>
        public Piece(XmlDocument xml)
        {
            _Copy((Piece)FromXmlDocument(xml));
        }
        protected void _Copy(Piece other)
        {
            // copy attributes.
        }
        /// <summary>
        /// Initialize any values for the Piece.
        /// Resets the HP to Maximum.
        /// Initializes all Moves in MoveSet.
        /// </summary>
        /// <returns>
        /// The max hp that the current hp is reset to.</returns>
        public void Initialize()
        {
            _HP = MAX_HP;
            foreach (Move m in _moveSet)
            {
                m.Initialize();
            }
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
        /// <summary>
        /// Initializes all of the Piece memembers
        /// according to data placed in a well formed
        /// Xml element.
        /// </summary>
        /// <param name="xml">An xml node containing all the member data.</param>
        public Object FromXmlDocument(XmlDocument xml)
        {
        	Initialize();
        	if ( xml.FirstChild.Name == "Piece" )
        	{
        		//TODO
        	}
        	else
        	{
        		Console.Error.WriteLine( "xmlDocument is not a piece" );
        	}
            return (Object)new Piece();
        }
        /// <summary>
        /// Write all the piece data to a well formatted 
        /// XML document for human readable storage.
        /// </summary>
        /// <returns>An XmlDocument</returns>
        public XmlDocument ToXmlDocument()
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
    public interface Move : Object
    {
        /// <summary>
        /// Type property </summary>
        /// <value>
        /// The type of move this object is an instance of.</value>
        MoveType Type
        {
            get;
        }
    }
    /// <summary>
    /// A base class for attacks.
    /// <Implements>Move</Implements>
    /// </summary>
    public class Attack : Move
    {
    	/// <summary>
    	/// The name of the attack, user customizable.
    	/// Inheritable, protected, String.
    	/// </summary>
        protected String _name;
        /// <summary>
        /// The Name of the attack for aesthetic purposes.
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
        }
        /// <summary>
        /// The internal storage of MAX_POINTS.
        /// Inheritable, protected, int.
        /// </summary>
        protected int _MAX_POINTS;
        /// <summary>
        /// The Maximum amount of points/uses this attack
        /// will be Initialized to.
        /// </summary>
        public int MAX_POINTS
        {
            get
            {
                return _MAX_POINTS;
            }
        }
        /// <summary>
        /// Internal representation for Points.
        /// Inheritable, protected, int.
        /// </summary>
        protected int _points;
        /// <summary>
        /// The current amount of points/uses that
        /// the Attack has left.
        /// </summary>
        public int Points
        {
            get
            {
                return _points;
            }
        }
        /// <summary>
        /// Identifies this as a Move of type Attack.
        /// </summary>
        public MoveType Type
        {
            get
            {
                return MoveType.Attack;
            }
        }
        /// <summary>
        /// Default constructor, Initializes members to zero.
        /// </summary>
        public Attack()
        {
        	Initialize();
        }
        /// <summary>
        /// Constructor for Attack, probably not very useful besides
        /// testing the members in a unit outside all the derived classes.
        /// Will also help 
        /// </summary>
        /// <param name="name">Aesthetic Identifier</param>
        /// <param name="points">Number of times Attack may be "used"</param>
        public Attack( String name, int points )
        {
        	_name = name;
        	_MAX_POINTS = points;
        	reset();
        }
        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int use()
        {
            _points = _points - 1;
            if (_points < 0)
            	_points = 0;
            return _points;
        }
        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int use(int uses)
        {
            _points = _points - uses;
            if (_points < 0)
            	_points = 0;
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
        /// <summary>
        /// Resets member data to zero state, usually 
        /// brings Point back to MAX_POINTS, etc.
        /// </summary>
        public virtual void Initialize()
        {
        	_name = "";
        	_MAX_POINTS = 0;
        }
        /// <summary>
        /// Formats member data into an XML document.
        /// Required by all inheriting Classes. Allows for a simple
        /// and loopable Constructor method.  Also eases loading/saving
        /// from file.
        /// </summary>
        /// <returns>
        /// An xml document containing all of the member data of an attack.
        /// </returns>
        public virtual XmlDocument ToXmlDocument()
        {
        	XmlDocument xml = new XmlDocument();
        	return xml;
        }
        /// <summary>
        /// Loads member data from an xml document.
        /// Required by all inheriting Classes. Allows for a simple
        /// and loopable Constructor method.  Also eases loading/saving
        /// from file.
        /// </summary>
        /// <param name="xml">
        /// An xml document containing all of the member data of an attack.
        /// </param>
        public virtual Object FromXmlDocument(XmlDocument xml)
        {
            Initialize();
            if (xml.FirstChild.Name == "Attack")
            {
                //TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an Attack");
            }
            return (Object)new Attack();        	
        }
    }
    /// <summary>
    /// An area of effect attack for wide spread multispace attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class AreaOfEffectAbility : Attack
    {
        private int[,] _areaOfEffect;
        /// <summary>
        /// An array of integers expressing the size and shape of the ability.
        /// </summary>
        public int[,] AreaOfEffect
        {
            get
            {
                return _areaOfEffect;
            }
        }
        /// <summary>
        /// Constructs an AreaOfEffectAbility given all the necessary memember data.
        /// </summary>
        /// <param name="name">Aesthetic identifier.</param>
        /// <param name="points">
        /// How many times the ability may be used.
        /// </param>
        /// <param name="areaOfEffect">
        /// An array of integers expressing the size and shape of the ability.
        /// </param>
        public AreaOfEffectAbility(String name, int points, int[,] areaOfEffect) : base( name, points )
        {
            _areaOfEffect = areaOfEffect;
        }
        /// <summary>
        /// Resets memeber data to un-used state, affects points.
        /// </summary>
        public override void Initialize()
        {
            reset();
        }
        /// <summary>
        /// Useful for saving data to file.
        /// </summary>
        /// <returns>
        /// An xml document containing AreaOfEffectAbility memeber data.
        /// </returns>
        public override XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(""); //TODO
            return xml;
        }
        /// <summary>
        /// Loads the AreaOfEffectAbility from an XML document
        /// </summary>
        /// <param name="xml">
        /// An XML document containing AreaOfEffectAbility member data.
        /// </param>
        public override Object FromXmlDocument(XmlDocument xml)
        {
            Initialize();
            if (xml.FirstChild.Name == "AreaOfEffectAbility")
            {
                //TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an AreaOfEffectAbility");
            }
            return (Object)new Attack();
        }
    }
    /// <summary>
    /// Directional attack for ranged attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class DirectionalAbility : Attack
    {
        private BoardVector _vector;
        /// <summary>
        /// The Direction in which the Ability acts.
        /// </summary>
        public BoardVector Vector
        {
            get
            {
                return _vector;
            }
        }
        private int _damage;
        /// <summary>
        /// The amount of damage this Ability gives to the target.
        /// </summary>
        public int Damage
        {
            get
            {
                return _damage;
            }
        }
        //private bool _ranged;
        //private bool _stopable;
        /// <summary>
        /// Constructs a DirectionalAbility given all of the 
        /// necessary memeber data.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="points"></param>
        /// <param name="vector"></param>
        /// <param name="damage"></param>
        public DirectionalAbility(String name, int points, 
                                  BoardVector vector, int damage) : base( name, points )
        {
            _vector = vector;
            _damage = damage;
        }
        /// <summary>
        /// Re-Initializes the DirectionalAbility to a fresh
        /// un-used state.
        /// </summary>
        public override void Initialize()
        {
            reset();
        }
        /// <summary>
        /// Constructs a DirectionalAbility from an xml document.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument containing DirectionAbility member data.
        /// </param>
        public DirectionalAbility(XmlDocument xml)
        {
            FromXmlDocument(xml);
        }
        /// <summary>
        /// Calls ToXMLString() inorder to form a more perfect Union.
        /// </summary>
        /// <returns>
        /// An XmlDocument containing DirectionAbility member data.
        /// </returns>
        public override XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(ToXMLString());
            return xml;
        }
        /// <summary>
        /// Loads the member data from an xml document.
        /// </summary>
        /// <param name="xml">
        /// Xml document containing correctly formatted Directional Ability
        ///  member data.
        /// </param>
        public override Object FromXmlDocument(XmlDocument xml)
        {
            return (Object) new Attack();
        }
        /// <summary>
        /// Forms an Xml Snippet representing this attack.
        /// </summary>
        /// <returns>String that uses xml syntax.</returns>
        public String ToXMLString()
        {
            String repr = "<attack name=\"" + _name +
                "\" direction=\"" + _vector.Direction +
                "\" length=\"" + _vector.Length +
                "\" damage=\"" + _damage +
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
        /// <summary>
        /// This is the inheritable offset (BoardLocation type) that holds an
        /// X, Y offset pair.
        /// </summary>
        protected BoardLocation _offset;
        /// <summary>
        /// The Offset of the Movement. It holds how far a piece may travel
        /// using this Move.
        /// </summary>
        public BoardLocation Offset
        {
            get
            {
                return _offset;
            }
        }
        //protected BoardVector _vector;
        /// <summary>
        /// This determines whether other pieces may block this 
        /// piece's movement. It is an inheritable boolean.
        /// </summary>
        protected bool _jump;
        /// <summary>
        /// The accessor for the Jump (bool) attribute.
        /// </summary>
        public bool Jump
        {
            get
            {
                return _jump;
            }
        }
        // constructors
        /// <summary>
        /// Deprecated Constructor.
        /// </summary>
        /// <param name="right">The right offset.</param>
        /// <param name="forward">The forward offset.</param>
        /// <param name="jump">
        /// False if this piece may be blocked by other pieces.
        /// </param>
        [Obsolete ("Use the BoardLocation constructor instead.")]
        public Movement(int right, int forward, bool jump)
        {
            _offset.Y = forward;
            _offset.X = right;
            //_vector.fromOffset(_offset);
            _jump = jump;
        }
        /// <summary>
        /// Standard Movement Constructor from a BoardLocation format offset.
        /// Defaults to Jump = False.
        /// </summary>
        /// <param name="offset">The X, Y offsets of the move.</param>
        public Movement(BoardLocation offset)
        {
            _setUp(offset, false);
        }
        /// <summary>
        /// Construct a Movement using a BoardLocation offset
        /// and a jump.
        /// </summary>
        /// <param name="offset">The X,Y offsets of the move.</param>
        /// <param name="jump">
        /// Whether this peice can be blocked by other peices.
        /// </param>
        public Movement(BoardLocation offset, bool jump)
        {
            _setUp(offset, jump);
        }
        /// <summary>
        /// Construct a Movement from xml data.
        /// </summary>
        /// <param name="xml">Well formed xml document.</param>
        public Movement(XmlDocument xml)
        {
            FromXmlDocument(xml);
        }
        private void _setUp(BoardLocation offset, bool jump)
        {
            _offset = offset;
            //_vector.fromOffset(_offset);
            _jump = jump;
        }
        // public methods
        /// <summary>
        /// A simple move operation, given a start point.
        /// </summary>
        /// <param name="bLoc">The starting point of the travel.</param>
        /// <returns>
        /// A new BoardLocation that is offset from bLoc
        /// </returns>
        public BoardLocation moveFrom(BoardLocation bLoc)
        {
            return bLoc + _offset;
        }
        /// <summary>
        /// A basic move operation, given a start point and
        /// distance to travel.
        /// </summary>
        /// <param name="bLoc">The starting point of the travel.</param>
        /// <param name="distance">The number of blocks to move.</param>
        /// <returns>
        /// A new BoardLocation that is offset a distance from bLoc
        /// </returns>
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
        /// <summary>
        /// Returns what type of Move, in this case Movement.
        /// </summary>
        public MoveType Type
        {
            get
            {
                return MoveType.Movement;
            }
        }
        /// <summary>
        /// Does absolutely nothing.
        /// </summary>
        public void Initialize()
        {
        }
        /// <summary>
        /// Forms an XmlNode of a well formed xml representation
        /// of the Movement memeber data.
        /// </summary>
        /// <returns>Well formed Xml Document.</returns>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<Movement><Offset type=\"RPChess.BoardLocation\">" +
			            "<X type=\"int\">" + this.Offset.X + "</X>" +
			            "<Y type=\"int\">" + this.Offset.Y + "</Y>" +
			            "</Offset>" +
			            "<Jump type=\"bool\">" + this._jump + "</Jump>" +
			            "</Movement>");
            return xml;
        }
        /// <summary>
        /// Initializes movement from Xml Element.
        /// </summary>
        /// <param name="xml">
        /// Xml element containing Movement data.
        /// </param>
		public Object FromXmlDocument(XmlDocument xml)
        {
			_offset = new BoardLocation(0, 0);
			_jump = false;
            if (xml.FirstChild.Name == "Movement")
            {
				
				foreach (XmlNode kid in xml.FirstChild.ChildNodes)
				{
					switch ( kid.Name.ToLower() )
					{
					case "forward":
					case "y":
					case "row":
						this._offset.Y = Int32.Parse(kid.InnerText);						
						Console.Error.WriteLine("Xml element: " + xml.ToString() +
						                        " is malformed. But will be " +
						                        "fixed after saving.");
						break;
					case "right":
					case "x":
					case "column":
						this._offset.X = Int32.Parse(kid.InnerText);						
						Console.Error.WriteLine("Xml element: " + 
						                        xml.ToString() +
						                        " is malformed. But will be " +
						                        "fixed after saving.");
						break;
					case "offset":
						foreach (XmlNode grankid in kid.ChildNodes)
						{
							switch ( grankid.Name.ToLower() )
							{
							case "forward":
							case "y":
							case "row":
								this._offset.Y = Int32.Parse(grankid.InnerText);
								break;
							case "right":
							case "x":
							case "column":
								this._offset.X = Int32.Parse(grankid.InnerText);
								break;
							default:
								Console.Error.WriteLine("Xml element: " + 
								                        kid.ToString() +
								                        " is malformed.");
								break;
							}
						}
						break;
					case "jump":
						this._jump = Boolean.Parse(kid.InnerText);
						break;
					default:
						Console.Error.WriteLine("Xml element: " +
						                        xml.ToString() +
						                        " is malformed.");
						break;
					}
				}
            }
			else
			{
				Console.Error.WriteLine("Xml element: " + xml.FirstChild.OuterXml +
					" is not a Movement xml element.");
			}
			return (Object)this;
        }
        // Overriden Object Methods
        /// <summary>
        /// Overrides ToString, returns a String of the form:
        /// "RPChess.Movement( X, Y )"
        /// </summary>
        /// <returns>String representation of memember data</returns>
        public override string ToString()
        {
            return base.ToString() + _offset;
        }
        
        /// <summary>
        /// Compares the object's members.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns>True if objects have same members.</returns>
		public override bool Equals(object obj)
		{
			// the gimmes
			if (obj == null)
				return false;			
			if (base.Equals(obj))
			    return true;
			if (this.GetType() != obj.GetType()) 
				return false;
			
			// the detials
			Movement other = (Movement)obj;
			if ( !( this._offset.Equals(other.Offset) ) )
				return false;
			if ( !(this._jump.Equals(other.Jump) ) )
				return false;
			return true;
		}
		/// <summary>
		/// Gets rid of a warning, returns the base object
		/// GetHashCode()
		/// </summary>
		/// <returns>base.GetHashCode()</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}
