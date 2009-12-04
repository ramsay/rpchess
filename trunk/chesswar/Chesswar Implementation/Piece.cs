//-----------------------------------------------------------------------
// <copyright file="Piece.cs" company="BENTwerx">
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
    /// A class that holds the stats and other data for a
    /// Game Piece that is univeral.
    /// </summary>
    public class Piece : IBoardSpace
    {
        /// <summary>
        /// An Enum that gives names to all of the base types for Pieces.  One
        /// might use it to index an array with descriptive keywords instead of
        /// integers.
        /// </summary>
        public enum Identifier {King, Queen, Rook, Bishop, Knight, Pawn };        

        /// <summary>
        /// The piece identifier.
        /// </summary>
        private Identifier id;

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
        public ReadOnlyCollection<IMove> Specials;

        ////private List<IEffects> effects;
        ////public ReadOnlyCollection<IEffects> Effects;

        /// <summary>
        /// Initializes a new instance of the Piece class that has all private
        /// fields set to null or 0.
        /// </summary>
        public Piece()
        {
            this.id = Identifier.Pawn;
            this.cost = 0;
            this.name = null;
            this.max = 0;
            this.melee = 0;
            this.move = 0;
            this.save = 0;
            this.specials = null;
        }

        /// <summary>
        /// Initializes a new instance of the Piece class.
        /// </summary>
        /// <param name="id">The Identifier value of the piece.</param>
        /// <param name="name">A descriptive string</param>
        /// <param name="max">The maximum number of this piece per team</param>
        /// <param name="cost">The cost.</param>
        /// <param name="move"></param>
        /// <param name="save"></param>
        /// <param name="melee"></param>
        /// <param name="specials"></param>
        public Piece(
            Identifier id,
            string name,
            uint max,
            uint cost,
            uint move,
            int save,
            int melee,
            List<IMove> specials)
        {
            this.id = id;
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
			this.ReadXml(XmlReader.Create(xml.InnerXml));
            /*if (xml.FirstChild.Name == "Piece")
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
                                // TODO: Add specials support.
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
            }*/
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

        public Identifier Id
        {
            get
            {
                return this.id;
            }
        }

        public string Symbol
        {
            get
            {
                switch (id)
                {
                    case Identifier.King:
                        return "\u265A";
                    case Identifier.Queen:
                        return "\u265B";
                    case Identifier.Bishop:
                        return "\u265D";
                    case Identifier.Knight:
                        return "\u265E";
                    case Identifier.Rook:
                        return "\u265C";
                    default: // Pawn
                        return "\u265F";
                }
            }
        }

        /// <summary>
        /// Gets name of this piece.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the maximum number per 100 squares that a team can have of 
        /// this piece
        /// </summary>
        public uint Maximum
        {
            get
            {
                return this.max;
            }
        }

        /// <summary>
        /// Gets the team building cost of this piece.
        /// </summary>
        public uint Cost
        {
            get
            {
                return this.cost;
            }
        }

        /// <summary>
        /// Gets the attribute of the piece that determines how many spaces  
        /// this Piece can move on it's move phase.
        /// </summary>
        public uint Move
        {
            get
            {
                return this.move;
            }
        }

        /// <summary>
        /// Gets the base value of this Piece's melee attack.
        /// </summary>
        public int Melee
        {
            get
            {
                return this.melee;
            }
        }

        /// <summary>
        /// Gets the value that determines the resolve of this Piece.
        /// </summary>
        public int Save
        {
            get
            {
                return this.save;
            }
        }

        public bool Brave
        {
            get
            {
                return false;
            }
        }

        public bool Scary
        {
            get
            {
                return false;
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
        /// Adds the base melee value with a random D6 roll that will be used
        /// for a melee attack.
        /// </summary>
        /// <returns>
        /// An integer value that a target piece must defend.
        /// </returns>
        public void Attack(Piece defender)
        {
            int roll = this.melee - defender.melee + Dice.RollD6();

            if (roll > 5)
            {
                // Defender is destoryed.
                //defender.Die()
            }
            else if (roll == 5)
            {
                if (!defender.MakeSave() || !defender.MakeSave())
                {
                    // Defender is destoryed.
                    //defender.Die()
                }
            }
            else if (roll == 4)
            {
                if (!defender.MakeSave())
                {
                    // Defender is destoryed.
                    //defender.Die()
                }
            }
            else if (roll < 2)
            {
                //Attacker destroyed unless save is made.
                //Scary attacker must only save if the defender is brave
                if (this.Scary && defender.Brave ||
                    !this.Scary)
                {
                    if (!this.MakeSave())
                    {
                        //this.Die();
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to make a save from a given event value
        /// </summary>
        /// <param name="value">
        /// A value that must be beat inorder to survive, charge, etc.
        /// </param>
        /// <returns>True if this Piece has made it's saving throw.</returns>
        public bool MakeSave()
        {
            int roll = Dice.RollD6();
            if (roll == 1 || roll < this.save)
            {
                return false;
            }
            return true;
        }

        /// <summary>Checks the stats </summary>
        public bool Charge(Piece target)
        {
            if (!this.Brave && target.Scary)
            {
                return this.MakeSave();
            }
            return true;
        }

        /// <summary>
        /// Initializes all of the Piece memembers
        /// according to data placed in a well formed
        /// Xml element.
        /// </summary>
        /// <param name="xml">An xml node containing all the member data.</param>
        /// <returns>A Piece type-casted as an IRPChessObject.</returns>
        public void ReadXml(XmlReader xr)
        {
			//TODO
        }

        /// <summary>
        /// Write all the piece data to a well formatted 
        /// XML document for human readable storage.
        /// </summary>
        /// <returns>An XmlDocument</returns>
        public void WriteXml(XmlWriter xw)
        {
            xw.WriteElementString("Name", this.name);
            xw.WriteElementString("id", this.id.ToString());
            xw.WriteElementString("Max", this.max.ToString());
            xw.WriteElementString("Cost", this.cost.ToString());
            xw.WriteElementString("Move", this.move.ToString());
            xw.WriteElementString("Save", this.save.ToString());
            xw.WriteElementString("Melee", this.melee.ToString());
            xw.WriteStartElement("Specials");
            if (this.specials != null)
            {
                foreach (IMove m in this.specials)
                {
                    //TODO
                }
            }
            xw.WriteEndElement();
        }
		
		public XmlSchema GetSchema()
		{
			return(null);
		}
    }
}
