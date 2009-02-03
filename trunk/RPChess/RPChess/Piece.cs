namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    public enum PieceType { King, Queen, Rook, Bishop, Knight, Pawn };

    /// <summary>
    /// A class that holds the stats and other data for a
    /// Game Piece that is univeral.
    /// </summary>
    public class Piece : IBoardSpace
    {
        private PieceType pieceType;

        /// <summary>
        /// Maximum number of this kind of units in army. The final army 
        /// before set-up may not have more units of this type
        /// </summary>
        private uint max;

        /// <summary>
        /// Cost in points per unit of this type.
        /// </summary>
        private uint cost;

        /// <summary>
        /// Maximum squares that unit can be move per movement phase 
        /// (excluding charge)
        /// </summary>
        private uint move;

        /// <summary>
        /// Saving throw. A d6 roll which must be made for unit to survive 
        /// most hits or to charge scary target
        /// </summary>
        private int save;

        /// <summary>
        /// Modifier to melee rolls.
        /// </summary>
        private int melee;

        /// <summary>
        /// The customizable piece name, this is protected
        /// and inheritable string.
        /// </summary>
        private string name;

        /// <summary>
        /// Any special abilities
        /// </summary>
        private List<IMove> specials;

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
        public Piece(
            PieceType pieceType,
            string name, 
            uint max,
            uint cost,
            uint move,
            int save,
            int melee,
            List<IMove> specials)
        {
            this.pieceType = pieceType;
            this.name = name;
            this.max = max;
            this.cost = cost;
            this.save = save;
            this.melee = melee;
            this.specials = specials;
            
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the Piece class from an XmlDocument.
        /// </summary>
        /// <param name="xml">XmlDocument of piece data.</param>
        public Piece(XmlDocument xml)
        {
            if (xml.FirstChild.Name == "Piece")
            {
                foreach (XmlNode kid in xml.FirstChild.ChildNodes)
                {
                    switch (kid.Name.ToLower())
                    {
                        case "name":
                            this.name = kid.InnerText;
                            break;
                        case "max":
                            this.max = UInt32.Parse(kid.InnerText);
                            break;
                        case "cost":
                            this.cost = UInt32.Parse(kid.InnerText);
                            break;
                        case "move":
                            this.move = UInt32.Parse(kid.InnerText);
                            break;
                        case "save":
                            this.save = Int32.Parse(kid.InnerText);
                            break;
                        case "melee":
                            this.melee = Int32.Parse(kid.InnerText);
                            break;
                        case "specials":
                            foreach (XmlNode grankid in kid.ChildNodes)
                            {
                                ////specials.Add(new IMove(grankid));
                            }

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
                Console.Error.WriteLine("xmlDocument is not a piece");
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
        public IMove[] Specials
        {
            get
            {
                return (IMove[])this.specials.ToArray();
            }
        }

        /// <summary>
        /// Initialize any values for the Piece.
        /// Resets the HitPoints to Maximum.
        /// Initializes all Moves in MoveSet.
        /// </summary>
        public void Initialize()
        {
            if (specials != null)
            {
                foreach (IMove m in this.specials)
                {
                    m.Initialize();
                }
            }
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
            repr.AppendLine("<piece type=\"" + this.pieceType + "\">");
            repr.AppendLine("<max>" + this.max + "</max>");
            repr.AppendLine("<cost>" + this.cost + "</cost>");
            repr.AppendLine("<move>" + this.move + "</move>");
            repr.AppendLine("<save>" + this.save + "</save>");
            repr.AppendLine("<melee>" + this.melee + "</melee>");
            repr.AppendLine("<specials>");
            foreach (IMove m in this.specials)
            {
                repr.Append(m.ToXmlDocument().InnerXml);
            }
            repr.AppendLine("</specials>");
            repr.AppendLine("</Piece>");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(repr.ToString());
            return xml;
        }
    }
}
