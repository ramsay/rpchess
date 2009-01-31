/// <summary>
/// This is the Model code all of the game mechanics are in here.
/// </sumary>
namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// This enum matches words to cardinal directions (Forward, ForwardRight, 
    /// BackwardLeft, ...). This adheres to the standards of trigonometry:
    /// Each direction is the radian angle * 4/PI
    /// </summary>
    public enum MoveDirection
    {
        Right,
        FowardRight,
        Forward,
        ForwardLeft,
        Left,
        BackwardLeft,
        Backward,
        BackwardRight
    }

    /// <summary>
    /// Enum for determining which type of move the class is implementing.
    /// </summary>
    public enum MoveType
    {
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
        Attack
    }

    /// <summary>
    /// The root of all [evil!] RPChess IObjects all classes must implement
    /// this interface.  All other interfaces implement this interface.  
    /// Guarantees a uniform XML Serialization behavior.
    /// </summary>
    public interface IObject
    {
        /// <summary>
        /// Returns the IObject to the state it would be as if the game had not
        /// started.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Makes all IObjects comply to RPChess proprietary XML Serialization.
        /// </summary>
        /// <param name="xml">XmlDocument containing a Serialized IObject</param>
        /// <returns>The Constructed IObject Type-Casted</returns>
        IObject FromXmlDocument(XmlDocument xml);

        /// <summary>
        /// Makes all IObjects comply to RPChess proprietary XML Serialization.
        /// </summary>
        /// <returns>
        /// An xml snippet containing all the properties and fields 
        /// of the IObject
        /// </returns>
        XmlDocument ToXmlDocument();
    }

    /// <summary>
    /// An interface to hold empty board spaces and pieces.
    /// </summary>
    public interface IBoardSpace : IObject
    {
        /// <summary>
        /// Gets a value indicating whether this IBoardSpace is empty.
        /// </summary>
        bool IsEmpty
        {
            get;
        }
    }

    /// <summary>
    /// This is a struct that matches a distance and a direction
    /// for use on board topology.
    /// </summary>
    public struct BoardVector : IObject
    {
        /// <summary>
        /// The direction of the vector, enum.
        /// </summary>
        public MoveDirection Direction;

        /// <summary>
        /// The length of the Vector.
        /// </summary>
        private uint length;

        /// <summary>
        /// Gets or sets the length of the vector.
        /// It can only be positive and maxes at the sqrt(Int32.MaxValue) for 
        /// use of pythagorean theorem.
        /// </summary>
        public int Length
        {
            get
            {
                return (int)this.length;
            }

            set
            {
                if (value > BoardLocation.BoardLimit)
                {
                    this.length = BoardLocation.BoardLimit;
                }
                else if (value < 0)
                {
                    this.length = 0;
                }
                else
                {
                    this.length = (uint)value;
                }
            }
        }

        /// <summary>
        /// Converts the Vector to an offset stored in BoardLocation,
        /// uses Sin and Cos to form the X and Y offsets.
        /// </summary>
        /// <returns type="BoardLocation">
        /// the offset relatively equivalent to this vector</returns>
        public BoardLocation ToOffset()
        {
            BoardLocation b = new BoardLocation();
            double theta = (double)this.Direction * Math.PI / 4.0F;
            double y = Math.Sin(theta) * (double)this.Length;
            b.Y = (int)y;
            double x = Math.Cos(theta) * (double)this.Length;
            b.X = (int)x;
            return b;
        }

        /// <summary>
        /// Creates a vector from an X,Y offset.
        /// </summary>
        /// <param name="offset" type="BoardLocation">
        /// The offset to convert this vector to.</param>
        public void FromOffset(BoardLocation offset)
        {
            this.Length = (int)Math.Sqrt((double)((offset.X * offset.X)
                + (offset.Y * offset.Y)));
            this.Direction = (MoveDirection)(((Math.Atan2(
                (double)offset.Y, (double)offset.X) * 4 / Math.PI) + 8) % 8);
        }

        /// <summary>
        /// Initializes Direction to 0 radians or 
        /// MoveDirection.Right and Length to 0.
        /// </summary>
        public void Initialize()
        {
            this.Length = 0;
            this.Direction = MoveDirection.Right;
        }

        /// <summary>
        /// Initializes a new instance of the BoardVector struct 
        /// from an XmlDocument.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument possibly containing a BoardVector.
        /// </param>
        /// <returns>An EmptySpace reference.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            return (IObject)new BoardLocation();
        }

        /// <summary>
        /// Serializes the BoardVector to Xml.
        /// </summary>
        /// <returns>
        /// XmlDocument containing the serialized instance of this 
        /// BoardVector.
        /// </returns>
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }

    /// <summary>
    /// This is a simple X,Y pair that is used for storing locations and
    /// offsets.
    /// </summary>
    public struct BoardLocation : IObject
    {
        /// <summary>
        /// The maximum allowed distance across the board.
        /// </summary>
        public const uint BoardLimit = 46340;

        /// <summary>
        /// The X value of the BoardLocation.
        /// </summary>
        private int x;

        /// <summary>
        /// The Y value of the BoardLocation
        /// </summary>
        private int y;

        /// <summary>
        /// Initializes a new instance of the BoardLocation struct.
        /// </summary>
        /// <param name="x">Horizontal axis value</param>
        /// <param name="y">Vertical axis value</param>
        public BoardLocation(int x, int y)
        {
            this.x = 0;
            this.y = 0;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X axis(horizontal) offset. 
        /// Positive goes right. The maximum absolute Value is the 
        /// sqrt(Int32.MaxValue).
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (value > (int)BoardLimit)
                {
                    this.x = (int)BoardLimit;
                }
                else if (value < (int)-BoardLimit)
                {
                    this.x = (int)-BoardLimit;
                }
                else
                {
                    this.x = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Y axis(vertical) offset. 
        /// Positive goes up. The maximum absolute Value is the 
        /// sqrt(Int32.MaxValue).
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value > (int)BoardLimit)
                {
                    this.y = (int)BoardLimit;
                }
                else if (value < (int)-BoardLimit)
                {
                    this.y = (int)-BoardLimit;
                }
                else
                {
                    this.y = value;
                }
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
        public static BoardLocation operator +(
            BoardLocation b1,
            BoardLocation b2)
        {
            BoardLocation sum = b1;
            sum.X += b2.X;
            sum.Y += b2.Y;
            return sum;
        }

        /// <summary>
        /// IObject value equals override
        /// </summary>
        /// <param name="obj">Another IObject</param>
        /// <returns>
        /// True if the X and Y values are the same.</returns>
        public bool Equals(IObject obj)
        {
            // the gimmes
            if (obj == null)
            {
                return false;
            }

            if (base.Equals(obj))
            {
                return true;
            }

            ////if (this.GetType() != obj.GetType()) 
            ////    return false;

            BoardLocation b = (BoardLocation)obj;
            return this.X == b.X && this.Y == b.Y;
        }

        /// <summary>
        /// hashcode override
        /// </summary>
        /// <returns>The hashcode of this IObject.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// To string override for easier debugging and testing.
        /// </summary>
        /// <returns>
        /// A string representation of the form: ( int, int )</returns>
        public override string ToString()
        {
            return "( " + this.X + ", " + this.Y + " )";
        }

        /// <summary>
        /// Initializes BoardLocation to [0,0].
        /// </summary>
        public void Initialize()
        {
            this.x = 0;
            this.y = 0;
        }

        /// <summary>
        /// Initializes a new instance of the BoardLocation struct 
        /// from an XmlDocument.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument possibly containing a BoardLocation.
        /// </param>
        /// <returns>An EmptySpace reference.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            return (IObject)new BoardLocation();
        }

        /// <summary>
        /// Serializes the BoardLocation to Xml.
        /// </summary>
        /// <returns>
        /// XmlDocument containing the serialized instance of this 
        /// BoardLocation.
        /// </returns>
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }

    /// <summary>
    /// An EmptySpace placeholder. Returns true to IsEmpty... always.
    /// </summary>
    public sealed class EmptySpace : IBoardSpace
    {
        /// <summary>
        /// The Real EmptySpace, ta da!
        /// </summary>
        private static EmptySpace instance = new EmptySpace();

        /// <summary>
        /// Initializes static members of EmptySpace, nothing to
        /// do here.
        /// </summary>
        ////static EmptySpace()
        ////{
        ////}

        /// <summary>
        /// Gets the EmptySpace Instance.
        /// </summary>
        public static EmptySpace Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this is an EmptySpace
        /// Why, yes... yes, it is! Always true.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Does nothing, here for completeness.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Checks to see if XmlDocument is blank.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument possibly containing an EmptySpace
        /// </param>
        /// <returns>An EmptySpace reference.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            return (IObject)new EmptySpace();
        }

        /// <summary>
        /// Serializes an EmptySpace, wtf?
        /// </summary>
        /// <returns>Blank XmlDocument</returns>
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }

    /// <summary>
    /// I don't know anymore.
    /// Model-View-Controller design.
    /// </summary>
    public class Model : IObject
    {
        /// <summary>
        /// It's the WhiteTeam.
        /// TODO: Make a RPChess.Team instead of Piece[].
        /// </summary>
        private Piece[] whiteTeam;

        /// <summary>
        /// It's the BlackTeam.
        /// TODO: Make a RPChess.Team instead of Piece[].
        /// </summary>
        private Piece[] blackTeam;

        /// <summary>
        /// Initializes a new instance of the Model class.
        /// Default Chessboard, Default White Team, 
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
        public Model()
        {
            this.whiteTeam = new Piece[8];
            this.blackTeam = new Piece[8];
            this.whiteTeam[0] = new Piece(new XmlDocument());
            this.blackTeam[0] = new Piece(new XmlDocument());

            // TODO: Add all pieces (built-ins) to each team.
        }

        /// <summary>
        /// Take care of any initialization that the class may have.
        /// </summary>
        public void Initialize()
        {
            Board.Instance.Initialize();
            this.whiteTeam.Initialize();
            this.blackTeam.Initialize();
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
        /// <returns>A Model type-casted as an IObject.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Model")
            {
                return (IObject)new Model();
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a RPChess.Model");
                return (IObject)new Model();
            }
        }
    }

    /// <summary>
    /// The implementation of the Model interface.
    /// </summary>
    public sealed class Board : IObject
    {
        /// <summary>
        /// The width of the board.
        /// </summary>
        public readonly int Width = 10;

        /// <summary>
        /// The length of the board.
        /// </summary>
        public readonly int Length = 10;

        /// <summary>
        /// Static instance of the Board.
        /// </summary>
        private static Board instance = new Board(); // readonly

        /// <summary>
        /// State of the board.
        /// </summary>
        private IBoardSpace[][] boardState;

        /// <summary>
        /// Initializes static members of the Board class.
        /// Only called once.
        /// </summary>
        static Board()
        {
            ////Initialize();
        }

        /// <summary>
        /// Gets the Instance of the Board.
        /// </summary>
        public static Board Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets access to the BoardState as a 2d array of 
        /// IBoardSpaces.
        /// </summary>
        public IBoardSpace[][] BoardState
        {
            get
            {
                return this.boardState;
            }
        }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        public void Initialize()
        {
            this.boardState = new IBoardSpace[this.Length][];
            for (int row = 0; row < this.Length; row++)
            {
                IBoardSpace[] b = new IBoardSpace[this.Width];
                for (int col = 0; col < this.Width; col++)
                {
                    IBoardSpace s = (IBoardSpace)EmptySpace.Instance;
                }
            }
        }

        /// <summary>
        /// Creates a Board type-casted as an IObject.
        /// TODO: Convert to constructor.
        /// </summary>
        /// <param name="xml">
        /// The xml snipet containing a serialized board.
        /// </param>
        /// <returns>
        /// A Board type-casted as an IObject.
        /// </returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Board")
            {
                // TODO                
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a RPChess.Board");
            }

            return (IObject)new Board();
        }

        /// <summary>
        /// Serializes the Board
        /// </summary>
        /// <returns>
        /// An XmlDocument containing the board state
        /// </returns>
        public XmlDocument ToXmlDocument()
        {
            StringBuilder repr = new StringBuilder();
            repr.AppendLine("<Board ");
            repr.AppendLine("</Board>");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(repr.ToString());
            return xml;
        }
    }

    /// <summary>
    /// This will be essentially an ArraList of Pieces.
    /// </summary>
    public class Team : IObject
    {
        /// <summary>
        /// The required King Piece of the Team.
        /// </summary>
        private Piece king;

        ////private ArrayList _Pieces;

        /// <summary>
        /// Initializes a new instance of the Team class.
        /// </summary>
        public Team()
        {
            this.king = new Piece();
        }

        /// <summary>
        /// Gets the King of the Team.
        /// </summary>
        public Piece King
        {
            get
            {
                return this.king;
            }
        }

        /// <summary>
        /// Initializes each Piece on the team.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Creates a Team type-casted as an IObject.
        /// TODO: Convert to constructor.
        /// </summary>
        /// <param name="xml">
        /// The xml snipet containing a serialized team.
        /// </param>
        /// <returns>
        /// A Team type-casted as an IObject.
        /// </returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Team")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a piece");
            }

            return (IObject)new Team();
        }

        /// <summary>
        /// Serializes the Team instance.
        /// </summary>
        /// <returns>A serialized Team instance as an XmlDocument</returns>
        public XmlDocument ToXmlDocument()
        {
            return new XmlDocument();
        }
    }

    /// <summary>
    /// A class that holds the stats and other data for a
    /// Game Piece that is univeral.
    /// </summary>
    public class Piece : IBoardSpace
    {
        /// <summary>
        /// The maximum amount for the piece
        /// Hit Points. Pseudo constant.
        /// Inheritable, protected int.
        /// </summary>
        private int maxHP;

        /// <summary>
        /// The customizable piece name, this is protected
        /// and inheritable string.
        /// </summary>
        private string name;

        /// <summary>
        /// The internal Move set field.
        /// Holds a list of all the moves
        /// the piece can make.
        /// </summary>
        private IMove[] moveSet;

        /// <summary>
        /// The current Hit Points of the Piece.
        /// </summary>
        private int hp;

        /// <summary>
        /// Initializes a new instance of the Piece class.
        /// </summary>
        public Piece()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Piece class.
        /// </summary>
        /// <param name="name">Name for piece</param>
        /// <param name="maxHP">Maximum HitPoints</param>
        /// <param name="moveSet">Set of moves</param>
        public Piece(string name, int maxHP, IMove[] moveSet)
        {
            this.name = name;
            this.maxHP = maxHP;
            this.moveSet = moveSet;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the Piece class from an XmlDocument.
        /// </summary>
        /// <param name="xml">XmlDocument of piece data.</param>
        public Piece(XmlDocument xml)
        {
            this._Copy((Piece)this.FromXmlDocument(xml));
        }
        
        /// <summary>
        /// Gets the current HitPoints of the piece.
        /// </summary>
        public int HitPoints
        {
            get
            {
                return this.hp;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Piece is empty, always false.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the Maximum HitPoints of the piece.
        /// </summary>
        public int MAX_HP
        {
            get
            {
                return this.maxHP;
            }
        }

        /// <summary>
        /// Gets name of the piece.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets a set of the piece's available moves.
        /// </summary>
        public IMove[] MoveSet
        {
            get
            {
                return this.moveSet;
            }
        }

        /// <summary>
        /// Initialize any values for the Piece.
        /// Resets the HitPoints to Maximum.
        /// Initializes all Moves in MoveSet.
        /// </summary>
        public void Initialize()
        {
            this.hp = this.MAX_HP;
            if (moveSet.Length > 0)
            {
                foreach (IMove m in this.moveSet)
                {
                    m.Initialize();
                }
            }
        }

        /// <summary>
        /// Reduce the HitPoints by the damage amount.
        /// </summary>
        /// <param name="damage">
        /// A positive amount to decrease this Piece's HitPoints</param>
        /// <returns>
        /// Current HitPoints after damage has been taken.</returns>
        public int TakeDamage(uint damage)
        {
            return this.hp -= (int)damage;
        }

        /// <summary>
        /// Recover HitPoints.
        /// </summary>
        /// <param name="heal">
        /// A positive amount to heal the piece.</param>
        /// <returns>Current HitPoints after healing.</returns>
        public int HealHitPoints(uint heal)
        {
            return this.hp += (int)heal;
        }

        /// <summary>
        /// Initializes all of the Piece memembers
        /// according to data placed in a well formed
        /// Xml element.
        /// </summary>
        /// <param name="xml">An xml node containing all the member data.</param>
        /// <returns>A Piece type-casted as an IObject.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Piece")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not a piece");
            }

            return (IObject)new Piece();
        }

        /// <summary>
        /// Write all the piece data to a well formatted 
        /// XML document for human readable storage.
        /// </summary>
        /// <returns>An XmlDocument</returns>
        public XmlDocument ToXmlDocument()
        {
            StringBuilder repr = new StringBuilder();
            repr.AppendLine("<piece name=\"" + this.name +
            "\"HitPoints=\"" + this.maxHP +
            "\"/>\r\n");
            foreach (IMove m in this.moveSet)
            {
                repr.Append(m.ToString());
            }

            repr.AppendLine("</Piece>");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(repr.ToString());
            return xml;
        }

        /// <summary>
        /// Copy Constructor.
        /// Might not be a good idea.
        /// </summary>
        /// <param name="other">A piece to copy here</param>
        protected void _Copy(Piece other)
        {
            // copy attributes.
        }
    }

    /// <summary>
    /// An interface for the different actions a Piece can do.
    /// </summary>
    public interface IMove : IObject
    {
        /// <summary>
        /// Gets the Type property of a Move
        /// </summary>
        /// <value>The type of move this IObject is an instance of.</value>
        MoveType Type
        {
            get;
        }
    }

    /// <summary>
    /// A base class for attacks.
    /// <Implements>Move</Implements>
    /// </summary>
    public class Attack : IMove
    {
        /// <summary>
        /// The name of the attack, user customizable.
        /// Inheritable, protected, string.
        /// </summary>
        private string name;

        /// <summary>
        /// The internal storage of MaxPoints.
        /// Inheritable, protected, int.
        /// </summary>
        private int pointsMax;
        
        /// <summary>
        /// Internal representation for Points.
        /// Inheritable, protected, int.
        /// </summary>
        private int points;

        /// <summary>
        /// Initializes a new instance of the Attack class.
        /// Default constructor, Initializes members to zero.
        /// </summary>
        public Attack()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the Attack class.
        /// This is probably not very useful besides testing the members in a 
        /// unit outside all the derived classes.
        /// </summary>
        /// <param name="name">Aesthetic Identifier</param>
        /// <param name="points">Number of times Attack may be "used"</param>
        public Attack(string name, int points)
        {
            this.name = name;
            this.pointsMax = points;
            this.Reset();
        }

        /// <summary>
        /// Gets the Name of the attack for aesthetic purposes.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the Maximum amount of ability points this attack will be 
        /// Initialized to.
        /// </summary>
        public int MaxPoints
        {
            get
            {
                return this.pointsMax;
            }
        }

        /// <summary>
        /// Gets the current amount of ability points that the Attack has left.
        /// </summary>
        public int Points
        {
            get
            {
                return this.points;
            }
        }

        /// <summary>
        /// Gets the MoveType,
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
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int Use()
        {
            this.points--;
            if (this.points < 0)
            {
                this.points = 0;
            }

            return this.points;
        }

        /// <summary>
        /// Use the ability, decreases the Ability's points by 1.
        /// </summary>
        /// <param name="uses">The number of simultaneous uses.</param>
        /// <returns>Returns the remaining points.</returns>
        public int Use(int uses)
        {
            this.points -= uses;
            if (this.points < 0)
            {
                this.points = 0;
            }

            return this.points;
        }

        /// <summary>
        /// Resets the points to MaxPoints.
        /// </summary>
        /// <returns>Returns the remaining points.</returns>
        public int Reset()
        {
            this.points = this.pointsMax;
            return this.points;
        }

        /// <summary>
        /// Resets member data to zero state, usually 
        /// brings Point back to MaxPoints, etc.
        /// </summary>
        public virtual void Initialize()
        {
            this.name = string.Empty;
            this.pointsMax = 0;
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
        /// <returns>An Attack Type-Casted as an IObject.</returns>
        public virtual IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "Attack")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an Attack");
            }

            return (IObject)new Attack();
        }
    }

    /// <summary>
    /// An area of effect attack for wide spread multispace attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class AreaOfEffectAbility : Attack
    {
        /// <summary>
        /// The 2d integer array that represents the AreaOfEffect Shape.
        /// </summary>
        private int[,] areaOfEffect;
        
        /// <summary>
        /// Initializes a new instance of the AreaOfEffectAbility class.
        /// </summary>
        /// <param name="name">Aesthetic identifier.</param>
        /// <param name="points">
        /// How many times the ability may be used.
        /// </param>
        /// <param name="areaOfEffect">
        /// An array of integers expressing the size and shape of the ability.
        /// </param>
        public AreaOfEffectAbility(string name, int points, int[,] areaOfEffect)
            : base(name, points)
        {
            this.areaOfEffect = areaOfEffect;
        }

        /// <summary>
        /// Gets an array of integers expressing the size and shape of the
        /// ability.
        /// </summary>
        public int[,] AreaOfEffect
        {
            get
            {
                return this.areaOfEffect;
            }
        }

        /// <summary>
        /// Resets memeber data to un-used state, affects points.
        /// </summary>
        public override void Initialize()
        {
            Reset();
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
            xml.LoadXml(string.Empty); // TODO
            return xml;
        }

        /// <summary>
        /// Loads the AreaOfEffectAbility from an XML document
        /// </summary>
        /// <param name="xml">
        /// An XML document containing AreaOfEffectAbility member data.
        /// </param>
        /// <returns>An AreaOfEffectAbility Type-Casted as an IObject</returns>
        public override IObject FromXmlDocument(XmlDocument xml)
        {
            this.Initialize();
            if (xml.FirstChild.Name == "AreaOfEffectAbility")
            {
                // TODO
            }
            else
            {
                Console.Error.WriteLine("xmlDocument is not an AreaOfEffectAbility");
            }

            return (IObject)new Attack();
        }
    }

    /// <summary>
    /// Directional attack for ranged attacks.
    /// <Implements>Move</Implements>
    /// <Implements>Attack</Implements>
    /// </summary>
    public class DirectionalAbility : Attack
    {
        /// <summary>
        /// The direction of the attack.
        /// </summary>
        private BoardVector vector;

        /// <summary>
        /// The Damage factor of the ability.
        /// </summary>
        private int damage;

        ////private bool _ranged;

        ////private bool _stopable;

        /// <summary>
        /// Initializes a new instance of the DirectionalAbility class.
        /// </summary>
        /// <param name="name">Fancy name of the DirectionalAbility.</param>
        /// <param name="points">The Ability Poins the ability costs.</param>
        /// <param name="vector">The direction of the DirectionalAbility</param>
        /// <param name="damage">The damage factor of the attack.</param>
        public DirectionalAbility(
            string name,
            int points,
            BoardVector vector,
            int damage)
            : base(name, points)
        {
            this.vector = vector;
            this.damage = damage;
        }

        /// <summary>
        /// Initializes a new instance of the DirectionalAbility class from an
        /// xml document.
        /// </summary>
        /// <param name="xml">
        /// An XmlDocument containing DirectionAbility member data.
        /// </param>
        public DirectionalAbility(XmlDocument xml)
        {
            this.FromXmlDocument(xml);
        }

        /// <summary>
        /// Gets the Direction in which the Ability acts.
        /// </summary>
        public BoardVector Vector
        {
            get
            {
                return this.vector;
            }
        }

        /// <summary>
        /// Gets the amount of damage this Ability gives to the target.
        /// </summary>
        public int Damage
        {
            get
            {
                return this.damage;
            }
        }
        
        /// <summary>
        /// Re-Initializes the DirectionalAbility to a fresh
        /// un-used state.
        /// </summary>
        public override void Initialize()
        {
            Reset();
        }

        /// <summary>
        /// Calls ToXMLstring() inorder to form a more perfect Union.
        /// </summary>
        /// <returns>
        /// An XmlDocument containing DirectionAbility member data.
        /// </returns>
        public override XmlDocument ToXmlDocument()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(this.ToXMLstring());
            return xml;
        }

        /// <summary>
        /// Loads the member data from an xml document.
        /// </summary>
        /// <param name="xml">
        /// Xml document containing correctly formatted Directional Ability
        ///  member data.
        /// </param>
        /// <returns>A Directional Ability Type-Casted as an IObject</returns>
        public override IObject FromXmlDocument(XmlDocument xml)
        {
            return (IObject)new Attack();
        }

        /// <summary>
        /// Forms an Xml Snippet representing this attack.
        /// </summary>
        /// <returns>string that uses xml syntax.</returns>
        public string ToXMLstring()
        {
            string repr = "<attack name=\"" + Name +
            "\" direction=\"" + this.vector.Direction +
            "\" length=\"" + this.vector.Length +
            "\" damage=\"" + this.damage +
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
    public class Movement : IMove
    {
        /// <summary>
        /// This is the inheritable offset (BoardLocation type) that holds an
        /// X, Y offset pair.
        /// </summary>
        private BoardLocation offset;

        ////protected BoardVector vector;

        /// <summary>
        /// This determines whether other pieces may block this 
        /// piece's movement. It is an inheritable boolean.
        /// </summary>
        private bool jump;

        /// <summary>
        /// Initializes a new instance of the Movement class.
        /// Deprecated Constructor.
        /// </summary>
        /// <param name="right">The right offset.</param>
        /// <param name="forward">The forward offset.</param>
        /// <param name="jump">
        /// False if this piece may be blocked by other pieces.
        /// </param>
        [Obsolete("Use the BoardLocation constructor instead.")]
        public Movement(int right, int forward, bool jump)
        {
            this.offset.Y = forward;
            this.offset.X = right;
            ////vector.FromOffset(offset);
            this.jump = jump;
        }

        /// <summary>
        /// Initializes a new instance of the Movement class.
        /// Defaults to Jump = False.
        /// </summary>
        /// <param name="offset">The X, Y offsets of the move.</param>
        public Movement(BoardLocation offset)
        {
            this._setUp(offset, false);
        }

        /// <summary>
        /// Initializes a new instance of the Movement class.
        /// </summary>
        /// <param name="offset">The X,Y offsets of the move.</param>
        /// <param name="jump">
        /// Whether this peice can be blocked by other peices.
        /// </param>
        public Movement(BoardLocation offset, bool jump)
        {
            this._setUp(offset, jump);
        }

        /// <summary>
        /// Initializes a new instance of the Movement class from xml data.
        /// </summary>
        /// <param name="xml">Well formed xml document.</param>
        public Movement(XmlDocument xml)
        {
            this.FromXmlDocument(xml);
        }

        /// <summary>
        /// Gets the Offset of the Movement. It holds how far a piece may 
        /// travel using this Move.
        /// </summary>
        public BoardLocation Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Movement can't be blocked.
        /// </summary>
        public bool Jump
        {
            get
            {
                return this.jump;
            }
        }

        /// <summary>
        /// Gets what type of Move, in this case Movement.
        /// </summary>
        public MoveType Type
        {
            get
            {
                return MoveType.Movement;
            }
        }

        /// <summary>
        /// A simple move operation, given a start point.
        /// </summary>
        /// <param name="brdLoc">The starting point of the travel.</param>
        /// <returns>
        /// A new BoardLocation that is offset from bLoc
        /// </returns>
        public BoardLocation MoveFrom(BoardLocation brdLoc)
        {
            return brdLoc + this.offset;
        }

        /// <summary>
        /// A basic move operation, given a start point and
        /// distance to travel.
        /// </summary>
        /// <param name="brdLoc">The starting point of the travel.</param>
        /// <param name="distance">The number of blocks to move.</param>
        /// <returns>
        /// A new BoardLocation that is offset a distance from bLoc
        /// </returns>
        public BoardLocation MoveFrom(BoardLocation brdLoc, int distance)
        {
            if (!this.jump)
            {
                BoardVector v = new BoardVector();
                v.FromOffset(this.offset);
                if (distance < v.Length)
                {
                    v.Length = distance;
                }

                return brdLoc + v.ToOffset();
            }

            return brdLoc;
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
            "<Jump type=\"bool\">" + this.jump + "</Jump>" +
            "</Movement>");
            return xml;
        }

        /// <summary>
        /// Initializes movement from Xml Element.
        /// </summary>
        /// <param name="xml">
        /// Xml element containing Movement data.
        /// </param>
        /// <returns>The xml snippet that serializes a movement.</returns>
        public IObject FromXmlDocument(XmlDocument xml)
        {
            this.offset = new BoardLocation(0, 0);
            this.jump = false;
            if (xml.FirstChild.Name == "Movement")
            {
                foreach (XmlNode kid in xml.FirstChild.ChildNodes)
                {
                    switch (kid.Name.ToLower())
                    {
                        case "forward":
                        case "y":
                        case "row":
                            this.offset.Y = Int32.Parse(kid.InnerText);
                            Console.Error.WriteLine("Xml element: " + xml.ToString() +
                                    " is malformed. But will be " +
                                    "fixed after saving.");
                            break;
                        case "right":
                        case "x":
                        case "column":
                            this.offset.X = Int32.Parse(kid.InnerText);
                            Console.Error.WriteLine("Xml element: " +
                                    xml.ToString() +
                                    " is malformed. But will be " +
                                    "fixed after saving.");
                            break;
                        case "offset":
                            foreach (XmlNode grankid in kid.ChildNodes)
                            {
                                switch (grankid.Name.ToLower())
                                {
                                    case "forward":
                                    case "y":
                                    case "row":
                                        this.offset.Y = Int32.Parse(grankid.InnerText);
                                        break;
                                    case "right":
                                    case "x":
                                    case "column":
                                        this.offset.X = Int32.Parse(grankid.InnerText);
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
                            this.jump = Boolean.Parse(kid.InnerText);
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

            return (IObject)this;
        }

        /// <summary>
        /// Overrides ToString, returns a string of the form:
        /// "RPChess.Movement( X, Y )"
        /// </summary>
        /// <returns>string representation of memember data</returns>
        public override string ToString()
        {
            return base.ToString() + this.offset;
        }

        /// <summary>
        /// Compares the IObject's members.
        /// </summary>
        /// <param name="obj">Another IObject.</param>
        /// <returns>True if IObjects have same members.</returns>
        public bool Equals(IObject obj)
        {
            // the gimmes
            if (obj == null)
            {
                return false;
            }

            if (base.Equals(obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            // the detials
            Movement other = (Movement)obj;
            if (!this.offset.Equals(other.Offset))
            {
                return false;
            }

            if (!this.jump.Equals(other.Jump))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets rid of a warning, returns the base IObject
        /// GetHashCode()
        /// </summary>
        /// <returns>Just returns base.GetHashCode()</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Little useless code snippet that abstracts the constructor code 
        /// from the different constructor overloads.
        /// </summary>
        /// <param name="offset">
        /// The X,Y pair that holds an offset on the board
        /// </param>
        /// <param name="jump">
        /// If true the Movement can't be blocked (Flying)
        /// </param>
        private void _setUp(BoardLocation offset, bool jump)
        {
            this.offset = offset;
            ////vector.FromOffset(offset);
            this.jump = jump;
        }
    }
}
