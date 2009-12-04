//-----------------------------------------------------------------------
// <copyright file="Army.cs" company="BENTwerx">
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
    /// Holds the values of a unique Army race.
    /// </summary>
    public class Army : IXmlSerializable
    {
        private string name;
        private uint initiative;
        private Piece[] staff;
        public ReadOnlyCollection<Piece> Staff;
        private uint wealth;
        private string description;

        /// <summary>
        /// Constructs a null and zeroed instance of Army.
        /// </summary>
        public Army()
        {
            this.name = null;
            this.initiative = 0;
            staff = null;
            this.wealth = 0;
            this.description = null;
            this.Staff = null; // new ReadOnlyCollection<Piece>(this.staff);
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
        /// <param name="staff">
        /// A list of Pieces that give the specifications for each of the 
        /// staff positions.
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
            Piece[] staff,
            uint wealth,
            string description)
        {
            this.name = name;
            this.initiative = initiative;
            this.staff = staff;
            this.Staff = new ReadOnlyCollection<Piece>(this.staff);
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
            this.staff = null;
            this.Staff = new ReadOnlyCollection<Piece>(this.staff);
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
        /// Sums all of the Move attributes of the different Staff positions.
        /// </summary>
        /// <returns>A uint sum of all move attributes.</returns>
        public uint SumMove()
        {
            uint movesum = 0;
            foreach (Piece p in Staff)
            {
                movesum += p.Move;
            }
            return movesum;
        }

        /// <summary>
        /// Sums each of the Save attributes of the different Staff positions.
        /// Warning: May be negative.
        /// </summary>
        /// <returns>An integer Sum of the Save attributes.</returns>
        public int SumSave()
        {
            int savesum = 0;
            foreach (Piece p in this.Staff)
            {
                savesum += p.Save;
            }
            return savesum;
        }

        /// <summary>
        /// Sums the Melee attributes of each Army Staff position.
        /// </summary>
        /// <returns>An integer sum of the Melee attributes.</returns>
        public int SumMelee()
        {
            int meleesum = 0;
            foreach (Piece p in this.Staff)
            {
                meleesum += p.Melee;
            }
            return meleesum;
        }

		// Xml Serialization Infrastructure
		
		
        /// <summary>
        /// Implements the IRPChessObject serialization method.
        /// </summary>
        /// <returns>An XmlDocument describing this Army instance.</returns>
		public void WriteXml (XmlWriter xw)
		{
            xw.WriteElementString("Name", name);
            xw.WriteElementString("Initiative", this.initiative.ToString());
            xw.WriteElementString("Wealth", this.wealth.ToString());
            xw.WriteElementString("Description", this.description);
            xw.WriteStartElement("Staff");
			XmlSerializer pserializer = new XmlSerializer(typeof(Piece));
            foreach (Piece p in this.Staff)
            {
                pserializer.Serialize(xw, p);
            }
			//xw.WriteElementString("Author", author);
			//xw.WriteElementString("email", email);
			//xw.WriteElementString("License", license);
            xw.WriteEndElement();
		}
		
        /// <summary>
        /// Implements the IRPChessObject de-serialization method.
        /// </summary>
        /// <param name="xmldoc">An XmlDocument specifing an Army.</param>
        /// <returns>An Army instance type-casted as an IRPChessObject.</returns>
		public void ReadXml (XmlReader reader)
		{
            reader.Read();
            List<Piece> plist = new List<Piece>();
			if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Army")
			{
                reader.ReadToDescendant("Name");
                this.name = reader.ReadElementContentAsString();
                this.initiative = UInt32.Parse(reader.ReadElementString("Initiative"));
                this.wealth = UInt32.Parse(reader.ReadElementString("Wealth"));
                this.description = reader.ReadElementString("Description");
                //this.author = reader.ReadElementString("Author");
				//this.email = reader.ReadElementString("email");
				//this.license = reader.ReadElementString("License");
				
				//_Enabled = Boolean.Parse(reader["Enabled"]);
				//_Color = Color.FromArgb(Int32.Parse(reader["Color"]));
				if (reader.ReadToDescendant("Piece"))
				{
                    Piece p;
					while (reader.LocalName == "Piece")
					{
						p = new Piece();
						p.ReadXml(reader);
						plist.Add(p);
                        reader.Read();
					}
				}
				reader.Read();
			}
            this.staff = new Piece[plist.Count];
            plist.CopyTo(this.staff);

            this.Staff = new ReadOnlyCollection<Piece>(this.staff);
		}

		public XmlSchema GetSchema()
		{
			return(null);
		}
    }
}
