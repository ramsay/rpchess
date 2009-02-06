//-----------------------------------------------------------------------
// <copyright file="Army.cs" company="BENTwerx">
//     GPLv3 Copyright 2008 Robert Ramsay
// </copyright>
// <author>Robert Ramsay</author>
//-----------------------------------------------------------------------

namespace RPChess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Holds the values of a unique Army race.
    /// </summary>
    public class Army : IRPChessObject
    {
        private string name;
        private uint initiative;
        private Piece[] ranks;
        public ReadOnlyCollection<Piece> Ranks;
        private uint wealth;
        private string description;

        /// <summary>
        /// Constructs a null and zeroed instance of Army.
        /// </summary>
        public Army()
        {
            this.name = null;
            this.initiative = 0;
            this.ranks = null;
            this.wealth = 0;
            this.description = null;
            this.Ranks = new ReadOnlyCollection<Piece>(this.ranks);
        }

        /// <summary>
        /// Constructs an Army from fully qualified set of fields.
        /// </summary>
        /// <param name="name">
        /// The descriptive and possibly fantastical name.
        /// </param>
        /// <param name="initiative">
        /// The value that determines which player will go first.
        /// </param>
        /// <param name="ranks">
        /// A list of Pieces that give the specifications for each of the 
        /// chess pieces.
        /// </param>
        /// <param name="wealth">
        /// The amount of points per 100 squares that can be used to build a 
        /// team roster
        /// </param>
        /// <param name="description">
        /// Gives an imaginative description or backstory to the Race.
        /// </param>
        public Army(
            string name,
            uint initiative,
            Piece[] ranks,
            uint wealth,
            string description)
        {
            this.name = name;
            this.initiative = initiative;
            this.ranks = ranks;
            this.Ranks = new ReadOnlyCollection<Piece>(ranks);
            this.wealth = wealth;
            this.description = description;
        }

        /// <summary>
        /// Constructs an Army from an XmlDocument.
        /// </summary>
        /// <param name="xmldoc">
        /// An XmlDocument that holds all of the information for an army.
        /// </param>
        public Army(XmlDocument xmldoc)
        {
            this.name = null;
            this.initiative = 0;
            this.ranks = null;
            this.wealth = 0;
            this.description = null;
        }

        /// <summary>
        /// Currently empty.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Gets the descriptive and possibly fantastical name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the value that determines which player will go first.
        /// </summary>
        public uint Initiative
        {
            get
            {
                return this.initiative;
            }
        }

        /// <summary>
        /// Gets the amount of points per 100 squares that can be used to 
        /// build a team roster.
        /// </summary>
        public uint Wealth
        {
            get
            {
                return this.wealth;
            }
        }

        /// <summary>
        /// Gets the an imaginative description or backstory to the Race.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Sums all of the Move attributes of the different Ranks.
        /// </summary>
        /// <returns>A uint sum of all move attributes.</returns>
        public uint SumMove()
        {
            uint movesum = 0;
            foreach (Piece p in Ranks)
            {
                movesum += p.Move;
            }
            return movesum;
        }

        /// <summary>
        /// Sums each of the Save attributes of the different Ranks.
        /// Warning: May be negative.
        /// </summary>
        /// <returns>An integer Sum of the Save attributes.</returns>
        public int SumSave()
        {
            int savesum = 0;
            foreach (Piece p in Ranks)
            {
                savesum += p.Save;
            }
            return savesum;
        }

        /// <summary>
        /// Sums the Melee attributes of each Army Rank.
        /// </summary>
        /// <returns>An integer sum of the Melee attributes.</returns>
        public int SumMelee()
        {
            int meleesum = 0;
            foreach (Piece p in Ranks)
            {
                meleesum += p.Melee;
            }
            return meleesum;
        }

        /// <summary>
        /// Implements the IRPChessObject serialization method.
        /// </summary>
        /// <returns>An XmlDocument describing this Army instance.</returns>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument xmldoc = new XmlDocument();
            StringBuilder repr = new StringBuilder();
            repr.AppendLine("<army name=\"" + name + "\">");
            repr.AppendLine("\t<initiative>" + this.initiative + "<\\initiative>");
            repr.AppendLine("\t<wealth>" + this.wealth + "<\\wealth>");
            repr.AppendLine("\t<description>" + this.description + "<\\description>");
            repr.AppendLine("\t<ranks>");
            foreach (Piece p in Ranks)
            {
                repr.AppendLine(p.ToXmlDocument().FirstChild.InnerXml);
            }
            repr.AppendLine("\t<\\ranks>");
            repr.Append("<\\army>");
            xmldoc.InnerXml = repr.ToString();
            return xmldoc;
        }

        /// <summary>
        /// Implements the IRPChessObject de-serialization method.
        /// </summary>
        /// <param name="xmldoc">An XmlDocument specifing an Army.</param>
        /// <returns>An Army instance type-casted as an IRPChessObject.</returns>
        public IRPChessObject FromXmlDocument(XmlDocument xmldoc)
        {
            return (IRPChessObject)new Army();
        }
    }
}
