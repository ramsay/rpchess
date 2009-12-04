//-----------------------------------------------------------------------
// <copyright file="Model.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace chesswar
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;


    /// <summary>
    /// An interface to hold empty board spaces and pieces.
    /// </summary>
    public interface IBoardSpace : IXmlSerializable
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
    public struct BoardVector : IXmlSerializable
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
        public void ReadXml(XmlReader xr)
        {
            //TODO
        }

        /// <summary>
        /// Serializes the BoardVector to Xml.
        /// </summary>
        /// <returns>
        /// XmlDocument containing the serialized instance of this 
        /// BoardVector.
        /// </returns>
        public void WriteXml(XmlWriter xw)
        {
            //TODO
        }
		
		public XmlSchema GetSchema()
		{
			return(null);
		}
    }

    /// <summary>
    /// This is a simple X,Y pair that is used for storing locations and
    /// offsets.
    /// </summary>
    public struct BoardLocation : IXmlSerializable
    {
        /// <summary>
        /// The maximum allowed distance across the board.
        /// </summary>
        public const uint BoardLimit = 65536;

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
        /// IXmlSerializable value equals override
        /// </summary>
        /// <param name="obj">Another IXmlSerializable</param>
        /// <returns>
        /// True if the X and Y values are the same.</returns>
        public bool Equals(IXmlSerializable obj)
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
        /// <returns>The hashcode of this IXmlSerializable.</returns>
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
        public void ReadXml(XmlReader xr)
        {
            //TODO
        }

        /// <summary>
        /// Serializes the BoardLocation to Xml.
        /// </summary>
        /// <returns>
        /// XmlDocument containing the serialized instance of this 
        /// BoardLocation.
        /// </returns>
        public void WriteXml(XmlWriter xw)
        {
			//TODO
        }
		
		public XmlSchema GetSchema()
		{
			return(null);
		}
    }

    /// <summary>
    /// I don't know anymore.
    /// Model-View-Controller design.
    /// </summary>
    public class Model : IXmlSerializable
    {
        struct pid
        {
            public bool white; // int player;
            public int index;

            public pid(bool IsWhite, int Index)
            {
                this.white = IsWhite;
                this.index = Index;
            }
        }

        /// <summary>
        /// It's the whiteRoster.
        /// </summary>
        private List<Piece> whiteRoster;
        public ReadOnlyCollection<Piece> WhiteRoster;
        /// <summary>
        /// It's the blackRoster.
        /// </summary>
        private List<Piece> blackRoster;
        public ReadOnlyCollection<Piece> BlackRoster;

        private int players = 2;

        private pid[,] board;
        ////private Board board; // Superflous for now.

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
            this.whiteRoster = new List<Piece>(8);
            this.blackRoster = new List<Piece>(8);
            this.whiteRoster.Add(new Piece());
            this.WhiteRoster = new ReadOnlyCollection<Piece>(this.whiteRoster);
            this.blackRoster.Add(new Piece());
            this.BlackRoster = new ReadOnlyCollection<Piece>(this.blackRoster);
            this.board = new pid[8,8];
        }

        /// <summary>
        /// Initializes a new instance of the Model class.
        /// </summary>
        /// <param name="whiteRoster">
        /// A List of pieces that will be used by player white.
        /// </param>
        /// <param name="blackRoster">
        /// A List of pieces that will be used by player black.</param>
        /// <param name="Ranks">
        /// The number of rows on the board.
        /// </param>
        /// <param name="Files">
        /// The number of columns on the board.\
        /// </param>
        public Model(
            List<Piece> whiteRoster,
            List<Piece> blackRoster,
            int Ranks,
            int Files)
        {
            this.whiteRoster = whiteRoster;
            this.blackRoster = blackRoster;
            this.board = new pid[Ranks, Files];
        }

        /// <summary>
        /// Take care of any initialization that the class may have.
        /// </summary>
        public void Initialize()
        {
            foreach (Piece p in whiteRoster)
            {
                p.Initialize();
            }

            foreach (Piece p in blackRoster)
            {
                p.Initialize();
            }

            for (int i = board.GetLowerBound(0); i <= board.GetUpperBound(0); i++)
            {
                for (int j = board.GetLowerBound(1); j < board.GetUpperBound(1); j++)
                {
                    board[i, j].index = -1;
                }
            }
        }

        public IBoardSpace this[int row, int col]
        {
            get
            {
                try
                {
                    pid i = board[row, col];
                    if (i.white)
                    {
                        return WhiteRoster[i.index];
                    }
                    else
                    {
                        return BlackRoster[i.index];
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public int Files
        {
            get
            {
                return board.GetUpperBound(1);
            }
        }

        public int Ranks
        {
            get
            {
                return board.GetUpperBound(0);
            }
        }

        public int PlayerCount
        {
            get
            {
                return this.players;
            }
        }

        /// <summary>
        /// Put all the data stored in this class into an XmlDocument
        /// for human readable/editable file storage.
        /// </summary>
        /// <returns>The data in a well formatted XML document</returns>
        public void WriteXml(XmlWriter xw)
        {
			//TODO
        }

        /// <summary>
        /// Load all of the data to this board from a well formatted XML
        /// document.
        /// </summary>
        /// <param name="xml" type="XmlDocument">
        /// An XmlDocument that points to a xml document with the data in a
        /// specific format.</param>
        /// <returns>A Model type-casted as an IXmlSerializable.</returns>
        public void ReadXml(XmlReader xr)
        {
			//TODO
        }
		
		public XmlSchema GetSchema()
		{
			return(null);
		}
    }
}
